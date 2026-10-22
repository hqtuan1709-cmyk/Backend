namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;

public sealed record EmployeeResponse(
    int Id,
    string FullName,
    string Email,
    string Department,
    string Position,
    bool IsActive);
