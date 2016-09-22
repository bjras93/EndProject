namespace LifeStruct
{
    using System.ComponentModel.DataAnnotations;
    public class HealthModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage= "You need to fill in a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You need to write something")]
        public string Content { get; set; }
        public string UserId { get; set; }
        public string Tags { get; set; }
        public int Likes { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "You need to pick which post you want")]
        public int Type { get; set; }

    }
}