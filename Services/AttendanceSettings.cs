namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;

public sealed class AttendanceSettings
{
    public string WorkplaceName { get; set; } = "Địa điểm demo";
    public string WorkplaceAddress { get; set; } = "Địa điểm demo - thay bằng địa chỉ nơi làm việc";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double RadiusMeters { get; set; } = 150;
}

