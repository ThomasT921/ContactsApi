using Contacts.Models;

namespace Contacts.DataAccess
{
    public class ContactsDataAccess : IContactsDataAccess
    {
        private static List<Contact> _contacts = new()
        {
            new Contact(1, "Dave", "Jones", "123@gmail.com", new Address("123 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(2, "Jim", "Jones", "120@gmail.com", new Address("1223 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(3, "John", "Jones", "1111@gmail.com", new Address("14423 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(4, "Dave", "Jones", "2132@gmail.com", new Address("1235 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(5, "Tom", "Jones", "1564@gmail.com", new Address("12354 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(6, "Thomas", "Jones", "20@gmail.com", new Address("126563 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(7, "Tim", "Jones", "73@gmail.com", new Address("1111123 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(8, "Chris", "Jones", "654@gmail.com", new Address("12323 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(9, "Jame", "Jones", "777@gmail.com", new Address("124543 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
            new Contact(10, "Steve", "Jones", "345@gmail.com", new Address("1235555 any street", "Knoxville", "TN", "12345"), ContactFreq.AccountInfo, "5553215123"),
        };


        public void Save(Contact contact)
        {
            _contacts = _contacts.Find(c => c.Id == contact.Id) != null ? _contacts.Where(x => x.Id != contact.Id).ToList() : _contacts;
            _contacts.Add(contact);
        }

        public void Remove(Contact contact)
        {
            _contacts.Remove(contact);
        }

        public List<Contact> GetContacts()
        {
            return _contacts;
        }
        public Contact? GetContactById(int id)
        {
            return _contacts.SingleOrDefault(x => x.Id == id) ?? null;
        }
    }
}
