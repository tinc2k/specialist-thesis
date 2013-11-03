using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wayfarer.Mvc.Repositories
{
    public interface IGeolocationRepository
    {
        void StoreGeolocation(string ip, decimal longitude, decimal latitude, string username = null);
    }
}
