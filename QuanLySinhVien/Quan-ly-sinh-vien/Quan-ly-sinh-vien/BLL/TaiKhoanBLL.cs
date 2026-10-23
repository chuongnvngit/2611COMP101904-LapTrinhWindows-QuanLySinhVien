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

        public bool TaoTaiKhoan(TaiKhoanDTO tk)
        {
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap) ||
                string.IsNullOrWhiteSpace(tk.MatKhau) ||
                string.IsNullOrWhiteSpace(tk.HoTen) ||
                string.IsNullOrWhiteSpace(tk.VaiTro))
            {
                throw new Exception("Vui lòng nhập đầy đủ thông tin bắt buộc!");
            }

            if (tk.MatKhau.Trim().Length < 6)
            {
                throw new Exception("Mật khẩu phải chứa ít nhất 6 ký tự!");
            }

            if (tk.VaiTro != "Admin" && tk.VaiTro != "GiaoVu" && tk.VaiTro != "GiangVien")
            {
                throw new Exception("Vai trò tài khoản không hợp lệ. Chỉ chấp nhận Admin, GiaoVu hoặc GiangVien.");
            }

            return dal.TaoTaiKhoan(tk);
        }
    }
}
