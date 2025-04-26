
using BACKENDD.Models;

namespace BACKENDD.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Пusto or no
            if (context.Contacts.Any())
                return;

            // Добавляем начальные данные
            var contacts = new Contact[]
            {
                new Contact { Name = "Пример", SecName = "Пример", Age = 30, Email = "Пример@example.com", Message = "Пример!!!!" },
            };

            context.Contacts.AddRange(contacts);
            context.SaveChanges();  // Сохраняем изменения
        }
    }
}
