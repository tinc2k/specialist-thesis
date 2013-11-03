using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    interface IAdminRepository
    {
        List<UserProfile> GetActiveProfiles();
    }
}
