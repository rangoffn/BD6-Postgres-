// IContactService.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BACKENDD.Models
{
    public interface IContactService
    {
        Task<bool> SaveContactAsync(Contact contact);
        List<Contact> GetAllContacts();
        Task<Contact> GetContactByIdAsync(int id);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(int id);
        List<Department> GetAllDepartments();
        List<ContactType> GetAllContactTypes();
    }
}