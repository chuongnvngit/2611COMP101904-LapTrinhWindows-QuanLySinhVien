-- =============================================
-- HỆ THỐNG QUẢN LÝ SINH VIÊN (C# WinForms - SQL Server)
-- Tệp: Scripts/Database.sql
-- =============================================

USE master;
GO

-- 1. TẠO CƠ SỞ DỮ LIỆU NẾU CHƯA TỒN TẠI
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QLSinhVien')
BEGIN
    CREATE DATABASE QLSinhVien;
END
GO

USE QLSinhVien;
GO

-- =============================================
-- 1. TẠO CÁC BẢNG DỮ LIỆU (GIỮ NGUYÊN CẤU TRÚC GỐC)
-- =============================================

-- Bảng Khoa
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Khoa')
BEGIN
    CREATE TABLE Khoa (
        MaKhoa VARCHAR(10) NOT NULL,
        TenKhoa NVARCHAR(100) NOT NULL,
        CONSTRAINT PK_Khoa PRIMARY KEY (MaKhoa)
    );
END
GO

-- Bảng Lop (Giữ nguyên VARCHAR(10))
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Lop')
BEGIN
    CREATE TABLE Lop (
        MaLop VARCHAR(10) NOT NULL,
        TenLop NVARCHAR(100) NOT NULL,
        MaKhoa VARCHAR(10) NOT NULL,
        KhoaHoc VARCHAR(20) NULL,
        CONSTRAINT PK_Lop PRIMARY KEY (MaLop),
        CONSTRAINT FK_Lop_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
    );
END
ELSE
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Lop') AND name = 'KhoaHoc')
    BEGIN
        ALTER TABLE Lop ADD KhoaHoc VARCHAR(20) NULL;
    END
END
GO

-- Bảng SinhVien (Giữ nguyên VARCHAR(10))
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SinhVien')
BEGIN
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
END
GO

-- Bảng MonHoc
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MonHoc')
BEGIN
    CREATE TABLE MonHoc (
        MaMH VARCHAR(10) NOT NULL,
        TenMH NVARCHAR(100) NOT NULL,
        SoTinChi INT NOT NULL,
        CONSTRAINT PK_MonHoc PRIMARY KEY (MaMH),
        CONSTRAINT CK_MonHoc_SoTinChi CHECK (SoTinChi > 0)
    );
END
GO

-- Bảng KetQua (Điểm số)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'KetQua')
BEGIN
    CREATE TABLE KetQua (
        MaSV VARCHAR(10) NOT NULL,
        MaMH VARCHAR(10) NOT NULL,
        Diem DECIMAL(4,2) NULL,
        CONSTRAINT PK_KetQua PRIMARY KEY (MaSV, MaMH),
        CONSTRAINT CK_KetQua_Diem CHECK (Diem >= 0.00 AND Diem <= 10.00),
        CONSTRAINT FK_KetQua_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) ON DELETE CASCADE,
        CONSTRAINT FK_KetQua_MonHoc FOREIGN KEY (MaMH) REFERENCES MonHoc(MaMH)
    );
END
GO

-- Bảng TaiKhoan
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TaiKhoan')
BEGIN
    CREATE TABLE TaiKhoan (
        TenDangNhap VARCHAR(50) NOT NULL,
        MatKhau VARCHAR(100) NOT NULL,
        HoTen NVARCHAR(100) NOT NULL,
        VaiTro VARCHAR(20) NOT NULL,
        CONSTRAINT PK_TaiKhoan PRIMARY KEY (TenDangNhap),
        CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('Admin', 'GiaoVu', 'GiangVien'))
    );
END
GO

-- =============================================
-- 2. STORED PROCEDURES
-- =============================================

CREATE OR ALTER PROCEDURE sp_KiemTraDangNhap
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

CREATE OR ALTER PROCEDURE sp_LayDanhSachSinhVien
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh,
        sv.DiaChi, sv.HinhAnh, sv.MaLop,
        l.TenLop, l.KhoaHoc, k.MaKhoa, k.TenKhoa
    FROM SinhVien sv
    INNER JOIN Lop l ON sv.MaLop = l.MaLop
    INNER JOIN Khoa k ON l.MaKhoa = k.MaKhoa
    ORDER BY sv.MaSV ASC;
END;
GO

