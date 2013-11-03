using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wayfarer.Mvc.Hubs;
using Wayfarer.Mvc.Models;
using Wayfarer.Mvc.Repositories;

namespace Wayfarer.Mvc.Controllers
{
    public class HomeController : Controller
    {

        private IProfileRepository _repository;
        private Microsoft.AspNet.SignalR.IHubContext _wayfarerHub;
        public HomeController(IProfileRepository repository)
        {
            this._repository = repository;
            this._wayfarerHub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<WayfarerHub>();
        }
        public HomeController() : this (new ProfileRepository() ){/*dependency injection*/}

        public ActionResult Index()
        {
            /*user logged in?*/
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            else
            {
                ViewBag.friends = _repository.GetFriends(User.Identity.Name);
                ViewBag.feed = _repository.GetFeed(User.Identity.Name);
                return View();
            }
        }

        [Authorize]
        public bool PostStatus(string new_post_content, bool new_post_limited)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (new_post_limited)
                {
                    _repository.UpsertStatus(User.Identity.Name, Audience.Friends, new_post_content);
                }
                else
                {
                    _repository.UpsertStatus(User.Identity.Name, Audience.Public, new_post_content);
                }
                _wayfarerHub.Clients.All.refreshFeed(); /*[tc] #todo kill this after debugging*/
                //_wayfarerHub.Clients.Group(User.Identity.Name).RefreshFeed();
                return true;
            }
            return false;
        }

        [Authorize]
        public string EditProfile(FormCollection form, string email, string phone, List<string> region_name, List<string> region_value, List<bool> region_limited)
        {
            var response = _repository.EditProfile(User.Identity.Name, email, phone, region_name, region_value, region_limited);
            return (response) ? "true" : "false";
        }

        [Authorize]
        public string Friend(string username)
        {
            _repository.Friend(User.Identity.Name, username);
            return "true";
        }

        [Authorize]
        public string Unfriend(string username)
        {
            _repository.Unfriend(User.Identity.Name, username);
            return "true";
        }

    }
}
