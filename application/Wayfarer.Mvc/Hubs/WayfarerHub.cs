using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Wayfarer.Mvc.Repositories;
using System.Threading.Tasks;

namespace Wayfarer.Mvc.Hubs
{
    public class WayfarerHub : Hub
    {
        private IProfileRepository _profileRepo;
        private IGeolocationRepository _geolocRepo;
        private ISearchRepository _searchRepo;

        public WayfarerHub(IProfileRepository profileRepo, IGeolocationRepository geolocRepo, ISearchRepository searchRepo)
        {
            this._profileRepo = profileRepo;
            this._geolocRepo = geolocRepo;
            this._searchRepo = searchRepo;
        }
        public WayfarerHub() : this (new ProfileRepository(), new GeolocationRepository(), new SearchRepository() ){/*dependency injection*/}

        public override Task OnConnected()
        {
            SubscribeToFriendEvents();
            return base.OnDisconnected();
        }

        public override Task OnDisconnected()
        {
            UnsubscribeFromFriendEvents();
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            SubscribeToFriendEvents();
            return base.OnReconnected();
        }

        public void SaveGeolocation(decimal longitude, decimal latitude)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = Context.User.Identity.IsAuthenticated ? Context.User.Identity.Name : null;
            
            _geolocRepo.StoreGeolocation(ip, longitude, latitude, user);
        }

        public void SubscribeToUserEvents(string username)
        {
            if (Context.User.Identity.IsAuthenticated && Context.User.Identity.Name != username)
            {
                Groups.Add(Context.ConnectionId, username);
            }
        }

        private void SubscribeToFriendEvents()
        {
            if (Context.User.Identity.Name != String.Empty)
            {
                var friends = _profileRepo.GetFriends(Context.User.Identity.Name);
                foreach (var friend in friends)
                {
                    Groups.Add(Context.ConnectionId, friend.UserName);  /*subscribe to your friends events*/
                }
            }
        }

        private void UnsubscribeFromFriendEvents()
        {
            if (Context.User.Identity.Name != String.Empty)
            {
                var friends = _profileRepo.GetFriends(Context.User.Identity.Name);
                foreach (var friend in friends)
                {
                    Groups.Remove(Context.ConnectionId, friend.UserName);  /*unsubscribe from your friends events*/
                }
            }
        }

    }
}