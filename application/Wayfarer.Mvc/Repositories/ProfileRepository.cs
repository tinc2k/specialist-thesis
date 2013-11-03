using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        WayfarerContext _context;

        public ProfileRepository()
        {
            _context = new WayfarerContext();
        }


        /*Profile & Profile Regions*/
        public UserProfile GetProfileByUsername(string username)
        {
            return _context.UserProfiles.FirstOrDefault(up => up.UserName == username);
        }
   
        public List<UserProfileRegion> GetRegions(string username, string requestor)
        {
            var user = GetProfileByUsername(username);
            var friendly = _context.Friendships.Any(f => f.Profile.UserName == username && f.Friend.UserName == requestor) || username == requestor;

            if (friendly)
                return user.Regions.Where(r => r.Audience == Audience.Friends || r.Audience == Audience.Public).ToList();

            else
                return user.Regions.Where(r => r.Audience == Audience.Public).ToList();
        }

        public bool EditProfile (string username, string email, string phone, List<string> regions_name, List<string> regions_value, List<bool> regions_limited)
        {
            var user = GetProfileByUsername(username);
            user.Email = email;
            user.Phone = phone;

            /*kill current regions*/
            var regions = _context.UserProfileRegions.Where(upr => upr.User.UserName == username).ToList();
            foreach (var region in regions)
            {
                _context.UserProfileRegions.Remove(region);
            }
            /*create new regions*/
            for (int i = 0; i < regions_name.Count; i++)
            {
                if (regions_name[i] != "" && regions_value[i] != "") {
                    _context.UserProfileRegions.Add(new UserProfileRegion()
                    {
                        Audience = regions_limited[i] ? Audience.Friends : Audience.Public,
                        Name = regions_name[i],
                        User = user,
                        Value = regions_value[i]
                    });
                }
                
            }
            _context.SaveChanges();
            return true;

        }

        public void UpsertProfileRegion(string username, Audience audience, string name, string value, int? id = null)
        {
            var user = GetProfileByUsername(username);
            if (user != null) /*found user*/
            {
                if (id != null) /*update*/
                {
                    var region = _context.UserProfileRegions.FirstOrDefault(upr => upr.UserProfileRegionId == id);
                    if (region != null)
                    {
                        region.Audience = audience;
                        region.Name = name;
                        region.User = user; /*lol*/
                        region.Value = value;
                    }
                }
                else /*insert*/
                {
                    _context.UserProfileRegions.Add(new UserProfileRegion()
                    {
                        Audience = audience,
                        Name = name,
                        User = user,
                        Value = value
                    });
                }
                _context.SaveChanges();
            }
        }

        public void DeleteProfileRegion(int id)
        {
            var region = _context.UserProfileRegions.FirstOrDefault(upr => upr.UserProfileRegionId == id);
            if (region != null)
                _context.UserProfileRegions.Remove(region);
            _context.SaveChanges();
        }


        /*Statuses & Status Comments*/

        public Status GetStatusById(int id)
        {
            return _context.Statuses.Include("Comments").FirstOrDefault(s => s.Id == id);
        }
        
        public List<Status> GetUserStatuses(string username, string requestor, int? skip = null, int? beforeId = null, int? afterId = null)
        {
            var user = GetProfileByUsername(username);
            var friendly = _context.Friendships.Any(f => f.Profile.UserName == username && f.Friend.UserName == requestor) || username == requestor;
            IEnumerable<Status> query = null;

            if (friendly)
            {
                if (beforeId == null && afterId == null)
                    query = user.Statuses.Where(s => s.Audience == Audience.Friends || s.Audience == Audience.Public);
                else if (beforeId != null) /* 'before' means chronologically BEFORE the status with a certain id (smaller id)*/
                    query = user.Statuses.Where(s => (s.Audience == Audience.Friends || s.Audience == Audience.Public) && s.Id < beforeId);
                else if (afterId != null)
                    query = user.Statuses.Where(s => (s.Audience == Audience.Friends || s.Audience == Audience.Public) && s.Id > afterId);
            }
            else
            {
                if (beforeId == null && afterId == null)
                    query = user.Statuses.Where(s => s.Audience == Audience.Public);
                else if (beforeId != null) /* 'before' means chronologically BEFORE the status with a certain id (smaller id)*/
                    query = user.Statuses.Where(s => s.Audience == Audience.Public && s.Id < beforeId);
                else if (afterId != null)
                    query = user.Statuses.Where(s => s.Audience == Audience.Public && s.Id > afterId);
            }

            if (skip == null)
                return query.OrderByDescending(o => o.Id).Take(Config.QueryLimit).ToList();
            else
                return query.OrderByDescending(o => o.Id).Skip(skip.Value).Take(Config.QueryLimit).ToList();
        }

        public List<Status> GetFeed(string requestor, int? skip = null, int? beforeId = null, int? afterId = null)
        {
            var publicIds = _context.Friendships.Where(f => f.Profile.UserName == requestor).Select(s => s.Friend.UserId).ToList();
            var friendshipIds = _context.Friendships.Where(f => f.Friend.UserName == requestor).Select(s => s.Profile.UserId).ToList();

            publicIds.Add(_context.UserProfiles.First(u => u.UserName == requestor).UserId); /*add me*/
            friendshipIds.Add(_context.UserProfiles.First(u => u.UserName == requestor).UserId); /*add me*/

            IEnumerable<Status> query = null;

            if (beforeId == null && afterId == null)
                query = _context.Statuses.Include("Author").Where(s =>
                    (publicIds.Contains(s.Author.UserId) && s.Audience == Audience.Public) ||
                    (friendshipIds.Contains(s.Author.UserId) && s.Audience == Audience.Friends)
                    );
            else if (beforeId != null)
                query = _context.Statuses.Include("Author").Where(s =>
                    ((publicIds.Contains(s.Author.UserId) && s.Audience == Audience.Public) ||
                    (friendshipIds.Contains(s.Author.UserId) && s.Audience == Audience.Friends))
                    && s.Id < beforeId);
            else if (afterId != null)
                query = _context.Statuses.Include("Author").Where(s =>
                    ((publicIds.Contains(s.Author.UserId) && s.Audience == Audience.Public) ||
                    (friendshipIds.Contains(s.Author.UserId) && s.Audience == Audience.Friends))
                    && s.Id > afterId);
            if (skip == null)
                return query.OrderByDescending(o => o.Id).Take(Config.QueryLimit).ToList();
            else
                return query.OrderByDescending(o => o.Id).Skip(skip.Value).Take(Config.QueryLimit).ToList();
        }

        public void UpsertStatus(string username, Audience audience, string status, int? id = null)
        {
            var user = GetProfileByUsername(username);
            if (user != null) /*found user*/
            {
                if (id == null) /*insert*/
                {
                    _context.Statuses.Add(new Status()
                    {
                        Audience = audience,
                        Author = user,
                        Content = status,
                        TimePosted = DateTime.UtcNow,
                        TimeLastEdited = DateTime.UtcNow
                    });
                }
                else /*update*/
                {
                    var status_from_db = _context.Statuses.FirstOrDefault(s => s.Id == id);
                    if (status_from_db != null)
                    {
                        status_from_db.Audience = audience;
                        status_from_db.Author = user; /*lol*/
                        status_from_db.Content = status;
                        status_from_db.TimeLastEdited = DateTime.UtcNow;
                    }
                }
                _context.SaveChanges();
            }
        }

        public void DeleteStatus(int id)
        {
            var status = _context.Statuses.FirstOrDefault(s => s.Id == id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                _context.SaveChanges();
            }
        }

        /*Friends*/

        public List<UserProfile> GetFriends(string username)
        {
            var friendships = _context.Friendships.Include("Friend").Where(f => f.Profile.UserName == username).ToList();
            var results = new List<UserProfile>();

            foreach (var friend in friendships)
            {
                var r = _context.UserProfiles.FirstOrDefault(up => up.UserName == friend.Friend.UserName);
                if (r != null)
                    results.Add(r);
            }
            return results;
        }

        public bool AreTheseFriends(string username, string friend)
        {
            return _context.Friendships.Any(f => f.Profile.UserName == username && f.Friend.UserName == friend);
        }

        public bool Friend(string username, string friend)
        {
            var user = GetProfileByUsername(username);
            var new_friend = GetProfileByUsername(friend);

            var already_friends = _context.Friendships.Any(f => f.Profile.UserName == username && f.Friend.UserName == friend);
            if (!already_friends)
            {
                _context.Friendships.Add(new Friendship()
                {
                    Profile = user,
                    Friend = new_friend
                });
                _context.SaveChanges();
            }
            return true;
        }

        public bool Unfriend(string username, string friend)
        {
            var user = GetProfileByUsername(username);
            var old_friend = GetProfileByUsername(friend);

            var friendship = _context.Friendships.FirstOrDefault(f => f.Profile.UserName == username && f.Friend.UserName == friend);
            
            if (friendship != null)
            {
                _context.Friendships.Remove(friendship);
                _context.SaveChanges();
            }
            return true;
        }

        /*Notifications*/
        public List<NotificationStack> GetUnreadNotifications(string username)
        {
            var notifications = _context.Notifications.Where(n => n.User.UserName == username && n.Read == false).ToList();
            notifications.ForEach(n => n.Read = true);
            return notifications;
        }

    }
}