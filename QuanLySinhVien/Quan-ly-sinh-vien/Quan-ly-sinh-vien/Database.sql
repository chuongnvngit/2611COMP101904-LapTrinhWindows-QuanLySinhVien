-- =============================================
-- HỆ THỐNG QUẢN LÝ SINH VIÊN
-- C# WinForms - SQL Server
-- Tệp: Scripts/Database.sql
-- Phụ trách: TV1 - CSDL & DAL
-- =============================================

-- LƯU Ý: Script này sẽ XÓA database QLSinhVien cũ nếu đã tồn tại
-- và tạo lại từ đầu. Chỉ dùng khi khởi tạo/reset database.

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QLSinhVien')
BEGIN
    ALTER DATABASE QLSinhVien SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLSinhVien;
END
GO

CREATE DATABASE QLSinhVien;
GO

USE QLSinhVien;
GO

-- =============================================
-- 1. TẠO CÁC BẢNG
-- =============================================

CREATE TABLE Khoa (
    MaKhoa VARCHAR(10) NOT NULL,
    TenKhoa NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_Khoa PRIMARY KEY (MaKhoa)
);
GO

CREATE TABLE Lop (
    MaLop VARCHAR(10) NOT NULL,
    TenLop NVARCHAR(100) NOT NULL,
    MaKhoa VARCHAR(10) NOT NULL,
    CONSTRAINT PK_Lop PRIMARY KEY (MaLop),
    CONSTRAINT FK_Lop_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);
GO

CREATE TABLE SinhVien (
    MaSV VARCHAR(10) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(5) NOT NULL,
    DiaChi NVARCHAR(200) NULL,
    HinhAnh NVARCHAR(255) NULL,
    MaLop VARCHAR(10) NOT NULL,
    CONSTRAINT PK_SinhVien PRIMARY KEY (MaSV),
    CONSTRAINT CK_SinhVien_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);
GO

CREATE TABLE MonHoc (
    MaMH VARCHAR(10) NOT NULL,
    TenMH NVARCHAR(100) NOT NULL,
    SoTinChi INT NOT NULL,
    CONSTRAINT PK_MonHoc PRIMARY KEY (MaMH),
    CONSTRAINT CK_MonHoc_SoTinChi CHECK (SoTinChi > 0)
);
GO

CREATE TABLE KetQua (
    MaSV VARCHAR(10) NOT NULL,
    MaMH VARCHAR(10) NOT NULL,
    Diem DECIMAL(4,2) NULL,
    CONSTRAINT PK_KetQua PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT CK_KetQua_Diem CHECK (Diem >= 0.00 AND Diem <= 10.00),
    CONSTRAINT FK_KetQua_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE,
    CONSTRAINT FK_KetQua_MonHoc FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH)
);
GO

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) NOT NULL,
    MatKhau VARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    VaiTro VARCHAR(20) NOT NULL,
    CONSTRAINT PK_TaiKhoan PRIMARY KEY (TenDangNhap),
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('Admin', 'GiaoVu', 'GiangVien'))
);
GO

-- =============================================
-- 2. STORED PROCEDURES
-- =============================================

CREATE PROCEDURE sp_KiemTraDangNhap
    @TenDangNhap VARCHAR(50),
    @MatKhau VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TenDangNhap, MatKhau, HoTen, VaiTro
    FROM TaiKhoan
    WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau;
END;
GO

CREATE PROCEDURE sp_LayDanhSachSinhVien
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh,
        sv.DiaChi, sv.HinhAnh, sv.MaLop,
        l.TenLop, k.MaKhoa, k.TenKhoa
    FROM SinhVien sv
    INNER JOIN Lop l ON sv.MaLop = l.MaLop
    INNER JOIN Khoa k ON l.MaKhoa = k.MaKhoa
    ORDER BY sv.MaSV ASC;
END;
GO

CREATE PROCEDURE sp_LocSinhVien
    @MaKhoa VARCHAR(10) = NULL,
    @MaLop VARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh,
        sv.DiaChi, sv.HinhAnh, sv.MaLop,
        l.TenLop, k.TenKhoa
    FROM SinhVien sv
    INNER JOIN Lop l ON sv.MaLop = l.MaLop
    INNER JOIN Khoa k ON l.MaKhoa = k.MaKhoa
    WHERE (@MaKhoa IS NULL OR @MaKhoa = '' OR k.MaKhoa = @MaKhoa)
      AND (@MaLop IS NULL OR @MaLop = '' OR l.MaLop = @MaLop)
    ORDER BY sv.MaSV ASC;
END;
GO

CREATE PROCEDURE sp_LayBangDiemSinhVien
    @MaSV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        mh.MaMH,
        mh.TenMH,
        mh.SoTinChi,
        kq.Diem
    FROM KetQua kq
    INNER JOIN MonHoc mh ON kq.MaMH = mh.MaMH
    WHERE kq.MaSV = @MaSV
    ORDER BY mh.MaMH ASC;
END;
GO

CREATE PROCEDURE sp_LayDanhSachNhapDiem
    @MaLop VARCHAR(10),
    @MaMH VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        sv.MaSV,
        sv.HoTen,
        l.TenLop,
        @MaMH AS MaMH,
        kq.Diem AS Diem
    FROM SinhVien sv
    INNER JOIN Lop l ON sv.MaLop = l.MaLop
    LEFT JOIN KetQua kq ON sv.MaSV = kq.MaSV AND kq.MaMH = @MaMH
    WHERE sv.MaLop = @MaLop
    ORDER BY sv.MaSV ASC;
