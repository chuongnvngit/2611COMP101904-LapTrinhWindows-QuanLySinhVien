using Quan_ly_sinh_vien.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DAL
{
    public class KetQuaDAL
    {
        // Lấy danh sách để nhập điểm theo Lớp và Môn học
        public DataTable LayDanhSachNhapDiem(string maLop, string maMH)
        {
            SqlParameter[] p = {
                new SqlParameter("@MaLop", maLop),
                new SqlParameter("@MaMH", maMH)
            };
            return Database.ExecuteProcedure("sp_LayDanhSachNhapDiem", p);
        }

        // Lưu điểm (Nếu đã có thì UPDATE, chưa có thì INSERT)
        public bool LuuDiem(KetQuaDTO kq)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM KetQua WHERE MaSV = @MaSV AND MaMH = @MaMH)
                           BEGIN
                               UPDATE KetQua SET Diem = @Diem WHERE MaSV = @MaSV AND MaMH = @MaMH
                           END
                           ELSE
                           BEGIN
                               INSERT INTO KetQua (MaSV, MaMH, Diem) VALUES (@MaSV, @MaMH, @Diem)
                           END";
            SqlParameter[] p = {
                new SqlParameter("@MaSV", kq.MaSV),
                new SqlParameter("@MaMH", kq.MaMH),
                new SqlParameter("@Diem", kq.Diem.HasValue ? (object)kq.Diem.Value : DBNull.Value)
            };
            return Database.ExecuteNonQuery(sql, p) > 0;
        }

        // Lấy bảng điểm của 1 sinh viên (phục vụ in ấn/báo cáo)
        public DataTable LayBangDiemSinhVien(string maSV)
        {
            SqlParameter[] p = { new SqlParameter("@MaSV", maSV) };
            return Database.ExecuteProcedure("sp_LayBangDiemSinhVien", p);
        }

        // Lấy GPA và tổng số tín chỉ tích lũy
        public DataTable TinhDiemTrungBinh(string maSV)
        {
            SqlParameter[] p = { new SqlParameter("@MaSV", maSV) };
            return Database.ExecuteProcedure("sp_TinhDiemTrungBinh", p);
        }
    }
}
