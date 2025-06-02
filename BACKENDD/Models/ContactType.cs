using System.ComponentModel.DataAnnotations;

namespace BACKENDD.Models
{
    public class ContactType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}