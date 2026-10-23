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
    public class TaiKhoanDAL
    {
        public TaiKhoanDTO DangNhap(string username, string password)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", username),
                new SqlParameter("@MatKhau", password)
            };

            DataTable dt = Database.ExecuteProcedure(
                "sp_KiemTraDangNhap",
                parameters
            );

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];

                return new TaiKhoanDTO(
                    r["TenDangNhap"].ToString(),
                    r["MatKhau"].ToString(),
                    r["HoTen"].ToString(),
                    r["VaiTro"].ToString()
                );
            }

            return null;
        }

        public DataTable LayTatCaTaiKhoan()
        {
            return Database.ExecuteQuery(
                "SELECT TenDangNhap, HoTen, VaiTro FROM TaiKhoan"
            );
        }

        public bool TaoTaiKhoan(TaiKhoanDTO tk)
        {
            SqlParameter paramKetQua = new SqlParameter("@KetQua", SqlDbType.Int);
            paramKetQua.Direction = ParameterDirection.Output;

            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", tk.TenDangNhap.Trim()),
                new SqlParameter("@MatKhau", tk.MatKhau.Trim()),
                new SqlParameter("@HoTen", tk.HoTen.Trim()),
                new SqlParameter("@VaiTro", tk.VaiTro.Trim()),
                paramKetQua 
            };

            Database.ExecuteProcedure("sp_TaoTaiKhoan", parameters);

            int ketQua = Convert.ToInt32(paramKetQua.Value);
            return ketQua == 1;
        }
    }
}
