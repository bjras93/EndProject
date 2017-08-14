using System.ComponentModel.DataAnnotations;

namespace LifeStruct.Models.Fitness
{
    public class FitnessModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Description")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Tags { get; set; }
        public string Img { get; set; }
        public string Author { get; set; }
        public int Likes { get; set; }
    }
}