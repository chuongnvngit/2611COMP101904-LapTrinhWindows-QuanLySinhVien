using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmMonHoc : Form
    {
        private readonly MonHocBLL monHocBLL = new MonHocBLL();

        private DataTable danhSachMonHoc;

        public FrmMonHoc()
        {
            InitializeComponent();
        }

        // =========================================================
        // LOAD FORM
        // =========================================================
        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            HienThiDanhSach();
            LamMoi();
        }

        // =========================================================
        // HIỂN THỊ DANH SÁCH MÔN HỌC
        // =========================================================
        private void HienThiDanhSach()
        {
            try
            {
                danhSachMonHoc = monHocBLL.LayDanhSach();

                dgvMonHoc.DataSource = null;
                dgvMonHoc.AutoGenerateColumns = true;
                dgvMonHoc.DataSource = danhSachMonHoc;

                if (dgvMonHoc.Columns.Contains("MaMH"))
                {
                    dgvMonHoc.Columns["MaMH"].HeaderText =
                        "Mã môn học";
                }

                if (dgvMonHoc.Columns.Contains("TenMH"))
                {
                    dgvMonHoc.Columns["TenMH"].HeaderText =
                        "Tên môn học";
                }

                if (dgvMonHoc.Columns.Contains("SoTinChi"))
                {
                    dgvMonHoc.Columns["SoTinChi"].HeaderText =
                        "Số tín chỉ";
                }

                dgvMonHoc.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách môn học!\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // THÊM MÔN HỌC
        // =========================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maMH = txtMaMH.Text.Trim();
                string tenMH = txtTenMH.Text.Trim();

                if (string.IsNullOrWhiteSpace(maMH))
                {
                    MessageBox.Show(
                        "Vui lòng nhập mã môn học!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMaMH.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(tenMH))
                {
                    MessageBox.Show(
                        "Vui lòng nhập tên môn học!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTenMH.Focus();
                    return;
                }

                int soTinChi;

                if (!int.TryParse(
                    txtSoTinChi.Text.Trim(),
                    out soTinChi))
                {
                    MessageBox.Show(
                        "Số tín chỉ phải là số nguyên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtSoTinChi.Focus();
                    return;
                }

                if (soTinChi <= 0)
                {
                    MessageBox.Show(
                        "Số tín chỉ phải lớn hơn 0!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtSoTinChi.Focus();
                    return;
                }

                MonHocDTO monHoc = new MonHocDTO
                {
                    MaMH = maMH,
                    TenMH = tenMH,
                    SoTinChi = soTinChi
                };

                bool ketQua = monHocBLL.Them(monHoc);

                if (ketQua)
                {
                    MessageBox.Show(
                        "Thêm môn học thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show(
                        "Thêm môn học không thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra khi thêm môn học:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẬP NHẬT MÔN HỌC
        // =========================================================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string maMH = txtMaMH.Text.Trim();
                string tenMH = txtTenMH.Text.Trim();

                if (string.IsNullOrWhiteSpace(maMH))
                {
                    MessageBox.Show(
                        "Vui lòng chọn môn học cần cập nhật!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(tenMH))
                {
                    MessageBox.Show(
                        "Tên môn học không được để trống!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTenMH.Focus();
                    return;
                }

                int soTinChi;

                if (!int.TryParse(
                    txtSoTinChi.Text.Trim(),
                    out soTinChi))
                {
                    MessageBox.Show(
                        "Số tín chỉ phải là số nguyên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtSoTinChi.Focus();
                    return;
                }

                if (soTinChi <= 0)
                {
                    MessageBox.Show(
                        "Số tín chỉ phải lớn hơn 0!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtSoTinChi.Focus();
                    return;
                }

                MonHocDTO monHoc = new MonHocDTO
                {
                    MaMH = maMH,
                    TenMH = tenMH,
                    SoTinChi = soTinChi
                };

                bool ketQua = monHocBLL.CapNhat(monHoc);

                if (ketQua)
                {
                    MessageBox.Show(
                        "Cập nhật môn học thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show(
                        "Cập nhật môn học không thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
        // XÓA MÔN HỌC
        // =========================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaMH.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn môn học cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa môn học '" +
                txtMaMH.Text.Trim() +
                "' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool ketQua =
                    monHocBLL.Xoa(txtMaMH.Text.Trim());

                if (ketQua)
                {
                    MessageBox.Show(
                        "Xóa môn học thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show(
                        "Xóa môn học không thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
            txtMaMH.ReadOnly = false;

            txtMaMH.Clear();
            txtTenMH.Clear();
            txtSoTinChi.Clear();
            txtTimKiem.Clear();

            dgvMonHoc.ClearSelection();

            txtMaMH.Focus();
        }

        // =========================================================
        // CLICK VÀO DÒNG TRONG DATAGRIDVIEW
        // =========================================================
        private void dgvMonHoc_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvMonHoc.Rows[e.RowIndex];

            if (row.Cells["MaMH"].Value != null)
            {
                txtMaMH.Text =
                    row.Cells["MaMH"].Value.ToString();
            }

            if (row.Cells["TenMH"].Value != null)
            {
                txtTenMH.Text =
                    row.Cells["TenMH"].Value.ToString();
            }

            if (row.Cells["SoTinChi"].Value != null)
            {
                txtSoTinChi.Text =
                    row.Cells["SoTinChi"].Value.ToString();
            }

            txtMaMH.ReadOnly = true;
        }

        // =========================================================
        // TÌM KIẾM
        // =========================================================
        private void txtTimKiem_TextChanged(
            object sender,
            EventArgs e)
        {
            if (danhSachMonHoc == null)
                return;

            string tuKhoa =
                txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                dgvMonHoc.DataSource =
                    danhSachMonHoc;

                return;
            }

            DataView view =
                danhSachMonHoc.DefaultView;

            string tuKhoaAnToan =
                tuKhoa.Replace("'", "''");

            view.RowFilter =
                "MaMH LIKE '%" +
                tuKhoaAnToan +
                "%' OR TenMH LIKE '%" +
                tuKhoaAnToan +
                "%'";

            dgvMonHoc.DataSource = view;
        }

        // =========================================================
        // CÁC EVENT CÓ THỂ ĐƯỢC DESIGNER TẠO
        // =========================================================
        private void lblTieuDe_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblMaMH_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblTenMH_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblSoTinChi_Click(
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