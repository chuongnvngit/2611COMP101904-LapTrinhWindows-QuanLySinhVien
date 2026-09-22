using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien.DAL
{
    public class SinhVienDAL
    {
        // Lấy tất cả sinh viên
        public DataTable LayTatCa()
        {
            return Database.ExecuteProcedure("sp_LayDanhSachSinhVien");
        }

        public DataTable TimKiem(string tuKhoa)
        {
            string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.DiaChi, sv.HinhAnh, sv.MaLop, l.TenLop, k.TenKhoa 
                   FROM SinhVien sv 
                   INNER JOIN Lop l ON sv.MaLop = l.MaLop 
                   INNER JOIN Khoa k ON l.MaKhoa = k.MaKhoa 
                   WHERE sv.MaSV LIKE @Keyword OR sv.HoTen LIKE @Keyword";
            SqlParameter[] p = { new SqlParameter("@Keyword", "%" + tuKhoa.Trim() + "%") };
            return Database.ExecuteQuery(sql, p);
        }
        // Lọc sinh viên theo khoa, lớp và khóa học
        public DataTable LocSinhVien(string maKhoa, string maLop, string khoaHoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaKhoa", string.IsNullOrEmpty(maKhoa) ? (object)DBNull.Value : maKhoa),
        new SqlParameter("@MaLop", string.IsNullOrEmpty(maLop) ? (object)DBNull.Value : maLop),
        new SqlParameter("@KhoaHoc", string.IsNullOrEmpty(khoaHoc) ? (object)DBNull.Value : khoaHoc)
            };
            return Database.ExecuteProcedure("sp_LocSinhVien", parameters);
        }

        // Thêm sinh viên
        public bool Them(SinhVienDTO sv)
        {
            string sql = @"
                INSERT INTO SinhVien
                (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, HinhAnh, MaLop)
                VALUES
                (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @HinhAnh, @MaLop)";

            SqlParameter[] p =
            {
                new SqlParameter("@MaSV", sv.MaSV),
                new SqlParameter("@HoTen", sv.HoTen),
                new SqlParameter("@NgaySinh", sv.NgaySinh),
                new SqlParameter("@GioiTinh", sv.GioiTinh),
                new SqlParameter(
                    "@DiaChi",
                    string.IsNullOrEmpty(sv.DiaChi)
                        ? (object)DBNull.Value
                        : sv.DiaChi
                ),
                new SqlParameter(
                    "@HinhAnh",
                    string.IsNullOrEmpty(sv.HinhAnh)
                        ? (object)DBNull.Value
                        : sv.HinhAnh
                ),
                new SqlParameter("@MaLop", sv.MaLop)
            };

            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // Cập nhật sinh viên
        public bool CapNhat(SinhVienDTO sv)
        {
            string sql = @"
                UPDATE SinhVien
                SET HoTen = @HoTen,
                    NgaySinh = @NgaySinh,
                    GioiTinh = @GioiTinh,
                    DiaChi = @DiaChi,
                    HinhAnh = @HinhAnh,
                    MaLop = @MaLop
                WHERE MaSV = @MaSV";

            SqlParameter[] p =
            {
                new SqlParameter("@MaSV", sv.MaSV),
                new SqlParameter("@HoTen", sv.HoTen),
                new SqlParameter("@NgaySinh", sv.NgaySinh),
                new SqlParameter("@GioiTinh", sv.GioiTinh),
                new SqlParameter(
                    "@DiaChi",
                    string.IsNullOrEmpty(sv.DiaChi)
                        ? (object)DBNull.Value
                        : sv.DiaChi
                ),
                new SqlParameter(
                    "@HinhAnh",
                    string.IsNullOrEmpty(sv.HinhAnh)
                        ? (object)DBNull.Value
                        : sv.HinhAnh
                ),
                new SqlParameter("@MaLop", sv.MaLop)
            };

            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // Xóa sinh viên
        public bool Xoa(string maSV)
        {
            string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";

            SqlParameter[] p =
            {
                new SqlParameter("@MaSV", maSV)
            };

            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // Kiểm tra mã sinh viên đã tồn tại
        public bool KiemTraTrungMa(string maSV)
        {
            string sql = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";

            SqlParameter[] p =
            {
                new SqlParameter("@MaSV", maSV)
            };

            return (int)Database.ExecuteScalar(sql, p) > 0;
        }
    }
}
