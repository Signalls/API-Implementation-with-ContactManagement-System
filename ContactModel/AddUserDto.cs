using System.ComponentModel.DataAnnotations;

namespace ContactModel
{

    public class AddUserDto
    {


        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
