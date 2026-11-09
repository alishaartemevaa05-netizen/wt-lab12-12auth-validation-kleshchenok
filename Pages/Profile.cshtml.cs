using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wt_lab12_12auth_validation_kleshchenok.Pages;

public class ProfileModel : PageModel
{
    public string? UserLogin { get; private set; }

    public IActionResult OnGet()
    {
        // Защита страницы: если в Session нет логина — пользователь не входил
        UserLogin = HttpContext.Session.GetString("UserLogin");

        if (UserLogin == null)
        {
            return RedirectToPage("/Login");
        }

        return Page();
    }

    // Выход: очищаем Session и возвращаем на страницу входа
    public IActionResult OnPost()
    {
        HttpContext.Session.Clear();

        return RedirectToPage("/Login");
    }
}
