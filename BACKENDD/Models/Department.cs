// Models/Department.cs
using System.ComponentModel.DataAnnotations;

namespace BACKENDD.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}

