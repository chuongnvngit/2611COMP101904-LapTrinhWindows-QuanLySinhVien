namespace Quan_ly_sinh_vien
{
    partial class FrmKhoa
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
            this.lblMaKhoa = new System.Windows.Forms.Label();
            this.txtMaKhoa = new System.Windows.Forms.TextBox();
            this.lblTenKhoa = new System.Windows.Forms.Label();
            this.txtTenKhoa = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvKhoa = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaKhoa
            // 
            this.lblMaKhoa.AutoSize = true;
            this.lblMaKhoa.Location = new System.Drawing.Point(41, 51);
            this.lblMaKhoa.Name = "lblMaKhoa";
            this.lblMaKhoa.Size = new System.Drawing.Size(59, 16);
            this.lblMaKhoa.TabIndex = 0;
            this.lblMaKhoa.Text = "Mã khoa";
            this.lblMaKhoa.Click += new System.EventHandler(this.lblMaKhoa_Click);
            // 
            // txtMaKhoa
            // 
            this.txtMaKhoa.Location = new System.Drawing.Point(120, 45);
            this.txtMaKhoa.Name = "txtMaKhoa";
            this.txtMaKhoa.Size = new System.Drawing.Size(196, 22);
            this.txtMaKhoa.TabIndex = 1;
            this.txtMaKhoa.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblTenKhoa
            // 
            this.lblTenKhoa.AutoSize = true;
            this.lblTenKhoa.Location = new System.Drawing.Point(41, 115);
            this.lblTenKhoa.Name = "lblTenKhoa";
            this.lblTenKhoa.Size = new System.Drawing.Size(64, 16);
            this.lblTenKhoa.TabIndex = 2;
            this.lblTenKhoa.Text = "Tên khoa";
            // 
            // txtTenKhoa
            // 
            this.txtTenKhoa.Location = new System.Drawing.Point(120, 109);
            this.txtTenKhoa.Name = "txtTenKhoa";
            this.txtTenKhoa.Size = new System.Drawing.Size(196, 22);
            this.txtTenKhoa.TabIndex = 3;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(30, 188);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Location = new System.Drawing.Point(131, 188);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(83, 23);
            this.btnCapNhat.TabIndex = 5;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(231, 188);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(332, 188);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 23);
            this.btnLamMoi.TabIndex = 7;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(41, 250);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(62, 16);
            this.lblTimKiem.TabIndex = 8;
            this.lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(120, 247);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(196, 22);
            this.txtTimKiem.TabIndex = 9;
            // 
            // dgvKhoa
            // 
            this.dgvKhoa.AllowUserToAddRows = false;
            this.dgvKhoa.AllowUserToDeleteRows = false;
            this.dgvKhoa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhoa.Location = new System.Drawing.Point(30, 293);
            this.dgvKhoa.MultiSelect = false;
            this.dgvKhoa.Name = "dgvKhoa";
            this.dgvKhoa.ReadOnly = true;
            this.dgvKhoa.RowHeadersWidth = 51;
            this.dgvKhoa.RowTemplate.Height = 24;
            this.dgvKhoa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhoa.Size = new System.Drawing.Size(372, 150);
            this.dgvKhoa.TabIndex = 10;
            this.dgvKhoa.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhoa_CellContentClick);
            // 
            // FrmKhoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 503);
            this.Controls.Add(this.dgvKhoa);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtTenKhoa);
            this.Controls.Add(this.lblTenKhoa);
            this.Controls.Add(this.txtMaKhoa);
            this.Controls.Add(this.lblMaKhoa);
            this.Name = "FrmKhoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ KHOA";
            this.Load += new System.EventHandler(this.FrmKhoa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaKhoa;
        private System.Windows.Forms.TextBox txtMaKhoa;
        private System.Windows.Forms.Label lblTenKhoa;
        private System.Windows.Forms.TextBox txtTenKhoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvKhoa;
    }
}