using System.ComponentModel.DataAnnotations.Schema;

namespace SoftFlix_API.Models
{
    public class UserFavorite
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public SoftFlixUser? SoftFlixUser { get; set; }


        public int MediId { get; set; }
        [ForeignKey("MediId")]
        public Media? Media { get; set; }
    }
}
