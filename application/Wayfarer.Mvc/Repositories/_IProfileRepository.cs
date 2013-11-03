using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public interface IProfileRepository
    {
        /*Profile & Profile Regions*/
        UserProfile GetProfileByUsername(string username);
        List<UserProfileRegion> GetRegions(string username, string requestor);
        bool EditProfile (string username, string email, string phone, List<string> regions_name, List<string> regions_value, List<bool> regions_limited);
        
        /*Statuses & Status Comments*/
        Status GetStatusById(int id);
        List<Status> GetUserStatuses(string username, string requestor, int? skip = null, int? beforeId = null, int? afterId = null);
        List<Status> GetFeed(string requestor, int? skip = null, int? beforeId = null, int? afterId = null);
        void UpsertStatus(string username, Audience audience, string status, int? id = null);
        void DeleteStatus(int id);

        /*Friends*/
        List<UserProfile> GetFriends(string username);
        bool AreTheseFriends(string username, string friend);
        bool Friend(string username, string friend);
        bool Unfriend(string username, string friend);

        /*Notifications*/
        List<NotificationStack> GetUnreadNotifications(string username);
    }
}
