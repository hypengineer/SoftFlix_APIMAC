using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftFlix_API.Models
{
    public class Media
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = "";

        [Column(TypeName = "nvarchar(500)")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 10)]
        public float IMDBRating { get; set; }

        public bool Passive { get; set; }



        public List<MediaCategory>? MediaCategories { get; set; }

        public List<MediaStar>? MediaStars { get; set; }

        public List<MediaDirector>? MediaDirectors { get; set; }
        public List<MediaRestriction>? MediaRestrictions { get; set; }

    }
}
