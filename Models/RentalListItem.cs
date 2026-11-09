namespace wt_lab12_12auth_validation_kleshchenok.Models;

public class RentalListItem
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string CarName { get; set; } = "";
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public RentalStatus Status { get; set; }
}