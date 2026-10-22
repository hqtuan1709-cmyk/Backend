using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;

public sealed class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int LifetimeMinutes { get; set; } = 60;
}

public sealed record IssuedToken(string Token, DateTimeOffset ExpiresAtUtc);

public interface IJwtTokenService
{
    Task<IssuedToken> CreateAsync(ApplicationUser user, CancellationToken cancellationToken);
}

public sealed class JwtTokenService(
    IOptions<JwtSettings> options,
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext db) : IJwtTokenService
{
    private readonly JwtSettings _settings = options.Value;

    public async Task<IssuedToken> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_settings.Secret);
        if (keyBytes.Length < 32 || string.IsNullOrWhiteSpace(_settings.Issuer) || string.IsNullOrWhiteSpace(_settings.Audience))
            throw new InvalidOperationException("JWT is not configured. Set Jwt__Issuer, Jwt__Audience and a Jwt__Secret of at least 32 characters.");

        var roles = await userManager.GetRolesAsync(user);
        var employeeId = await db.Employees.Where(e => e.UserId == user.Id)
            .Select(e => (int?)e.Id).SingleOrDefaultAsync(cancellationToken);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? user.Email ?? user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        if (employeeId.HasValue)
            claims.Add(new Claim("employeeId", employeeId.Value.ToString()));

        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(Math.Clamp(_settings.LifetimeMinutes, 5, 1440));
        var signingKey = new SymmetricSecurityKey(keyBytes);
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt.UtcDateTime,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
        return new IssuedToken(new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}

