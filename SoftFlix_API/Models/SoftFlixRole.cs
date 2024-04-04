using Microsoft.AspNetCore.Identity;

namespace SoftFlix_API.Models
{
    public class SoftFlixRole:IdentityRole<long>
    {
        public SoftFlixRole() : base()
        {

        }

        public SoftFlixRole(string roleName) : base(roleName)
        {

        }

    }
}

