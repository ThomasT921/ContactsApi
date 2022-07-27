namespace Contacts.Models
{
    public class Contact
    {
        public Contact(int id, string firstName, string lastName, string email, Address address, ContactFreq freq, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            ContactFreq = freq;
            Phone = phone;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public ContactFreq ContactFreq { get; set; }
        public Contact ReplaceProperties(string firstName, string lastName, string email, string street, string city, string state, string zip,
            ContactFreq freq, string phone) => new (Id, firstName, lastName, email, new Address(street,city,state,zip),freq, phone);
    }

    public enum ContactFreq
    {
        AccountInfo,
        MarketingInfo,
        ThirdPartyMarketingInfo
    }

    public class Address
    {
        public Address(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            Zip = zip;
        }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
