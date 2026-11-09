using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace wt_lab12_12auth_validation_kleshchenok.Models;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttribute<DisplayAttribute>();
        return attr?.Name ?? value.ToString();
    }
}