using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YouGo.Models
{
    public class DietModel
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Title")]
        public string Title { get; set; }
        public string User { get; set; }
        public int Hearts { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Description")]
        [AllowHtml]
        public string Description { get; set; }
        public int Days { get; set; }
        public int Meals { get; set; }
        public string Img { get; set; }
    }
}