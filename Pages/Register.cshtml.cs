using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using AppUser = wt_lab12_12auth_validation_kleshchenok.Models.User;   // псевдоним: в PageModel уже есть свойство User

namespace wt_lab12_12auth_validation_kleshchenok.Pages;

public class RegisterModel : PageModel
{
    private readonly AppDbContext _context;

    public RegisterModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Введите логин")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Логин должен содержать от 3 до 50 символов")]
    [RegularExpression(@"^[A-Za-z0-9_.-]+$",
        ErrorMessage = "Логин может содержать только латинские буквы, цифры и символы _ . -")]
    public string Login { get; set; } = "";

    [BindProperty]
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(100, MinimumLength = 4,
        ErrorMessage = "Пароль должен содержать не менее 4 символов")]
    public string Password { get; set; } = "";

    [BindProperty]
    [Display(Name = "Повторите пароль")]
    [Required(ErrorMessage = "Повторите пароль")]
    // Полное имя атрибута: иначе возможна ошибка CS0104 (неоднозначность с Microsoft.AspNetCore.Mvc.CompareAttribute)
    [System.ComponentModel.DataAnnotations.Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; } = "";

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("UserLogin") != null)
        {
            return RedirectToPage("/Profile");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // 1) Серверная валидация формы (пустые поля, длина, формат логина, совпадение паролей)
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // 2) Логин должен быть уникальным
        var exists = await _context.Users.AnyAsync(x => x.Login == Login);
        if (exists)
        {
            ModelState.AddModelError(nameof(Login), "Пользователь с таким логином уже существует.");
            return Page();
        }

        // 3) Сохраняем пользователя в PostgreSQL (INSERT INTO "Users" ...)
        _context.Users.Add(new AppUser { Login = Login, Password = Password });
        await _context.SaveChangesAsync();

        // 4) Переход на страницу входа: сообщение и логин передаются через TempData
        TempData["Message"] = "Регистрация выполнена, аккаунт сохранён в базе данных. Теперь войдите, указав логин и пароль.";
        TempData["RegisteredLogin"] = Login;
        return RedirectToPage("/Login");
    }
}
