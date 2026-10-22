using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Controllers;
[ApiController, Route("api/auth")]
public sealed class AuthController(IAuthService auth) : ControllerBase
{
    [AllowAnonymous, HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await auth.RegisterAsync(request);
        return result.Ok ? Ok(new { success = true, result.Message }) : BadRequest(new { success = false, result.Message });
    }
    [AllowAnonymous, HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await auth.LoginAsync(request);
        return result.Ok ? Ok(new { success = true, otpRequired = true, result.Message }) : Unauthorized(new { success = false, result.Message });
    }
    [AllowAnonymous, HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
    {
        var result = await auth.VerifyOtpAsync(request);
        return result.Ok ? Ok(new { success = true, result.Message, token = result.Token, role = result.Role, fullName = result.Name }) : Unauthorized(new { success = false, result.Message });
    }
}

