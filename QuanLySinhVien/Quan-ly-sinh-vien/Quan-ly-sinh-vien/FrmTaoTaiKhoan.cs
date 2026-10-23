using System;
using System.Data;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmTaoTaiKhoan : Form
    {
        private readonly TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

        public FrmTaoTaiKhoan()
        {
            InitializeComponent();
        }

        private void FrmTaoTaiKhoan_Load(object sender, EventArgs e)
        {
            // Thiết lập các mục cho ComboBox Vai trò
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.Add("Admin");
            cboVaiTro.Items.Add("GiaoVu");
            cboVaiTro.Items.Add("GiangVien");
            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList; // Ngăn người dùng gõ tay
            cboVaiTro.SelectedIndex = -1;

            LamMoi();
        }

        // =========================================================
        // LÀM MỚI FORM (XÓA TRẮNG)
        // =========================================================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            cboVaiTro.SelectedIndex = -1;

            txtTenDangNhap.Focus();
        }

        // =========================================================
        // NÚT LƯU (TẠO TÀI KHOẢN MỚI)
        // =========================================================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    MatKhau = txtMatKhau.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    VaiTro = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : ""
                };

                bool ketQua = taiKhoanBLL.TaoTaiKhoan(tk);

                if (ketQua)
                {
                    MessageBox.Show("Tạo tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại trong hệ thống. Vui lòng chọn tên khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    txtTenDangNhap.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi tạo tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (ex.Message.Contains("Mật khẩu"))
                {
                    txtMatKhau.Focus();
                }
            }
        }
    }
}