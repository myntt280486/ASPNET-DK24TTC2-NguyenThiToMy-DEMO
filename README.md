# Tố My Cafe - Website Bán Cafe Giải Khát - ASP.NET MVC

## Giới thiệu
Website bán cafe và đồ uống giải khát **Tố My Cafe** được xây dựng bằng ASP.NET MVC Core 9.0 và SQL Server. Hệ thống cung cấp đầy đủ chức năng cho cả người dùng và quản trị viên.

## Công nghệ sử dụng
- **Framework**: ASP.NET Core MVC 9.0
- **Database**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core 9.0
- **Authentication**: ASP.NET Core Identity
- **Frontend**: HTML, CSS, JavaScript, Bootstrap 5
- **Session Management**: ASP.NET Core Session

## Tính năng

### Dành cho Người dùng (User)

#### 1. Quản lý Sản phẩm và Đặt hàng
- ✅ **Xem danh mục sản phẩm**: Hiển thị các loại sản phẩm (Cà phê, Trà, Nước ép, Sinh tố, Bánh ngọt)
- ✅ **Chi tiết sản phẩm**: Xem thông tin chi tiết (tên, mô tả, giá, tùy chọn size/đường/đá, hình ảnh)
- ✅ **Tìm kiếm & Lọc**:
  - Tìm kiếm theo tên sản phẩm
  - Lọc theo danh mục
  - Lọc theo khoảng giá
- ✅ **Giỏ hàng**:
  - Thêm/Bớt/Cập nhật số lượng sản phẩm
  - Xem tổng tiền tạm tính
  - Lưu trữ giỏ hàng trong Session
- ✅ **Thanh toán**:
  - Nhập thông tin giao hàng
  - Lựa chọn phương thức thanh toán (COD, Thẻ/Ví điện tử)
  - Xác nhận và đặt hàng

#### 2. Quản lý Tài khoản & Cá nhân hóa
- ✅ **Đăng ký/Đăng nhập**: Bằng email và mật khẩu
- ✅ **Quản lý thông tin cá nhân**: Cập nhật tên, địa chỉ, số điện thoại
- ✅ **Lịch sử đơn hàng**: Xem tất cả đơn hàng đã đặt với trạng thái
  - Đang xử lý (Pending)
  - Đang xử lý (Processing)
  - Đang giao (Shipping)
  - Đã hoàn thành (Completed)
  - Đã hủy (Cancelled)
- ✅ **Theo dõi đơn hàng**: Kiểm tra trạng thái hiện tại của đơn hàng
- ✅ **Đánh giá sản phẩm**: Đăng tải đánh giá (text & sao) sau khi mua hàng

### Dành cho Quản trị viên (Admin)

#### 1. Quản lý Sản phẩm (Product Management)
- ✅ Thêm/Sửa/Xóa sản phẩm (tên, mô tả, giá, hình ảnh)
- ✅ Upload hình ảnh sản phẩm
- ✅ Quản lý trạng thái sản phẩm (Active/Inactive)
- ✅ Quản lý Danh mục: Thêm/Sửa/Xóa các danh mục sản phẩm
- ✅ Quản lý Thuộc tính: Thiết lập các tùy chọn sản phẩm (Size S/M/L, Lượng đường, Đá)

#### 2. Quản lý Đơn hàng (Order Management)
- ✅ Xem danh sách tất cả đơn hàng
- ✅ Lọc đơn hàng theo trạng thái
- ✅ Cập nhật trạng thái đơn hàng
- ✅ Xem chi tiết đơn hàng (sản phẩm, số lượng, tổng tiền, thông tin khách hàng)
- ✅ Xóa đơn hàng

#### 3. Quản lý Khách hàng (User Management)
- ✅ Xem danh sách tất cả người dùng đã đăng ký
- ✅ Xem chi tiết thông tin khách hàng
- ✅ Xem lịch sử mua hàng của khách hàng
- ✅ Xem tổng số đơn hàng và tổng chi tiêu
- ✅ Khóa/Mở khóa tài khoản người dùng

#### 4. Dashboard
- ✅ Thống kê tổng quan:
  - Tổng số sản phẩm
  - Tổng số đơn hàng
  - Tổng số người dùng
  - Tổng doanh thu
  - Số đơn hàng đang chờ xử lý
- ✅ Danh sách đơn hàng gần đây

## Cấu trúc Database

### Bảng chính:
1. **AspNetUsers** (Identity): Thông tin người dùng
   - Mở rộng: FullName, Address

2. **Categories**: Danh mục sản phẩm
   - Id, Name, Description

3. **Products**: Sản phẩm
   - Id, Name, Description, Price, ImageUrl, IsActive, CategoryId

