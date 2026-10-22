# AttendanceApi backend

Backend ASP.NET Core Web API cho ứng dụng chấm công Android. Cấu trúc theo luồng Controllers → Services → Repositories → EF Core / SQL Server.

## Chạy cục bộ

1. Mở solution `Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.slnx` trong Visual Studio.
2. Chọn profile `http` và chạy API. API dùng cổng `5193`; Android Emulator gọi `http://10.0.2.2:5193`.
3. Khi khởi động, `DatabaseSeeder` gọi `EnsureCreatedAsync` và tạo bảng/roles nếu database mới. Cấu hình đang nhắm `AppChamCong_NguyenThanhDanh` trên SQL Server `LAPTOP-DB5QE99E` bằng Windows Authentication.

## Bí mật cục bộ

Project dùng User Secrets với ID riêng, không dùng ID của project tham khảo. Không commit thông tin SMTP hoặc khóa ký JWT. Từ thư mục project, cấu hình:

- `Smtp:Host`, `Smtp:Port`, `Smtp:EnableSsl`, `Smtp:Username`, `Smtp:Password`, `Smtp:From`
- `Attendance:QrSecret`
- `Jwt:Secret`

Dùng mật khẩu ứng dụng của nhà cung cấp email, không dùng mật khẩu đăng nhập thông thường. JWT secret và QR secret cần là chuỗi ngẫu nhiên riêng của bạn.

## API chính

- `POST /api/auth/register`, `POST /api/auth/login`, `POST /api/auth/verify-otp`
- Admin: CRUD `/api/employees`, `/api/shifts`, `/api/attendance`; `GET /api/reports/attendance`
- Nhân viên: `POST /api/attendance/check-in`, `POST /api/attendance/check-out`, `GET /api/attendance/my`
- Admin cấp QR theo ngày: `GET /api/attendance/qr`
- Kiểm tra sức khỏe: `GET /api/health`; OpenAPI: `/openapi/v1.json`; Swagger UI: `/swagger`

Ứng dụng tham khảo dùng `EnsureCreated` lúc chạy, không dùng EF migrations. Do đó không cần chạy `Update-Database` cho phiên bản này.
