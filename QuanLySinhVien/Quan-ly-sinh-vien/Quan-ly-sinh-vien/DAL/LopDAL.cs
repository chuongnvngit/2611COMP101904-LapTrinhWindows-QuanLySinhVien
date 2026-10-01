using Quan_ly_sinh_vien.DTO;
using System.Data;
using System.Data.SqlClient;

namespace Quan_ly_sinh_vien.DAL
{
    public class LopDAL
    {
        // =========================================================
        // LẤY TẤT CẢ LỚP
        // =========================================================
        public DataTable LayTatCa()
        {
            string sql = @"
                SELECT 
                    l.MaLop,
                    l.TenLop,
                    l.MaKhoa,
                    k.TenKhoa
                FROM Lop l
                INNER JOIN Khoa k 
                    ON l.MaKhoa = k.MaKhoa
                ORDER BY l.MaLop ASC";

            return Database.ExecuteQuery(sql);
        }

        // =========================================================
        // LẤY LỚP THEO KHOA
        // =========================================================
        public DataTable LayTheoKhoa(string maKhoa)
        {
            string sql = @"
                SELECT 
                    MaLop,
                    TenLop,
                    MaKhoa
                FROM Lop
                WHERE MaKhoa = @MaKhoa
                ORDER BY MaLop ASC";

            SqlParameter[] p =
            {
                new SqlParameter("@MaKhoa", maKhoa)
            };

            return Database.ExecuteQuery(sql, p);
        }

        // =========================================================
        // THÊM LỚP
        // =========================================================
        public bool Them(LopDTO l)
        {
            string sql = @"
                INSERT INTO Lop
                (
                    MaLop,
                    TenLop,
                    MaKhoa
                )
                VALUES
                (
                    @MaLop,
                    @TenLop,
                    @MaKhoa
                )";

            SqlParameter[] p =
            {
                new SqlParameter("@MaLop", l.MaLop),
                new SqlParameter("@TenLop", l.TenLop),
                new SqlParameter("@MaKhoa", l.MaKhoa)
            };

            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // =========================================================
        // CẬP NHẬT LỚP
        // =========================================================
        public bool CapNhat(LopDTO l)
        {
            string sql = @"
                UPDATE Lop
                SET
                    TenLop = @TenLop,
                    MaKhoa = @MaKhoa
                WHERE MaLop = @MaLop";

            SqlParameter[] p =
            {
                new SqlParameter("@MaLop", l.MaLop),
                new SqlParameter("@TenLop", l.TenLop),
                new SqlParameter("@MaKhoa", l.MaKhoa)
            };

            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // =========================================================
        // XÓA LỚP
        // =========================================================
        public bool Xoa(string maLop)
        {
            string sql =
                "DELETE FROM Lop WHERE MaLop = @MaLop";

            SqlParameter[] p =
            {
                new SqlParameter("@MaLop", maLop)
            };

            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // =========================================================
        // KIỂM TRA TRÙNG MÃ LỚP
        // =========================================================
        public bool KiemTraTrungMa(string maLop)
        {
            string sql =
                "SELECT COUNT(*) FROM Lop WHERE MaLop = @MaLop";

            SqlParameter[] p =
            {
                new SqlParameter("@MaLop", maLop)
            };

            return (int)Database.ExecuteScalar(sql, p) > 0;
        }
    }
}