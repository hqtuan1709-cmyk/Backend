using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;
public sealed class JwtService(UserManager<ApplicationUser> users, IConfiguration config) : IJwtService
{
    public async Task<string> CreateAsync(ApplicationUser user)
    {
        var secret = config["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret is missing.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Sub, user.Id), new(ClaimTypes.NameIdentifier, user.Id), new(ClaimTypes.Name, user.UserName ?? user.Email ?? user.Id), new(JwtRegisteredClaimNames.Email, user.Email ?? ""), new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
        claims.AddRange((await users.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role)));
        var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(config.GetValue("Jwt:ExpiryMinutes", 120)), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
