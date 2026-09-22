using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DTO
{
    public class KetQuaDTO
    {
        public string MaSV { get; set; }
        public string MaMH { get; set; }
        public decimal? Diem { get; set; }

        public KetQuaDTO() { }

        public KetQuaDTO(string maSV, string maMH, decimal? diem)
        {
            MaSV = maSV;
            MaMH = maMH;
            Diem = diem;
        }
    }
}
