// ContactController.cs
using BACKENDD.Models;
using Microsoft.AspNetCore.Mvc;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public IActionResult ShowContacts()
    {
        var contacts = _contactService.GetAllContacts();
        return View(contacts);
    }
}