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

            this.Text =
                "QUẢN LÝ SINH VIÊN - "
                + taiKhoan.HoTen
                + " - "
                + taiKhoan.VaiTro;

            lblTrangThai.Text =
                "Người dùng: "
                + taiKhoan.HoTen
                + " | Vai trò: "
                + taiKhoan.VaiTro;
        }

        // =========================================================
        // PHÂN QUYỀN
        // =========================================================
        private void PhanQuyen()
        {
            if (taiKhoan == null)
                return;

            // Mặc định: ẩn các chức năng quản lý
            menuKhoa.Visible = false;
            menuLop.Visible = false;
            menuMonHoc.Visible = false;
            menuSinhVien.Visible = false;
            menuKetQua.Visible = false;
            menuKetQuaHocTap.Visible = false;

            btnKhoa.Visible = false;
            btnLop.Visible = false;
            btnMonHoc.Visible = false;
            btnSinhVien.Visible = false;
            btnKetQua.Visible = false;

            // =========================
            // ADMIN
            // =========================
            if (taiKhoan.VaiTro == "Admin")
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
            }

            // =========================
            // GIÁO VỤ
            // =========================
            else if (taiKhoan.VaiTro == "GiaoVu")
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
            }

            // =========================
            // GIẢNG VIÊN
            // =========================
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
        // ĐĂNG XUẤT
        // =========================================================
        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // =========================================================
        // THOÁT CHƯƠNG TRÌNH
        // =========================================================
        private void menuThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",
                "Thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

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
            MessageBox.Show(
                "Bạn đang ở trang chủ.",
                "Trang chủ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
            MessageBox.Show(
                "Chức năng Quản lý Môn học đang được xây dựng.",
                "Quản lý Môn học",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void menuMonHoc_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Quản lý Môn học đang được xây dựng.",
                "Quản lý Môn học",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =========================================================
        // SINH VIÊN
        // =========================================================
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Quản lý Sinh viên đang được xây dựng.",
                "Quản lý Sinh viên",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void menuSinhVien_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Quản lý Sinh viên đang được xây dựng.",
                "Quản lý Sinh viên",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =========================================================
        // KẾT QUẢ
        // =========================================================
        private void btnKetQua_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Kết quả học tập đang được xây dựng.",
                "Kết quả học tập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void menuKetQua_Click(object sender, EventArgs e)
        {
            // Không cần xử lý gì ở menu cha Kết quả
        }

        private void menuKetQuaHocTap_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Kết quả học tập đang được xây dựng.",
                "Kết quả học tập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}