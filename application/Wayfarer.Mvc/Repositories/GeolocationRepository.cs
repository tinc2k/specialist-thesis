using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public class GeolocationRepository : IGeolocationRepository
    {
        WayfarerContext _context;

        public GeolocationRepository()
        {
            _context = new WayfarerContext();
        }

        public void StoreGeolocation(string ip, decimal longitude, decimal latitude, string username = null)
        {
            _context.Geolocations.Add(new Geolocation()
            {
                Ip = ip,
                Longitude = longitude,
                Latitude = latitude,
                User = (username == null) ? null : _context.UserProfiles.First(up => up.UserName == username),
                Time = DateTime.UtcNow
            });
            _context.SaveChanges();
        }

    }
}