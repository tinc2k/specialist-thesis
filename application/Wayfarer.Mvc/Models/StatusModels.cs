using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wayfarer.Mvc.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public UserProfile Author { get; set; }
        public string Content { get; set; }
        public Audience Audience { get; set; }
        public DateTime TimePosted { get; set; }
        public DateTime TimeLastEdited { get; set; }
    }

}