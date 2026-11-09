using System.ComponentModel.DataAnnotations;

namespace wt_lab12_12auth_validation_kleshchenok.Models;

/// <summary>Пользователь приложения. Пароль хранится открытым текстом ТОЛЬКО в учебных целях.</summary>
public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Login { get; set; } = "";

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public string Password { get; set; } = "";
}