4. **ProductOptions**: Tùy chọn sản phẩm
   - Id, ProductId, Group, Name, PriceAdjustment

5. **Orders**: Đơn hàng
   - Id, UserId, OrderDate, TotalAmount, Status, ShippingAddress, PhoneNumber, PaymentMethod

6. **OrderDetails**: Chi tiết đơn hàng
   - Id, OrderId, ProductId, Quantity, Price, SelectedOptions

7. **Reviews**: Đánh giá sản phẩm
   - Id, ProductId, UserId, Rating, Comment, CreatedDate

## Cài đặt và Chạy ứng dụng

### Yêu cầu hệ thống:
- .NET 9.0 SDK
- SQL Server hoặc SQL Server LocalDB
- Visual Studio 2022 hoặc VS Code

### Các bước cài đặt:

1. **Clone hoặc tải project về máy**

2. **Cấu hình Connection String**
   - Mở file `appsettings.json`
   - Kiểm tra connection string (mặc định sử dụng LocalDB):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WebBanNuoc;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Restore packages**
   ```bash
   cd WebBanNuoc
   dotnet restore
   ```

4. **Tạo Database**
   ```bash
   dotnet ef database update
   ```

5. **Chạy ứng dụng**
   
   **Cách 1**: Sử dụng script PowerShell
   ```powershell
   .\start.ps1
   ```

   **Cách 2**: Sử dụng dotnet CLI
   ```bash
   cd WebBanNuoc
   dotnet run
   ```

6. **Truy cập ứng dụng**
   - Mở trình duyệt và truy cập: `http://localhost:5000`

### Tài khoản Admin mặc định:
- **Email**: admin@webbannuoc.com
- **Password**: Admin@123

### Dữ liệu mẫu:
Khi chạy lần đầu, hệ thống sẽ tự động tạo:
- 5 danh mục sản phẩm
- 6 sản phẩm mẫu
- Các tùy chọn cho mỗi sản phẩm (Size, Đường, Đá)
- Tài khoản Admin

## Cấu trúc thư mục

```
WebBanNuoc/
├── Areas/
│   └── Admin/
│       └── Controllers/          # Controllers cho Admin
├── Controllers/                  # Controllers cho User
├── Data/                        # DbContext và Seeder
├── Models/                      # Entity models
├── ViewModels/                  # View models
├── Views/                       # Razor views
├── wwwroot/                     # Static files
│   ├── css/
│   ├── js/
│   └── images/
│       └── products/            # Thư mục lưu hình ảnh sản phẩm
├── Migrations/                  # EF Core migrations
├── appsettings.json            # Cấu hình ứng dụng
└── Program.cs                  # Entry point

```

## Các Controller chính

### User Controllers:
- **HomeController**: Trang chủ
- **AccountController**: Đăng ký, đăng nhập, quản lý tài khoản
- **ProductsController**: Xem sản phẩm, tìm kiếm, lọc
- **CartController**: Quản lý giỏ hàng
- **OrdersController**: Đặt hàng, xem lịch sử đơn hàng
- **ReviewsController**: Đánh giá sản phẩm

### Admin Controllers:
- **DashboardController**: Trang tổng quan
- **ProductsController**: Quản lý sản phẩm
- **CategoriesController**: Quản lý danh mục
- **OrdersController**: Quản lý đơn hàng
- **UsersController**: Quản lý người dùng

## Tính năng nổi bật

1. **Session-based Shopping Cart**: Giỏ hàng được lưu trong Session, không cần đăng nhập
2. **Role-based Authorization**: Phân quyền Admin/User
3. **Image Upload**: Upload và quản lý hình ảnh sản phẩm
4. **Product Options**: Hỗ trợ nhiều tùy chọn cho sản phẩm (Size, đường, đá)
5. **Order Status Tracking**: Theo dõi trạng thái đơn hàng
6. **Review System**: Hệ thống đánh giá sản phẩm (chỉ cho người đã mua)
7. **Data Seeding**: Tự động tạo dữ liệu mẫu khi khởi động lần đầu

## Lưu ý

- Thư mục `wwwroot/images/products/` sẽ được tự động tạo khi upload hình ảnh sản phẩm
- Session timeout mặc định: 30 phút
- Để thay đổi cấu hình, chỉnh sửa file `appsettings.json`

## Hỗ trợ

Nếu gặp vấn đề khi cài đặt hoặc chạy ứng dụng, vui lòng kiểm tra:
1. Đã cài đặt .NET 9.0 SDK chưa
2. SQL Server LocalDB có hoạt động không
3. Connection string có đúng không
4. Đã chạy migration chưa (`dotnet ef database update`)

## Tác giả

Tố My Cafe - Website Bán Cafe Giải Khát - ASP.NET MVC Core
