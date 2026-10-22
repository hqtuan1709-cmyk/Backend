namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
public sealed class AttendanceRecord
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTimeOffset CheckInAt { get; set; }
    public DateTimeOffset? CheckOutAt { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string QrCode { get; set; } = string.Empty;
    public string Status { get; set; } = "OnTime";
    public string? Note { get; set; }
}
