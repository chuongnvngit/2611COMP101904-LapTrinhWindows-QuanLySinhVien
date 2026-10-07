using System;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmSinhVien : Form
    {
        // Khởi tạo các đối tượng BLL
        SinhVienBLL sinhVienBLL = new SinhVienBLL();
        KhoaBLL khoaBLL = new KhoaBLL();
        LopBLL lopBLL = new LopBLL();

        public FrmSinhVien()
        {
            InitializeComponent();
        }

        // =========================================================
        // 1. TẢI DỮ LIỆU KHI MỞ FORM
        // =========================================================
        private void FrmSinhVien_Load_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Đổ dữ liệu tĩnh cho ComboBox Khóa học
                cboKhoaHoc.Items.Clear();
                cboKhoaHoc.Items.Add("K47");
                cboKhoaHoc.Items.Add("K48");
                cboKhoaHoc.Items.Add("K49");
                cboKhoaHoc.Items.Add("K50");

                // Đổ dữ liệu Khoa
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.DataSource = khoaBLL.LayDanhSach();

                // Đổ dữ liệu Lớp
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";
                cboLop.DataSource = lopBLL.LayDanhSach();

                // Đổ dữ liệu Sinh Viên
                dgvSinhVien.DataSource = sinhVienBLL.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 2. CHỨC NĂNG LÀM MỚI (XÓA TRẮNG CÁC Ô NHẬP)
        // =========================================================
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            radNam.Checked = true; // Mặc định chọn Nam
            txtMaSV.Focus();

            // Xóa nội dung ô tìm kiếm và combobox khóa (nếu có)
            if (txtTimKiem != null) txtTimKiem.Clear();
            cboKhoaHoc.SelectedIndex = -1; // Reset combobox Khóa học về trống

            LoadData(); // Tải lại toàn bộ dữ liệu gốc
        }

        // =========================================================
        // 3. HIỂN THỊ DỮ LIỆU KHI CLICK VÀO BẢNG
        // =========================================================
        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                txtMaSV.Text = row.Cells["MaSV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
               // cboKhoa.SelectedValue = row.Cells["MaKhoa"].Value;
                cboLop.SelectedValue = row.Cells["MaLop"].Value;

                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam") radNam.Checked = true;
                else if (gioiTinh == "Nữ") radNu.Checked = true;
                else radKhac.Checked = true;
            }
        }

        // =========================================================
        // 4. CHỨC NĂNG THÊM SINH VIÊN
        // =========================================================
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nữ" : "Khác");

                SinhVienDTO sv = new SinhVienDTO()
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = gioiTinh,
                    DiaChi = txtDiaChi.Text.Trim(),
                    MaKhoa = cboKhoa.SelectedValue.ToString(),
                    MaLop = cboLop.SelectedValue.ToString()
                };

                if (sinhVienBLL.ThemSinhVien(sv))
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click_1(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // 5. CHỨC NĂNG SỬA SINH VIÊN
        // =========================================================
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            try
            {
                string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nữ" : "Khác");

                SinhVienDTO sv = new SinhVienDTO()
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = gioiTinh,
                    DiaChi = txtDiaChi.Text.Trim(),
                    MaKhoa = cboKhoa.SelectedValue.ToString(),
                    MaLop = cboLop.SelectedValue.ToString()
                };

                if (sinhVienBLL.CapNhatSinhVien(sv))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // 6. CHỨC NĂNG XÓA SINH VIÊN
        // =========================================================
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtMaSV.Text.Trim();
                if (string.IsNullOrEmpty(maSV))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên mã {maSV} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (sinhVienBLL.XoaSinhVien(maSV))
                    {
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        btnLamMoi_Click_1(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // 7. CHỨC NĂNG TÌM KIẾM
        // =========================================================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    LoadData();
                }
                else
                {
                    dgvSinhVien.DataSource = sinhVienBLL.TimKiem(tuKhoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // 8. CHỨC NĂNG LỌC (Theo Khoa, Lớp, Khóa Học)
        // =========================================================
        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các ComboBox.
                string maKhoa = cboKhoa.SelectedValue?.ToString() ?? "";
                string maLop = cboLop.SelectedValue?.ToString() ?? "";
                string khoaHoc = cboKhoaHoc.Text.Trim();

                // Nếu người dùng không chọn gì cả mà bấm Lọc, thì tải lại toàn bộ danh sách
                if (maKhoa == "" && maLop == "" && khoaHoc == "")
                {
                    LoadData();
                    return;
                }

                // Gọi hàm lọc (Nếu báo lỗi đỏ chữ LocSinhVien, hãy thêm // ở đầu dòng này)
                dgvSinhVien.DataSource = sinhVienBLL.LocSinhVien(maKhoa, maLop, khoaHoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}