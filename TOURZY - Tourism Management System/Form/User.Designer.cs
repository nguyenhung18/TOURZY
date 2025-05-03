namespace TOURZY___Tourism_Management_System
{
    partial class User
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User));
            this.label1 = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.btn_TimChuyenDi = new System.Windows.Forms.Button();
            this.btn_ChiTiet = new System.Windows.Forms.Button();
            this.btn_TaoChuyenMoi = new System.Windows.Forms.Button();
            this.btn_DanhGia = new System.Windows.Forms.Button();
            this.btn_DatChuyenDi = new System.Windows.Forms.Button();
            this.btn_DangXuat = new System.Windows.Forms.Button();
            this.btn_ThanhToan = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pbAva = new SATAUiFramework.Controls.SATAPictureBox();
            this.btn_X = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chuyenDi1 = new TOURZY___Tourism_Management_System.ChuyenDi();
            this.chiTietChuyenDi1 = new TOURZY___Tourism_Management_System.ChiTietChuyenDi();
            this.datChuyenDi1 = new TOURZY___Tourism_Management_System.DatChuyenDi();
            this.taoChuyenDiMoi1 = new TOURZY___Tourism_Management_System.TaoChuyenDiMoi();
            this.danhGiaChuyenDi1 = new TOURZY___Tourism_Management_System.DanhGiaChuyenDi();
            this.thanhToan1 = new TOURZY___Tourism_Management_System.ThanhToan();
            this.datChuyenDi2 = new TOURZY___Tourism_Management_System.DatChuyenDi();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAva)).BeginInit();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(148)))), ((int)(((byte)(223)))));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(554, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "TOURZY - Toursim Management System | User\'s Portal";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTen.Location = new System.Drawing.Point(171, 211);
            this.lblTen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(94, 33);
            this.lblTen.TabIndex = 8;
            this.lblTen.Text = " (Tên )";
            // 
            // btn_TimChuyenDi
            // 
            this.btn_TimChuyenDi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.btn_TimChuyenDi.FlatAppearance.BorderSize = 0;
            this.btn_TimChuyenDi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TimChuyenDi.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TimChuyenDi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_TimChuyenDi.Location = new System.Drawing.Point(-1, 275);
            this.btn_TimChuyenDi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_TimChuyenDi.Name = "btn_TimChuyenDi";
            this.btn_TimChuyenDi.Size = new System.Drawing.Size(324, 62);
            this.btn_TimChuyenDi.TabIndex = 12;
            this.btn_TimChuyenDi.Text = "Tìm chuyến đi ";
            this.btn_TimChuyenDi.UseVisualStyleBackColor = false;
            this.btn_TimChuyenDi.Click += new System.EventHandler(this.btn_TimChuyenDi_Click);
            // 
            // btn_ChiTiet
            // 
            this.btn_ChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.btn_ChiTiet.FlatAppearance.BorderSize = 0;
            this.btn_ChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ChiTiet.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ChiTiet.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_ChiTiet.Location = new System.Drawing.Point(0, 348);
            this.btn_ChiTiet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ChiTiet.Name = "btn_ChiTiet";
            this.btn_ChiTiet.Size = new System.Drawing.Size(323, 62);
            this.btn_ChiTiet.TabIndex = 13;
            this.btn_ChiTiet.Text = "Chi tiết chuyến đi";
            this.btn_ChiTiet.UseVisualStyleBackColor = false;
            this.btn_ChiTiet.Click += new System.EventHandler(this.btn_ChiTiet_Click);
            // 
            // btn_TaoChuyenMoi
            // 
            this.btn_TaoChuyenMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.btn_TaoChuyenMoi.FlatAppearance.BorderSize = 0;
            this.btn_TaoChuyenMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TaoChuyenMoi.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TaoChuyenMoi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_TaoChuyenMoi.Location = new System.Drawing.Point(0, 482);
            this.btn_TaoChuyenMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_TaoChuyenMoi.Name = "btn_TaoChuyenMoi";
            this.btn_TaoChuyenMoi.Size = new System.Drawing.Size(323, 62);
            this.btn_TaoChuyenMoi.TabIndex = 14;
            this.btn_TaoChuyenMoi.Text = "Tạo chuyến đi mới";
            this.btn_TaoChuyenMoi.UseVisualStyleBackColor = false;
            this.btn_TaoChuyenMoi.Click += new System.EventHandler(this.btn_TaoChuyenMoi_Click);
            // 
            // btn_DanhGia
            // 
            this.btn_DanhGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.btn_DanhGia.FlatAppearance.BorderSize = 0;
            this.btn_DanhGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DanhGia.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DanhGia.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DanhGia.Location = new System.Drawing.Point(-1, 574);
            this.btn_DanhGia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_DanhGia.Name = "btn_DanhGia";
            this.btn_DanhGia.Size = new System.Drawing.Size(324, 65);
            this.btn_DanhGia.TabIndex = 15;
            this.btn_DanhGia.Text = "Đánh giá chuyến đi";
            this.btn_DanhGia.UseVisualStyleBackColor = false;
            this.btn_DanhGia.Click += new System.EventHandler(this.btn_DanhGia_Click);
            // 
            // btn_DatChuyenDi
            // 
            this.btn_DatChuyenDi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.btn_DatChuyenDi.FlatAppearance.BorderSize = 0;
            this.btn_DatChuyenDi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DatChuyenDi.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DatChuyenDi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DatChuyenDi.Location = new System.Drawing.Point(-1, 421);
            this.btn_DatChuyenDi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_DatChuyenDi.Name = "btn_DatChuyenDi";
            this.btn_DatChuyenDi.Size = new System.Drawing.Size(324, 62);
            this.btn_DatChuyenDi.TabIndex = 16;
            this.btn_DatChuyenDi.Text = "Đặt chuyến đi";
            this.btn_DatChuyenDi.UseVisualStyleBackColor = false;
            this.btn_DatChuyenDi.Click += new System.EventHandler(this.btn_DatChuyenDi_Click);
            // 
            // btn_DangXuat
            // 
            this.btn_DangXuat.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btn_DangXuat.FlatAppearance.BorderSize = 0;
            this.btn_DangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DangXuat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangXuat.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btn_DangXuat.Location = new System.Drawing.Point(-1, 858);
            this.btn_DangXuat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_DangXuat.Name = "btn_DangXuat";
            this.btn_DangXuat.Size = new System.Drawing.Size(324, 58);
            this.btn_DangXuat.TabIndex = 18;
            this.btn_DangXuat.Text = "Đăng xuất";
            this.btn_DangXuat.UseVisualStyleBackColor = false;
            this.btn_DangXuat.Click += new System.EventHandler(this.btn_DangXuat_Click_1);
            // 
            // btn_ThanhToan
            // 
            this.btn_ThanhToan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.btn_ThanhToan.FlatAppearance.BorderSize = 0;
            this.btn_ThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ThanhToan.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ThanhToan.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_ThanhToan.Location = new System.Drawing.Point(-1, 668);
            this.btn_ThanhToan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ThanhToan.Name = "btn_ThanhToan";
            this.btn_ThanhToan.Size = new System.Drawing.Size(324, 69);
            this.btn_ThanhToan.TabIndex = 19;
            this.btn_ThanhToan.Text = "Thanh toán";
            this.btn_ThanhToan.UseVisualStyleBackColor = false;
            this.btn_ThanhToan.Click += new System.EventHandler(this.btn_ThanhToan_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(170)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btn_ThanhToan);
            this.panel2.Controls.Add(this.btn_DangXuat);
            this.panel2.Controls.Add(this.btn_DatChuyenDi);
            this.panel2.Controls.Add(this.btn_DanhGia);
            this.panel2.Controls.Add(this.btn_TaoChuyenMoi);
            this.panel2.Controls.Add(this.btn_ChiTiet);
            this.panel2.Controls.Add(this.btn_TimChuyenDi);
            this.panel2.Controls.Add(this.pbAva);
            this.panel2.Controls.Add(this.lblTen);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 1044);
            this.panel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(25, 211);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 33);
            this.label2.TabIndex = 20;
            this.label2.Text = " Xin Chào, ";
            // 
            // pbAva
            // 
            this.pbAva.BackColor = System.Drawing.Color.White;
            this.pbAva.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.pbAva.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(79)))), ((int)(((byte)(165)))));
            this.pbAva.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(68)))), ((int)(((byte)(142)))));
            this.pbAva.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.pbAva.BorderSize = 1;
            this.pbAva.GradientAngle = 50F;
            this.pbAva.Image = global::TOURZY___Tourism_Management_System.Properties.Resources.ChatGPT_Image_15_58_10_8_thg_4__2025;
            this.pbAva.Location = new System.Drawing.Point(84, 31);
            this.pbAva.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbAva.Name = "pbAva";
            this.pbAva.Size = new System.Drawing.Size(144, 144);
            this.pbAva.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAva.TabIndex = 9;
            this.pbAva.TabStop = false;
            // 
            // btn_X
            // 
            this.btn_X.BackColor = System.Drawing.Color.Firebrick;
            this.btn_X.FlatAppearance.BorderSize = 0;
            this.btn_X.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_X.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_X.ForeColor = System.Drawing.Color.White;
            this.btn_X.Location = new System.Drawing.Point(1655, 9);
            this.btn_X.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_X.Name = "btn_X";
            this.btn_X.Size = new System.Drawing.Size(84, 51);
            this.btn_X.TabIndex = 2;
            this.btn_X.Text = "X";
            this.btn_X.UseVisualStyleBackColor = false;
            this.btn_X.Click += new System.EventHandler(this.btn_X_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_X);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1923, 70);
            this.panel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chuyenDi1);
            this.flowLayoutPanel1.Controls.Add(this.chiTietChuyenDi1);
            this.flowLayoutPanel1.Controls.Add(this.datChuyenDi1);
            this.flowLayoutPanel1.Controls.Add(this.taoChuyenDiMoi1);
            this.flowLayoutPanel1.Controls.Add(this.danhGiaChuyenDi1);
            this.flowLayoutPanel1.Controls.Add(this.thanhToan1);
            this.flowLayoutPanel1.Controls.Add(this.datChuyenDi2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(323, 70);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1600, 1044);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // chuyenDi1
            // 
            this.chuyenDi1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chuyenDi1.BackgroundImage")));
            this.chuyenDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chuyenDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chuyenDi1.Location = new System.Drawing.Point(3, 2);
            this.chuyenDi1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chuyenDi1.Name = "chuyenDi1";
            this.chuyenDi1.Size = new System.Drawing.Size(1599, 906);
            this.chuyenDi1.TabIndex = 0;
            this.chuyenDi1.UserId = 0;
            this.chuyenDi1.Load += new System.EventHandler(this.chuyenDi1_Load);
            // 
            // chiTietChuyenDi1
            // 
            this.chiTietChuyenDi1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chiTietChuyenDi1.BackgroundImage")));
            this.chiTietChuyenDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chiTietChuyenDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chiTietChuyenDi1.Location = new System.Drawing.Point(3, 915);
            this.chiTietChuyenDi1.MaChuyenDi = null;
            this.chiTietChuyenDi1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.chiTietChuyenDi1.Name = "chiTietChuyenDi1";
            this.chiTietChuyenDi1.NgayBatDau = new System.DateTime(((long)(0)));
            this.chiTietChuyenDi1.Size = new System.Drawing.Size(1599, 906);
            this.chiTietChuyenDi1.TabIndex = 1;
            this.chiTietChuyenDi1.UserId = 0;
            // 
            // datChuyenDi1
            // 
            this.datChuyenDi1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("datChuyenDi1.BackgroundImage")));
            this.datChuyenDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.datChuyenDi1.Location = new System.Drawing.Point(3, 1831);
            this.datChuyenDi1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.datChuyenDi1.Name = "datChuyenDi1";
            this.datChuyenDi1.Size = new System.Drawing.Size(1600, 908);
            this.datChuyenDi1.TabIndex = 2;
            this.datChuyenDi1.UserId = 0;
            // 
            // taoChuyenDiMoi1
            // 
            this.taoChuyenDiMoi1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("taoChuyenDiMoi1.BackgroundImage")));
            this.taoChuyenDiMoi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.taoChuyenDiMoi1.Location = new System.Drawing.Point(3, 2749);
            this.taoChuyenDiMoi1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.taoChuyenDiMoi1.Name = "taoChuyenDiMoi1";
            this.taoChuyenDiMoi1.Size = new System.Drawing.Size(1600, 908);
            this.taoChuyenDiMoi1.TabIndex = 3;
            // 
            // danhGiaChuyenDi1
            // 
            this.danhGiaChuyenDi1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.danhGiaChuyenDi1.Location = new System.Drawing.Point(3, 3667);
            this.danhGiaChuyenDi1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.danhGiaChuyenDi1.Name = "danhGiaChuyenDi1";
            this.danhGiaChuyenDi1.Size = new System.Drawing.Size(1600, 908);
            this.danhGiaChuyenDi1.TabIndex = 4;
            // 
            // thanhToan1
            // 
            this.thanhToan1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("thanhToan1.BackgroundImage")));
            this.thanhToan1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.thanhToan1.Location = new System.Drawing.Point(3, 4585);
            this.thanhToan1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.thanhToan1.Name = "thanhToan1";
            this.thanhToan1.Size = new System.Drawing.Size(1600, 908);
            this.thanhToan1.TabIndex = 5;
            // 
            // datChuyenDi2
            // 
            this.datChuyenDi2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("datChuyenDi2.BackgroundImage")));
            this.datChuyenDi2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.datChuyenDi2.Location = new System.Drawing.Point(3, 5503);
            this.datChuyenDi2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.datChuyenDi2.Name = "datChuyenDi2";
            this.datChuyenDi2.Size = new System.Drawing.Size(1600, 908);
            this.datChuyenDi2.TabIndex = 6;
            this.datChuyenDi2.UserId = 0;
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1923, 1114);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "User";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAva)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTen;
        private SATAUiFramework.Controls.SATAPictureBox pbAva;
        private System.Windows.Forms.Button btn_TimChuyenDi;
        private System.Windows.Forms.Button btn_ChiTiet;
        private System.Windows.Forms.Button btn_TaoChuyenMoi;
        private System.Windows.Forms.Button btn_DanhGia;
        private System.Windows.Forms.Button btn_DatChuyenDi;
        private System.Windows.Forms.Button btn_DangXuat;
        private System.Windows.Forms.Button btn_ThanhToan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_X;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ChuyenDi chuyenDi1;
        private ChiTietChuyenDi chiTietChuyenDi1;
        private DatChuyenDi datChuyenDi1;
        private TaoChuyenDiMoi taoChuyenDiMoi1;
        private DanhGiaChuyenDi danhGiaChuyenDi1;
        private ThanhToan thanhToan1;
        private DatChuyenDi datChuyenDi2;
    }
}