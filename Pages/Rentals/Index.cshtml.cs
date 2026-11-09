using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages.Rentals;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    public IndexModel(AppDbContext context) { _context = context; }

    public List<RentalListItem> Rentals { get; set; } = new();

    public async Task OnGetAsync()
    {
        Rentals = await _context.Rentals.Select(r => new RentalListItem
        {
            Id = r.Id,
            CarId = r.CarId,
            CarName = _context.Cars.FirstOrDefault(c => c.Id == r.CarId)!.FullName ?? "(удалён)",
            CustomerId = r.CustomerId,
            CustomerName = _context.Customers.FirstOrDefault(c => c.Id == r.CustomerId)!.FullName ?? "(удалён)",
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalPrice = r.TotalPrice,
            Status = r.Status
        }).ToListAsync();
    }

    public async Task<IActionResult> OnPostReturnAsync(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null) return NotFound();

        rental.Status = RentalStatus.Completed;
        var car = await _context.Cars.FindAsync(rental.CarId);
        if (car != null) car.IsAvailable = true;

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}