using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Wayfarer.Mvc.Models
{
    public class WayfarerContext : DbContext
    {
        public WayfarerContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserProfileRegion> UserProfileRegions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        /*public DbSet<StatusComment> StatusComments { get; set; }*/
        public DbSet<Geolocation> Geolocations { get; set; }
        /*public DbSet<ChatLog> ChatLogs { get; set; }*/
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<NotificationStack> Notifications { get; set; }
        /*public DbSet<UserFriendRequest> FriendRequests { get; set; }*/

    }
}
