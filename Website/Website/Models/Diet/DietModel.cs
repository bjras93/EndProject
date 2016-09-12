namespace YouGo.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public class DietModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Description")]
        [AllowHtml]
        public string Description { get; set; }
        public string User { get; set; }
        public int Likes { get; set; }
        public int Weeks { get; set; }
        public string Img { get; set; }
    }
}