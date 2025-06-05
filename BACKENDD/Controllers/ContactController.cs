using Microsoft.AspNetCore.Mvc;
using BACKENDD.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


[Authorize(Roles = "Admin,User")]
public class ContactController : Controller
{
    private readonly AppDbContext _context;

    public ContactController(AppDbContext context)
    {
        _context = context;
    }

    // Метод для получения всех контактов
    public IActionResult Index()
    {
        var contacts = _context.Contacts.ToList();  // Получаем все контакты из базы
        return View(contacts);  // Отправляем данные в представление
    }
}
