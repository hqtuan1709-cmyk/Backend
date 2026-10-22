using System.ComponentModel.DataAnnotations;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
public sealed class VerifyOtpRequest
{
    [Required] public string Username { get; init; } = string.Empty;
    [Required, MinLength(6), MaxLength(6)] public string Code { get; init; } = string.Empty;
}
