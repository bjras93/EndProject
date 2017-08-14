using System.ComponentModel.DataAnnotations;

namespace LifeStruct.Models.Video
{
    public class VideoModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        [Required(ErrorMessage = "YouTubeId is required")]
        public string YouTubeId { get; set; }
        public string UserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select what type your video is")]
        public int Type { get; set; }
    }
}