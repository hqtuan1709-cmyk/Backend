using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
using Microsoft.AspNetCore.Identity;

namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;

public sealed class IdentityBootstrapper(
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration)
{
    public async Task EnsureReadyAsync(CancellationToken cancellationToken)
    {
        foreach (var role in new[] { "Admin", "Employee" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                    throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        var email = configuration["BootstrapAdmin:Email"];
        var userName = configuration["BootstrapAdmin:UserName"];
        var password = configuration["BootstrapAdmin:Password"];
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            return;

        var admin = await userManager.FindByEmailAsync(email);
        if (admin is null)
        {
            admin = new ApplicationUser { UserName = userName, Email = email, EmailConfirmed = true, LockoutEnabled = true, FullName = "System Administrator" };
            var createResult = await userManager.CreateAsync(admin, password);
            if (!createResult.Succeeded)
                throw new InvalidOperationException("Bootstrap admin could not be created: " + string.Join("; ", createResult.Errors.Select(e => e.Description)));
        }
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            var roleResult = await userManager.AddToRoleAsync(admin, "Admin");
            if (!roleResult.Succeeded)
                throw new InvalidOperationException("Bootstrap admin role could not be assigned: " + string.Join("; ", roleResult.Errors.Select(e => e.Description)));
        }
    }
}

