using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmLop : Form
    {
        private readonly LopBLL lopBLL = new LopBLL();
        private readonly KhoaBLL khoaBLL = new KhoaBLL();

        private DataTable danhSachLop;

        public FrmLop()
        {
            InitializeComponent();

            this.Load += FrmLop_Load;

            btnThem.Click += btnThem_Click;
            btnCapNhat.Click += btnCapNhat_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            dgvLop.CellClick += dgvLop_CellClick;

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        // =========================================================
        // LOAD FORM
        // =========================================================
        private void FrmLop_Load(object sender, EventArgs e)
        {
            HienThiDanhSachKhoa();
            HienThiDanhSach();
            LamMoi();
        }

        // =========================================================
        // HIỂN THỊ DANH SÁCH KHOA
        // =========================================================
        private void HienThiDanhSachKhoa()
        {
            try
            {
                DataTable dt = khoaBLL.LayDanhSach();

                cboKhoa.DataSource = dt;
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách khoa!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // HIỂN THỊ DANH SÁCH LỚP
        // =========================================================
        private void HienThiDanhSach()
        {
            try
            {
                danhSachLop = lopBLL.LayDanhSach();

                dgvLop.DataSource = null;
                dgvLop.AutoGenerateColumns = true;
                dgvLop.DataSource = danhSachLop;

                if (dgvLop.Columns.Contains("MaLop"))
                    dgvLop.Columns["MaLop"].HeaderText = "Mã lớp";

                if (dgvLop.Columns.Contains("TenLop"))
                    dgvLop.Columns["TenLop"].HeaderText = "Tên lớp";

                if (dgvLop.Columns.Contains("MaKhoa"))
                    dgvLop.Columns["MaKhoa"].HeaderText = "Mã khoa";

                if (dgvLop.Columns.Contains("TenKhoa"))
                    dgvLop.Columns["TenKhoa"].HeaderText = "Tên khoa";

                dgvLop.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách lớp!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // THÊM LỚP
        // =========================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maLop = txtMaLop.Text.Trim();
                string tenLop = txtTenLop.Text.Trim();

                if (string.IsNullOrWhiteSpace(maLop))
                {
                    MessageBox.Show(
                        "Vui lòng nhập mã lớp!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMaLop.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(tenLop))
                {
                    MessageBox.Show(
                        "Vui lòng nhập tên lớp!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTenLop.Focus();
                    return;
                }

                if (cboKhoa.SelectedIndex < 0)
                {
                    MessageBox.Show(
                        "Vui lòng chọn khoa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cboKhoa.Focus();
                    return;
                }

                string maKhoa =
                    cboKhoa.SelectedValue.ToString();

                LopDTO lop = new LopDTO
                {
                    MaLop = maLop,
                    TenLop = tenLop,
                    MaKhoa = maKhoa
                };

                bool ketQua = lopBLL.Them(lop);

                if (ketQua)
                {
                    MessageBox.Show(
                        "Thêm lớp thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra khi thêm lớp:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẬP NHẬT
        // =========================================================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhoa = "";

                if (cboKhoa.SelectedValue != null)
                    maKhoa = cboKhoa.SelectedValue.ToString();

                LopDTO lop = new LopDTO
                {
                    MaLop = txtMaLop.Text.Trim(),
                    TenLop = txtTenLop.Text.Trim(),
                    MaKhoa = maKhoa
                };

                bool ketQua = lopBLL.CapNhat(lop);

                if (ketQua)
                {
                    MessageBox.Show(
                        "Cập nhật lớp thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // XÓA
        // =========================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn lớp cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lớp '" +
                txtMaLop.Text.Trim() +
                "' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool ketQua =
                    lopBLL.Xoa(txtMaLop.Text.Trim());

                if (ketQua)
                {
                    MessageBox.Show(
                        "Xóa lớp thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // LÀM MỚI
        // =========================================================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            HienThiDanhSach();
            LamMoi();
        }

        private void LamMoi()
        {
            txtMaLop.ReadOnly = false;

            txtMaLop.Clear();
            txtTenLop.Clear();

            cboKhoa.SelectedIndex = -1;

            txtTimKiem.Clear();

            dgvLop.ClearSelection();

            txtMaLop.Focus();
        }

        // =========================================================
        // CLICK DÒNG
        // =========================================================
        private void dgvLop_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvLop.Rows[e.RowIndex];

            if (row.Cells["MaLop"].Value != null)
            {
                txtMaLop.Text =
                    row.Cells["MaLop"].Value.ToString();
            }

            if (row.Cells["TenLop"].Value != null)
            {
                txtTenLop.Text =
                    row.Cells["TenLop"].Value.ToString();
            }

            if (row.Cells["MaKhoa"].Value != null)
            {
                cboKhoa.SelectedValue =
                    row.Cells["MaKhoa"].Value.ToString();
            }

            txtMaLop.ReadOnly = true;
        }

        // =========================================================
        // TÌM KIẾM
        // =========================================================
        private void txtTimKiem_TextChanged(
            object sender,
            EventArgs e)
        {
            if (danhSachLop == null)
                return;

            string tuKhoa =
                txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                dgvLop.DataSource = danhSachLop;
                return;
            }

            DataView view =
                danhSachLop.DefaultView;

            string tuKhoaAnToan =
                tuKhoa.Replace("'", "''");

            view.RowFilter =
                "MaLop LIKE '%" + tuKhoaAnToan + "%' " +
                "OR TenLop LIKE '%" + tuKhoaAnToan + "%' " +
                "OR MaKhoa LIKE '%" + tuKhoaAnToan + "%' " +
                "OR TenKhoa LIKE '%" + tuKhoaAnToan + "%'";

            dgvLop.DataSource = view;
        }

        // =========================================================
        // EVENT DO DESIGNER TẠO
        // =========================================================
        private void lblMaLop_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblTimKiem_Click(
            object sender,
            EventArgs e)
        {
        }
    }
}