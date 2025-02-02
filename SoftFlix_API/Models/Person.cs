﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftFlix_API.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = "";
    }
}
