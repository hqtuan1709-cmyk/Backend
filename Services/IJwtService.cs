using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;
public interface IJwtService { Task<string> CreateAsync(ApplicationUser user); }
