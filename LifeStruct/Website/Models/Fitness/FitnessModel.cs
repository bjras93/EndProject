namespace LifeStruct.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FitnessModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You forgot to fill out Description")]
        public string Description { get; set; }
    }
}