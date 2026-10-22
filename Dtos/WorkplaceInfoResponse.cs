namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.DTOs;

public sealed record WorkplaceInfoResponse(
    string Name,
    string Address,
    double Latitude,
    double Longitude,
    double RadiusMeters);
