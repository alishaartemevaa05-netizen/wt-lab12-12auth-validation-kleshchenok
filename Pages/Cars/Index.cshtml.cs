using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Pages.Cars;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    public IndexModel(AppDbContext context) { _context = context; }

    public List<Car> Cars { get; set; } = new();

    // Свойства для фильтрации и поиска
    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public CarCategory? Category { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Cars.AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(c =>
                c.Brand.Contains(Search) ||
                c.Model.Contains(Search));
        }

        if (Category.HasValue)
        {
            query = query.Where(c => c.Category == Category.Value);
        }

        Cars = await query.OrderBy(c => c.Id).ToListAsync();
    }
}