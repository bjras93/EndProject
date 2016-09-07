using System.Web.Mvc;

namespace YouGo.Models
{
    public class DietModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public int Hearts { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public int Days { get; set; }
        public int Meals { get; set; }
        public string Img { get; set; }
    }
}