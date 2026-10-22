using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Repositories;
public interface IEmployeeRepository
{
    Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken ct = default);
    Task<Employee?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Employee?> GetByUserIdAsync(string userId, CancellationToken ct = default);
    Task<Employee> AddAsync(Employee employee, CancellationToken ct = default);
    Task UpdateAsync(Employee employee, CancellationToken ct = default);
    Task DeleteAsync(Employee employee, CancellationToken ct = default);
}
