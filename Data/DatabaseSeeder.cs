using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Data;
public static class DatabaseSeeder
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in new[] { "Admin", "Employee" })
            if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
    }
}

