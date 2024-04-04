using System.ComponentModel.DataAnnotations.Schema;

namespace SoftFlix_API.Models
{
    public class MediaRestriction
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }


        public byte RestrictionId { get; set; }
        [ForeignKey("RestrictionId")]
        public Restriction? Restriction { get; set; }
    }
}
