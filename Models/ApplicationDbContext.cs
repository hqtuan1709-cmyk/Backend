using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<OtpChallenge> OtpChallenges => Set<OtpChallenge>();
    public DbSet<AttendanceAlert> AttendanceAlerts => Set<AttendanceAlert>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.HasIndex(e => e.EmployeeCode).IsUnique();
            entity.Property(e => e.EmployeeCode).HasMaxLength(30).IsRequired();
            entity.Property(e => e.FullName).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.HasOne(e => e.User).WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasIndex(a => new { a.EmployeeId, a.WorkDate }).IsUnique();
            entity.Property(a => a.Status).HasMaxLength(30).IsRequired();
            entity.Property(a => a.AnomalyNote).HasMaxLength(500);
            entity.HasOne(a => a.Employee).WithMany(e => e.AttendanceRecords)
                .HasForeignKey(a => a.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AttendanceAlert>(entity =>
        {
            entity.Property(a => a.Type).HasMaxLength(40).IsRequired();
            entity.Property(a => a.Details).HasMaxLength(500).IsRequired();
            entity.HasOne(a => a.Employee).WithMany().HasForeignKey(a => a.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(a => a.AttendanceRecord).WithMany().HasForeignKey(a => a.AttendanceRecordId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<OtpChallenge>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.HasIndex(o => new { o.UserId, o.CreatedAtUtc });
            entity.Property(o => o.CodeHash).HasMaxLength(500).IsRequired();
            entity.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

