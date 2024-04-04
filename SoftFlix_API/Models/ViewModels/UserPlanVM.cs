using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftFlix_API.Models.ViewModels
{
	public class UserPlanVM
	{
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = "";

        [Range(0, float.MaxValue)]
        public float Price { get; set; }

        [Column(TypeName = "varchar(20)")]
        [StringLength(20, MinimumLength = 2)]
        public string Resolution { get; set; } = "";

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
    }
}

