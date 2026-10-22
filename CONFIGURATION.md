# Cấu hình backend hiện tại

- Target framework: .NET 10.
- Project name / assembly: Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.
- SQL Server: LAPTOP-DB5QE99E, Windows Authentication.
- Database theo project tham khảo: AppChamCong_NguyenThanhDanh.
- Schema được tạo khi API khởi động bởi `DatabaseSeeder.InitializeAsync` và `EnsureCreatedAsync`; EF migrations cũ được giữ nguyên trên đĩa nhưng loại khỏi build để không trộn schema.
- User Secrets ID là ID mới riêng của project này. Không dùng User Secrets ID của bạn tham khảo.

Connection string đặt trong `appsettings.json`. `Jwt:Secret` trong file là placeholder phát triển; hãy thay bằng secret cục bộ qua User Secrets trước khi demo.
