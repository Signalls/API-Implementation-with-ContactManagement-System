using ContactData.Entities;

namespace ContactData.Repository
{
    public interface IContactRepository
    {

        Contact AddNewContact(Contact contact);
        bool DeleteContact(Contact contact);
        bool DeleteContact(List<Contact> contacts);

        Contact UpdateContact(Contact contact);
        Contact GetConatctById(string id);
        IEnumerable<Contact> GetAllContact();
        public Contact GetUserByEmail(string email);

        IEnumerable<Contact> Paginate(List<Contact> contact, int perpage, int page);

    }
}
