using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LifeStruct.Models.Diet
{
    public class DietModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Description")]
        [AllowHtml]
        public string Description { get; set; }
        public string User { get; set; }
        public string Author { get; set; }
        public int Likes { get; set; }
        public int Weeks { get; set; }
        public string Tags { get; set; }
        public string Img { get; set; }
    }
}