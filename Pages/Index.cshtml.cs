using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    public IndexModel(AppDbContext context) { _context = context; }

    public int TotalCars { get; set; }
    public int AvailableCars { get; set; }
    public int TotalCustomers { get; set; }
    public int ActiveRentals { get; set; }

    public async Task OnGetAsync()
    {
        TotalCars = await _context.Cars.CountAsync();
        AvailableCars = await _context.Cars.CountAsync(c => c.IsAvailable);
        TotalCustomers = await _context.Customers.CountAsync();
        ActiveRentals = await _context.Rentals.CountAsync(r => r.Status == RentalStatus.Active);
    }
}