using System.ComponentModel.DataAnnotations;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
public sealed class AttendanceRequest
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}
