using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages.Cars;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;
    public EditModel(AppDbContext context) { _context = context; }

    [BindProperty]
    public Car Car { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return NotFound();
        Car = car;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var car = await _context.Cars.FindAsync(Car.Id);
        if (car == null) return NotFound();

        car.Brand = Car.Brand;
        car.Model = Car.Model;
        car.Year = Car.Year;
        car.Category = Car.Category;
        car.Transmission = Car.Transmission;
        car.FuelType = Car.FuelType;
        car.Seats = Car.Seats;
        car.DailyPrice = Car.DailyPrice;
        car.IsAvailable = Car.IsAvailable;
        car.Description = Car.Description;

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}