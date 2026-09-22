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
    public class KhoaDAL
    {
        public DataTable LayTatCa()
        {
            return Database.ExecuteQuery("SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY MaKhoa ASC");
        }

        public bool Them(KhoaDTO k)
        {
            string sql = "INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES (@MaKhoa, @TenKhoa)";
            SqlParameter[] p = {
                new SqlParameter("@MaKhoa", k.MaKhoa),
                new SqlParameter("@TenKhoa", k.TenKhoa)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool CapNhat(KhoaDTO k)
        {
            string sql = "UPDATE Khoa SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa";
            SqlParameter[] p = {
                new SqlParameter("@MaKhoa", k.MaKhoa),
                new SqlParameter("@TenKhoa", k.TenKhoa)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool Xoa(string maKhoa)
        {
            string sql = "DELETE FROM Khoa WHERE MaKhoa = @MaKhoa";
            SqlParameter[] p = { new SqlParameter("@MaKhoa", maKhoa) };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool KiemTraTrungMa(string maKhoa)
        {
            string sql = "SELECT COUNT(*) FROM Khoa WHERE MaKhoa = @MaKhoa";
            SqlParameter[] p = { new SqlParameter("@MaKhoa", maKhoa) };
            return (int)Database.ExecuteScalar(sql, p) > 0;
        }
    }
}
