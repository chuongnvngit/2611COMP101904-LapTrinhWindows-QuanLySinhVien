namespace Quan_ly_sinh_vien
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMonHoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSinhVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKetQua = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKetQuaHocTap = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblTrangThai = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnTrangChu = new System.Windows.Forms.ToolStripButton();
            this.btnSinhVien = new System.Windows.Forms.ToolStripButton();
            this.btnKhoa = new System.Windows.Forms.ToolStripButton();
            this.btnLop = new System.Windows.Forms.ToolStripButton();
            this.btnMonHoc = new System.Windows.Forms.ToolStripButton();
            this.btnKetQua = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHeThong,
            this.menuDanhMuc,
            this.menuQuanLy,
            this.menuKetQua});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuHeThong
            // 
            this.menuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDangXuat,
            this.menuThoat});
            this.menuHeThong.Name = "menuHeThong";
            this.menuHeThong.Size = new System.Drawing.Size(85, 26);
            this.menuHeThong.Text = "Hệ thống";
            // 
            // menuDangXuat
            // 
            this.menuDangXuat.Name = "menuDangXuat";
            this.menuDangXuat.Size = new System.Drawing.Size(160, 26);
            this.menuDangXuat.Text = "Đăng xuất";
            this.menuDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            // 
            // menuThoat
            // 
            this.menuThoat.Name = "menuThoat";
            this.menuThoat.Size = new System.Drawing.Size(160, 26);
            this.menuThoat.Text = "Thoát";
            this.menuThoat.Click += new System.EventHandler(this.menuThoat_Click);
            // 
            // menuDanhMuc
            // 
            this.menuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuKhoa,
            this.menuLop,
            this.menuMonHoc});
            this.menuDanhMuc.Name = "menuDanhMuc";
            this.menuDanhMuc.Size = new System.Drawing.Size(94, 26);
            this.menuDanhMuc.Text = "Danh mục ";
            // 
            // menuKhoa
            // 
            this.menuKhoa.Name = "menuKhoa";
            this.menuKhoa.Size = new System.Drawing.Size(150, 26);
            this.menuKhoa.Text = "Khoa";
            this.menuKhoa.Click += new System.EventHandler(this.menuKhoa_Click);
            // 
            // menuLop
            // 
            this.menuLop.Name = "menuLop";
            this.menuLop.Size = new System.Drawing.Size(150, 26);
            this.menuLop.Text = "Lớp";
            this.menuLop.Click += new System.EventHandler(this.menuLop_Click);
            // 
            // menuMonHoc
            // 
            this.menuMonHoc.Name = "menuMonHoc";
            this.menuMonHoc.Size = new System.Drawing.Size(150, 26);
            this.menuMonHoc.Text = "Môn học";
            this.menuMonHoc.Click += new System.EventHandler(this.menuMonHoc_Click);
            // 
            // menuQuanLy
            // 
            this.menuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSinhVien});
            this.menuQuanLy.Name = "menuQuanLy";
            this.menuQuanLy.Size = new System.Drawing.Size(73, 26);
            this.menuQuanLy.Text = "Quản lý";
            // 
            // menuSinhVien
            // 
            this.menuSinhVien.Name = "menuSinhVien";
            this.menuSinhVien.Size = new System.Drawing.Size(155, 26);
            this.menuSinhVien.Text = "Sinh viên ";
            this.menuSinhVien.Click += new System.EventHandler(this.menuSinhVien_Click);
            // 
            // menuKetQua
            // 
            this.menuKetQua.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuKetQuaHocTap});
            this.menuKetQua.Name = "menuKetQua";
            this.menuKetQua.Size = new System.Drawing.Size(74, 26);
            this.menuKetQua.Text = "Kết quả";
            this.menuKetQua.Click += new System.EventHandler(this.menuKetQua_Click);
            // 
            // menuKetQuaHocTap
            // 
            this.menuKetQuaHocTap.Name = "menuKetQuaHocTap";
            this.menuKetQuaHocTap.Size = new System.Drawing.Size(197, 26);
            this.menuKetQuaHocTap.Text = "Kết quả học tập";
            this.menuKetQuaHocTap.Click += new System.EventHandler(this.menuKetQuaHocTap_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTrangThai});
            this.statusStripMain.Location = new System.Drawing.Point(0, 424);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(800, 26);
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(118, 20);
            this.lblTrangThai.Text = "Chưa đăng nhập";
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTrangChu,
            this.btnSinhVien,
            this.btnKhoa,
            this.btnLop,
            this.btnMonHoc,
            this.btnKetQua});
            this.toolStripMain.Location = new System.Drawing.Point(0, 28);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(800, 27);
            this.toolStripMain.TabIndex = 4;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTrangChu.Image = ((System.Drawing.Image)(resources.GetObject("btnTrangChu.Image")));
            this.btnTrangChu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(29, 24);
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // btnSinhVien
            // 
            this.btnSinhVien.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSinhVien.Image = ((System.Drawing.Image)(resources.GetObject("btnSinhVien.Image")));
            this.btnSinhVien.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSinhVien.Name = "btnSinhVien";
            this.btnSinhVien.Size = new System.Drawing.Size(29, 28);
            this.btnSinhVien.Text = "Sinh viên ";
            this.btnSinhVien.Click += new System.EventHandler(this.btnSinhVien_Click);
            // 
            // btnKhoa
            // 
            this.btnKhoa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnKhoa.Image = ((System.Drawing.Image)(resources.GetObject("btnKhoa.Image")));
            this.btnKhoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKhoa.Name = "btnKhoa";
            this.btnKhoa.Size = new System.Drawing.Size(29, 28);
            this.btnKhoa.Text = "Khoa";
            this.btnKhoa.Click += new System.EventHandler(this.btnKhoa_Click);
            // 
            // btnLop
            // 
            this.btnLop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLop.Image = ((System.Drawing.Image)(resources.GetObject("btnLop.Image")));
            this.btnLop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLop.Name = "btnLop";
            this.btnLop.Size = new System.Drawing.Size(29, 28);
            this.btnLop.Text = "Lớp";
            this.btnLop.Click += new System.EventHandler(this.btnLop_Click);
            // 
            // btnMonHoc
            // 
            this.btnMonHoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMonHoc.Image = ((System.Drawing.Image)(resources.GetObject("btnMonHoc.Image")));
            this.btnMonHoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMonHoc.Name = "btnMonHoc";
            this.btnMonHoc.Size = new System.Drawing.Size(29, 28);
            this.btnMonHoc.Text = "Môn học";
            this.btnMonHoc.Click += new System.EventHandler(this.btnMonHoc_Click);
            // 
            // btnKetQua
            // 
            this.btnKetQua.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnKetQua.Image = ((System.Drawing.Image)(resources.GetObject("btnKetQua.Image")));
            this.btnKetQua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKetQua.Name = "btnKetQua";
            this.btnKetQua.Size = new System.Drawing.Size(29, 28);
            this.btnKetQua.Text = "Kết quả";
            this.btnKetQua.Click += new System.EventHandler(this.btnKetQua_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ SINH VIÊN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private System.Windows.Forms.ToolStripMenuItem menuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem menuKetQua;
        private System.Windows.Forms.ToolStripMenuItem menuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem menuThoat;
        private System.Windows.Forms.ToolStripMenuItem menuKhoa;
        private System.Windows.Forms.ToolStripMenuItem menuLop;
        private System.Windows.Forms.ToolStripMenuItem menuMonHoc;
        private System.Windows.Forms.ToolStripMenuItem menuSinhVien;
        private System.Windows.Forms.ToolStripMenuItem menuKetQuaHocTap;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThai;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnTrangChu;
        private System.Windows.Forms.ToolStripButton btnSinhVien;
        private System.Windows.Forms.ToolStripButton btnKhoa;
        private System.Windows.Forms.ToolStripButton btnLop;
        private System.Windows.Forms.ToolStripButton btnMonHoc;
        private System.Windows.Forms.ToolStripButton btnKetQua;
    }
}