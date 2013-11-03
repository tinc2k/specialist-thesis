using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Wayfarer.Mvc.Repositories;
using Wayfarer.Mvc.ViewModels;

namespace Wayfarer.Mvc.Controllers
{
    public class SearchController : ApiController /* WebAPI, motherfucker. */
    {
        private ISearchRepository _repository;
        
        public SearchController(ISearchRepository repository)
        {
            this._repository = repository;
        }

        public SearchController() : this (new SearchRepository() ){/*dependency injection*/}


        public IEnumerable<SearchItem> Get(string query)
        {
            var results = new List<SearchItem>();
            if (User.Identity.IsAuthenticated)
            {
                var feed = _repository.SearchUsers(query);
                results = GetSearchViewModel(feed, _repository);
            }
            return results;
        }


        private List<SearchItem> GetSearchViewModel(List<Models.UserProfile> userProfiles, ISearchRepository repo)
        {
            var results = new List<SearchItem>();
            foreach (var profile in userProfiles)
            {
                results.Add(new SearchItem()
                {
                    Id = profile.UserId,
                    Title = profile.UserName,
                    Url = "http://" + HttpContext.Current.Request.Url.Authority + "/profiles/" + profile.UserName
                });
            }
            return results;
        }

    }
}
