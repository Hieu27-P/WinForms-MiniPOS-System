CREATE DATABASE MiniPOS_DB;
GO
USE MiniPOS_DB;
GO

-- 1. Bảng Người dùng (Phân quyền Admin / Staff)
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role VARCHAR(20) CHECK (Role IN ('Admin', 'Staff')) NOT NULL,
    IsActive BIT DEFAULT 1
);

-- 2. Bảng Danh mục
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

-- 3. Bảng Sản phẩm
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductCode VARCHAR(20) UNIQUE NOT NULL,
    ProductName NVARCHAR(150) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Categories(CategoryId),
    UnitPrice DECIMAL(18, 0) NOT NULL CHECK (UnitPrice >= 0),
    StockQuantity INT NOT NULL DEFAULT 0 CHECK (StockQuantity >= 0),
    IsActive BIT DEFAULT 1
);

-- 4. Bảng Hóa đơn
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    OrderCode VARCHAR(30) UNIQUE NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    TotalAmount DECIMAL(18, 0) NOT NULL,
    Discount DECIMAL(18, 0) DEFAULT 0,
    FinalAmount DECIMAL(18, 0) NOT NULL
);

-- 5. Bảng Chi tiết hóa đơn
CREATE TABLE OrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18, 0) NOT NULL,
    SubTotal AS (Quantity * UnitPrice) -- Cột tự động tính toán
);

-- Dữ liệu mẫu (Mật khẩu mẫu đều là: 123456)
INSERT INTO Users (Username, PasswordHash, FullName, Role) VALUES 
('admin', 'e10adc3949ba59abbe56e057f20f883e', N'Quản trị viên', 'Admin'),
('thungan1', 'e10adc3949ba59abbe56e057f20f883e', N'Nguyễn Văn A', 'Staff');

INSERT INTO Categories (CategoryName) VALUES (N'Đồ uống'), (N'Thức ăn nhanh'), (N'Bánh ngọt');

INSERT INTO Products (ProductCode, ProductName, CategoryId, UnitPrice, StockQuantity) VALUES
('SP001', N'Cà phê Sữa Đá', 1, 29000, 50),
('SP002', N'Trà Đào Cam Sả', 1, 35000, 40),
('SP003', N'Bánh Mì Chảo', 2, 45000, 20),
('SP004', N'Tiramisu Matcha', 3, 39000, 15);
GO