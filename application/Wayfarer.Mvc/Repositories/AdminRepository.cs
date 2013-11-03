using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        WayfarerContext _context;

        public AdminRepository()
        {
            _context = new WayfarerContext();
        }

        public List<UserProfile> GetActiveProfiles()
        {
            /*[tc] #todo eventually, this will murder the server*/
            return _context.UserProfiles.Where(up => up.Disabled == false).ToList();
        }

    }
}