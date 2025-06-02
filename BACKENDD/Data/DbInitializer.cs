// DbInitializer.cs
using BACKENDD.Models;
using System.Linq;

namespace BACKENDD.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Departments.Any() || context.ContactTypes.Any() || context.Contacts.Any())
                return;

            // Departments
            var departments = new Department[]
            {
                new Department { Name = "IT", Description = "IT Department" },
                new Department { Name = "HR", Description = "Human Resources" },
                new Department { Name = "Sales", Description = "Sales Department" }
            };
            context.Departments.AddRange(departments);

            // ContactTypes
            var contactTypes = new ContactType[]
            {
                new ContactType { TypeName = "Client" },
                new ContactType { TypeName = "Partner" },
                new ContactType { TypeName = "Employee" }
            };
            context.ContactTypes.AddRange(contactTypes);

            context.SaveChanges();

            // Contacts
            var contacts = new Contact[]
            {
                new Contact {
                    Name = "Example",
                    SecName = "Example",
                    Age = 30,
                    Email = "example@example.com",
                    Message = "Example message",
                    DepartmentId = 1,
                    ContactTypeId = 1
                }
            };
            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }
    }
}