CREATE OR ALTER PROCEDURE sp_LocSinhVien
    @MaKhoa VARCHAR(10) = NULL,
    @MaLop VARCHAR(10) = NULL,
    @KhoaHoc VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh,
        sv.DiaChi, sv.HinhAnh, sv.MaLop,
        l.TenLop, l.KhoaHoc, k.TenKhoa
    FROM SinhVien sv
    INNER JOIN Lop l ON sv.MaLop = l.MaLop
    INNER JOIN Khoa k ON l.MaKhoa = k.MaKhoa
    WHERE (@MaKhoa IS NULL OR @MaKhoa = '' OR k.MaKhoa = @MaKhoa)
      AND (@MaLop IS NULL OR @MaLop = '' OR l.MaLop = @MaLop)
      AND (@KhoaHoc IS NULL OR @KhoaHoc = '' OR l.KhoaHoc = @KhoaHoc)
    ORDER BY sv.MaSV ASC;
END;
GO

CREATE OR ALTER PROCEDURE sp_LayBangDiemSinhVien
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

CREATE OR ALTER PROCEDURE sp_LayDanhSachNhapDiem
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

CREATE OR ALTER PROCEDURE sp_TinhDiemTrungBinh
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

-- 3.1. Tài khoản
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admin')
    INSERT INTO TaiKhoan VALUES ('admin', '123456', N'Quản trị viên Hệ thống', 'Admin');
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'giaovu')
    INSERT INTO TaiKhoan VALUES ('giaovu', '123456', N'Nguyễn Thị Thu (Giáo vụ)', 'GiaoVu');
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'giangvien')
    INSERT INTO TaiKhoan VALUES ('giangvien', '123456', N'Trần Văn Bình (Giảng viên)', 'GiangVien');

-- 3.2. Khoa
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'CNTT')
    INSERT INTO Khoa VALUES ('CNTT', N'Công nghệ Thông tin');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'TOAN')
    INSERT INTO Khoa VALUES ('TOAN', N'Toán - Tin học');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'HOA')
    INSERT INTO Khoa VALUES ('HOA', N'Hóa học');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'NGOAINGU')
    INSERT INTO Khoa VALUES ('NGOAINGU', N'Ngoại ngữ');

-- 3.3. Lớp học (Tên lớp đã bỏ chữ "Khóa" và bỏ số, MaLop <= 10 ký tự)
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT01')
    INSERT INTO Lop VALUES ('CNTT01', N'Cử nhân Công nghệ Thông tin', 'CNTT', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT02')
    INSERT INTO Lop VALUES ('CNTT02', N'Sư phạm Tin học', 'CNTT', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'TOAN01')
    INSERT INTO Lop VALUES ('TOAN01', N'Sư phạm Toán học', 'TOAN', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'ANH01')
    INSERT INTO Lop VALUES ('ANH01', N'Ngôn ngữ Anh', 'NGOAINGU', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT48')
    INSERT INTO Lop VALUES ('CNTT48', N'Kỹ thuật Phần mềm', 'CNTT', 'K48');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'TOAN49')
    INSERT INTO Lop VALUES ('TOAN49', N'Toán ứng dụng', 'TOAN', 'K49');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'HOA49')
    INSERT INTO Lop VALUES ('HOA49', N'Sư phạm Hóa học', 'HOA', 'K49');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT50')
    INSERT INTO Lop VALUES ('CNTT50', N'Hệ thống Thông tin', 'CNTT', 'K50');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'ANH51')
    INSERT INTO Lop VALUES ('ANH51', N'Sư phạm Tiếng Anh', 'NGOAINGU', 'K51');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'HOA51')
    INSERT INTO Lop VALUES ('HOA51', N'Hóa Dược', 'HOA', 'K51');

