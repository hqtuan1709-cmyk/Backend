using System.ComponentModel.DataAnnotations;

namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Dtos;

public sealed class RegisterRequest
{
    [Required, StringLength(50, MinimumLength = 3)]
    public string UserName { get; init; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; init; } = string.Empty;

    [Required, MinLength(8), StringLength(100)]
    public string Password { get; init; } = string.Empty;

    [Required, StringLength(150)]
    public string FullName { get; init; } = string.Empty;
}

public sealed class LoginRequest
{
    [Required]
    public string UserName { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}

public sealed class VerifyOtpRequest
{
    [Required]
    public Guid ChallengeId { get; init; }

    [Required, StringLength(6, MinimumLength = 6)]
    public string Code { get; init; } = string.Empty;
}

public sealed record LoginChallengeResponse(Guid ChallengeId, string MaskedEmail, DateTimeOffset ExpiresAtUtc);
public sealed record AuthResponse(string AccessToken, DateTimeOffset ExpiresAtUtc, string UserId, string UserName, string Role, int? EmployeeId);

public sealed class CreateEmployeeRequest
{
    [Required, StringLength(50, MinimumLength = 3)] public string UserName { get; init; } = string.Empty;
    [Required, EmailAddress] public string Email { get; init; } = string.Empty;
    [Required, MinLength(8), StringLength(100)] public string Password { get; init; } = string.Empty;
    [Required, StringLength(150)] public string FullName { get; init; } = string.Empty;
    [StringLength(100)] public string? Department { get; init; }
    [StringLength(30)] public string? EmployeeCode { get; init; }
}

public sealed class UpdateEmployeeRequest
{
    [Required, StringLength(150)] public string FullName { get; init; } = string.Empty;
    [EmailAddress] public string? Email { get; init; }
    [StringLength(100)] public string? Department { get; init; }
    public bool IsActive { get; init; } = true;
}

public sealed class UpdateAttendanceRequest
{
    public DateTimeOffset? CheckInAt { get; init; }
    public DateTimeOffset? CheckOutAt { get; init; }
    public string? Status { get; init; }
    [StringLength(500)] public string? AnomalyNote { get; init; }
}

public sealed class AttendancePunchRequest
{
    [Required] public string QrCode { get; init; } = string.Empty;
    [Range(-90, 90)] public double Latitude { get; init; }
    [Range(-180, 180)] public double Longitude { get; init; }
}
