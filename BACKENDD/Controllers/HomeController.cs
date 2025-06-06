using BACKENDD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BACKENDD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMemoryCache _memoryCache;


        public HomeController(
            IContactService contactService,
            IMemoryCache memoryCache = null) 
        {
            _contactService = contactService;
            _memoryCache = memoryCache;
            
        }



        public IActionResult Index()
        {
            const string cacheKey = "contacts_cache_key";

            // Пытаемся получить данные из кэша
            if (!_memoryCache.TryGetValue(cacheKey, out List<Contact> contacts))
            {
                // Если данных нет в кэше, загружаем их из базы
                contacts = _contactService.GetAllContacts();

                // Настраиваем параметры кэширования (время жизни 5 минут)
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, contacts, cacheOptions);
            }

            return View();
        }
        public IActionResult NewTABBB()

        {
  
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        // Обработка формы и сохранение контакта
        [HttpPost]
        public async Task<IActionResult> Check(Contact contact)
        {
            if (ModelState.IsValid)
            {
                bool success = await _contactService.SaveContactAsync(contact);

                if (success)
                {
                    return RedirectToAction("ShowContacts");  // Перенаправляем на страницу с контактами
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при сохранении данных.");
                }
            }

            return View("Index");
        }

        // Метод для отображения всех сохраненных контактов
        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();  // Получаем все контакты
            return View(contacts);  // Отправляем их в представление
        }
    }
}
