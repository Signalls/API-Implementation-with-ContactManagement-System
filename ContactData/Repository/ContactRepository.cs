using ContactData.Entities;

namespace ContactData.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DbContactContext _context;

        public ContactRepository(DbContactContext context)
        {
            _context = context;
        }


        public Contact AddNewContact(Contact contact)
        {
            if (contact == null)
                throw new NotImplementedException();
            _context.Contacts.Add(contact);
            var status = _context.SaveChanges();
            if (status > 0)
                return contact;

            return null;
        }

        public bool DeleteContact(Contact contact)
        {
            if (contact == null)
                throw new NotImplementedException();
            _context.Remove(contact);
            var status = _context.SaveChanges();
            if (status > 0) return true;
            return false;
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return _context.Contacts.ToList();
        }


        public bool DeleteContact(List<Contact> contact)
        {
            if (contact.Count() < 0)
                throw new NotImplementedException(nameof(contact));
            _context.RemoveRange(contact);
            var status = _context.SaveChanges();
            if (status > 0)
                return true;

            return false;

        }


        public Contact GetConatctById(string id)
        {
            return (Contact)_context.Contacts.FirstOrDefault(u => u.Id == id);
        }

        public Contact UpdateContact(Contact contact)
        {
            if (contact == null)
                throw new NotImplementedException(nameof(contact));
            _context.Update(contact);
            var status = _context.SaveChanges();

            if (status > 0)
                return contact;


            return null;

        }

        public IEnumerable<Contact> Paginate(List<Contact> contact, int perpage, int page)
        {
            page = page < 1 ? 1 : page;
            perpage = page < 1 ? 5 : perpage;
            if (contact.Count > 0)
            {
                //list.Skip((page-1)* perpage).Take(perpage)
                var paginated = contact.Skip(page - 1).Take(perpage).ToList();
                return paginated;

            }

            return new List<Contact>();
        }

        public Contact GetUserByEmail(string email)
        {
            return _context.Contacts.FirstOrDefault(x => x.Email == email);

        }

    }
}
