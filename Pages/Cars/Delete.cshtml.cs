using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages.Cars;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;
    public DeleteModel(AppDbContext context) { _context = context; }

    [BindProperty]
    public Car? Car { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        if (Car == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}