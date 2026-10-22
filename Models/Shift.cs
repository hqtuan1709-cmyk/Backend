namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
public sealed class Shift
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsActive { get; set; } = true;
}
