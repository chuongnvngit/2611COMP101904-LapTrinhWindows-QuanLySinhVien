using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_sinh_vien.DTO
{
    public class KhoaDTO
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }

        public KhoaDTO() { }

        public KhoaDTO(string maKhoa, string tenKhoa)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
        }
    }
}
