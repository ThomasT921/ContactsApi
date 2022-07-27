using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{

    public class ContactItemViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;
        [Required]
        public string State { get; set; } = string.Empty;
        [Required]
        public string Zip { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string ContactFreq { get; set; } = string.Empty;
    }
    public class ContactInfoViewModel
    {
       public ContactItemViewModel[] Items { get; set; } = Array.Empty<ContactItemViewModel>();
        public StateViewModel[] States { get; set; } = Array.Empty<StateViewModel>();
        public string[] FreqStrings { get; set; } = Array.Empty<string>();

    }

    public class StateViewModel
    {
        public string Abv { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}
