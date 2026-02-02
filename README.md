# BeautySC - Backend API

BeautySC là một ứng dụng thương mại điện tử hiện đại với backend được xây dựng bằng ASP.NET Core 6.0+. Hệ thống cung cấp đầy đủ các chức năng quản lý sản phẩm, đơn hàng, thanh toán, quản lý skincare và quản trị người dùng.

## 📋 Mục đích dự án

Cung cấp một nền tảng API RESTful toàn diện cho:

- Quản lý sản phẩm và danh mục skincare
- Xử lý đơn hàng và giỏ hàng
- Thanh toán qua VNPay
- Quản lý người dùng và xác thực
- Hệ thống kiểm tra loại da (Skin Type Quiz)
- Quản lý thành phần sản phẩm và công dụng
- Ghi chú và đánh giá sản phẩm
- Quản lý voucher và khuyến mãi
- Giao hàng và địa chỉ
- Quản lý blog content

## 🏗️ Kiến trúc dự án

```
BeautySCProject/
├── BeautySCProject.API/           # Layer API chính
│   ├── Controllers/               # Xử lý HTTP requests
│   ├── Services/                  # Business Logic
│   ├── Models/                    # DTOs và View Models
│   ├── DependencyInjection.cs    # Cấu hình DI
│   └── Program.cs                 # Cấu hình ứng dụng
├── BeautySCProject.Data/         # Data Access Layer
│   ├── Entities/                 # Models cơ sở dữ liệu
│   ├── DbContext/                # Entity Framework DbContext
│   └── Migrations/               # Database migrations
├── BeautySCProject.Service/      # Service Layer
│   ├── Services/                 # Business logic implementations
│   ├── Interfaces/               # Service interfaces
│   └── DependencyInjection.cs   # Service DI setup
└── BeautySCProject.Common/       # Shared Layer
    ├── Helpers/                  # Utilities (Constants, EmailTemplate)
    ├── VnPayLibrary.cs          # VNPay helper
    └── MethodResult.cs          # Response wrapper
```

## 🛠️ Công nghệ sử dụng

### Backend
- **Framework**: ASP.NET Core 6.0+
- **Database**: MySQL 8.0
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI
- **Password Hashing**: BCrypt

### External Services
- **Payment**: VNPay
- **Email**: Gmail SMTP
- **Cache**: In-memory (có thể mở rộng)

### Tools
- **Docker**: Containerization (tuỳ chọn)
- **Postman**: API Testing

## 🚀 Bắt đầu nhanh

### Yêu cầu
- .NET SDK 6.0 trở lên
- MySQL 8.0+
- Visual Studio 2022 hoặc VS Code

### Cài đặt và chạy

#### Cách 1: Chạy local (Khuyến nghị)

```bash
# Clone repository
git clone <repository-url>
cd BeautySC_BE

# Restore dependencies
dotnet restore src/

# Cập nhật database
cd src/BeautySCProject.API
dotnet ef database update

# Chạy ứng dụng
dotnet run

# Backend sẽ chạy tại https://localhost:5001
# Swagger UI: https://localhost:5001/swagger/index.html
```

#### Cách 2: Sử dụng Docker (tuỳ chọn)

```bash
# Clone repository
git clone <repository-url>
cd BeautySC_BE

# Khởi động services
docker-compose up -d

# Backend sẽ chạy tại http://localhost:5000
# Swagger UI: http://localhost:5000/swagger
```

## 📁 Cấu trúc thư mục chi tiết

### Controllers
- **AuthenticationController** - Đăng nhập, đăng ký, xác thực
- **CustomerController** - Quản lý hồ sơ người dùng
- **ProductController** - Quản lý sản phẩm
- **CategoryController** - Quản lý danh mục
- **OrderController** - Quản lý đơn hàng
- **PaymentController** - Xử lý thanh toán VNPay
- **VoucherController** - Quản lý voucher/khuyến mãi
- **FeedbackController** - Quản lý ghi chú/đánh giá
- **ShippingAddressController** - Quản lý địa chỉ giao hàng
- **SkinTypeController** - Quản lý loại da (Quiz system)
- **IngredientController** - Quản lý thành phần sản phẩm
- **FunctionController** - Quản lý công dụng sản phẩm
- **BlogController** - Quản lý bài viết blog
- **BrandController** - Quản lý thương hiệu
- **TransactionController** - Lịch sử giao dịch

