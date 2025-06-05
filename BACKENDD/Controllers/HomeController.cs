using BACKENDD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACKENDD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult NewTABBB()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
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

        [Authorize(Roles = "Admin,User")]
        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();  // �������� ��� ��������
            return View(contacts);  // ���������� �� � �������������
        }
    }
}
