using System;
using System.Windows.Forms;
using Quan_ly_sinh_vien.DTO;

namespace Quan_ly_sinh_vien
{
    public partial class FrmMain : Form
    {
        private readonly TaiKhoanDTO taiKhoan;

        public FrmMain(TaiKhoanDTO taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
            HienThiThongTinTaiKhoan();
            PhanQuyen();
        }

        // =========================================================
        // HIỂN THỊ THÔNG TIN TÀI KHOẢN
        // =========================================================
        private void HienThiThongTinTaiKhoan()
        {
            if (taiKhoan == null)
            {
                this.Text = "QUẢN LÝ SINH VIÊN";
                lblTrangThai.Text = "Chưa đăng nhập";
                return;
            }

            this.Text = "QUẢN LÝ SINH VIÊN - " + taiKhoan.HoTen + " - " + taiKhoan.VaiTro;
            lblTrangThai.Text = "Người dùng: " + taiKhoan.HoTen + " | Vai trò: " + taiKhoan.VaiTro;
        }

        // =========================================================
        // PHÂN QUYỀN
        // =========================================================
        private void PhanQuyen()
        {
            if (taiKhoan == null)
                return;

            // Mặc định: ẩn toàn bộ chức năng quản lý và tạo tài khoản
            menuKhoa.Visible = false;
            menuLop.Visible = false;
            menuMonHoc.Visible = false;
            menuSinhVien.Visible = false;
            menuKetQua.Visible = false;
            menuKetQuaHocTap.Visible = false;
            menuTaoTaiKhoan.Visible = false;

            btnKhoa.Visible = false;
            btnLop.Visible = false;
            btnMonHoc.Visible = false;
            btnSinhVien.Visible = false;
            btnKetQua.Visible = false;
            btnTaoTaiKhoan.Visible = false;

            // Quyền chung cho cả Admin và Giáo vụ
            if (taiKhoan.VaiTro == "Admin" || taiKhoan.VaiTro == "GiaoVu")
            {
                menuKhoa.Visible = true;
                menuLop.Visible = true;
                menuMonHoc.Visible = true;
                menuSinhVien.Visible = true;
                menuKetQua.Visible = true;
                menuKetQuaHocTap.Visible = true;

                btnKhoa.Visible = true;
                btnLop.Visible = true;
                btnMonHoc.Visible = true;
                btnSinhVien.Visible = true;
                btnKetQua.Visible = true;

                if (taiKhoan.VaiTro == "Admin")
                {
                    menuTaoTaiKhoan.Visible = true;
                    btnTaoTaiKhoan.Visible = true;
                }
            }
            else if (taiKhoan.VaiTro == "GiangVien")
            {
                menuSinhVien.Visible = true;
                menuKetQua.Visible = true;
                menuKetQuaHocTap.Visible = true;

                btnSinhVien.Visible = true;
                btnKetQua.Visible = true;
            }
        }

        private void MoForm(Form form)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.GetType() == form.GetType())
                {
                    child.Activate();
                    form.Dispose();
                    return;
                }
            }
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================
        private void FrmMain_Load(object sender, EventArgs e)
        {
            HienThiThongTinTaiKhoan();
            PhanQuyen();
        }

        // =========================================================
        // ĐĂNG XUẤT VÀ THOÁT
        // =========================================================
        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (Form child in this.MdiChildren)
                {
                    child.Close();
                    child.Dispose();
                }
                this.Close();
            }
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // =========================================================
        // TRANG CHỦ
        // =========================================================

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có đang mở Form con nào không (Khoa, Lớp, Sinh viên...)
            if (this.MdiChildren.Length > 0)
            {
                // 2. Hiển thị hộp thoại cảnh báo người dùng
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đóng các cửa sổ hiện tại và quay về Trang chủ không?\n(Mọi dữ liệu chưa bấm Lưu sẽ bị mất)",
                    "Xác nhận chuyển trang",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // 3. Nếu người dùng đồng ý quay về
                if (result == DialogResult.Yes)
                {
                    // Duyệt qua tất cả các form con đang mở và đóng chúng lại
                    foreach (Form child in this.MdiChildren)
                    {
                        child.Close();
                        child.Dispose();
                    }
                }
            }
            else
            {
                // Nếu không có cửa sổ con nào đang mở, báo cho họ biết họ đã ở trang chủ
                MessageBox.Show("Bạn đang ở trang chủ.", "Trang chủ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // =========================================================
        // KHOA
        // =========================================================
        private void btnKhoa_Click(object sender, EventArgs e)
        {
            MoForm(new FrmKhoa());
        }

        private void menuKhoa_Click(object sender, EventArgs e)
        {
            MoForm(new FrmKhoa());
        }

        // =========================================================
        // LỚP
        // =========================================================
        private void btnLop_Click(object sender, EventArgs e)
        {
            MoForm(new FrmLop());
        }

        private void menuLop_Click(object sender, EventArgs e)
        {
            MoForm(new FrmLop());
        }

        // =========================================================
        // MÔN HỌC
        // =========================================================
        private void btnMonHoc_Click(object sender, EventArgs e)
        {
            MoForm(new FrmMonHoc());
        }

        private void menuMonHoc_Click(object sender, EventArgs e)
        {
            MoForm(new FrmMonHoc());
        }

        // =========================================================
        // SINH VIÊN
        // =========================================================
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            MoForm(new FrmSinhVien());
        }

        private void menuSinhVien_Click(object sender, EventArgs e)
        {
            MoForm(new FrmSinhVien());
        }

        // =========================================================
        // KẾT QUẢ
        // =========================================================
        private void btnKetQua_Click(object sender, EventArgs e)
        {
            MoForm(new FrmKetQua());
        }

        private void menuKetQua_Click(object sender, EventArgs e)
        {
            // Không cần xử lý gì ở menu cha Kết quả
        }

        private void menuKetQuaHocTap_Click(object sender, EventArgs e)
        {
            MoForm(new FrmKetQua());
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void menuTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            FrmTaoTaiKhoan frm = new FrmTaoTaiKhoan();
            frm.ShowDialog(); 
        }

        private void btnHocPhi_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem Form Học Phí đã mở chưa, nếu mở rồi thì focus vào nó
            foreach (Form childForm in this.MdiChildren)
            {
                if (childForm is FrmHocPhi)
                {
                    childForm.Activate();
                    return;
                }
            }

            // 2. Tắt các Form khác đang mở (dọn dẹp giao diện)
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }

            // 3. Mở Form Học phí
            FrmHocPhi frm = new FrmHocPhi();
            frm.MdiParent = this; // Hiển thị bên trong Form Main
            frm.Dock = DockStyle.Fill; // Phóng to lấp đầy khoảng trống
            frm.Show();
        }
    }
}