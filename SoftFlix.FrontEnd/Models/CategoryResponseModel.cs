using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftFlix.FrontEnd.Models
{
    public class CategoryResponseModel
    {
        public short Id { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = "";
    }
}
