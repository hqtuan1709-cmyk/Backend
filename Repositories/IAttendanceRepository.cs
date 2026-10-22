using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Repositories;
public interface IAttendanceRepository
{
    Task<IReadOnlyList<AttendanceRecord>> GetAllAsync(int? employeeId = null, CancellationToken ct = default);
    Task<AttendanceRecord?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<AttendanceRecord?> GetOpenRecordAsync(int employeeId, DateOnly day, CancellationToken ct = default);
    Task<AttendanceRecord> AddAsync(AttendanceRecord record, CancellationToken ct = default);
    Task UpdateAsync(AttendanceRecord record, CancellationToken ct = default);
    Task DeleteAsync(AttendanceRecord record, CancellationToken ct = default);
}
