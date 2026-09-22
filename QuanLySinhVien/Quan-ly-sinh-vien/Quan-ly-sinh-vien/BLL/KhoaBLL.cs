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
    public class KhoaBLL
    {
        private readonly KhoaDAL dal = new KhoaDAL();

        public DataTable LayDanhSach()
        {
            return dal.LayTatCa();
        }

        public bool Them(KhoaDTO k)
        {
            if (string.IsNullOrWhiteSpace(k.MaKhoa))
                throw new Exception("Mã khoa không được để trống!");
            if (string.IsNullOrWhiteSpace(k.TenKhoa))
                throw new Exception("Tên khoa không được để trống!");

            if (dal.KiemTraTrungMa(k.MaKhoa.Trim()))
                throw new Exception($"Mã khoa '{k.MaKhoa}' đã tồn tại!");

            return dal.Them(k);
        }

        public bool CapNhat(KhoaDTO k)
        {
            if (string.IsNullOrWhiteSpace(k.MaKhoa))
                throw new Exception("Mã khoa không hợp lệ!");
            if (string.IsNullOrWhiteSpace(k.TenKhoa))
                throw new Exception("Tên khoa không được để trống!");

            return dal.CapNhat(k);
        }

        public bool Xoa(string maKhoa)
        {
            if (string.IsNullOrWhiteSpace(maKhoa))
                throw new Exception("Vui lòng chọn mã khoa cần xóa!");

            try
            {
                return dal.Xoa(maKhoa.Trim());
            }
            catch
            {
                throw new Exception("Không thể xóa khoa này do đang có lớp học trực thuộc!");
            }
        }
    }
}
