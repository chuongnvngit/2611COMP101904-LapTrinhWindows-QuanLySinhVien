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
    public class SinhVienBLL
    {
        private readonly SinhVienDAL dal = new SinhVienDAL();

        public DataTable LayDanhSach()
        {
            return dal.LayTatCa();
        }

        public DataTable TimKiem(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
                return dal.LayTatCa();
            return dal.TimKiem(tuKhoa);
        }

        public DataTable LocSinhVien(string maKhoa, string maLop, string khoaHoc)
        {
            return dal.LocSinhVien(maKhoa, maLop, khoaHoc);
        }

        public bool ThemSinhVien(SinhVienDTO sv)
        {
            KiemTraHopLe(sv);

            if (dal.KiemTraTrungMa(sv.MaSV.Trim()))
            {
                throw new Exception($"Mã sinh viên '{sv.MaSV}' đã tồn tại trong hệ thống!");
            }

            return dal.Them(sv);
        }

        public bool CapNhatSinhVien(SinhVienDTO sv)
        {
            KiemTraHopLe(sv);

            if (!dal.KiemTraTrungMa(sv.MaSV.Trim()))
            {
                throw new Exception($"Không tìm thấy sinh viên có mã '{sv.MaSV}' để cập nhật!");
            }

            return dal.CapNhat(sv);
        }

        public bool XoaSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new Exception("Vui lòng chọn hoặc nhập mã sinh viên cần xóa!");
            }

            if (!dal.KiemTraTrungMa(maSV.Trim()))
            {
                throw new Exception($"Không tìm thấy sinh viên có mã '{maSV}'!");
            }

            return dal.Xoa(maSV.Trim());
        }

        // Kiểm tra hợp lệ dữ liệu trước khi đẩy xuống DAL
        private void KiemTraHopLe(SinhVienDTO sv)
        {
            if (string.IsNullOrWhiteSpace(sv.MaSV))
                throw new Exception("Mã sinh viên không được để trống!");

            if (string.IsNullOrWhiteSpace(sv.HoTen))
                throw new Exception("Họ tên sinh viên không được để trống!");

            if (string.IsNullOrWhiteSpace(sv.MaLop))
                throw new Exception("Vui lòng chọn lớp học cho sinh viên!");

            if (sv.NgaySinh > DateTime.Now)
                throw new Exception("Ngày sinh không thể lớn hơn ngày hiện tại!");

            int tuoi = DateTime.Now.Year - sv.NgaySinh.Year;
            if (tuoi < 17 || tuoi > 70)
                throw new Exception("Độ tuổi sinh viên không hợp lệ (phải từ 17 tuổi trở lên)!");
        }
    }
}