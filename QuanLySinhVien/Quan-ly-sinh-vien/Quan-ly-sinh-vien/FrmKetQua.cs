using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmKetQua : Form
    {
        private readonly KetQuaBLL ketQuaBLL = new KetQuaBLL();
        private readonly LopBLL lopBLL = new LopBLL();
        private readonly MonHocBLL monHocBLL = new MonHocBLL();
        private readonly SinhVienBLL sinhVienBLL = new SinhVienBLL();

        public FrmKetQua()
        {
            InitializeComponent();
        }

        private void FrmKetQua_Load(object sender, EventArgs e)
        {
            LoadComboBoxLop();
            LoadComboBoxMonHoc();

            // Trang trí DataGridView cho đẹp
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.MultiSelect = false;
            dgvKetQua.ReadOnly = true;
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.BackgroundColor = Color.White;
        }

        // =========================================================
        // NẠP DỮ LIỆU VÀO COMBOBOX
        // =========================================================
        private void LoadComboBoxLop()
        {
            try
            {
                cboLop.DataSource = lopBLL.LayDanhSach();
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";
                cboLop.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxMonHoc()
        {
            try
            {
                cboMonHoc.DataSource = monHocBLL.LayDanhSach();
                cboMonHoc.DisplayMember = "TenMH";
                cboMonHoc.ValueMember = "MaMH";
                cboMonHoc.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // NÚT LẤY DANH SÁCH NHẬP ĐIỂM
        // =========================================================
        private void btnLayDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                string maLop = cboLop.SelectedValue?.ToString();
                string maMH = cboMonHoc.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(maMH))
                {
                    MessageBox.Show("Vui lòng chọn Lớp và Môn học để lấy danh sách nhập điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dt = ketQuaBLL.LayDanhSachNhapDiem(maLop, maMH);
                dgvKetQua.DataSource = dt;

                // Tùy chỉnh tiêu đề cột
                if (dgvKetQua.Columns.Contains("MaSV")) dgvKetQua.Columns["MaSV"].HeaderText = "Mã SV";
                if (dgvKetQua.Columns.Contains("HoTen")) dgvKetQua.Columns["HoTen"].HeaderText = "Họ tên";
                if (dgvKetQua.Columns.Contains("TenLop")) dgvKetQua.Columns["TenLop"].HeaderText = "Lớp";
                if (dgvKetQua.Columns.Contains("MaMH")) dgvKetQua.Columns["MaMH"].Visible = false; // Ẩn cột mã môn
                if (dgvKetQua.Columns.Contains("Diem")) dgvKetQua.Columns["Diem"].HeaderText = "Điểm số";

                // Highlight những ô chưa có điểm
                foreach (DataGridViewRow row in dgvKetQua.Rows)
                {
                    if (row.Cells["Diem"].Value == DBNull.Value)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }

                XoaTrangO_NhapDiem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CLICK VÀO DATAGRIDVIEW ĐỂ ĐƯA LÊN Ô NHẬP
        // =========================================================
        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                txtMaSV.Text = row.Cells["MaSV"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtDiem.Text = row.Cells["Diem"].Value?.ToString();

                // Lấy mã môn học từ combobox (Vì đang xem bảng điểm theo môn)
                txtMaMH.Text = cboMonHoc.SelectedValue?.ToString();
            }
        }

        // =========================================================
        // NÚT LƯU ĐIỂM (INSERT HOẶC UPDATE TỰ ĐỘNG BẰNG SP)
        // =========================================================
        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSV.Text))
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên từ danh sách bên dưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal? diem = null;
                if (!string.IsNullOrWhiteSpace(txtDiem.Text))
                {
                    if (!decimal.TryParse(txtDiem.Text.Trim(), out decimal parsedDiem))
                    {
                        MessageBox.Show("Điểm số phải là dạng số (Ví dụ: 8.5)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDiem.Focus();
                        return;
                    }
                    diem = parsedDiem;
                }

                KetQuaDTO kq = new KetQuaDTO()
                {
                    MaSV = txtMaSV.Text,
                    MaMH = txtMaMH.Text,
                    Diem = diem
                };

                if (ketQuaBLL.LuuDiem(kq))
                {
                    MessageBox.Show("Lưu điểm thành công cho sinh viên " + txtHoTen.Text, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLayDanhSach_Click(sender, e); // Refresh lại bảng sau khi lưu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // NÚT XEM BẢNG ĐIỂM CÁ NHÂN VÀ TÍNH GPA TỔNG HỢP HỆ 10 & HỆ 4
        // =========================================================
        private void btnXemDiemCaNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtMaSV.Text.Trim();
                if (string.IsNullOrEmpty(maSV))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập Mã SV cần xem bảng điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Tải bảng điểm cá nhân
                DataTable dtBangDiem = ketQuaBLL.LayBangDiemSinhVien(maSV);
                dgvKetQua.DataSource = dtBangDiem;

                if (dgvKetQua.Columns.Contains("MaMH")) dgvKetQua.Columns["MaMH"].HeaderText = "Mã môn học";
                if (dgvKetQua.Columns.Contains("TenMH")) dgvKetQua.Columns["TenMH"].HeaderText = "Tên môn học";
                if (dgvKetQua.Columns.Contains("SoTinChi")) dgvKetQua.Columns["SoTinChi"].HeaderText = "Số tín chỉ";
                if (dgvKetQua.Columns.Contains("Diem")) dgvKetQua.Columns["Diem"].HeaderText = "Điểm số";

                // 2. Tự động tính toán điểm trung bình hệ 10, hệ 4 và số tín chỉ
                int tongTinChi = 0;
                decimal tongDiem10_TinChi = 0;
                decimal tongDiem4_TinChi = 0;

                foreach (DataRow row in dtBangDiem.Rows)
                {
                    if (row["SoTinChi"] != DBNull.Value && row["Diem"] != DBNull.Value)
                    {
                        int stc = Convert.ToInt32(row["SoTinChi"]);
                        decimal diem10 = Convert.ToDecimal(row["Diem"]);
                        decimal diem4 = ChuyenDiemHe4(diem10);

                        tongTinChi += stc;
                        tongDiem10_TinChi += (diem10 * stc);
                        tongDiem4_TinChi += (diem4 * stc);
                    }
                }

                // 3. Hiển thị thông tin tổng quát ra Label
                if (tongTinChi > 0)
                {
                    decimal gpa10 = Math.Round(tongDiem10_TinChi / tongTinChi, 2);
                    decimal gpa4 = Math.Round(tongDiem4_TinChi / tongTinChi, 2);
                    string xepLoai = XepLoaiHocTap(gpa4);

                    lblThongTinGPA.Text = $"- Tổng số tín chỉ đã tích lũy: {tongTinChi}\n" +
                                          $"- Điểm trung bình tích lũy (hệ 10): {gpa10}\n" +
                                          $"- Điểm trung bình tích lũy (hệ 4): {gpa4}\n" +
                                          $"- Xếp loại học tập: {xepLoai} (tính đến học kỳ hiện tại, lưu ý đây không phải là kết quả tốt nghiệp)";

                    lblThongTinGPA.Visible = true;

                    // Tự động đẩy DataGridView xuống để không đè lên 4 dòng thông tin
                    dgvKetQua.Top = lblThongTinGPA.Bottom + 15;
                }
                else
                {
                    lblThongTinGPA.Text = "Sinh viên chưa có điểm tích lũy.";
                    lblThongTinGPA.Visible = true;
                    dgvKetQua.Top = lblThongTinGPA.Bottom + 15;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrangO_NhapDiem();
            cboLop.SelectedIndex = -1;
            cboMonHoc.SelectedIndex = -1;
            dgvKetQua.DataSource = null;

            // Xóa thông tin và trả DataGridView về vị trí gốc[cite: 19]
            lblThongTinGPA.Visible = false;
            dgvKetQua.Top = 390;
        }

        private void XoaTrangO_NhapDiem()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtMaMH.Clear();
            txtDiem.Clear();
        }

        private void btnInBangDiem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn sinh viên chưa
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên để in bảng điểm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. CHẶN LỖI: Kiểm tra xem lưới đã có cột "TenMH" chưa
            if (!dgvKetQua.Columns.Contains("TenMH"))
            {
                MessageBox.Show("Lưới dữ liệu hiện tại chưa có bảng điểm cá nhân.\n\nVui lòng bấm nút 'Xem điểm TB (GPA)' để tải bảng điểm của sinh viên này trước khi in!",
                                "Thao tác sai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. Nếu mọi thứ đã hợp lệ, tiến hành mở hộp thoại in
            ppdInBangDiem.Document = pdInBangDiem;
            ppdInBangDiem.Width = 850;
            ppdInBangDiem.Height = 800;
            ppdInBangDiem.ShowDialog();
        }

        // =========================================================
        // IN BẢNG ĐIỂM KÈM GPA HỆ 4 VÀ XẾP LOẠI 
        // =========================================================
        private void pdInBangDiem_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            using (Font headerFont = new Font("Times New Roman", 18, FontStyle.Bold))
            using (Font infoFont = new Font("Times New Roman", 12, FontStyle.Regular))
            using (Font tableHeaderFont = new Font("Times New Roman", 12, FontStyle.Bold))
            using (Font tableRowFont = new Font("Times New Roman", 11, FontStyle.Regular))
            using (Pen pen = new Pen(Color.Black, 1))
            {
                // 1. Vẽ Header Trường và Tiêu đề
                g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP.HỒ CHÍ MINH", infoFont, Brushes.Black, 50, 45);
                g.DrawString("BẢNG KẾT QUẢ HỌC TẬP SINH VIÊN", headerFont, Brushes.Navy, 180, 95);

                // 2. THÊM LOGO VÀO GÓC TRÊN BÊN PHẢI 
                try
                {
                    if (Quan_ly_sinh_vien.Properties.Resources.LogoTruong != null)
                    {
                        g.DrawImage(Quan_ly_sinh_vien.Properties.Resources.LogoTruong, 640, 35, 110, 80);
                    }
                }
                catch { /* Bỏ qua nếu chưa chèn logo */ }

                // 3. Vẽ thông tin cơ bản của sinh viên
                g.DrawString($"Mã sinh viên: {txtMaSV.Text}", infoFont, Brushes.Black, 50, 160);
                g.DrawString($"Họ tên sinh viên: {txtHoTen.Text}", infoFont, Brushes.Black, 400, 160);

                // 4. Khởi tạo thông số bảng (Table)
                int x = 50;
                int y = 220;
                int rowHeight = 35;
                int[] colWidths = { 50, 120, 300, 100, 100 };

                g.FillRectangle(Brushes.LightGray, x, y, colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4], rowHeight);
                g.DrawRectangle(pen, x, y, colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4], rowHeight);

                int currentX = x;
                string[] headers = { "STT", "Mã MH", "Tên Môn Học", "Tín chỉ", "Điểm" };
                for (int i = 0; i < headers.Length; i++)
                {
                    g.DrawRectangle(pen, currentX, y, colWidths[i], rowHeight);
                    g.DrawString(headers[i], tableHeaderFont, Brushes.Black, currentX + 5, y + 8);
                    currentX += colWidths[i];
                }

                y += rowHeight;

                // 5. Duyệt qua DataGridView để in danh sách điểm và tích lũy GPA
                int stt = 1;
                int tongTinChi = 0;
                decimal tongDiem10_TinChi = 0;
                decimal tongDiem4_TinChi = 0;

                foreach (DataGridViewRow row in dgvKetQua.Rows)
                {
                    if (row.IsNewRow) continue;

                    currentX = x;

                    string maMH = row.Cells["MaMH"].Value?.ToString() ?? "";
                    string tenMH = row.Cells["TenMH"].Value?.ToString() ?? "";
                    string tinChiStr = row.Cells["SoTinChi"].Value?.ToString() ?? "0";
                    string diemStr = row.Cells["Diem"].Value?.ToString() ?? "0";

                    string[] rowData = { stt.ToString(), maMH, tenMH, tinChiStr, diemStr };

                    for (int i = 0; i < rowData.Length; i++)
                    {
                        g.DrawRectangle(pen, currentX, y, colWidths[i], rowHeight);
                        g.DrawString(rowData[i], tableRowFont, Brushes.Black, currentX + 5, y + 8);
                        currentX += colWidths[i];
                    }

                    // Tích lũy tính toán GPA
                    if (int.TryParse(tinChiStr, out int stc) && decimal.TryParse(diemStr, out decimal diem10))
                    {
                        decimal diem4 = ChuyenDiemHe4(diem10);
                        tongTinChi += stc;
                        tongDiem10_TinChi += (diem10 * stc);
                        tongDiem4_TinChi += (diem4 * stc);
                    }

                    y += rowHeight;
                    stt++;
                }

                // 6. IN TỔNG KẾT TÍN CHỈ VÀ XẾP LOẠI CUỐI TRANG
                decimal gpa10 = tongTinChi > 0 ? Math.Round(tongDiem10_TinChi / tongTinChi, 2) : 0;
                decimal gpa4 = tongTinChi > 0 ? Math.Round(tongDiem4_TinChi / tongTinChi, 2) : 0;
                string xepLoai = XepLoaiHocTap(gpa4);

                y += 20;
                using (Font summaryFont = new Font("Times New Roman", 12, FontStyle.Bold))
                using (Font italicFont = new Font("Times New Roman", 11, FontStyle.Italic))
                {
                    g.DrawString($"- Tổng số tín chỉ đã tích lũy: {tongTinChi}", summaryFont, Brushes.Black, 50, y);
                    y += 25;
                    g.DrawString($"- Điểm trung bình tích lũy (hệ 10): {gpa10}", summaryFont, Brushes.Black, 50, y);
                    y += 25;
                    g.DrawString($"- Điểm trung bình tích lũy (hệ 4): {gpa4}", summaryFont, Brushes.Black, 50, y);
                    y += 25;

                    string strXepLoai = $"- Xếp loại học tập: {xepLoai}";
                    g.DrawString(strXepLoai, summaryFont, Brushes.Black, 50, y);

                    // Đo độ dài chuỗi xếp loại để in tiếp phần ghi chú in nghiêng nằm ngang hàng
                    float textWidth = g.MeasureString(strXepLoai, summaryFont).Width;
                    g.DrawString(" (tính đến học kỳ hiện tại, lưu ý đây không phải là kết quả tốt nghiệp)", italicFont, Brushes.Black, 45 + textWidth, y);
                }
            }

            e.HasMorePages = false;
        }

        // =========================================================
        // BỘ CÔNG CỤ QUY ĐỔI ĐIỂM VÀ XẾP LOẠI HỌC VỤ
        // =========================================================
        private decimal ChuyenDiemHe4(decimal diem10)
        {
            if (diem10 >= 8.5m) return 4.0m; // A
            if (diem10 >= 8.0m) return 3.5m; // B+
            if (diem10 >= 7.0m) return 3.0m; // B
            if (diem10 >= 6.5m) return 2.5m; // C+
            if (diem10 >= 5.5m) return 2.0m; // C
            if (diem10 >= 5.0m) return 1.5m; // D+
            if (diem10 >= 4.0m) return 1.0m; // D
            return 0.0m; // F
        }

        private string XepLoaiHocTap(decimal gpa4)
        {
            if (gpa4 >= 3.6m) return "Xuất sắc";
            if (gpa4 >= 3.2m) return "Giỏi";
            if (gpa4 >= 2.5m) return "Khá";
            if (gpa4 >= 2.0m) return "Trung bình";
            if (gpa4 >= 1.0m) return "Yếu";
            return "Kém";
        }
    }
}