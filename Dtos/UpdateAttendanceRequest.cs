using System.ComponentModel.DataAnnotations;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
public sealed class UpdateAttendanceRequest
{
    [MaxLength(30)] public string? Status { get; init; }
    [MaxLength(500)] public string? Note { get; init; }
}
