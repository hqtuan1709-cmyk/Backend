namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;

public sealed class OtpChallenge
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CodeHash { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAtUtc { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
    public int FailedAttempts { get; set; }
    public bool IsConsumed { get; set; }
    public ApplicationUser User { get; set; } = null!;
}
