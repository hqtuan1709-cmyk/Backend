using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Repositories;
public interface IShiftRepository
{
    Task<IReadOnlyList<Shift>> GetAllAsync(CancellationToken ct = default);
    Task<Shift?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Shift> AddAsync(Shift shift, CancellationToken ct = default);
    Task UpdateAsync(Shift shift, CancellationToken ct = default);
    Task DeleteAsync(Shift shift, CancellationToken ct = default);
}
