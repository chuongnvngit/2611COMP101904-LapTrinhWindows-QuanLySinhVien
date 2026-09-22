-- =============================================
-- HỆ THỐNG QUẢN LÝ SINH VIÊN (C# WinForms - SQL Server)
-- Tệp: Scripts/Database.sql
-- Phụ trách: TV1 - CSDL & DAL
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
-- 1. TẠO CÁC BẢNG DỮ LIỆU
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

-- Bảng Lop (Đã có sẵn cột KhoaHoc)
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

-- Bảng SinhVien
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

-- SP 1: Xác thực đăng nhập
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

-- SP 2: Lấy danh sách toàn bộ sinh viên kèm lớp, khóa, khoa
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

-- SP 3: Lọc sinh viên theo 3 tiêu chí: Khoa, Lớp, Khóa học
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

-- SP 4: Lấy bảng điểm chi tiết của 1 sinh viên (in bảng điểm / xem trước)
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

-- SP 5: Lấy danh sách nhập điểm theo lớp và môn học
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

-- SP 6: Tính điểm trung bình tích lũy và tổng số tín chỉ
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
-- 3. DỮ LIỆU MẪU (CHÈN NẾU CHƯA CÓ)
-- =============================================

-- Tài khoản
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admin')
    INSERT INTO TaiKhoan VALUES ('admin', '123456', N'Quản trị viên Hệ thống', 'Admin');
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'giaovu')
    INSERT INTO TaiKhoan VALUES ('giaovu', '123456', N'Nguyễn Thị Thu (Giáo vụ)', 'GiaoVu');
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'giangvien')
    INSERT INTO TaiKhoan VALUES ('giangvien', '123456', N'Trần Văn Bình (Giảng viên)', 'GiangVien');

-- Khoa
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'CNTT')
    INSERT INTO Khoa VALUES ('CNTT', N'Công nghệ Thông tin');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'TOAN')
    INSERT INTO Khoa VALUES ('TOAN', N'Toán - Tin học');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'HOA')
    INSERT INTO Khoa VALUES ('HOA', N'Hóa học');
IF NOT EXISTS (SELECT 1 FROM Khoa WHERE MaKhoa = 'NGOAINGU')
    INSERT INTO Khoa VALUES ('NGOAINGU', N'Ngoại ngữ');

-- Lớp (gồm K47, K48, K49, K50, K51)
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT01')
    INSERT INTO Lop VALUES ('CNTT01', N'Cử nhân CNTT Khóa 47', 'CNTT', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT02')
    INSERT INTO Lop VALUES ('CNTT02', N'Sư phạm Tin học Khóa 47', 'CNTT', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'TOAN01')
    INSERT INTO Lop VALUES ('TOAN01', N'Sư phạm Toán Khóa 47', 'TOAN', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'ANH01')
    INSERT INTO Lop VALUES ('ANH01', N'Ngôn ngữ Anh Khóa 47', 'NGOAINGU', 'K47');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT_K48')
    INSERT INTO Lop VALUES ('CNTT_K48', N'Công nghệ Thông tin Khóa 48', 'CNTT', 'K48');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'TOAN_K49')
    INSERT INTO Lop VALUES ('TOAN_K49', N'Sư phạm Toán Khóa 49', 'TOAN', 'K49');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'CNTT_K50')
    INSERT INTO Lop VALUES ('CNTT_K50', N'Kỹ thuật Phần mềm Khóa 50', 'CNTT', 'K50');
IF NOT EXISTS (SELECT 1 FROM Lop WHERE MaLop = 'ANH_K51')
    INSERT INTO Lop VALUES ('ANH_K51', N'Ngôn ngữ Anh Khóa 51', 'NGOAINGU', 'K51');

