using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public interface ISearchRepository
    {
        List<UserProfile> SearchUsers(string query);
        List<Status> SearchStatuses(string query);
    }
}
