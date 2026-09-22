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
    public class KetQuaBLL
    {
        private readonly KetQuaDAL dal = new KetQuaDAL();

        // Lấy danh sách sinh viên để nhập điểm
        public DataTable LayDanhSachNhapDiem(string maLop, string maMH)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                throw new Exception("Vui lòng chọn lớp học!");

            if (string.IsNullOrWhiteSpace(maMH))
                throw new Exception("Vui lòng chọn môn học!");

            return dal.LayDanhSachNhapDiem(maLop, maMH);
        }

        // Lưu điểm
        public bool LuuDiem(KetQuaDTO kq)
        {
            if (kq == null)
                throw new Exception("Dữ liệu kết quả không hợp lệ!");

            if (string.IsNullOrWhiteSpace(kq.MaSV))
                throw new Exception("Thiếu thông tin mã sinh viên!");

            if (string.IsNullOrWhiteSpace(kq.MaMH))
                throw new Exception("Thiếu thông tin mã môn học!");

            if (kq.Diem.HasValue)
            {
                if (kq.Diem.Value < 0.0m || kq.Diem.Value > 10.0m)
                {
                    throw new Exception(
                        $"Điểm số '{kq.Diem.Value}' không hợp lệ! " +
                        "Điểm phải nằm trong thang điểm [0 - 10]."
                    );
                }
            }

            return dal.LuuDiem(kq);
        }

        // Lấy bảng điểm của sinh viên
        public DataTable LayBangDiemSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new Exception(
                    "Vui lòng chọn mã sinh viên để xem bảng điểm!"
                );

            return dal.LayBangDiemSinhVien(maSV.Trim());
        }

        // Tính điểm trung bình
        public DataTable TinhDiemTrungBinh(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new Exception(
                    "Vui lòng chọn mã sinh viên để tính GPA!"
                );

            return dal.TinhDiemTrungBinh(maSV.Trim());
        }
    }
}
