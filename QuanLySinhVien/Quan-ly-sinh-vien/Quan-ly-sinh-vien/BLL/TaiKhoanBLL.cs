using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Quan_ly_sinh_vien.DAL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien.BLL
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL dal = new TaiKhoanDAL();

        public TaiKhoanDTO DangNhap(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Tên đăng nhập không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Mật khẩu không được để trống!");
            }

            TaiKhoanDTO tk = dal.DangNhap(username.Trim(), password);
            if (tk == null)
            {
                throw new Exception("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
            return tk;
        }

        public DataTable LayDanhSachTaiKhoan()
        {
            return dal.LayTatCaTaiKhoan();
        }
    }
}
