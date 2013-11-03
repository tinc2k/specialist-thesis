using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Wayfarer.Mvc.Models;
using Wayfarer.Mvc.Repositories;
using Wayfarer.Mvc.ViewModels;

namespace Wayfarer.Mvc.Controllers
{
    public class FeedController : ApiController /* WebAPI */
    {
        private IProfileRepository _repository;
        
        public FeedController(IProfileRepository repository)
        {
            this._repository = repository;
        }

        public FeedController() : this (new ProfileRepository() ){/*dependency injection*/}


        /*Get Profile Regions*/
        
        /*New / Edit Profile Region*/

        /*Get Global Feed*/
        public IEnumerable<FeedItem> Get(int? skip = null, int? beforeId = null, int? afterId = null)
        {
            var results = new List<FeedItem>();
            if (User.Identity.IsAuthenticated)
            {
                var feed = _repository.GetFeed(User.Identity.Name, skip, beforeId, afterId);
                results = GetFeedViewModel(feed, _repository);
            }
            return results;
        }

        /*Get User Feed*/
        public IEnumerable<FeedItem> Get(string username, int? skip = null, int? beforeId = null, int? afterId = null)
        {
            var results = new List<FeedItem>();
            if (User.Identity.IsAuthenticated)
            {
                var feed = _repository.GetUserStatuses(username, User.Identity.Name, skip, beforeId, afterId);
                results = GetFeedViewModel(feed, _repository);
            }
            return results;
        }

        /*New / Edit Status*/

        /*Delete Status*/

        /*New / Edit Comment*/

        /*Delete Comment*/

        /*!Internal!*/

        private List<FeedItem> GetFeedViewModel(List<Status> feed, IProfileRepository repo)
        {
            var results = new List<FeedItem>();
            foreach (var feedItem in feed)
            {
                results.Add(new FeedItem()
                {
                    StatusId = feedItem.Id,
                    Audience = feedItem.Audience.ToString(),
                    AuthorId = feedItem.Author.UserId,
                    AuthorName = feedItem.Author.UserName,
                    AuthorUrl = "http://" + HttpContext.Current.Request.Url.Authority + "/profiles/" + feedItem.Author.UserName,
                    Content = feedItem.Content,
                    TimePosted = DateTimeToNiceString(feedItem.TimePosted),
                    TimeEdited = (feedItem.TimePosted != feedItem.TimeLastEdited) ? DateTimeToNiceString(feedItem.TimeLastEdited) : null,
                    Comments = new List<FeedItemComment>()
                });
            }
            return results;
        }

        private string DateTimeToNiceString(DateTime arg)
        {
            return arg.Hour + ":" + arg.Minute + " " + arg.Date.Day + "." + arg.Month + "." + arg.Year + ".";
        }

    }
}
