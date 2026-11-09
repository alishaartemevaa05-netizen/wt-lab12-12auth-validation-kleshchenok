using System.ComponentModel.DataAnnotations;

namespace wt_lab12_12auth_validation_kleshchenok.Models;

public class Car
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите марку автомобиля")]
    [StringLength(50)]
    [Display(Name = "Марка")]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите модель")]
    [StringLength(50)]
    [Display(Name = "Модель")]
    public string Model { get; set; } = string.Empty;

    [Range(1990, 2030, ErrorMessage = "Год от 1990 до 2030")]
    [Display(Name = "Год выпуска")]
    public int Year { get; set; } = DateTime.Now.Year;

    [Display(Name = "Категория")]
    public CarCategory Category { get; set; }

    [Display(Name = "Коробка передач")]
    public Transmission Transmission { get; set; }

    [Display(Name = "Топливо")]
    public FuelType FuelType { get; set; }

    [Range(2, 9, ErrorMessage = "Мест: от 2 до 9")]
    [Display(Name = "Мест")]
    public int Seats { get; set; } = 5;

    [Range(1, 10000, ErrorMessage = "Цена: от 1 до 10000")]
    [Display(Name = "Цена за сутки")]
    public decimal DailyPrice { get; set; }

    [Display(Name = "Доступен")]
    public bool IsAvailable { get; set; } = true;

    [StringLength(500)]
    [Display(Name = "Описание")]
    public string? Description { get; set; }

    public string FullName => $"{Brand} {Model}";

    [StringLength(30)]
    [Display(Name = "Цвет")]
    public string? Color { get; set; }
}