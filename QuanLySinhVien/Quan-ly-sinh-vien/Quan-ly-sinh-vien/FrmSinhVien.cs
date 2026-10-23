using System;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

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

        private string currentImageFileName = "Images"; // Lưu tên file ảnh đang chọn

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
                cboKhoaHoc.Items.Add("K51");
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
                cboKhoa.SelectedIndex = -1;
                cboLop.SelectedIndex = -1;
                cboKhoaHoc.SelectedIndex = -1;
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

            currentImageFileName = "Images";
            picHinhAnh.Image = null;

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

                txtMaSV.Text = row.Cells["MaSV"].Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                dtpNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NgaySinh"].Value) : DateTime.Now;
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";

                cboKhoa.SelectedValue = row.Cells["MaKhoa"].Value?.ToString() ?? "";
                cboLop.SelectedValue = row.Cells["MaLop"].Value?.ToString() ?? "";
                cboKhoaHoc.Text = row.Cells["KhoaHoc"].Value?.ToString() ?? "";

                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString() ?? "";
                if (gioiTinh == "Nam") radNam.Checked = true;
                else if (gioiTinh == "Nữ") radNu.Checked = true;
                else radKhac.Checked = true;

                // Xử lý hiển thị ảnh của sinh viên khi click chọn dòng
                currentImageFileName = row.Cells["HinhAnh"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(currentImageFileName))
                {
                    string imagePath = Path.Combine(Application.StartupPath, "Images", currentImageFileName);
                    if (File.Exists(imagePath))
                    {
                        picHinhAnh.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        picHinhAnh.Image = null; // Không tìm thấy file vật lý
                    }
                }
                else
                {
                    picHinhAnh.Image = null; // Sinh viên chưa có ảnh
                }
            }
        }

        // =========================================================
        // HÀM KIỂM TRA NHẬP LIỆU
        // =========================================================
        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sinh viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Kiểm tra ComboBox Khoa đã được chọn chưa (Khắc phục lỗi NullReferenceException)
            if (cboKhoa.SelectedIndex == -1 || cboKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khoa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoa.Focus();
                return false;
            }

            // Kiểm tra ComboBox Lớp đã được chọn chưa
            if (cboLop.SelectedIndex == -1 || cboLop.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLop.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }

            return true;
        }

        // =========================================================
        // 4. CHỨC NĂNG THÊM SINH VIÊN
        // =========================================================
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Bắt buộc nhập đủ thông tin mới cho chạy tiếp
                if (!KiemTraNhapLieu()) return;

                string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nữ" : "Khác");

                SinhVienDTO sv = new SinhVienDTO()
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = gioiTinh,
                    DiaChi = txtDiaChi.Text.Trim(),
                    HinhAnh = currentImageFileName,
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
                // Bắt buộc nhập đủ thông tin mới cho chạy tiếp
                if (!KiemTraNhapLieu()) return;

                string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nữ" : "Khác");

                SinhVienDTO sv = new SinhVienDTO()
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = gioiTinh,
                    DiaChi = txtDiaChi.Text.Trim(),
                    HinhAnh = currentImageFileName,
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

        // Sự kiện click nút In thẻ
        private void btnInThe_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn sinh viên trên lưới chưa bằng cách check TextBox
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn một sinh viên trên danh sách để in thẻ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gắn tài liệu cần in vào hộp thoại xem trước và hiển thị
            ppdInThe.Document = pdInThe;
            ppdInThe.Width = 800;
            ppdInThe.Height = 600;
            ppdInThe.ShowDialog();
        }

        // In thẻ sinh viên ra trang giấy

