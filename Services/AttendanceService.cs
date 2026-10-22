using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Models;
using Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Repositories;
using Microsoft.Extensions.Options;

namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;

public sealed class AttendanceService(IAttendanceRepository records, IOptions<AttendanceSettings> attendanceOptions)
{
    private readonly AttendanceSettings _settings = attendanceOptions.Value;

    public WorkplaceInfoResponse GetWorkplaceInfo() => new(
        _settings.WorkplaceName,
        _settings.WorkplaceAddress,
        _settings.Latitude,
        _settings.Longitude,
        _settings.RadiusMeters);

    public async Task<AttendanceRecord> CheckInAsync(Employee employee, AttendanceRequest request, CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        if (await records.GetOpenRecordAsync(employee.Id, today, ct) is not null)
            throw new InvalidOperationException("Bạn đã chấm công vào hôm nay.");

        ValidateWorkplaceLocation(request);

        return await records.AddAsync(new AttendanceRecord
        {
            EmployeeId = employee.Id,
            CheckInAt = DateTimeOffset.UtcNow,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            // Keep the legacy database column; mark this check-in as GPS-based.
            QrCode = "GPS",
            Status = "OnTime"
        }, ct);
    }

    private static double DistanceInMeters(double latitude1, double longitude1, double latitude2, double longitude2)
    {
        const double earthRadiusMeters = 6_371_000;
        var lat1 = DegreesToRadians(latitude1);
        var lat2 = DegreesToRadians(latitude2);
        var deltaLat = DegreesToRadians(latitude2 - latitude1);
        var deltaLon = DegreesToRadians(longitude2 - longitude1);
        var a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(deltaLon / 2), 2);
        return earthRadiusMeters * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }

    private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

    public async Task<AttendanceRecord?> CheckOutAsync(int employeeId, AttendanceRequest request, CancellationToken ct = default)
    {
        var item = await records.GetOpenRecordAsync(employeeId, DateOnly.FromDateTime(DateTime.Now), ct);
        if (item is null) return null;
        ValidateWorkplaceLocation(request);
        item.CheckOutAt = DateTimeOffset.UtcNow;
        await records.UpdateAsync(item, ct);
        return item;
    }

    private void ValidateWorkplaceLocation(AttendanceRequest request)
    {
        if (!request.Latitude.HasValue || !request.Longitude.HasValue ||
            request.Latitude.Value < -90 || request.Latitude.Value > 90 ||
            request.Longitude.Value < -180 || request.Longitude.Value > 180)
            throw new InvalidOperationException("Không nhận được tọa độ GPS hợp lệ từ thiết bị.");
        if (_settings.Latitude is < -90 or > 90 || _settings.Longitude is < -180 or > 180 || _settings.RadiusMeters <= 0)
            throw new InvalidOperationException("Cấu hình Attendance trong appsettings.json chưa hợp lệ.");

        var distanceMeters = DistanceInMeters(
            request.Latitude.Value,
            request.Longitude.Value,
            _settings.Latitude,
            _settings.Longitude);
        if (distanceMeters > _settings.RadiusMeters)
            throw new InvalidOperationException(
                $"Bạn đang cách {_settings.WorkplaceName} khoảng {distanceMeters:0} m; bán kính chấm công cho phép là {_settings.RadiusMeters:0} m.");
    }
}

