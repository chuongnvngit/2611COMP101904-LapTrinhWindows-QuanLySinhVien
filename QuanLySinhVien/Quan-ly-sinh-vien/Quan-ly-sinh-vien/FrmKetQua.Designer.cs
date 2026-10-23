namespace Quan_ly_sinh_vien
{
    partial class FrmKetQua
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLayDanhSach = new System.Windows.Forms.Button();
            this.cboMonHoc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnXemDiemCaNhan = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuuDiem = new System.Windows.Forms.Button();
            this.txtDiem = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaMH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.lblThongTinGPA = new System.Windows.Forms.Label();
            // Khởi tạo các đối tượng in ấn
            this.btnInBangDiem = new System.Windows.Forms.Button();
            this.pdInBangDiem = new System.Drawing.Printing.PrintDocument();
            this.ppdInBangDiem = new System.Windows.Forms.PrintPreviewDialog();

            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLayDanhSach);
            this.groupBox1.Controls.Add(this.cboMonHoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboLop);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn danh sách cần nhập điểm";
            // 
            // btnLayDanhSach
            // 
            this.btnLayDanhSach.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLayDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnLayDanhSach.Location = new System.Drawing.Point(680, 36);
            this.btnLayDanhSach.Name = "btnLayDanhSach";
            this.btnLayDanhSach.Size = new System.Drawing.Size(180, 40);
            this.btnLayDanhSach.TabIndex = 4;
            this.btnLayDanhSach.Text = "Hiển thị danh sách SV";
            this.btnLayDanhSach.UseVisualStyleBackColor = false;
            this.btnLayDanhSach.Click += new System.EventHandler(this.btnLayDanhSach_Click);
            // 
            // cboMonHoc
            // 
            this.cboMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonHoc.FormattingEnabled = true;
            this.cboMonHoc.Location = new System.Drawing.Point(400, 43);
            this.cboMonHoc.Name = "cboMonHoc";
            this.cboMonHoc.Size = new System.Drawing.Size(250, 27);
            this.cboMonHoc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Môn học:";
            // 
            // cboLop
            // 
            this.cboLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLop.FormattingEnabled = true;
            this.cboLop.Location = new System.Drawing.Point(90, 43);
            this.cboLop.Name = "cboLop";
            this.cboLop.Size = new System.Drawing.Size(200, 27);
            this.cboLop.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lớp:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnInBangDiem);
            this.groupBox2.Controls.Add(this.btnXemDiemCaNhan);
            this.groupBox2.Controls.Add(this.btnLamMoi);
            this.groupBox2.Controls.Add(this.btnLuuDiem);
            this.groupBox2.Controls.Add(this.txtDiem);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtMaMH);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtHoTen);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMaSV);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(30, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cập nhật điểm";
            // 
            // btnXemDiemCaNhan
            // 
            this.btnXemDiemCaNhan.BackColor = System.Drawing.Color.MediumPurple;
            this.btnXemDiemCaNhan.ForeColor = System.Drawing.Color.White;
            this.btnXemDiemCaNhan.Location = new System.Drawing.Point(320, 100);
            this.btnXemDiemCaNhan.Name = "btnXemDiemCaNhan";
            this.btnXemDiemCaNhan.Size = new System.Drawing.Size(190, 40);
            this.btnXemDiemCaNhan.TabIndex = 10;
            this.btnXemDiemCaNhan.Text = "Xem điểm TB (GPA)";
            this.btnXemDiemCaNhan.UseVisualStyleBackColor = false;
            this.btnXemDiemCaNhan.Click += new System.EventHandler(this.btnXemDiemCaNhan_Click);
            // 
            // btnInBangDiem
            // 
            this.btnInBangDiem.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnInBangDiem.ForeColor = System.Drawing.Color.White;
            this.btnInBangDiem.Location = new System.Drawing.Point(530, 100);
            this.btnInBangDiem.Name = "btnInBangDiem";
            this.btnInBangDiem.Size = new System.Drawing.Size(150, 40);
            this.btnInBangDiem.TabIndex = 11;
            this.btnInBangDiem.Text = "In Bảng Điểm";
            this.btnInBangDiem.UseVisualStyleBackColor = false;
            this.btnInBangDiem.Click += new System.EventHandler(this.btnInBangDiem_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Gray;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(700, 100);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 40);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuuDiem
            // 
            this.btnLuuDiem.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLuuDiem.ForeColor = System.Drawing.Color.White;
            this.btnLuuDiem.Location = new System.Drawing.Point(180, 100);
            this.btnLuuDiem.Name = "btnLuuDiem";
            this.btnLuuDiem.Size = new System.Drawing.Size(120, 40);
            this.btnLuuDiem.TabIndex = 8;
            this.btnLuuDiem.Text = "Lưu điểm";
            this.btnLuuDiem.UseVisualStyleBackColor = false;
            this.btnLuuDiem.Click += new System.EventHandler(this.btnLuuDiem_Click);
            // 
            // txtDiem
            // 
            this.txtDiem.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiem.Location = new System.Drawing.Point(740, 43);
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.Size = new System.Drawing.Size(120, 27);
            this.txtDiem.TabIndex = 7;
            this.txtDiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(670, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Điểm:";
            // 
            // txtMaMH
            // 
            this.txtMaMH.Location = new System.Drawing.Point(530, 43);
            this.txtMaMH.Name = "txtMaMH";
            this.txtMaMH.ReadOnly = true;
            this.txtMaMH.Size = new System.Drawing.Size(120, 27);
            this.txtMaMH.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mã MH:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(260, 43);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.ReadOnly = true;
            this.txtHoTen.Size = new System.Drawing.Size(180, 27);
            this.txtHoTen.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Họ tên:";
            // 
            // txtMaSV
            // 
            this.txtMaSV.Location = new System.Drawing.Point(90, 43);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.Size = new System.Drawing.Size(90, 27);
            this.txtMaSV.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mã SV:";
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(30, 390);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.RowHeadersWidth = 51;
            this.dgvKetQua.RowTemplate.Height = 24;
            this.dgvKetQua.Size = new System.Drawing.Size(920, 230);
            this.dgvKetQua.TabIndex = 2;
            this.dgvKetQua.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKetQua_CellClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label7.Location = new System.Drawing.Point(315, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(350, 32);
            this.label7.TabIndex = 3;
            this.label7.Text = "QUẢN LÝ ĐIỂM HỌC TẬP";
            // 
            // lblThongTinGPA
            // 
            this.lblThongTinGPA.AutoSize = true;
            this.lblThongTinGPA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinGPA.ForeColor = System.Drawing.Color.Firebrick;
            this.lblThongTinGPA.Location = new System.Drawing.Point(30, 355);
            this.lblThongTinGPA.Name = "lblThongTinGPA";
            this.lblThongTinGPA.Size = new System.Drawing.Size(262, 23);
            this.lblThongTinGPA.TabIndex = 4;
            this.lblThongTinGPA.Text = "Thông tin GPA sẽ hiển thị ở đây";
            this.lblThongTinGPA.Visible = false;
            // 
            // pdInBangDiem
            // 
            this.pdInBangDiem.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pdInBangDiem_PrintPage);
            // 
            // ppdInBangDiem
            // 
            this.ppdInBangDiem.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppdInBangDiem.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppdInBangDiem.ClientSize = new System.Drawing.Size(400, 300);
            this.ppdInBangDiem.Enabled = true;
            this.ppdInBangDiem.Name = "ppdInBangDiem";
            this.ppdInBangDiem.Visible = false;
            // 
            // FrmKetQua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.lblThongTinGPA);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmKetQua";
            this.Text = "Quản lý điểm số sinh viên";
            this.Load += new System.EventHandler(this.FrmKetQua_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboMonHoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLayDanhSach;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnLuuDiem;
        private System.Windows.Forms.TextBox txtDiem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMaMH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnXemDiemCaNhan;
        private System.Windows.Forms.Label lblThongTinGPA;
        private System.Windows.Forms.Button btnInBangDiem;
        private System.Drawing.Printing.PrintDocument pdInBangDiem;
        private System.Windows.Forms.PrintPreviewDialog ppdInBangDiem;
    }
}