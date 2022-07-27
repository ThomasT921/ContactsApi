using Contacts.Models;

namespace Contacts
{
    public interface IContactsDataAccess
    {
        public void Save(Contact contact);
        public void Remove(Contact contact);
        public List<Contact> GetContacts();
        public Contact? GetContactById(int id);
    }
}
