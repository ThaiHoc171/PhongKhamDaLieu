namespace Clinic.WinForms.Forms.BenhNhan
{
	partial class AddBenhNhanForm
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
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.btnThemAvt = new Guna.UI2.WinForms.Guna2Button();
			this.picAvt = new Guna.UI2.WinForms.Guna2PictureBox();
			this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
			this.rdoNu = new System.Windows.Forms.RadioButton();
			this.rdoNam = new System.Windows.Forms.RadioButton();
			this.lbNgaySinh = new System.Windows.Forms.Label();
			this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbDiaChi = new System.Windows.Forms.Label();
			this.lbGioiTinh = new System.Windows.Forms.Label();
			this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbEmail = new System.Windows.Forms.Label();
			this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbSdt = new System.Windows.Forms.Label();
			this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbHoTen = new System.Windows.Forms.Label();
			this.lbThongTin = new System.Windows.Forms.Label();
			this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbGhiChu = new System.Windows.Forms.Label();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAvt)).BeginInit();
			this.SuspendLayout();
			// 
			// btnLuu
			// 
			this.btnLuu.BackColor = System.Drawing.Color.Transparent;
			this.btnLuu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(221)))), ((int)(((byte)(104)))));
			this.btnLuu.BorderRadius = 15;
			this.btnLuu.BorderThickness = 2;
			this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLuu.FillColor = System.Drawing.Color.White;
			this.btnLuu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLuu.ForeColor = System.Drawing.Color.Black;
			this.btnLuu.Image = global::Clinic.WinForms.Properties.Resources.check_mark;
			this.btnLuu.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnLuu.ImageSize = new System.Drawing.Size(30, 30);
			this.btnLuu.Location = new System.Drawing.Point(640, 515);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.Color.Transparent;
			this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnExit.FillColor = System.Drawing.Color.Transparent;
			this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnExit.ForeColor = System.Drawing.Color.White;
			this.btnExit.Image = global::Clinic.WinForms.Properties.Resources.exit;
			this.btnExit.ImageSize = new System.Drawing.Size(30, 30);
			this.btnExit.Location = new System.Drawing.Point(726, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(778, 42);
			this.pnlHeader.TabIndex = 10;
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.Black;
			this.pnlContent.BorderRadius = 3;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.pnlMain);
			this.pnlContent.Controls.Add(this.lbThongTin);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(778, 570);
			this.pnlContent.TabIndex = 5;
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlMain.Controls.Add(this.txtGhiChu);
			this.pnlMain.Controls.Add(this.lbGhiChu);
			this.pnlMain.Controls.Add(this.btnThemAvt);
			this.pnlMain.Controls.Add(this.picAvt);
			this.pnlMain.Controls.Add(this.dtpNgaySinh);
			this.pnlMain.Controls.Add(this.rdoNu);
			this.pnlMain.Controls.Add(this.rdoNam);
			this.pnlMain.Controls.Add(this.lbNgaySinh);
			this.pnlMain.Controls.Add(this.txtDiaChi);
			this.pnlMain.Controls.Add(this.lbDiaChi);
			this.pnlMain.Controls.Add(this.lbGioiTinh);
			this.pnlMain.Controls.Add(this.txtEmail);
			this.pnlMain.Controls.Add(this.lbEmail);
			this.pnlMain.Controls.Add(this.txtSDT);
			this.pnlMain.Controls.Add(this.lbSdt);
			this.pnlMain.Controls.Add(this.txtHoTen);
			this.pnlMain.Controls.Add(this.lbHoTen);
			this.pnlMain.Location = new System.Drawing.Point(12, 77);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(750, 432);
			this.pnlMain.TabIndex = 12;
			// 
			// btnThemAvt
			// 
			this.btnThemAvt.BackColor = System.Drawing.Color.Transparent;
			this.btnThemAvt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnThemAvt.BorderRadius = 15;
			this.btnThemAvt.BorderThickness = 2;
			this.btnThemAvt.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnThemAvt.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnThemAvt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnThemAvt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnThemAvt.FillColor = System.Drawing.Color.White;
			this.btnThemAvt.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnThemAvt.ForeColor = System.Drawing.Color.Black;
			this.btnThemAvt.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnThemAvt.ImageSize = new System.Drawing.Size(30, 30);
			this.btnThemAvt.Location = new System.Drawing.Point(59, 117);
			this.btnThemAvt.Name = "btnThemAvt";
			this.btnThemAvt.Size = new System.Drawing.Size(140, 40);
			this.btnThemAvt.TabIndex = 12;
			this.btnThemAvt.Text = "Chọn ảnh";
			this.btnThemAvt.Click += new System.EventHandler(this.btnThemAvt_Click);
			// 
			// picAvt
			// 
			this.picAvt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.picAvt.Image = global::Clinic.WinForms.Properties.Resources.profile_picture;
			this.picAvt.ImageFlip = Guna.UI2.WinForms.Enums.FlipOrientation.Horizontal;
			this.picAvt.ImageRotate = 0F;
			this.picAvt.Location = new System.Drawing.Point(65, 3);
			this.picAvt.Name = "picAvt";
			this.picAvt.Size = new System.Drawing.Size(125, 108);
			this.picAvt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picAvt.TabIndex = 28;
			this.picAvt.TabStop = false;
			// 
			// dtpNgaySinh
			// 
			this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgaySinh.Location = new System.Drawing.Point(454, 121);
			this.dtpNgaySinh.Name = "dtpNgaySinh";
			this.dtpNgaySinh.Size = new System.Drawing.Size(192, 36);
			this.dtpNgaySinh.TabIndex = 27;
			this.dtpNgaySinh.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			// 
			// rdoNu
			// 
			this.rdoNu.AutoSize = true;
			this.rdoNu.Location = new System.Drawing.Point(556, 71);
			this.rdoNu.Name = "rdoNu";
			this.rdoNu.Size = new System.Drawing.Size(62, 32);
			this.rdoNu.TabIndex = 26;
			this.rdoNu.TabStop = true;
			this.rdoNu.Text = "Nữ";
			this.rdoNu.UseVisualStyleBackColor = true;
			// 
			// rdoNam
			// 
			this.rdoNam.AutoSize = true;
			this.rdoNam.Location = new System.Drawing.Point(454, 71);
			this.rdoNam.Name = "rdoNam";
			this.rdoNam.Size = new System.Drawing.Size(78, 32);
			this.rdoNam.TabIndex = 25;
			this.rdoNam.TabStop = true;
			this.rdoNam.Text = "Nam";
			this.rdoNam.UseVisualStyleBackColor = true;
			// 
			// lbNgaySinh
			// 
			this.lbNgaySinh.AutoSize = true;
			this.lbNgaySinh.Location = new System.Drawing.Point(301, 121);
			this.lbNgaySinh.Name = "lbNgaySinh";
			this.lbNgaySinh.Size = new System.Drawing.Size(123, 28);
			this.lbNgaySinh.TabIndex = 24;
			this.lbNgaySinh.Text = "Ngày Sinh:";
			// 
			// txtDiaChi
			// 
			this.txtDiaChi.BorderRadius = 15;
			this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDiaChi.DefaultText = "";
			this.txtDiaChi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtDiaChi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtDiaChi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtDiaChi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtDiaChi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtDiaChi.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDiaChi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtDiaChi.Location = new System.Drawing.Point(31, 282);
			this.txtDiaChi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtDiaChi.Name = "txtDiaChi";
			this.txtDiaChi.PlaceholderText = "Nhập địa chỉ";
			this.txtDiaChi.SelectedText = "";
			this.txtDiaChi.Size = new System.Drawing.Size(694, 40);
			this.txtDiaChi.TabIndex = 21;
			// 
			// lbDiaChi
			// 
			this.lbDiaChi.AutoSize = true;
			this.lbDiaChi.Location = new System.Drawing.Point(26, 248);
			this.lbDiaChi.Name = "lbDiaChi";
			this.lbDiaChi.Size = new System.Drawing.Size(88, 28);
			this.lbDiaChi.TabIndex = 20;
			this.lbDiaChi.Text = "Địa chỉ:";
			// 
			// lbGioiTinh
			// 
			this.lbGioiTinh.AutoSize = true;
			this.lbGioiTinh.Location = new System.Drawing.Point(301, 73);
			this.lbGioiTinh.Name = "lbGioiTinh";
			this.lbGioiTinh.Size = new System.Drawing.Size(115, 28);
			this.lbGioiTinh.TabIndex = 19;
			this.lbGioiTinh.Text = "Giới Tính:";
			// 
			// txtEmail
			// 
			this.txtEmail.BorderRadius = 15;
			this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmail.DefaultText = "";
			this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtEmail.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtEmail.Location = new System.Drawing.Point(306, 200);
			this.txtEmail.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.PlaceholderText = "Example@example.com";
			this.txtEmail.SelectedText = "";
			this.txtEmail.Size = new System.Drawing.Size(419, 40);
			this.txtEmail.TabIndex = 18;
			// 
			// lbEmail
			// 
			this.lbEmail.AutoSize = true;
			this.lbEmail.Location = new System.Drawing.Point(301, 166);
			this.lbEmail.Name = "lbEmail";
			this.lbEmail.Size = new System.Drawing.Size(74, 28);
			this.lbEmail.TabIndex = 17;
			this.lbEmail.Text = "Email:";
			// 
			// txtSDT
			// 
			this.txtSDT.BorderRadius = 15;
			this.txtSDT.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSDT.DefaultText = "";
			this.txtSDT.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtSDT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtSDT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSDT.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSDT.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSDT.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSDT.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSDT.Location = new System.Drawing.Point(31, 200);
			this.txtSDT.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtSDT.Name = "txtSDT";
			this.txtSDT.PlaceholderText = "Nhập số điện thoại";
			this.txtSDT.SelectedText = "";
			this.txtSDT.Size = new System.Drawing.Size(240, 40);
			this.txtSDT.TabIndex = 16;
			this.txtSDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSDT_KeyPress);
			// 
			// lbSdt
			// 
			this.lbSdt.AutoSize = true;
			this.lbSdt.Location = new System.Drawing.Point(26, 166);
			this.lbSdt.Name = "lbSdt";
			this.lbSdt.Size = new System.Drawing.Size(148, 28);
			this.lbSdt.TabIndex = 15;
			this.lbSdt.Text = "Số điện thoại:";
			// 
			// txtHoTen
			// 
			this.txtHoTen.BorderRadius = 15;
			this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtHoTen.DefaultText = "";
			this.txtHoTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtHoTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtHoTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtHoTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtHoTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtHoTen.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtHoTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtHoTen.Location = new System.Drawing.Point(427, 27);
			this.txtHoTen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtHoTen.Name = "txtHoTen";
			this.txtHoTen.PlaceholderText = "Nhập Họ và Tên";
			this.txtHoTen.SelectedText = "";
			this.txtHoTen.Size = new System.Drawing.Size(298, 40);
			this.txtHoTen.TabIndex = 14;
			// 
			// lbHoTen
			// 
			this.lbHoTen.AutoSize = true;
			this.lbHoTen.Location = new System.Drawing.Point(299, 28);
			this.lbHoTen.Name = "lbHoTen";
			this.lbHoTen.Size = new System.Drawing.Size(117, 28);
			this.lbHoTen.TabIndex = 13;
			this.lbHoTen.Text = "Họ và Tên:";
			// 
			// lbThongTin
			// 
			this.lbThongTin.AutoSize = true;
			this.lbThongTin.ForeColor = System.Drawing.Color.Black;
			this.lbThongTin.Location = new System.Drawing.Point(162, 46);
			this.lbThongTin.Name = "lbThongTin";
			this.lbThongTin.Size = new System.Drawing.Size(468, 28);
			this.lbThongTin.TabIndex = 12;
			this.lbThongTin.Text = "THÊM THÔNG TIN CÁ NHÂN BỆNH NHÂN";
			// 
			// txtGhiChu
			// 
			this.txtGhiChu.BorderRadius = 15;
			this.txtGhiChu.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtGhiChu.DefaultText = "";
			this.txtGhiChu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtGhiChu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtGhiChu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtGhiChu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtGhiChu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtGhiChu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGhiChu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtGhiChu.Location = new System.Drawing.Point(31, 362);
			this.txtGhiChu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtGhiChu.Name = "txtGhiChu";
			this.txtGhiChu.PlaceholderText = "Nhập ghi chú";
			this.txtGhiChu.SelectedText = "";
			this.txtGhiChu.Size = new System.Drawing.Size(694, 40);
			this.txtGhiChu.TabIndex = 30;
			// 
			// lbGhiChu
			// 
			this.lbGhiChu.AutoSize = true;
			this.lbGhiChu.Location = new System.Drawing.Point(26, 328);
			this.lbGhiChu.Name = "lbGhiChu";
			this.lbGhiChu.Size = new System.Drawing.Size(99, 28);
			this.lbGhiChu.TabIndex = 29;
			this.lbGhiChu.Text = "Ghi chú:";
			// 
			// AddBenhNhanForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 570);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddBenhNhanForm";
			this.Text = "AddBenhNhanForm";
			this.pnlHeader.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAvt)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private System.Windows.Forms.Panel pnlMain;
		private Guna.UI2.WinForms.Guna2Button btnThemAvt;
		private Guna.UI2.WinForms.Guna2PictureBox picAvt;
		private System.Windows.Forms.DateTimePicker dtpNgaySinh;
		private System.Windows.Forms.RadioButton rdoNu;
		private System.Windows.Forms.RadioButton rdoNam;
		private System.Windows.Forms.Label lbNgaySinh;
		private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
		private System.Windows.Forms.Label lbDiaChi;
		private System.Windows.Forms.Label lbGioiTinh;
		private Guna.UI2.WinForms.Guna2TextBox txtEmail;
		private System.Windows.Forms.Label lbEmail;
		private Guna.UI2.WinForms.Guna2TextBox txtSDT;
		private System.Windows.Forms.Label lbSdt;
		private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
		private System.Windows.Forms.Label lbHoTen;
		private System.Windows.Forms.Label lbThongTin;
		private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
		private System.Windows.Forms.Label lbGhiChu;
	}
}