-- Cập nhật đồng bộ lại tên lớp cũ trên máy của bạn (bỏ chữ Khóa và bỏ số)
UPDATE Lop SET TenLop = N'Cử nhân Công nghệ Thông tin', KhoaHoc = 'K47' WHERE MaLop = 'CNTT01';
UPDATE Lop SET TenLop = N'Sư phạm Tin học', KhoaHoc = 'K47' WHERE MaLop = 'CNTT02';
UPDATE Lop SET TenLop = N'Sư phạm Toán học', KhoaHoc = 'K47' WHERE MaLop = 'TOAN01';
UPDATE Lop SET TenLop = N'Ngôn ngữ Anh', KhoaHoc = 'K47' WHERE MaLop = 'ANH01';
UPDATE Lop SET TenLop = N'Kỹ thuật Phần mềm', KhoaHoc = 'K48' WHERE MaLop = 'CNTT48';
UPDATE Lop SET TenLop = N'Toán ứng dụng', KhoaHoc = 'K49' WHERE MaLop = 'TOAN49';
UPDATE Lop SET TenLop = N'Sư phạm Hóa học', KhoaHoc = 'K49' WHERE MaLop = 'HOA49';
UPDATE Lop SET TenLop = N'Hệ thống Thông tin', KhoaHoc = 'K50' WHERE MaLop = 'CNTT50';
UPDATE Lop SET TenLop = N'Sư phạm Tiếng Anh', KhoaHoc = 'K51' WHERE MaLop = 'ANH51';
UPDATE Lop SET TenLop = N'Hóa Dược', KhoaHoc = 'K51' WHERE MaLop = 'HOA51';
GO

-- 3.4. Môn học
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'COMP1019')
    INSERT INTO MonHoc VALUES ('COMP1019', N'Lập trình trên Windows', 3);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'COMP1002')
    INSERT INTO MonHoc VALUES ('COMP1002', N'Cấu trúc dữ liệu và Giải thuật', 4);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'MATH1001')
    INSERT INTO MonHoc VALUES ('MATH1001', N'Đại số tuyến tính', 3);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'ENG1001')
    INSERT INTO MonHoc VALUES ('ENG1001', N'Tiếng Anh giao tiếp', 2);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'CHEM1001')
    INSERT INTO MonHoc VALUES ('CHEM1001', N'Hóa đại cương', 3);
GO

-- 3.5. Bổ sung 40 Sinh viên mẫu trải đều các lớp và khóa
DECLARE @ListSV TABLE (
    MaSV VARCHAR(10), HoTen NVARCHAR(100), NgaySinh DATE, GioiTinh NVARCHAR(5), DiaChi NVARCHAR(200), HinhAnh NVARCHAR(255), MaLop VARCHAR(10)
);

INSERT INTO @ListSV VALUES
-- K47: CNTT01 (4 SV)
('SV001', N'Nguyễn Văn An', '2005-02-15', N'Nam', N'280 An Dương Vương, Q.5, TP.HCM', '', 'CNTT01'),
('SV002', N'Trần Thị Bích', '2005-08-20', N'Nữ', N'TP. Thủ Dầu Một, Bình Dương', '', 'CNTT01'),
('SV006', N'Bùi Hữu Đạt', '2005-04-10', N'Nam', N'Quận 10, TP.HCM', '', 'CNTT01'),
('SV007', N'Võ Cẩm Ly', '2005-11-22', N'Nữ', N'Bến Tre', '', 'CNTT01'),

-- K47: CNTT02 (4 SV)
('SV003', N'Lê Hoàng Long', '2005-11-05', N'Nam', N'Biên Hòa, Đồng Nai', '', 'CNTT02'),
('SV008', N'Phan Gia Bảo', '2005-01-18', N'Nam', N'Quận Tân Bình, TP.HCM', '', 'CNTT02'),
('SV009', N'Đặng Thu Hà', '2005-07-09', N'Nữ', N'Bình Phước', '', 'CNTT02'),
('SV010', N'Hồ Minh Trí', '2005-12-14', N'Nam', N'Quận 3, TP.HCM', '', 'CNTT02'),

-- K47: TOAN01 (4 SV)
('SV004', N'Phạm Thùy Dương', '2005-05-12', N'Nữ', N'Tân An, Long An', '', 'TOAN01'),
('SV011', N'Trương Công Tuấn', '2005-03-25', N'Nam', N'Tây Ninh', '', 'TOAN01'),
('SV012', N'Nguyễn Thanh Trúc', '2005-09-17', N'Nữ', N'Bình Thuận', '', 'TOAN01'),
('SV013', N'Lý Vĩnh Phát', '2005-06-30', N'Nam', N'Quận 6, TP.HCM', '', 'TOAN01'),

-- K47: ANH01 (4 SV)
('SV005', N'Đỗ Minh Quân', '2005-09-30', N'Nam', N'Mỹ Tho, Tiền Giang', '', 'ANH01'),
('SV014', N'Cao Thảo My', '2005-02-08', N'Nữ', N'Quận 7, TP.HCM', '', 'ANH01'),
('SV015', N'Phan Đình Phùng', '2005-10-15', N'Nam', N'Vũng Tàu', '', 'ANH01'),
('SV016', N'Dương Tuyết Mai', '2005-08-03', N'Nữ', N'Đồng Tháp', '', 'ANH01'),

