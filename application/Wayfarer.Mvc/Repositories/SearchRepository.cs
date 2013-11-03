using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        WayfarerContext _context;

        public SearchRepository()
        {
            _context = new WayfarerContext();
        }

        public List<UserProfile> SearchUsers(string query)
        {
            return _context.UserProfiles.Where(up => up.UserName.Contains(query)).Take(Config.QueryLimit).ToList();
        }

        public List<Status> SearchStatuses(string query)
        {
            var public_statuses = _context.Statuses.Where(s => s.Audience == Audience.Public && s.Content.Contains(query));
            /*[tc] #todo add private statuses that the user is allowed to see, will problably require adding another interface method/implementation
             with a 'requester' parameter to handle permissions correctly*/
            return public_statuses.ToList();
        }

    }

}