### Services
Chứa các business logic và handler cho các chức năng chính của ứng dụng.

### Models
- **Request Models**: Dữ liệu từ client
- **Response Models**: Dữ liệu trả về client
- **DTOs**: Data Transfer Objects
- **View Models**: Models cho API responses

## ⚙️ Cấu hình

### Các file cấu hình chính

**appsettings.json** - Cấu hình chung
```json
{
  "ConnectionStrings": {
    "BeautySCDbConnection": "Server=your_server; Port=3306; Database=beautysc_db; Uid=your_user; Pwd=your_password; SslMode=Preferred"
  },
  "AuthenticationConfiguration": {
    "SecretKey": "your_secret_key",
    "ValidIssuer": "your_issuer",
    "ValidAudience": "your_audience",
    "TokenExpiryInMinutes": 60
  },
  "MailConfiguration": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "BeautySC",
    "FromEmail": "your_email@gmail.com",
    "Password": "your_app_password"
  },
  "VnPayConfig": {
    "TmnCode": "your_tmn_code",
    "HashSecret": "your_hash_secret",
    "PaymentUrl": "https://sandbox.vnpayment.vn/paygate"
  }
}
```

**appsettings.Development.json** - Cấu hình development
- Full logging
- Debugging enabled
- Local database connection

**appsettings.Production.json** - Cấu hình production
- Optimized logging
- Production database connection
- Security configurations

## 📦 Cài đặt Dependencies

Các dependencies chính trong BeautySCProject.API.csproj:

- **Microsoft.EntityFrameworkCore** v6.0+ - ORM
- **MySql.EntityFrameworkCore** v6.0+ - MySQL provider
- **Microsoft.AspNetCore.Authentication.JwtBearer** v6.0+ - JWT authentication
- **Swashbuckle.AspNetCore** v6.0+ - Swagger documentation
- **BCrypt.Net-Next** v4.0+ - Password hashing (tuỳ chọn)

## 🔐 Xác thực & Phân quyền

- **JWT Token-based authentication** - Xác thực dựa trên JWT
- **Role-based access control** - Admin, Staff, Customer
- **Email verification** - Xác minh email
- **Refresh token mechanism** - Làm mới token
- **Password hashing** - Mã hoá mật khẩu an toàn

## 💾 Cơ sở dữ liệu

Được khởi tạo từ script `beautysc_db.sql`. Bao gồm các bảng chính:

- **user** - Người dùng
- **refresh_token** - Làm mới token
- **shipping_address** - Địa chỉ giao hàng
- **category** - Danh mục sản phẩm
- **product** - Sản phẩm
- **order** - Đơn hàng
- **order_detail** - Chi tiết đơn hàng
- **feedback** - Ghi chú/đánh giá
- **voucher** - Voucher/khuyến mãi
- **skin_type** - Loại da
- **ingredient** - Thành phần sản phẩm
- **product_function** - Công dụng sản phẩm
- **blog** - Bài viết blog
- **brand** - Thương hiệu
- **transaction** - Giao dịch thanh toán

## 📧 Email Configuration

Hệ thống sử dụng Gmail SMTP để gửi email:

- Cấu hình trong `appsettings.json`
- Hỗ trợ email xác thực, thông báo đơn hàng, password reset
- Email templates được quản lý trong `Common/Helpers/EmailTemplates.cs`

## 💳 Thanh toán (VNPay)

Tích hợp cổng thanh toán VNPay:

- **VNPay configuration** trong `appsettings.json`
- **PaymentController** xử lý thanh toán
- **VnPayLibrary** class cung cấp utility functions
- Hỗ trợ callback xử lý kết quả thanh toán

