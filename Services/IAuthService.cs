using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;
public interface IAuthService
{
    Task<(bool Ok, string Message)> RegisterAsync(RegisterRequest request);
    Task<(bool Ok, string Message)> LoginAsync(LoginRequest request);
    Task<(bool Ok, string Message, string? Token, string? Role, string? Name)> VerifyOtpAsync(VerifyOtpRequest request);
}

