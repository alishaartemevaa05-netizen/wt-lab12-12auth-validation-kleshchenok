using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Models;

namespace wt_lab12_12auth_validation_kleshchenok.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Car> Cars => Set<Car>();
	public DbSet<Customer> Customers => Set<Customer>();
	public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Настройка Enum в строки для БД
		modelBuilder.Entity<Car>(e =>
		{
			e.Property(c => c.Category).HasConversion<string>().HasMaxLength(20);
			e.Property(c => c.Transmission).HasConversion<string>().HasMaxLength(20);
			e.Property(c => c.FuelType).HasConversion<string>().HasMaxLength(20);
			e.Property(c => c.DailyPrice).HasPrecision(10, 2);
		});

		modelBuilder.Entity<Rental>(e =>
		{
			e.Property(r => r.Status).HasConversion<string>().HasMaxLength(20);
			e.Property(r => r.TotalPrice).HasPrecision(10, 2);
		});
	}
}