-- K48: CNTT48 (4 SV)
('SV4801', N'Trần Minh Khang', '2004-03-12', N'Nam', N'Quận 1, TP.HCM', '', 'CNTT48'),
('SV4802', N'Huỳnh Kim Ngân', '2004-09-19', N'Nữ', N'Vĩnh Long', '', 'CNTT48'),
('SV4803', N'Tạ Quang Hùng', '2004-11-02', N'Nam', N'Quận 12, TP.HCM', '', 'CNTT48'),
('SV4804', N'Nguyễn Tuấn Kiệt', '2004-05-24', N'Nam', N'Bình Chánh, TP.HCM', '', 'CNTT48'),

-- K49: TOAN49 (4 SV)
('SV4901', N'Nguyễn Hoàng Yến', '2005-07-25', N'Nữ', N'Pleiku, Gia Lai', '', 'TOAN49'),
('SV4902', N'Trịnh Quốc Toản', '2005-04-14', N'Nam', N'Buôn Ma Thuột, Đắk Lắk', '', 'TOAN49'),
('SV4903', N'Lê Ánh Nguyệt', '2005-10-08', N'Nữ', N'Lâm Đồng', '', 'TOAN49'),
('SV4904', N'Hoàng Trọng Nghĩa', '2005-12-29', N'Nam', N'Nha Trang, Khánh Hòa', '', 'TOAN49'),

-- K49: HOA49 (4 SV)
('SV4905', N'Lưu Khánh Vy', '2005-03-01', N'Nữ', N'Phan Thiết, Bình Thuận', '', 'HOA49'),
('SV4906', N'Đinh Hữu Thành', '2005-06-16', N'Nam', N'Quận 4, TP.HCM', '', 'HOA49'),
('SV4907', N'Mai Phương Thảo', '2005-08-11', N'Nữ', N'Hóc Môn, TP.HCM', '', 'HOA49'),
('SV4908', N'Châu Vĩnh Thuận', '2005-11-04', N'Nam', N'Quận 8, TP.HCM', '', 'HOA49'),

-- K50: CNTT50 (4 SV)
('SV5001', N'Lê Quốc Huy', '2006-10-18', N'Nam', N'Ninh Kiều, Cần Thơ', '', 'CNTT50'),
('SV5002', N'Vũ Ngọc Diệp', '2006-01-26', N'Nữ', N'Củ Chi, TP.HCM', '', 'CNTT50'),
('SV5003', N'Ngô Tấn Tài', '2006-06-14', N'Nam', N'An Giang', '', 'CNTT50'),
('SV5004', N'Lâm Mỹ Dung', '2006-08-27', N'Nữ', N'Kiên Giang', '', 'CNTT50'),

-- K51: ANH51 (4 SV)
('SV5101', N'Võ Mai Chi', '2007-12-05', N'Nữ', N'Hải Châu, Đà Nẵng', '', 'ANH51'),
('SV5102', N'Nguyễn Bá Phong', '2007-02-14', N'Nam', N'TP. Huế', '', 'ANH51'),
('SV5103', N'Tô Ngọc Trâm', '2007-05-30', N'Nữ', N'Quảng Nam', '', 'ANH51'),
('SV5104', N'Trần Đức Thịnh', '2007-09-09', N'Nam', N'Quảng Ngãi', '', 'ANH51'),

-- K51: HOA51 (4 SV)
('SV5105', N'Dương Khắc Tiệp', '2007-03-21', N'Nam', N'Quy Nhơn, Bình Định', '', 'HOA51'),
('SV5106', N'Hà Lan Hương', '2007-07-19', N'Nữ', N'Tuy Hòa, Phú Yên', '', 'HOA51'),
('SV5107', N'Phùng Khắc Khoan', '2007-10-04', N'Nam', N'Kon Tum', '', 'HOA51'),
('SV5108', N'Đoàn Thúy Kiều', '2007-11-15', N'Nữ', N'Hậu Giang', '', 'HOA51');

-- Chèn sinh viên vào bảng chính nếu chưa tồn tại
INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, HinhAnh, MaLop)
SELECT src.MaSV, src.HoTen, src.NgaySinh, src.GioiTinh, src.DiaChi, src.HinhAnh, src.MaLop
FROM @ListSV src
WHERE NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = src.MaSV);
GO

