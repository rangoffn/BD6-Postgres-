using BACKENDD.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        // Метод для сохранения контакта
        public async Task<bool> SaveContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact); 
            await _context.SaveChangesAsync(); 
            return true;  
        }

        
        public List<Contact> GetAllContacts()
        {
            return _context.Contacts.OrderBy(c => c.Id).ToList(); 
        }
    }
}
