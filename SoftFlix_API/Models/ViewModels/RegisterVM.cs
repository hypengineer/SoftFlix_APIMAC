using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SoftFlix_API.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Bu alan zorunludur")]
        [StringLength(100,MinimumLength =2)]
        public string UserName { get; set; } = "";
        public DateTime BirthDate { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = "";

      

        public string EmailAddress { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        [NotMapped]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = "";
    }
}
