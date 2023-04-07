namespace ContactModel
{
    public class UpdateContactDto
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
