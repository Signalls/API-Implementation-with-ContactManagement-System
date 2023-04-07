using System.ComponentModel.DataAnnotations;

namespace ContactData.Entities
{
    public class Contact
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Length Must Be between 3- 10 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Length Must Be between 3- 10 characters")]

        public string LastName { get; set; }
        public string FullName { get { return FirstName + LastName; } }
        [Required]
        public string Address { get; set; }

        [MaxLength(100)]
        public string? ImageUrl { get; set; }
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }



    }
}
