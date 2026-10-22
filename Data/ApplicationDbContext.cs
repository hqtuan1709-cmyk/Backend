using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Data;
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Employee>().HasIndex(x => x.IdentityUserId).IsUnique();
        builder.Entity<Employee>().HasOne(x => x.User).WithOne(x => x.Employee).HasForeignKey<Employee>(x => x.IdentityUserId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<AttendanceRecord>().HasOne(x => x.Employee).WithMany(x => x.AttendanceRecords).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Employee>().Property(x => x.FullName).HasMaxLength(150).IsRequired();
        builder.Entity<Employee>().Property(x => x.Department).HasMaxLength(100);
        builder.Entity<AttendanceRecord>().Property(x => x.Status).HasMaxLength(30).IsRequired();
        builder.Entity<AttendanceRecord>().HasIndex(x => new { x.EmployeeId, x.CheckInAt });
        builder.Entity<OtpCode>().HasIndex(x => new { x.UserId, x.ExpiresAt });
        builder.Entity<OtpCode>().Property(x => x.CodeHash).HasMaxLength(64).IsRequired();
    }
}

