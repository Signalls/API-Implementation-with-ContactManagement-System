using Microsoft.AspNetCore.Identity;

namespace ContactData.Entities
{
    public class AppUser : IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Roles { get; set; }

    }
}
