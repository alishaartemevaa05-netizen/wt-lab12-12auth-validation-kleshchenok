using System.ComponentModel.DataAnnotations;

namespace wt_lab12_12auth_validation_kleshchenok.Models;

public class RentalCreateRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Укажите автомобиль")]
    public int CarId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Укажите клиента")]
    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
}