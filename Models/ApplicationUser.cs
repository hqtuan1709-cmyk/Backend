using Microsoft.AspNetCore.Identity;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
public sealed class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public Employee? Employee { get; set; }
}
