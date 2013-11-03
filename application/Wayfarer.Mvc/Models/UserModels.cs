using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Wayfarer.Mvc.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Disabled { get; set; }
        public virtual ICollection<UserProfileRegion> Regions { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }
    }

    public class Friendship
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FriendshipId { get; set; }
        public UserProfile Profile { get; set; }
        public UserProfile Friend { get; set; }
    }

    public class UserProfileRegion
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserProfileRegionId { get; set; }
        public UserProfile User { get; set; }
        public Audience Audience { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Geolocation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GeolocationId { get; set; }
        public string Ip { get; set; }
        public UserProfile User { get; set; }
        public DateTime Time { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; } 
    }

    public class NotificationStack  /*user notifications that wait until the user is online*/
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public UserProfile User { get; set; }
        public string Message { get; set; }
        public bool Read { get; set; }
        /* [tc] #todo notifications will probably require various types and classes in the future,
         * just to make various actions available to the user when he/she receives a notification. */
    }

    /* ASP.NET MVC4 Generated */
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /* ASP.NET MVC4 Generated */
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    /* ASP.NET MVC4 Generated */
    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    /* ASP.NET MVC4 Generated */
    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }
    }

    /* ASP.NET MVC4 Generated */
    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

}