private void pdInThe_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Chuyển toàn bộ Font sang Times New Roman
            using (Font titleFont = new Font("Times New Roman", 16, FontStyle.Bold))
            using (Font normalFont = new Font("Times New Roman", 11, FontStyle.Regular))
            using (Font boldFont = new Font("Times New Roman", 11, FontStyle.Bold))
            using (Pen pen = new Pen(Color.Navy, 2))
            {
                // 1. Vẽ khung thẻ sinh viên
                Rectangle cardRect = new Rectangle(50, 50, 480, 260);
                g.DrawRectangle(pen, cardRect);

                // 2. Tên Trường (Dịch sang X = 65 để tránh đè logo)
                g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP.HỒ CHÍ MINH", boldFont, Brushes.Navy, 65, 65);

                // 3. Tiêu đề Thẻ
                g.DrawString("THẺ SINH VIÊN", titleFont, Brushes.Red, 175, 95);

                // 4. THÊM LOGO VÀO GÓC TRÊN BÊN PHẢI (Kéo ngang 85px, cao 65px để không bị ép)
                try
                {
                    if (Quan_ly_sinh_vien.Properties.Resources.LogoTruong != null)
                    {
                        g.DrawImage(Quan_ly_sinh_vien.Properties.Resources.LogoTruong, 430, 55, 85, 65);
                    }
                }
                catch { /* Bỏ qua nếu chưa chèn logo */ }

                // 5. Vị trí hiển thị ảnh sinh viên trên thẻ
                Rectangle photoRect = new Rectangle(70, 130, 100, 130);

                if (!string.IsNullOrEmpty(currentImageFileName))
                {
                    string fullImagePath = Path.Combine(Application.StartupPath, "Images", currentImageFileName);
                    if (File.Exists(fullImagePath))
                    {
                        using (Image studentImg = Image.FromFile(fullImagePath))
                        {
                            g.DrawImage(studentImg, photoRect);
                        }
                    }
                    else
                    {
                        DrawEmptyPhotoBox(g, photoRect);
                    }
                }
                else
                {
                    DrawEmptyPhotoBox(g, photoRect);
                }

                g.DrawRectangle(Pens.Gray, photoRect);

                // 6. Vẽ thông tin chi tiết sinh viên
                int startX = 190;
                int startY = 140;
                int spacing = 30;

                g.DrawString("Họ tên:", boldFont, Brushes.Black, startX, startY);
                g.DrawString(txtHoTen.Text, normalFont, Brushes.Black, startX + 70, startY);

                g.DrawString("MSSV:", boldFont, Brushes.Black, startX, startY + spacing);
                g.DrawString(txtMaSV.Text, normalFont, Brushes.Black, startX + 70, startY + spacing);

                g.DrawString("Khoa:", boldFont, Brushes.Black, startX, startY + spacing * 2);
                g.DrawString(cboKhoa.Text, normalFont, Brushes.Black, startX + 70, startY + spacing * 2);
            }

            e.HasMorePages = false;
        }

        // =========================================================
        // HÀM HỖ TRỢ VẼ KHUNG TRỐNG KHI SINH VIÊN KHÔNG CÓ ẢNH
        // =========================================================
        private void DrawEmptyPhotoBox(Graphics g, Rectangle rect)
        {
            g.FillRectangle(Brushes.WhiteSmoke, rect);
            using (Font italicFont = new Font("Times New Roman", 9, FontStyle.Italic))
            {
                g.DrawString("Không có ảnh", italicFont, Brushes.Gray, rect.X + 12, rect.Y + 55);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = ofd.FileName;

                    // Tạo thư mục Images trong thư mục chạy ứng dụng nếu chưa có
                    string targetDir = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(targetDir))
                    {
                        Directory.CreateDirectory(targetDir);
                    }

                    // Đặt tên file độc lập bằng GUID để tránh trùng lặp
                    currentImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourcePath);
                    string destPath = Path.Combine(targetDir, currentImageFileName);

                    // Copy file ảnh vào thư mục dự án
                    File.Copy(sourcePath, destPath, true);

                    // Hiển thị lên PictureBox
                    picHinhAnh.Image = Image.FromFile(destPath);
                }
            }
        }
    }
}