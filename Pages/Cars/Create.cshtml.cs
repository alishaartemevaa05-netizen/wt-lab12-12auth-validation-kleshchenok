using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages.Cars;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    public CreateModel(AppDbContext context) { _context = context; }

    [BindProperty]
    public Car Car { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Cars.Add(Car);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}