-- =============================================
-- 3.6. BỔ SUNG DỮ LIỆU ĐIỂM SỐ MẪU
-- =============================================

-- Sinh viên K47
-- SV001 (CNTT01) - Đã có điểm để test in bảng điểm đầy đủ
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV001' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV001', 'COMP1019', 8.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV001' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV001', 'COMP1002', 7.25);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV001' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV001', 'ENG1001', 9.00);

-- SV002, SV006, SV007 (CNTT01)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV002' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV002', 'COMP1019', 9.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV002' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV002', 'COMP1002', 8.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV006' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV006', 'COMP1019', 7.75);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV006' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV006', 'COMP1002', 6.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV007' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV007', 'COMP1019', 8.20);

-- SV003, SV008, SV009, SV010 (CNTT02)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV003' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV003', 'COMP1019', 6.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV003' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV003', 'ENG1001', 7.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV008' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV008', 'COMP1019', 8.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV009' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV009', 'COMP1019', 7.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV010' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV010', 'COMP1019', 8.75);

-- SV004, SV011, SV012, SV013 (TOAN01)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV004' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV004', 'MATH1001', 8.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV004' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV004', 'COMP1002', 7.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV011' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV011', 'MATH1001', 9.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV012' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV012', 'MATH1001', 8.25);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV013' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV013', 'MATH1001', 6.75);

-- SV005, SV014, SV015, SV016 (ANH01)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV005' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV005', 'ENG1001', 8.75);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV014' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV014', 'ENG1001', 9.25);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV015' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV015', 'ENG1001', 7.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV016' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV016', 'ENG1001', 8.50);

-- Sinh viên K48 (CNTT48)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4801' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV4801', 'COMP1019', 8.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4801' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV4801', 'COMP1002', 7.80);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4802' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV4802', 'COMP1019', 8.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4803' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV4803', 'COMP1002', 7.20);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4804' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV4804', 'COMP1019', 6.80);

-- Sinh viên K49 (TOAN49 & HOA49)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4901' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV4901', 'MATH1001', 9.20);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4902' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV4902', 'MATH1001', 8.60);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4903' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV4903', 'MATH1001', 7.40);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4905' AND MaMH = 'CHEM1001')
    INSERT INTO KetQua VALUES ('SV4905', 'CHEM1001', 8.40);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4906' AND MaMH = 'CHEM1001')
    INSERT INTO KetQua VALUES ('SV4906', 'CHEM1001', 7.90);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV4907' AND MaMH = 'CHEM1001')
    INSERT INTO KetQua VALUES ('SV4907', 'CHEM1001', 8.10);

-- Sinh viên K50 (CNTT50)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5001' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV5001', 'COMP1019', 7.80);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5001' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV5001', 'COMP1002', 8.20);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5002' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV5002', 'COMP1019', 8.80);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5003' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV5003', 'COMP1002', 6.90);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5004' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV5004', 'COMP1019', 7.50);

-- Sinh viên K51 (ANH51 & HOA51)
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5101' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV5101', 'ENG1001', 9.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5102' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV5102', 'ENG1001', 9.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5103' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV5103', 'ENG1001', 8.30);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5105' AND MaMH = 'CHEM1001')
    INSERT INTO KetQua VALUES ('SV5105', 'CHEM1001', 8.60);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV5106' AND MaMH = 'CHEM1001')
    INSERT INTO KetQua VALUES ('SV5106', 'CHEM1001', 7.70);
GO

-- =============================================
-- 4. KIỂM TRA DỮ LIỆU & THỦ TỤC
-- =============================================

PRINT N'=============================================';
PRINT N'CẬP NHẬT CSDL QLSinhVien THÀNH CÔNG!';
PRINT N'=============================================';
GO

-- Kiểm tra tổng số sinh viên (40 SV)
SELECT COUNT(*) AS TongSoSinhVien FROM SinhVien;

-- Kiểm tra danh sách lớp (Không có chữ Khóa, không có số)
SELECT MaLop, TenLop, KhoaHoc, MaKhoa FROM Lop ORDER BY KhoaHoc, MaLop;

-- Kiểm tra xem trước bảng điểm SV001
EXEC sp_LayBangDiemSinhVien @MaSV = 'SV001';
EXEC sp_TinhDiemTrungBinh @MaSV = 'SV001';
GO