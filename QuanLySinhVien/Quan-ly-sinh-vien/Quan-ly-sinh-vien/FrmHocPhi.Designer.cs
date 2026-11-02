namespace Quan_ly_sinh_vien
{
    partial class FrmHocPhi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.btnTinhHocPhi = new System.Windows.Forms.Button();
            this.btnCapNhatHocPhi = new System.Windows.Forms.Button();
            this.txtDongHocPhi = new System.Windows.Forms.TextBox();
            this.lblThongTin = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.lblNhapTien = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // dgvSinhVien
            this.dgvSinhVien.AllowUserToAddRows = false;
            this.dgvSinhVien.AllowUserToDeleteRows = false;
            this.dgvSinhVien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSinhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSinhVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Location = new System.Drawing.Point(30, 80);
            this.dgvSinhVien.MultiSelect = false;
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.ReadOnly = true;
            this.dgvSinhVien.RowHeadersWidth = 51;
            this.dgvSinhVien.RowTemplate.Height = 24;
            this.dgvSinhVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSinhVien.Size = new System.Drawing.Size(975, 263);
            this.dgvSinhVien.TabIndex = 0;

            // btnTinhHocPhi
            this.btnTinhHocPhi.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTinhHocPhi.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinhHocPhi.ForeColor = System.Drawing.Color.White;
            this.btnTinhHocPhi.Location = new System.Drawing.Point(390, 36);
            this.btnTinhHocPhi.Name = "btnTinhHocPhi";
            this.btnTinhHocPhi.Size = new System.Drawing.Size(150, 38);
            this.btnTinhHocPhi.TabIndex = 2;
            this.btnTinhHocPhi.Text = "Tính Học Phí";
            this.btnTinhHocPhi.UseVisualStyleBackColor = false;

            // btnCapNhatHocPhi
            this.btnCapNhatHocPhi.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCapNhatHocPhi.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatHocPhi.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatHocPhi.Location = new System.Drawing.Point(740, 36);
            this.btnCapNhatHocPhi.Name = "btnCapNhatHocPhi";
            this.btnCapNhatHocPhi.Size = new System.Drawing.Size(150, 38);
            this.btnCapNhatHocPhi.TabIndex = 4;
            this.btnCapNhatHocPhi.Text = "Cập Nhật Học Phí";
            this.btnCapNhatHocPhi.UseVisualStyleBackColor = false;

            // txtDongHocPhi
            this.txtDongHocPhi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDongHocPhi.Location = new System.Drawing.Point(570, 40);
            this.txtDongHocPhi.Name = "txtDongHocPhi";
            this.txtDongHocPhi.Size = new System.Drawing.Size(150, 30);
            this.txtDongHocPhi.TabIndex = 5;

            // lblNhapTien
            this.lblNhapTien.AutoSize = true;
            this.lblNhapTien.Location = new System.Drawing.Point(566, 18);
            this.lblNhapTien.Name = "lblNhapTien";
            this.lblNhapTien.Size = new System.Drawing.Size(130, 19);
            this.lblNhapTien.TabIndex = 6;
            this.lblNhapTien.Text = "Nhập số tiền đóng:";

            // lblThongTin
            this.lblThongTin.AutoSize = true;
            this.lblThongTin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTin.Location = new System.Drawing.Point(30, 95);
            this.lblThongTin.Name = "lblThongTin";
            this.lblThongTin.Size = new System.Drawing.Size(414, 23);
            this.lblThongTin.TabIndex = 3;
            this.lblThongTin.Text = "Chọn một sinh viên và bấm tính để xem chi tiết...";

            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Navy;
            this.lblTieuDe.Location = new System.Drawing.Point(368, 26);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(278, 32);
            this.lblTieuDe.TabIndex = 1;
            this.lblTieuDe.Text = "QUẢN LÝ HỌC PHÍ ";

            // lblDonGia
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(30, 45);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(276, 19);
            this.lblDonGia.TabIndex = 0;
            this.lblDonGia.Text = "Đơn giá/1 tín chỉ (VNĐ): 700000 VNĐ";

            // groupBox1
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblNhapTien);
            this.groupBox1.Controls.Add(this.txtDongHocPhi);
            this.groupBox1.Controls.Add(this.btnCapNhatHocPhi);
            this.groupBox1.Controls.Add(this.lblThongTin);
            this.groupBox1.Controls.Add(this.btnTinhHocPhi);
            this.groupBox1.Controls.Add(this.lblDonGia);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 388);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 232);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tra cứu & Cập nhật Học phí";

            // FrmHocPhi
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.dgvSinhVien);
            this.Name = "FrmHocPhi";
            this.Text = "Quản lý Học phí";
            this.Load += new System.EventHandler(this.FrmHocPhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.Button btnTinhHocPhi;
        private System.Windows.Forms.Button btnCapNhatHocPhi;
        private System.Windows.Forms.TextBox txtDongHocPhi;
        private System.Windows.Forms.Label lblThongTin;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Label lblNhapTien;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}