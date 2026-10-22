namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;
public interface IOtpService
{
    Task CreateAndSendAsync(string userId, string email, CancellationToken ct = default);
    bool Verify(string userId, string code);
}

