using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.ViewModels
{
    public class FeedItem
    {
        public int StatusId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUrl { get; set; }
        public string Content { get; set; }
        public string Audience { get; set; }
        public string TimePosted { get; set; }
        public string TimeEdited { get; set; }
        public List<FeedItemComment> Comments { get; set; }
    }

    public class FeedItemComment
    {
        public int StatusCommentId { get; set; }
        public int StatusId { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string TimePosted { get; set; }
        public string TimeEdited { get; set; }
    }
}