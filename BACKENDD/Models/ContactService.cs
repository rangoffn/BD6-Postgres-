// ContactService.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BACKENDD.Models
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveContactAsync(Contact contact)
        {
            // Проверяем, что выбранные Department и ContactType существуют
            if (contact.DepartmentId <= 0 || contact.ContactTypeId <= 0)
            {
                return false;
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return true;
        }
        public List<Contact> GetAllContacts()
        {
            return _context.Contacts
                .Include(c => c.Department)  // Подгружаем связанный отдел
                .Include(c => c.ContactType) // Подгружаем связанный тип
                .ToList();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _context.Contacts
                .Include(c => c.Department)
                .Include(c => c.ContactType)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            try
            {
                var existingContact = await _context.Contacts.FindAsync(contact.Id);
                if (existingContact == null) return false;

                // Обновляем только изменяемые поля
                existingContact.Name = contact.Name;
                existingContact.SecName = contact.SecName;
                existingContact.Age = contact.Age;
                existingContact.Email = contact.Email;
                existingContact.Message = contact.Message;
                existingContact.DepartmentId = contact.DepartmentId;
                existingContact.ContactTypeId = contact.ContactTypeId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                Console.WriteLine($"Ошибка при обновлении: ");

                return false;
            }
        }
        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public List<ContactType> GetAllContactTypes()
        {
            return _context.ContactTypes.ToList();
        }
    }
}