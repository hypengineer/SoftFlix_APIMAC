using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftFlix.FrontEnd.Models
{
	public class MediaResponseModel
	{
        public int Id { get; set; }

       
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = "";

        
        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 10)]
        public float IMDBRating { get; set; }

        public bool Passive { get; set; }
    }
}