END;
GO

CREATE PROCEDURE sp_TinhDiemTrungBinh
    @MaSV VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        @MaSV AS MaSV,
        ISNULL(SUM(mh.SoTinChi), 0) AS TongSoTinChi,
        CASE
            WHEN SUM(mh.SoTinChi) > 0 THEN
                ROUND(
                    SUM(kq.Diem * mh.SoTinChi)
                    / CAST(SUM(mh.SoTinChi) AS DECIMAL(10,2)),
                    2
                )
            ELSE 0.00
        END AS DiemTrungBinh
    FROM KetQua kq
    INNER JOIN MonHoc mh ON kq.MaMH = mh.MaMH
    WHERE kq.MaSV = @MaSV AND kq.Diem IS NOT NULL;
END;
GO

-- =============================================
-- 3. DỮ LIỆU MẪU
-- =============================================

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro) VALUES
('admin', '123456', N'Quản trị viên Hệ thống', 'Admin'),
('giaovu', '123456', N'Nguyễn Thị Thu (Giáo vụ)', 'GiaoVu'),
('giangvien', '123456', N'Trần Văn Bình (Giảng viên)', 'GiangVien');
GO

INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES
('CNTT', N'Công nghệ Thông tin'),
('TOAN', N'Toán - Tin học'),
('HOA', N'Hóa học'),
('NGOAINGU', N'Ngoại ngữ');
GO

INSERT INTO Lop (MaLop, TenLop, MaKhoa) VALUES
('CNTT01', N'Cử nhân CNTT Khóa 47', 'CNTT'),
('CNTT02', N'Sư phạm Tin học Khóa 47', 'CNTT'),
('TOAN01', N'Sư phạm Toán Khóa 47', 'TOAN'),
('ANH01', N'Ngôn ngữ Anh Khóa 47', 'NGOAINGU');
GO

INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, HinhAnh, MaLop) VALUES
('SV001', N'Nguyễn Văn An', '2005-02-15', N'Nam', N'280 An Dương Vương, Q.5, TP.HCM', '', 'CNTT01'),
('SV002', N'Trần Thị Bích', '2005-08-20', N'Nữ', N'Bình Dương', '', 'CNTT01'),
('SV003', N'Lê Hoàng Long', '2005-11-05', N'Nam', N'Biên Hòa, Đồng Nai', '', 'CNTT02'),
('SV004', N'Phạm Thùy Dương', '2005-05-12', N'Nữ', N'Tân An, Long An', '', 'TOAN01'),
('SV005', N'Đỗ Minh Quân', '2005-09-30', N'Nam', N'Mỹ Tho, Tiền Giang', '', 'ANH01');
GO

INSERT INTO MonHoc (MaMH, TenMH, SoTinChi) VALUES
('COMP1019', N'Lập trình trên Windows', 3),
('COMP1002', N'Cấu trúc dữ liệu và Giải thuật', 4),
('MATH1001', N'Đại số tuyến tính', 3),
('ENG1001', N'Tiếng Anh giao tiếp', 2);
GO

-- SV001 có ĐẦY ĐỦ 3 môn có điểm:
-- COMP1019 = 8.50
-- COMP1002 = 7.25
-- ENG1001  = 9.00
INSERT INTO KetQua (MaSV, MaMH, Diem) VALUES
('SV001', 'COMP1019', 8.50),
('SV001', 'COMP1002', 7.25),
('SV001', 'ENG1001', 9.00),
('SV002', 'COMP1019', 9.00),
('SV002', 'COMP1002', 8.00),
('SV003', 'COMP1019', 6.50),
('SV004', 'MATH1001', 8.00),
('SV005', 'ENG1001', 8.75);
GO

-- =============================================
-- 4. KIỂM TRA DỮ LIỆU
-- =============================================

PRINT N'=============================================';
PRINT N'KHỞI TẠO CSDL QLSinhVien THÀNH CÔNG!';
PRINT N'=============================================';
GO

SELECT * FROM TaiKhoan;
SELECT * FROM Khoa;
SELECT * FROM Lop;
SELECT * FROM SinhVien;
SELECT * FROM MonHoc;
SELECT * FROM KetQua;
GO

-- Kiểm tra riêng điểm của SV001: phải có 3 dòng
SELECT *
FROM KetQua
WHERE MaSV = 'SV001';
GO

-- =============================================
-- 5. KIỂM TRA STORED PROCEDURE
-- =============================================

EXEC sp_KiemTraDangNhap
    @TenDangNhap = 'admin',
    @MatKhau = '123456';
GO

EXEC sp_LayDanhSachSinhVien;
GO

EXEC sp_LocSinhVien
    @MaKhoa = 'CNTT',
    @MaLop = NULL;
GO

EXEC sp_LocSinhVien
    @MaKhoa = NULL,
    @MaLop = 'CNTT01';
GO

-- SV001 phải trả về 3 môn: COMP1002, COMP1019, ENG1001
EXEC sp_LayBangDiemSinhVien
    @MaSV = 'SV001';
GO

EXEC sp_LayDanhSachNhapDiem
    @MaLop = 'CNTT01',
    @MaMH = 'COMP1019';
GO

-- SV001: 9 tín chỉ, GPA = 7.94
EXEC sp_TinhDiemTrungBinh
    @MaSV = 'SV001';
GO

-- =============================================
-- KẾT THÚC DATABASE.SQL
-- =============================================
