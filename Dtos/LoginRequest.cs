using System.ComponentModel.DataAnnotations;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
public sealed class LoginRequest
{
    [Required] public string Username { get; init; } = string.Empty;
    [Required] public string Password { get; init; } = string.Empty;
}
