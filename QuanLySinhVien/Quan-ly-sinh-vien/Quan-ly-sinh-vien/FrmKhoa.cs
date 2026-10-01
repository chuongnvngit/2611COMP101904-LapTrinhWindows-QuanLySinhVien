using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmKhoa : Form
    {
        private readonly KhoaBLL khoaBLL = new KhoaBLL();

        private DataTable danhSachKhoa;

        public FrmKhoa()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================
        private void FrmKhoa_Load(object sender, EventArgs e)
        {
            HienThiDanhSach();
            LamMoi();
        }

        // =========================================================
        // HIỂN THỊ DANH SÁCH KHOA
        // =========================================================
        private void HienThiDanhSach()
        {
            try
            {
                danhSachKhoa = khoaBLL.LayDanhSach();

                MessageBox.Show(
                    "Đã lấy được " + danhSachKhoa.Rows.Count + " khoa từ database.",
                    "Kiểm tra dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dgvKhoa.DataSource = null;
                dgvKhoa.AutoGenerateColumns = true;
                dgvKhoa.DataSource = danhSachKhoa;

                dgvKhoa.ClearSelection();
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
        // THÊM KHOA
        // =========================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhoa = txtMaKhoa.Text.Trim();
                string tenKhoa = txtTenKhoa.Text.Trim();

                if (string.IsNullOrWhiteSpace(maKhoa))
                {
                    MessageBox.Show(
                        "Vui lòng nhập mã khoa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMaKhoa.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(tenKhoa))
                {
                    MessageBox.Show(
                        "Vui lòng nhập tên khoa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTenKhoa.Focus();
                    return;
                }

                KhoaDTO khoa = new KhoaDTO
                {
                    MaKhoa = maKhoa,
                    TenKhoa = tenKhoa
                };

                bool ketQua = khoaBLL.Them(khoa);

                if (ketQua)
                {
                    MessageBox.Show(
                        "Thêm khoa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show(
                        "Thêm khoa không thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra khi thêm khoa:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẬP NHẬT KHOA
        // =========================================================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                KhoaDTO khoa = new KhoaDTO
                {
                    MaKhoa = txtMaKhoa.Text.Trim(),
                    TenKhoa = txtTenKhoa.Text.Trim()
                };

                bool ketQua = khoaBLL.CapNhat(khoa);

                if (ketQua)
                {
                    MessageBox.Show(
                        "Cập nhật khoa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show(
                        "Cập nhật khoa không thành công!",
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
        // XÓA KHOA
        // =========================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoa.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn khoa cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khoa '" +
                txtMaKhoa.Text.Trim() +
                "' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool ketQua = khoaBLL.Xoa(txtMaKhoa.Text.Trim());

                if (ketQua)
                {
                    MessageBox.Show(
                        "Xóa khoa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiDanhSach();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show(
                        "Xóa khoa không thành công!",
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
            txtMaKhoa.ReadOnly = false;

            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtTimKiem.Clear();

            dgvKhoa.ClearSelection();

            txtMaKhoa.Focus();
        }

        // =========================================================
        // CLICK VÀO DÒNG TRONG DATAGRIDVIEW
        // =========================================================
        private void dgvKhoa_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvKhoa.Rows[e.RowIndex];

            if (row.Cells["MaKhoa"].Value != null)
            {
                txtMaKhoa.Text =
                    row.Cells["MaKhoa"].Value.ToString();
            }

            if (row.Cells["TenKhoa"].Value != null)
            {
                txtTenKhoa.Text =
                    row.Cells["TenKhoa"].Value.ToString();
            }

            // Không cho sửa mã khoa khi đang chọn dữ liệu
            txtMaKhoa.ReadOnly = true;
        }

        // =========================================================
        // TÌM KIẾM KHOA
        // =========================================================
        private void txtTimKiem_TextChanged(
            object sender,
            EventArgs e)
        {
            if (danhSachKhoa == null)
                return;

            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                dgvKhoa.DataSource = danhSachKhoa;
                return;
            }

            DataView view = danhSachKhoa.DefaultView;

            string tuKhoaAnToan =
                tuKhoa.Replace("'", "''");

            view.RowFilter =
                "MaKhoa LIKE '%" + tuKhoaAnToan + "%' " +
                "OR TenKhoa LIKE '%" + tuKhoaAnToan + "%'";

            dgvKhoa.DataSource = view;
        }

        // =========================================================
        // CÁC HÀM DO DESIGNER ĐÃ GẮN SỰ KIỆN
        // =========================================================

        // Designer đang gọi hàm này ở dòng 53
        private void lblMaKhoa_Click(object sender, EventArgs e)
        {
            // Không cần xử lý gì
        }

        // Designer đang gọi hàm này ở dòng 61
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Không cần xử lý gì
        }

        // Designer đang gọi hàm này ở dòng 146
        private void dgvKhoa_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // Không cần xử lý gì
        }
    }
}