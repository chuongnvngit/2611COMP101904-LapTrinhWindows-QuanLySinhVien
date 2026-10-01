using System;
using System.Data;
using Quan_ly_sinh_vien.DAL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien.BLL
{
    public class LopBLL
    {
        private readonly LopDAL dal = new LopDAL();

        // =========================================================
        // LẤY DANH SÁCH LỚP
        // =========================================================
        public DataTable LayDanhSach()
        {
            return dal.LayTatCa();
        }

        // =========================================================
        // LẤY LỚP THEO KHOA
        // =========================================================
        public DataTable LayTheoKhoa(string maKhoa)
        {
            return dal.LayTheoKhoa(maKhoa);
        }

        // =========================================================
        // THÊM LỚP
        // =========================================================
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
                throw new Exception(
                    $"Mã lớp '{l.MaLop}' đã tồn tại!");

            return dal.Them(l);
        }

        // =========================================================
        // CẬP NHẬT LỚP
        // =========================================================
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

        // =========================================================
        // XÓA LỚP
        // =========================================================
        public bool Xoa(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new Exception(
                    "Vui lòng chọn mã lớp cần xóa!");

            try
            {
                return dal.Xoa(maLop.Trim());
            }
            catch
            {
                throw new Exception(
                    "Không thể xóa lớp này do đang có sinh viên theo học!");
            }
        }
    }
}