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

            // �������� �������� ������ �� ����
            if (!_memoryCache.TryGetValue(cacheKey, out List<Contact> contacts))
            {
                // ���� ������ ��� � ����, ��������� �� �� ����
                contacts = _contactService.GetAllContacts();

                // ����������� ��������� ����������� (����� ����� 5 �����)
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

        // ��������� ����� � ���������� ��������
        [HttpPost]
        public async Task<IActionResult> Check(Contact contact)
        {
            if (ModelState.IsValid)
            {
                bool success = await _contactService.SaveContactAsync(contact);

                if (success)
                {
                    return RedirectToAction("ShowContacts");  // �������������� �� �������� � ����������
                }
                else
                {
                    ModelState.AddModelError("", "������ ��� ���������� ������.");
                }
            }

            return View("Index");
        }

        // ����� ��� ����������� ���� ����������� ���������
        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();  // �������� ��� ��������
            return View(contacts);  // ���������� �� � �������������
        }
    }
}
