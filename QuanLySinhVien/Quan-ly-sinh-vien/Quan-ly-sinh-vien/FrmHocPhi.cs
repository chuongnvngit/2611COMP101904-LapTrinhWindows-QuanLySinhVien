using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;

namespace Quan_ly_sinh_vien
{
    public partial class FrmHocPhi : Form
    {
        private readonly SinhVienBLL sinhVienBLL = new SinhVienBLL();
        private readonly KetQuaBLL ketQuaBLL = new KetQuaBLL();

        public FrmHocPhi()
        {
            InitializeComponent();
        }

        private void FrmHocPhi_Load(object sender, EventArgs e)
        {
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadDanhSachSinhVien();

            btnTinhHocPhi.Click += btnTinhHocPhi_Click;
        }

        private void LoadDanhSachSinhVien()
        {
            try
            {
                DataTable dt = sinhVienBLL.LayDanhSach();
                dgvSinhVien.DataSource = dt;

                if (dgvSinhVien.Columns.Contains("NganhHoc")) dgvSinhVien.Columns["NganhHoc"].HeaderText = "Ngành học";
                if (dgvSinhVien.Columns.Contains("KyNghiDinh116")) dgvSinhVien.Columns["KyNghiDinh116"].HeaderText = "Ký NĐ 116";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
        }

        private void btnTinhHocPhi_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvSinhVien.CurrentRow;
            string maSV = row.Cells["MaSV"].Value.ToString();
            string hoTen = row.Cells["HoTen"].Value.ToString();

            string nganhHoc = "Chưa xác định";
            if (dgvSinhVien.Columns.Contains("NganhHoc") && row.Cells["NganhHoc"].Value != DBNull.Value)
            {
                nganhHoc = row.Cells["NganhHoc"].Value.ToString();
            }

            bool kyNghiDinh = false;
            if (dgvSinhVien.Columns.Contains("KyNghiDinh116") && row.Cells["KyNghiDinh116"].Value != DBNull.Value)
            {
                kyNghiDinh = Convert.ToBoolean(row.Cells["KyNghiDinh116"].Value);
            }

            DataTable dtGPA = ketQuaBLL.TinhDiemTrungBinh(maSV);
            int tongTinChi = 0;
            if (dtGPA.Rows.Count > 0)
            {
                tongTinChi = Convert.ToInt32(dtGPA.Rows[0]["TongSoTinChi"]);
            }

            decimal donGia = 700000;
            decimal hocPhi = 0;
            string trangThai = "";

            if (kyNghiDinh)
            {
                hocPhi = 0;
                trangThai = "Học phí là 0";
                lblThongTin.ForeColor = Color.Green;
            }
            else
            {
                hocPhi = tongTinChi * donGia;
                trangThai = $"Học phí là {hocPhi:N0}đ";
                lblThongTin.ForeColor = Color.Firebrick;
            }

            lblThongTin.Text = $"Thông tin học phí:\n" +
                               $"- Họ tên: {hoTen} ({maSV})\n" +
                               $"- Tổng tín chỉ đã đăng ký: {tongTinChi}\n" +
                               $"* TRẠNG THÁI: {trangThai}";
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void lblThongTin_Click(object sender, EventArgs e)
        {
        }
    }
}