## 📚 API Endpoints

### Authentication
- `POST /api/authentication/login` - Đăng nhập
- `POST /api/authentication/register` - Đăng ký
- `POST /api/authentication/refresh-token` - Làm mới token
- `POST /api/authentication/logout` - Đăng xuất

### Products
- `GET /api/products` - Danh sách sản phẩm
- `GET /api/products/{id}` - Chi tiết sản phẩm
- `POST /api/products` - Tạo sản phẩm (Admin)
- `PUT /api/products/{id}` - Cập nhật sản phẩm (Admin)
- `DELETE /api/products/{id}` - Xoá sản phẩm (Admin)

### Orders
- `GET /api/orders` - Danh sách đơn hàng
- `POST /api/orders` - Tạo đơn hàng
- `GET /api/orders/{id}` - Chi tiết đơn hàng
- `PUT /api/orders/{id}` - Cập nhật trạng thái đơn hàng

### Categories
- `GET /api/categories` - Danh sách danh mục
- `POST /api/categories` - Tạo danh mục

### Customers
- `GET /api/customers/{id}` - Thông tin khách hàng
- `PUT /api/customers/{id}` - Cập nhật thông tin

### Payments
- `POST /api/payments/create-payment-url` - Tạo link thanh toán VNPay
- `GET /api/payments/return` - Xử lý callback thanh toán

### Feedback
- `GET /api/feedback` - Danh sách ghi chú
- `POST /api/feedback` - Tạo ghi chú
- `DELETE /api/feedback/{id}` - Xoá ghi chú

### Vouchers
- `GET /api/vouchers` - Danh sách voucher
- `POST /api/vouchers` - Tạo voucher (Admin)
- `PUT /api/vouchers/{id}` - Cập nhật voucher (Admin)

### Skin Type Quiz
- `GET /api/skin-type-questions` - Danh sách câu hỏi
- `GET /api/skin-type-answers` - Danh sách câu trả lời
- `POST /api/skin-test/submit` - Gửi kết quả quiz

### Địa chỉ giao hàng
- `GET /api/shipping-addresses` - Danh sách địa chỉ
- `POST /api/shipping-addresses` - Tạo địa chỉ mới
- `PUT /api/shipping-addresses/{id}` - Cập nhật địa chỉ
- `DELETE /api/shipping-addresses/{id}` - Xoá địa chỉ

## 🔄 CORS Configuration

Được cấu hình cho frontend:
- `http://localhost:3000` (development)
- `https://swp-fe-beta.vercel.app` (production)

## 👥 Các vai trò (Roles)

- **Admin** - Quản trị toàn bộ hệ thống
- **Staff** - Nhân viên hỗ trợ/quản lý
- **Customer** - Khách hàng mua hàng

## 📝 Logging & Debugging

- **Development environment**: Full logging và debugging
- **Production environment**: Cấu hình riêng trong `appsettings.Production.json`

## 🧪 Testing API

- Postman collection có sẵn trong thư mục project (nếu có)
- Swagger UI tích hợp để test API interactively
- URL: `/swagger/index.html`

## 🐛 Troubleshooting

### Database Connection Issues
- Xác minh MySQL đang chạy
- Kiểm tra connection string trong `appsettings.json`
- Đảm bảo database user có quyền truy cập

### Authentication Errors
- Xác minh JWT secret key được cấu hình
- Kiểm tra token expiration settings
- Đảm bảo Authorization header format đúng: `Bearer <token>`

### Email Not Sending
- Bật "Less secure app access" cho Gmail hoặc sử dụng App Passwords
- Xác minh SMTP configuration trong `appsettings.json`
- Kiểm tra email templates trong Common layer

### VNPay Payment Issues
- Xác minh VNPay configuration (TmnCode, HashSecret)
- Kiểm tra Payment URL (sandbox vs production)
- Verify callback handling

## 📄 License

Dự án này thuộc về BeautySC - SWP391 Course tại FPT University

## 👨‍💻 Development Team

BeautySC Development Team

---

**Last Updated**: February 2, 2026