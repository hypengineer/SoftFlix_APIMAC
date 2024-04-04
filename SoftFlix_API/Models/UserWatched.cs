using System.ComponentModel.DataAnnotations.Schema;

namespace SoftFlix_API.Models
{
    public class UserWatched
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public SoftFlixUser? SoftFlixUser { get; set; }


        public long EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode? Episode { get; set; }
    }
}
