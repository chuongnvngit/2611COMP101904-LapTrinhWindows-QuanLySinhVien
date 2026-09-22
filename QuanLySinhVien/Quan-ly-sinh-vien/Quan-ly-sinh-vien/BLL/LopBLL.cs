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
    public class LopBLL
    {
        private readonly LopDAL dal = new LopDAL();

        public DataTable LayDanhSach()
        {
            return dal.LayTatCa();
        }

        public DataTable LayTheoKhoa(string maKhoa)
        {
            return dal.LayTheoKhoa(maKhoa);
        }

        public DataTable LayDanhSachKhoaHoc()
        {
            return dal.LayDanhSachKhoaHoc();
        }

        public bool Them(LopDTO l)
        {
            if (l == null)
                throw new Exception("Dữ liệu lớp không hợp lệ!");

            if (string.IsNullOrWhiteSpace(l.MaLop))
                throw new Exception("Mã lớp không được để trống!");

            if (string.IsNullOrWhiteSpace(l.TenLop))
                throw new Exception("Tên lớp không được để trống!");

            if (string.IsNullOrWhiteSpace(l.MaKhoa))
                throw new Exception("Vui lòng chọn khoa trực thuộc!");

            if (dal.KiemTraTrungMa(l.MaLop.Trim()))
                throw new Exception($"Mã lớp '{l.MaLop}' đã tồn tại!");

            return dal.Them(l);
        }

        public bool CapNhat(LopDTO l)
        {
            if (l == null)
                throw new Exception("Dữ liệu lớp không hợp lệ!");

            if (string.IsNullOrWhiteSpace(l.MaLop))
                throw new Exception("Mã lớp không hợp lệ!");

            if (string.IsNullOrWhiteSpace(l.TenLop))
                throw new Exception("Tên lớp không được để trống!");

            if (string.IsNullOrWhiteSpace(l.MaKhoa))
                throw new Exception("Vui lòng chọn khoa trực thuộc!");

            return dal.CapNhat(l);
        }

        public bool Xoa(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new Exception("Vui lòng chọn mã lớp cần xóa!");

            try
            {
                return dal.Xoa(maLop.Trim());
            }
            catch
            {
                throw new Exception(
                    "Không thể xóa lớp này do đang có sinh viên theo học!"
                );
            }
        }
    }
}