-- Sinh viên mẫu
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV001')
    INSERT INTO SinhVien VALUES ('SV001', N'Nguyễn Văn An', '2005-02-15', N'Nam', N'280 An Dương Vương, Q.5, TP.HCM', '', 'CNTT01');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV002')
    INSERT INTO SinhVien VALUES ('SV002', N'Trần Thị Bích', '2005-08-20', N'Nữ', N'Bình Dương', '', 'CNTT01');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV003')
    INSERT INTO SinhVien VALUES ('SV003', N'Lê Hoàng Long', '2005-11-05', N'Nam', N'Biên Hòa, Đồng Nai', '', 'CNTT02');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV004')
    INSERT INTO SinhVien VALUES ('SV004', N'Phạm Thùy Dương', '2005-05-12', N'Nữ', N'Tân An, Long An', '', 'TOAN01');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV005')
    INSERT INTO SinhVien VALUES ('SV005', N'Đỗ Minh Quân', '2005-09-30', N'Nam', N'Mỹ Tho, Tiền Giang', '', 'ANH01');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV4801')
    INSERT INTO SinhVien VALUES ('SV4801', N'Trần Minh Khang', '2004-03-12', N'Nam', N'Quận 1, TP.HCM', '', 'CNTT_K48');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV4901')
    INSERT INTO SinhVien VALUES ('SV4901', N'Nguyễn Hoàng Yến', '2005-07-25', N'Nữ', N'Gia Lai', '', 'TOAN_K49');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV5001')
    INSERT INTO SinhVien VALUES ('SV5001', N'Lê Quốc Huy', '2006-10-18', N'Nam', N'Cần Thơ', '', 'CNTT_K50');
IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = 'SV5101')
    INSERT INTO SinhVien VALUES ('SV5101', N'Võ Mai Chi', '2007-12-05', N'Nữ', N'Đà Nẵng', '', 'ANH_K51');

-- Môn học
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'COMP1019')
    INSERT INTO MonHoc VALUES ('COMP1019', N'Lập trình trên Windows', 3);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'COMP1002')
    INSERT INTO MonHoc VALUES ('COMP1002', N'Cấu trúc dữ liệu và Giải thuật', 4);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'MATH1001')
    INSERT INTO MonHoc VALUES ('MATH1001', N'Đại số tuyến tính', 3);
IF NOT EXISTS (SELECT 1 FROM MonHoc WHERE MaMH = 'ENG1001')
    INSERT INTO MonHoc VALUES ('ENG1001', N'Tiếng Anh giao tiếp', 2);

-- Điểm số
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV001' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV001', 'COMP1019', 8.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV001' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV001', 'COMP1002', 7.25);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV001' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV001', 'ENG1001', 9.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV002' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV002', 'COMP1019', 9.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV002' AND MaMH = 'COMP1002')
    INSERT INTO KetQua VALUES ('SV002', 'COMP1002', 8.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV003' AND MaMH = 'COMP1019')
    INSERT INTO KetQua VALUES ('SV003', 'COMP1019', 6.50);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV004' AND MaMH = 'MATH1001')
    INSERT INTO KetQua VALUES ('SV004', 'MATH1001', 8.00);
IF NOT EXISTS (SELECT 1 FROM KetQua WHERE MaSV = 'SV005' AND MaMH = 'ENG1001')
    INSERT INTO KetQua VALUES ('SV005', 'ENG1001', 8.75);
GO

-- =============================================
-- 4. KIỂM TRA DỮ LIỆU & THỦ TỤC
-- =============================================

PRINT N'=============================================';
PRINT N'CẬP NHẬT CSDL QLSinhVien THÀNH CÔNG!';
PRINT N'=============================================';
GO

-- Xem danh sách khóa học hiện có
SELECT DISTINCT KhoaHoc FROM Lop ORDER BY KhoaHoc ASC;

-- Thử nghiệm đăng nhập
EXEC sp_KiemTraDangNhap @TenDangNhap = 'admin', @MatKhau = '123456';

-- Lấy danh sách toàn bộ sinh viên (đã có cột KhoaHoc)
EXEC sp_LayDanhSachSinhVien;

-- Thử lọc sinh viên theo Khóa học K50
EXEC sp_LocSinhVien @MaKhoa = NULL, @MaLop = NULL, @KhoaHoc = 'K50';

-- Bảng điểm cá nhân SV001
EXEC sp_LayBangDiemSinhVien @MaSV = 'SV001';

-- Tính điểm trung bình SV001
EXEC sp_TinhDiemTrungBinh @MaSV = 'SV001';
GO