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
    public class MonHocDAL
    {
        public DataTable LayTatCa()
        {
            return Database.ExecuteQuery("SELECT MaMH, TenMH, SoTinChi FROM MonHoc ORDER BY MaMH ASC");
        }

        public bool Them(MonHocDTO mh)
        {
            string sql = "INSERT INTO MonHoc (MaMH, TenMH, SoTinChi) VALUES (@MaMH, @TenMH, @SoTinChi)";
            SqlParameter[] p = {
                new SqlParameter("@MaMH", mh.MaMH),
                new SqlParameter("@TenMH", mh.TenMH),
                new SqlParameter("@SoTinChi", mh.SoTinChi)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool CapNhat(MonHocDTO mh)
        {
            string sql = "UPDATE MonHoc SET TenMH = @TenMH, SoTinChi = @SoTinChi WHERE MaMH = @MaMH";
            SqlParameter[] p = {
                new SqlParameter("@MaMH", mh.MaMH),
                new SqlParameter("@TenMH", mh.TenMH),
                new SqlParameter("@SoTinChi", mh.SoTinChi)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool Xoa(string maMH)
        {
            string sql = "DELETE FROM MonHoc WHERE MaMH = @MaMH";
            SqlParameter[] p = { new SqlParameter("@MaMH", maMH) };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        public bool KiemTraTrungMa(string maMH)
        {
            string sql = "SELECT COUNT(*) FROM MonHoc WHERE MaMH = @MaMH";
            SqlParameter[] p = { new SqlParameter("@MaMH", maMH) };
            return (int)Database.ExecuteScalar(sql, p) > 0;
        }
    }
}
