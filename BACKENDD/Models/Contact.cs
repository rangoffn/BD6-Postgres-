// Models/Contact.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKENDD.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Введите Имя")]
        [Required(ErrorMessage = "ЗАПОЛНИТЕ!")]
        public string Name { get; set; }

        [Display(Name = "Введите Фамилию")]
        [Required(ErrorMessage = "ЗАПОЛНИТЕ!")]
        public string SecName { get; set; }

        [Display(Name = "Введите Возраст")]
        [Required(ErrorMessage = "ЗАПОЛНИТЕ!")]
        public int Age { get; set; }

        [Display(Name = "Введите Почту")]
        [Required(ErrorMessage = "ЗАПОЛНИТЕ!")]
        public string Email { get; set; }

        [Display(Name = "Введите Сообщение")]
        [Required(ErrorMessage = "ЗАПОЛНИТЕ!")]
        public string Message { get; set; }

        // Внешние ключи для новых таблиц
        // Внешний ключ и навигационное свойство для Department
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Внешний ключ и навигационное свойство для ContactType
        [ForeignKey("ContactType")]
        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
    }
}
