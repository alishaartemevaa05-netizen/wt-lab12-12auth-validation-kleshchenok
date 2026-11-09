using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages.Rentals;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    public CreateModel(AppDbContext context) { _context = context; }

    [BindProperty]
    public RentalCreateRequest RentalRequest { get; set; } = new();

    public SelectList AvailableCars { get; set; } = null!;
    public SelectList Customers { get; set; } = null!;

    public void OnGet()
    {
        LoadLists();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        LoadLists();

        var car = await _context.Cars.FindAsync(RentalRequest.CarId);
        if (car == null || !car.IsAvailable)
            ModelState.AddModelError("", "Автомобиль недоступен");

        if (!ModelState.IsValid) return Page();

        var days = (RentalRequest.EndDate.Date - RentalRequest.StartDate.Date).Days;
        if (days <= 0) days = 1;

        var rental = new Rental
        {
            CarId = car!.Id,
            CustomerId = RentalRequest.CustomerId,
            StartDate = RentalRequest.StartDate.Date,
            EndDate = RentalRequest.EndDate.Date,
            TotalPrice = days * car.DailyPrice,
            Status = RentalStatus.Active
        };

        _context.Rentals.Add(rental);
        car.IsAvailable = false;
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void LoadLists()
    {
        AvailableCars = new SelectList(_context.Cars.Where(c => c.IsAvailable), "Id", "FullName");
        Customers = new SelectList(_context.Customers, "Id", "FullName");
    }
}