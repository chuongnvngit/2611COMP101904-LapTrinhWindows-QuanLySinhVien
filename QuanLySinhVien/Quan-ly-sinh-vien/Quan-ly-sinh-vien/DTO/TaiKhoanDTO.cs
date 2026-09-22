using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DTO
{
    public class TaiKhoanDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string VaiTro { get; set; }

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string tenDangNhap, string matKhau, string hoTen, string vaiTro)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            VaiTro = vaiTro;
        }
    }
}
