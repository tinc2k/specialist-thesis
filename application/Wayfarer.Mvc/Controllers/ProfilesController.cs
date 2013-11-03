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
    public class ProfilesController : Controller
    {

        private IProfileRepository _repository;
        public ProfilesController(IProfileRepository repository)
        {
            this._repository = repository;
        }
        public ProfilesController() : this (new ProfileRepository() ){/*dependency injection*/}

        [Authorize]
        public ActionResult Index(string id)
        {
            if (id == null || id == String.Empty) return RedirectToAction("Index", "Home");
            
            var profile = _repository.GetProfileByUsername(id);
            var regions = _repository.GetRegions(id, User.Identity.Name);
            var statuses = _repository.GetUserStatuses(id, User.Identity.Name);
            var friends = _repository.GetFriends(id);
            
            ViewBag.Id = id;
            ViewBag.Profile = profile;
            ViewBag.Regions = regions;
            ViewBag.Statuses = statuses;
            ViewBag.Friends = friends;
            ViewBag.IsProfileFriend = _repository.AreTheseFriends(User.Identity.Name, id);

            return View();
        }

    }
}
