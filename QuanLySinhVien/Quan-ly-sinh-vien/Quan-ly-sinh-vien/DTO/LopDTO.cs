namespace Quan_ly_sinh_vien.DTO
{
    public class LopDTO
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string MaKhoa { get; set; }

        public LopDTO()
        {
        }

        public LopDTO(
            string maLop,
            string tenLop,
            string maKhoa)
        {
            MaLop = maLop;
            TenLop = tenLop;
            MaKhoa = maKhoa;
        }
    }
}