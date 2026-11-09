using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;

namespace wt_lab12_12auth_validation_kleshchenok.Pages;

public class LoginModel : PageModel
{
    private readonly AppDbContext _context;

    public LoginModel(AppDbContext context)
    {
        _context = context;
    }

    // Model Binding: значения полей формы автоматически попадают в эти свойства
    [BindProperty]
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Введите логин")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Логин должен содержать от 3 до 50 символов")]
    public string Login { get; set; } = "";

    [BindProperty]
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(100, MinimumLength = 4,
        ErrorMessage = "Пароль должен содержать не менее 4 символов")]
    public string Password { get; set; } = "";

    public IActionResult OnGet()
    {
        // Уже вошедшему пользователю форма входа не нужна
        if (HttpContext.Session.GetString("UserLogin") != null)
        {
            return RedirectToPage("/Profile");
        }

        // После регистрации логин подставляется в форму автоматически
        Login = TempData["RegisteredLogin"] as string ?? "";
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // 1) Серверная валидация — ДО обращения к базе данных
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // 2) Поиск пользователя в PostgreSQL (SELECT ... WHERE "Login" = @p AND "Password" = @p)
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Login == Login && x.Password == Password);

        if (user == null)
        {
            // Пустой ключ "" — общая ошибка формы
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            return Page();
        }

        // 3) Запоминаем вход в Session
        HttpContext.Session.SetString("UserLogin", user.Login);

        return RedirectToPage("/Profile");
    }
}
