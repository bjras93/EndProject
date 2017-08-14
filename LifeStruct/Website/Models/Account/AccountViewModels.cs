using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace LifeStruct.Models.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "You forgot to fill out your name")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "You forgot to fill out your name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You forgot to fill out your height")]
        [Display(Name = "Height")]
        public string Height { get; set; }
        [Required(ErrorMessage = "You forgot to fill out your weight")]
        [Display(Name = "Weight")]
        public string Weight { get; set; }

        [Date(ErrorMessage = "Birthdate is incorrect.")]
        [Required(ErrorMessage = "Please fill out your birthday")]
        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please pick a gender")]
        [Display(Name = "Gender")]
        public int? Gender { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
    
    
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class DateAttribute : RangeAttribute
    {
        public DateAttribute()
            : base(typeof(DateTime), DateTime.Now.AddYears(-100).ToShortDateString(), DateTime.Now.AddYears(-18).ToShortDateString()) { }
    }
}