using Contacts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetContacts : ControllerBase
    {
        private readonly IContactsDataAccess _contactsAccess;

        private static List<StateViewModel> _states = new()
        {
            new StateViewModel {Abv = "AL", Name = "Alabama"},
            new StateViewModel {Abv = "AK", Name = "Alaska"},
            new StateViewModel {Abv = "AZ", Name = "Arizona"},
            new StateViewModel {Abv = "AR", Name = "Arkansas"},
            new StateViewModel {Abv = "CA", Name = "California"},
            new StateViewModel {Abv = "CO", Name = "Colorado"},
            new StateViewModel {Abv = "CT", Name = "Connecticut"},
            new StateViewModel {Abv = "DE", Name = "Delaware"},
            new StateViewModel {Abv = "DC", Name = "District Of Columbia"},
            new StateViewModel {Abv = "FL", Name = "Florida"},
            new StateViewModel {Abv = "GA", Name = "Georgia"},
            new StateViewModel {Abv = "HI", Name = "Hawaii"},
            new StateViewModel {Abv = "ID", Name = "Idaho"},
            new StateViewModel {Abv = "IL", Name = "Illinois"},
            new StateViewModel {Abv = "IN", Name = "Indiana"},
            new StateViewModel {Abv = "IA", Name = "Iowa"},
            new StateViewModel {Abv = "KS", Name = "Kansas"},
            new StateViewModel {Abv = "KY", Name = "Kentucky"},
            new StateViewModel {Abv = "LA", Name = "Louisiana"},
            new StateViewModel {Abv = "ME", Name = "Maine"},
            new StateViewModel {Abv = "MD", Name = "Maryland"},
            new StateViewModel {Abv = "MA", Name = "Massachusetts"},
            new StateViewModel {Abv = "MI", Name = "Michigan"},
            new StateViewModel {Abv = "MN", Name = "Minnesota"},
            new StateViewModel {Abv = "MS", Name = "Mississippi"},
            new StateViewModel {Abv = "MO", Name = "Missouri"},
            new StateViewModel {Abv = "MT", Name = "Montana"},
            new StateViewModel {Abv = "NE", Name = "Nebraska"},
            new StateViewModel {Abv = "NV", Name = "Nevada"},
            new StateViewModel {Abv = "NH", Name = "New Hampshire"},
            new StateViewModel {Abv = "NJ", Name = "New Jersey"},
            new StateViewModel {Abv = "NM", Name = "New Mexico"},
            new StateViewModel {Abv = "NY", Name = "New York"},
            new StateViewModel {Abv = "NC", Name = "North Carolina"},
            new StateViewModel {Abv = "ND", Name = "North Dakota"},
            new StateViewModel {Abv = "OH", Name = "Ohio"},
            new StateViewModel {Abv = "OK", Name = "Oklahoma"},
            new StateViewModel {Abv = "OR", Name = "Oregon"},
            new StateViewModel {Abv = "PA", Name = "Pennsylvania"},
            new StateViewModel {Abv = "RI", Name = "Rhode Island"},
            new StateViewModel {Abv = "SC", Name = "South Carolina"},
            new StateViewModel {Abv = "SD", Name = "South Dakota"},
            new StateViewModel {Abv = "TN", Name = "Tennessee"},
            new StateViewModel {Abv = "TX", Name = "Texas"},
            new StateViewModel {Abv = "UT", Name = "Utah"},
            new StateViewModel {Abv = "VT", Name = "Vermont"},
            new StateViewModel {Abv = "VA", Name = "Virginia"},
            new StateViewModel {Abv = "WA", Name = "Washington"},
            new StateViewModel {Abv = "WV", Name = "West Virginia"},
            new StateViewModel {Abv = "WI", Name = "Wisconsin"},
            new StateViewModel {Abv = "WY", Name = "Wyoming"}
        };

        private static Dictionary<string, ContactFreq> _contactFreqMap = new()
        {
            {"Contact only about account information", ContactFreq.AccountInfo},
            {"OK to contact with marketing information", ContactFreq.MarketingInfo},
            {"OK to contact with third-party marketing information", ContactFreq.ThirdPartyMarketingInfo},

        };

        public GetContacts(IContactsDataAccess contactsDataAccess)
        {
            _contactsAccess = contactsDataAccess;
        }
        [HttpGet]
        public ContactInfoViewModel GetContactsList()
        {
            var contacts = _contactsAccess.GetContacts().Select(x => new ContactItemViewModel
            {
                Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, City = x.Address.City, Email = x.Email, Phone = x.Phone, State = x.Address.State, Street = x.Address.Street, 
                Zip = x.Address.Zip, ContactFreq = _contactFreqMap.First(c => c.Value == x.ContactFreq).Key
            }).ToArray();

            return new ContactInfoViewModel {Items = contacts, States = _states.ToArray(), FreqStrings = _contactFreqMap.Keys.ToArray()};
        }


        [HttpPost("{id}")]
        [Consumes("application/json")]
        public ActionResult UpdateContact(int id, [FromBody]ContactItemViewModel contact)
        {
            if (!ModelState.IsValid)
                return StatusCode(400);
            var c = _contactsAccess.GetContactById(id);
            if (c == null)
                return NotFound();
            c = c.ReplaceProperties(contact.FirstName, contact.LastName, contact.Email, contact.Street, contact.City, contact.State, contact.Zip, _contactFreqMap.ContainsKey(contact.ContactFreq)? _contactFreqMap[contact.ContactFreq]: ContactFreq.AccountInfo, contact.Phone);
            _contactsAccess.Save(c);
            return Ok();
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult AddContact([FromBody] ContactItemViewModel contact)
        {
            if (!ModelState.IsValid)
                return StatusCode(400);
            var contacts = _contactsAccess.GetContacts();
            contact.Id = contacts.Count +1;

            _contactsAccess.Save(new Contact(contact.Id, contact.FirstName, contact.LastName, contact.Email,
                new Address(contact.Street, contact.City, contact.State, contact.Zip), _contactFreqMap.ContainsKey(contact.ContactFreq) ? _contactFreqMap[contact.ContactFreq] : ContactFreq.AccountInfo, contact.Phone));
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteContact(int id)
        {
            var c = _contactsAccess.GetContactById(id);
            if (c == null)
                return NotFound();
            _contactsAccess.Remove(c);
            return Ok();
        }
    }
}
