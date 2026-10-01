using System;
using System.Windows.Forms;
using Quan_ly_sinh_vien.BLL;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmDangNhap : Form
    {
        private readonly TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

        public FrmDangNhap()
        {
            InitializeComponent();
        }

        // =====================================================
        // NÚT ĐĂNG NHẬP
        // =====================================================
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            // Kiểm tra tên đăng nhập
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtTenDangNhap.Focus();
                return;
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtMatKhau.Focus();
                return;
            }

            try
            {
                // Gọi BLL để kiểm tra tài khoản
                TaiKhoanDTO taiKhoan = taiKhoanBLL.DangNhap(
                    tenDangNhap,
                    matKhau
                );

                // Kiểm tra kết quả
                if (taiKhoan == null)
                {
                    MessageBox.Show(
                        "Tên đăng nhập hoặc mật khẩu không chính xác!",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // Thông báo đăng nhập thành công
                MessageBox.Show(
                    "Đăng nhập thành công!\nXin chào " + taiKhoan.HoTen,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Tạo FrmMain
                FrmMain frmMain = new FrmMain(taiKhoan);

                // Khi FrmMain đóng:
                // quay trở lại FrmDangNhap
                frmMain.FormClosed += FrmMain_FormClosed;

                // Ẩn FrmDangNhap
                this.Hide();

                // Hiển thị FrmMain
                frmMain.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Đăng nhập thất bại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // KHI FRMMAIN ĐÓNG
        // =====================================================
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Xóa dữ liệu đăng nhập cũ
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();

            // Hiện lại FrmDangNhap
            this.Show();

            // Đưa con trỏ về ô tên đăng nhập
            txtTenDangNhap.Focus();
        }

        // =====================================================
        // NÚT THOÁT TRÊN FORM ĐĂNG NHẬP
        // =====================================================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",
                "Thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // =====================================================
        // CÁC EVENT CŨ CỦA DESIGNER
        // Giữ lại để tránh lỗi Designer
        // =====================================================

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
        }
    }
}