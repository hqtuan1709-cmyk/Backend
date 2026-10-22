namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;

public sealed class AttendanceAlert
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int? AttendanceRecordId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public bool IsResolved { get; set; }
    public Employee Employee { get; set; } = null!;
    public AttendanceRecord? AttendanceRecord { get; set; }
}
