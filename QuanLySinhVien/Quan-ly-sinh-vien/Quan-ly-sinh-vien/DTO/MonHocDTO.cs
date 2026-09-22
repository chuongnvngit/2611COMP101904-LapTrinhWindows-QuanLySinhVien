using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DTO
{
    public class MonHocDTO
    {
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public int SoTinChi { get; set; }

        public MonHocDTO() { }

        public MonHocDTO(string maMH, string tenMH, int soTinChi)
        {
            MaMH = maMH;
            TenMH = tenMH;
            SoTinChi = soTinChi;
        }
    }
}
