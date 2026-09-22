using Quan_ly_sinh_vien.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DAL
{
    public class LopDAL
    {
        public DataTable LayTatCa()
        {
            string sql = @"SELECT l.MaLop, l.TenLop, l.MaKhoa, l.KhoaHoc, k.TenKhoa 
                           FROM Lop l INNER JOIN Khoa k ON l.MaKhoa = k.MaKhoa";
            return Database.ExecuteQuery(sql);
        }

        public DataTable LayTheoKhoa(string maKhoa)
        {
            string sql = "SELECT MaLop, TenLop, KhoaHoc FROM Lop WHERE MaKhoa = @MaKhoa";
            SqlParameter[] p = { new SqlParameter("@MaKhoa", maKhoa) };
            return Database.ExecuteQuery(sql, p);
        }

        // Lấy danh sách các khóa học hiện có để nạp vào ComboBox lọc
        public DataTable LayDanhSachKhoaHoc()
        {
            string sql = "SELECT DISTINCT KhoaHoc FROM Lop WHERE KhoaHoc IS NOT NULL AND KhoaHoc <> '' ORDER BY KhoaHoc DESC";
            return Database.ExecuteQuery(sql);
        }

        public bool Them(LopDTO l)
        {
            string sql = "INSERT INTO Lop (MaLop, TenLop, MaKhoa, KhoaHoc) VALUES (@MaLop, @TenLop, @MaKhoa, @KhoaHoc)";
            SqlParameter[] p = {
                new SqlParameter("@MaLop", l.MaLop),
                new SqlParameter("@TenLop", l.TenLop),
                new SqlParameter("@MaKhoa", l.MaKhoa),
                new SqlParameter("@KhoaHoc", string.IsNullOrEmpty(l.KhoaHoc) ? (object)DBNull.Value : l.KhoaHoc)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool CapNhat(LopDTO l)
        {
            string sql = "UPDATE Lop SET TenLop = @TenLop, MaKhoa = @MaKhoa, KhoaHoc = @KhoaHoc WHERE MaLop = @MaLop";
            SqlParameter[] p = {
                new SqlParameter("@MaLop", l.MaLop),
                new SqlParameter("@TenLop", l.TenLop),
                new SqlParameter("@MaKhoa", l.MaKhoa),
                new SqlParameter("@KhoaHoc", string.IsNullOrEmpty(l.KhoaHoc) ? (object)DBNull.Value : l.KhoaHoc)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool Xoa(string maLop)
        {
            string sql = "DELETE FROM Lop WHERE MaLop = @MaLop";
            SqlParameter[] p = { new SqlParameter("@MaLop", maLop) };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool KiemTraTrungMa(string maLop)
        {
            string sql = "SELECT COUNT(*) FROM Lop WHERE MaLop = @MaLop";
            SqlParameter[] p = { new SqlParameter("@MaLop", maLop) };
            return (int)Database.ExecuteScalar(sql, p) > 0;
        }
    }
}
