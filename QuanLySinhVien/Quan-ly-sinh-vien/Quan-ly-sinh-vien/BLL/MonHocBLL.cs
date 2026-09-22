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
    public class MonHocBLL
    {
        private readonly MonHocDAL dal = new MonHocDAL();

        public DataTable LayDanhSach()
        {
            return dal.LayTatCa();
        }

        public bool Them(MonHocDTO mh)
        {
            if (string.IsNullOrWhiteSpace(mh.MaMH))
                throw new Exception("Mã môn học không được để trống!");
            if (string.IsNullOrWhiteSpace(mh.TenMH))
                throw new Exception("Tên môn học không được để trống!");
            if (mh.SoTinChi <= 0)
                throw new Exception("Số tín chỉ phải lớn hơn 0!");

            if (dal.KiemTraTrungMa(mh.MaMH.Trim()))
                throw new Exception($"Mã môn học '{mh.MaMH}' đã tồn tại!");

            return dal.Them(mh);
        }

        public bool CapNhat(MonHocDTO mh)
        {
            if (string.IsNullOrWhiteSpace(mh.MaMH))
                throw new Exception("Mã môn học không hợp lệ!");
            if (string.IsNullOrWhiteSpace(mh.TenMH))
                throw new Exception("Tên môn học không được để trống!");
            if (mh.SoTinChi <= 0)
                throw new Exception("Số tín chỉ phải lớn hơn 0!");

            return dal.CapNhat(mh);
        }

        public bool Xoa(string maMH)
        {
            if (string.IsNullOrWhiteSpace(maMH))
                throw new Exception("Vui lòng chọn môn học cần xóa!");

            try
            {
                return dal.Xoa(maMH.Trim());
            }
            catch
            {
                throw new Exception("Không thể xóa môn học này vì đã có dữ liệu điểm thi!");
            }
        }
    }
}