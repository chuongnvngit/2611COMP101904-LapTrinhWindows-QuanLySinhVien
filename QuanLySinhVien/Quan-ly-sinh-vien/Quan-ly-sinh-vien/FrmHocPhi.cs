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
        private readonly string vaiTro; // Biến lưu quyền người dùng

        // Nhận quyền từ FrmMain
        public FrmHocPhi(string vaiTroNguoiDung)
        {
            InitializeComponent();
            this.vaiTro = vaiTroNguoiDung;
        }

        private void FrmHocPhi_Load(object sender, EventArgs e)
        {
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // KIỂM TRA QUYỀN: Chỉ Admin mới thấy nút và ô nhập tiền
            if (vaiTro != "Admin")
            {
                txtDongHocPhi.Visible = false;
                btnCapNhatHocPhi.Visible = false;
                lblNhapTien.Visible = false;
            }

            LoadDanhSachSinhVien();
            btnTinhHocPhi.Click += btnTinhHocPhi_Click;
            btnCapNhatHocPhi.Click += btnCapNhatHocPhi_Click;
        }

        private void LoadDanhSachSinhVien()
        {
            try
            {
                DataTable dt = sinhVienBLL.LayDanhSach();
                dgvSinhVien.DataSource = dt;

                if (dgvSinhVien.Columns.Contains("NganhHoc")) dgvSinhVien.Columns["NganhHoc"].HeaderText = "Ngành học";
                if (dgvSinhVien.Columns.Contains("KyNghiDinh116")) dgvSinhVien.Columns["KyNghiDinh116"].HeaderText = "Ký NĐ 116";
                if (dgvSinhVien.Columns.Contains("HocPhiDaDong")) dgvSinhVien.Columns["HocPhiDaDong"].HeaderText = "Đã đóng (VNĐ)";
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

            bool kyNghiDinh = false;
            if (dgvSinhVien.Columns.Contains("KyNghiDinh116") && row.Cells["KyNghiDinh116"].Value != DBNull.Value)
            {
                kyNghiDinh = Convert.ToBoolean(row.Cells["KyNghiDinh116"].Value);
            }

            decimal hocPhiDaDong = 0;
            if (dgvSinhVien.Columns.Contains("HocPhiDaDong") && row.Cells["HocPhiDaDong"].Value != DBNull.Value)
            {
                hocPhiDaDong = Convert.ToDecimal(row.Cells["HocPhiDaDong"].Value);
            }

            DataTable dtGPA = ketQuaBLL.TinhDiemTrungBinh(maSV);
            int tongTinChi = 0;
            if (dtGPA.Rows.Count > 0)
            {
                tongTinChi = Convert.ToInt32(dtGPA.Rows[0]["TongSoTinChi"]);
            }

            decimal donGia = 700000;
            decimal hocPhiTong = 0;
            decimal hocPhiConNo = 0;

            if (kyNghiDinh)
            {
                lblThongTin.Text = $"Thông tin học phí:\n- Họ tên: {hoTen} ({maSV})\n- Tín chỉ: {tongTinChi}\n* TRẠNG THÁI: Học phí là 0đ (Được miễn giảm theo Nghị định 116)";
                lblThongTin.ForeColor = Color.Green;
            }
            else
            {
                hocPhiTong = tongTinChi * donGia;
                hocPhiConNo = hocPhiTong - hocPhiDaDong;
                if (hocPhiConNo < 0) hocPhiConNo = 0; // Tránh nợ âm nếu đóng dư

                lblThongTin.Text = $"Thông tin học phí:\n" +
                                   $"- Họ tên: {hoTen} ({maSV})\n" +
                                   $"- Tổng tín chỉ: {tongTinChi} x 700.000đ = {hocPhiTong:N0}đ\n" +
                                   $"- Đã đóng: {hocPhiDaDong:N0}đ\n" +
                                   $"* CÒN NỢ: {hocPhiConNo:N0}đ";

                lblThongTin.ForeColor = hocPhiConNo == 0 ? Color.Blue : Color.Firebrick;
            }
        }

        private void btnCapNhatHocPhi_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDongHocPhi.Text.Trim(), out decimal soTien) || soTien < 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ (lớn hơn hoặc bằng 0)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maSV = dgvSinhVien.CurrentRow.Cells["MaSV"].Value.ToString();

            try
            {
                // Gọi BLL cập nhật dữ liệu xuống CSDL
                bool result = sinhVienBLL.CapNhatHocPhi(maSV, soTien);
                if (result)
                {
                    MessageBox.Show("Cập nhật học phí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachSinhVien(); // Tải lại danh sách
                    txtDongHocPhi.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}