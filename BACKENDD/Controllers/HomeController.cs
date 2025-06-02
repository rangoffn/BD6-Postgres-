using BACKENDD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKENDD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Departments = _contactService.GetAllDepartments() ?? new List<Department>();
            ViewBag.ContactTypes = _contactService.GetAllContactTypes() ?? new List<ContactType>();
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
        [HttpGet]
        public IActionResult Check()
        {
            ViewBag.Departments = _contactService.GetAllDepartments() ?? new List<Department>();
            ViewBag.ContactTypes = _contactService.GetAllContactTypes() ?? new List<ContactType>();
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Check(Contact contact)
        {
     
                bool success = await _contactService.SaveContactAsync(contact);
                if (success)
                {
                    return Json(new { success = true });
                }
            

            return Json(new { success = false, message = "Ошибка при сохранении данных" });
        }



        [HttpGet]
        // Метод для отображения всех сохраненных контактов
        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();  // Получаем все контакты
            return View(contacts);  // Отправляем их в представление
        }

        
        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _contactService.GetAllDepartments();
            ViewBag.ContactTypes = _contactService.GetAllContactTypes();

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }



                var success = await _contactService.UpdateContactAsync(contact);
                if (success)
                {
                    return RedirectToAction(nameof(ShowContacts));
                }
                ModelState.AddModelError("", "Не удалось сохранить изменения");
            

            ViewBag.Departments = _contactService.GetAllDepartments();
            ViewBag.ContactTypes = _contactService.GetAllContactTypes();
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            bool success = await _contactService.DeleteContactAsync(id);
            if (success)
            {
                return RedirectToAction("ShowContacts");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetRecentContacts()
        {
            var contacts = _contactService.GetAllContacts()
                .OrderByDescending(c => c.Id)
                .Take(5)
                .ToList();

            return PartialView("_RecentContactsPartial", contacts);
        }


    }
}