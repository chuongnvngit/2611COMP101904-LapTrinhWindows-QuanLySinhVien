using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DTO
{
    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
        public string MaLop { get; set; }

        public SinhVienDTO() { }

        public SinhVienDTO(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string hinhAnh, string maLop)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            HinhAnh = hinhAnh;
            MaLop = maLop;
        }
    }
}
