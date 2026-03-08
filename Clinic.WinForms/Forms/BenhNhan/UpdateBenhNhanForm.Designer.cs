namespace Clinic.WinForms.Forms.BenhNhan
{
	partial class UpdateBenhNhanForm
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
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbGhiChu = new System.Windows.Forms.Label();
			this.btnDoiAvt = new Guna.UI2.WinForms.Guna2Button();
			this.picAvt = new Guna.UI2.WinForms.Guna2PictureBox();
			this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
			this.rdoNu = new System.Windows.Forms.RadioButton();
			this.rdoNam = new System.Windows.Forms.RadioButton();
			this.lbNgaySinh = new System.Windows.Forms.Label();
			this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbDiaChi = new System.Windows.Forms.Label();
			this.lbGioiTinh = new System.Windows.Forms.Label();
			this.lbThongTin = new System.Windows.Forms.Label();
			this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbEmail = new System.Windows.Forms.Label();
			this.lbSdt = new System.Windows.Forms.Label();
			this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbHoTen = new System.Windows.Forms.Label();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.lbMa = new System.Windows.Forms.Label();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.lbMaThongtin_value = new System.Windows.Forms.Label();
			this.lbMaThongTin = new System.Windows.Forms.Label();
			this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
			((System.ComponentModel.ISupportInitialize)(this.picAvt)).BeginInit();
			this.pnlContent.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
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
			this.btnExit.Location = new System.Drawing.Point(721, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
			this.txtGhiChu.Location = new System.Drawing.Point(30, 416);
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
			this.lbGhiChu.Location = new System.Drawing.Point(25, 382);
			this.lbGhiChu.Name = "lbGhiChu";
			this.lbGhiChu.Size = new System.Drawing.Size(99, 28);
			this.lbGhiChu.TabIndex = 29;
			this.lbGhiChu.Text = "Ghi chú:";
			// 
			// btnDoiAvt
			// 
			this.btnDoiAvt.BackColor = System.Drawing.Color.Transparent;
			this.btnDoiAvt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnDoiAvt.BorderRadius = 15;
			this.btnDoiAvt.BorderThickness = 2;
			this.btnDoiAvt.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnDoiAvt.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnDoiAvt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnDoiAvt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnDoiAvt.FillColor = System.Drawing.Color.White;
			this.btnDoiAvt.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDoiAvt.ForeColor = System.Drawing.Color.Black;
			this.btnDoiAvt.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnDoiAvt.ImageSize = new System.Drawing.Size(30, 30);
			this.btnDoiAvt.Location = new System.Drawing.Point(58, 171);
			this.btnDoiAvt.Name = "btnDoiAvt";
			this.btnDoiAvt.Size = new System.Drawing.Size(140, 40);
			this.btnDoiAvt.TabIndex = 12;
			this.btnDoiAvt.Text = "Đổi ảnh";
			this.btnDoiAvt.Click += new System.EventHandler(this.btnDoiAvt_Click);
			// 
			// picAvt
			// 
			this.picAvt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.picAvt.Image = global::Clinic.WinForms.Properties.Resources.profile_picture;
			this.picAvt.ImageFlip = Guna.UI2.WinForms.Enums.FlipOrientation.Horizontal;
			this.picAvt.ImageRotate = 0F;
			this.picAvt.Location = new System.Drawing.Point(64, 57);
			this.picAvt.Name = "picAvt";
			this.picAvt.Size = new System.Drawing.Size(125, 108);
			this.picAvt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picAvt.TabIndex = 28;
			this.picAvt.TabStop = false;
			// 
			// dtpNgaySinh
			// 
			this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgaySinh.Location = new System.Drawing.Point(453, 175);
			this.dtpNgaySinh.Name = "dtpNgaySinh";
			this.dtpNgaySinh.Size = new System.Drawing.Size(192, 36);
			this.dtpNgaySinh.TabIndex = 27;
			this.dtpNgaySinh.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			// 
			// rdoNu
			// 
			this.rdoNu.AutoSize = true;
			this.rdoNu.Location = new System.Drawing.Point(555, 125);
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
			this.rdoNam.Location = new System.Drawing.Point(453, 125);
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
			this.lbNgaySinh.Location = new System.Drawing.Point(300, 175);
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
			this.txtDiaChi.Location = new System.Drawing.Point(30, 336);
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
			this.lbDiaChi.Location = new System.Drawing.Point(25, 302);
			this.lbDiaChi.Name = "lbDiaChi";
			this.lbDiaChi.Size = new System.Drawing.Size(88, 28);
			this.lbDiaChi.TabIndex = 20;
			this.lbDiaChi.Text = "Địa chỉ:";
			// 
			// lbGioiTinh
			// 
			this.lbGioiTinh.AutoSize = true;
			this.lbGioiTinh.Location = new System.Drawing.Point(300, 127);
			this.lbGioiTinh.Name = "lbGioiTinh";
			this.lbGioiTinh.Size = new System.Drawing.Size(115, 28);
			this.lbGioiTinh.TabIndex = 19;
			this.lbGioiTinh.Text = "Giới Tính:";
			// 
			// lbThongTin
			// 
			this.lbThongTin.AutoSize = true;
			this.lbThongTin.ForeColor = System.Drawing.Color.Black;
			this.lbThongTin.Location = new System.Drawing.Point(57, 46);
			this.lbThongTin.Name = "lbThongTin";
			this.lbThongTin.Size = new System.Drawing.Size(543, 28);
			this.lbThongTin.TabIndex = 12;
			this.lbThongTin.Text = "ĐIỀU CHỈNH THÔNG TIN CÁ NHÂN BỆNH NHÂN:";
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
			this.txtEmail.Location = new System.Drawing.Point(305, 254);
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
			this.lbEmail.Location = new System.Drawing.Point(300, 220);
			this.lbEmail.Name = "lbEmail";
			this.lbEmail.Size = new System.Drawing.Size(74, 28);
			this.lbEmail.TabIndex = 17;
			this.lbEmail.Text = "Email:";
			// 
			// lbSdt
			// 
			this.lbSdt.AutoSize = true;
			this.lbSdt.Location = new System.Drawing.Point(25, 220);
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
			this.txtHoTen.Location = new System.Drawing.Point(426, 81);
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
			this.lbHoTen.Location = new System.Drawing.Point(298, 82);
			this.lbHoTen.Name = "lbHoTen";
			this.lbHoTen.Size = new System.Drawing.Size(117, 28);
			this.lbHoTen.TabIndex = 13;
			this.lbHoTen.Text = "Họ và Tên:";
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
			this.btnLuu.Location = new System.Drawing.Point(639, 550);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.Black;
			this.pnlContent.BorderRadius = 3;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.btnEdit);
			this.pnlContent.Controls.Add(this.lbMa);
			this.pnlContent.Controls.Add(this.pnlMain);
			this.pnlContent.Controls.Add(this.lbThongTin);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(773, 607);
			this.pnlContent.TabIndex = 6;
			// 
			// lbMa
			// 
			this.lbMa.AutoSize = true;
			this.lbMa.Location = new System.Drawing.Point(606, 46);
			this.lbMa.Name = "lbMa";
			this.lbMa.Size = new System.Drawing.Size(66, 28);
			this.lbMa.TabIndex = 51;
			this.lbMa.Text = "value";
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlMain.Controls.Add(this.lbMaThongtin_value);
			this.pnlMain.Controls.Add(this.lbMaThongTin);
			this.pnlMain.Controls.Add(this.txtGhiChu);
			this.pnlMain.Controls.Add(this.lbGhiChu);
			this.pnlMain.Controls.Add(this.btnDoiAvt);
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
			this.pnlMain.Size = new System.Drawing.Size(750, 467);
			this.pnlMain.TabIndex = 12;
			// 
			// lbMaThongtin_value
			// 
			this.lbMaThongtin_value.AutoSize = true;
			this.lbMaThongtin_value.Location = new System.Drawing.Point(298, 13);
			this.lbMaThongtin_value.Name = "lbMaThongtin_value";
			this.lbMaThongtin_value.Size = new System.Drawing.Size(66, 28);
			this.lbMaThongtin_value.TabIndex = 52;
			this.lbMaThongtin_value.Text = "value";
			// 
			// lbMaThongTin
			// 
			this.lbMaThongTin.AutoSize = true;
			this.lbMaThongTin.Location = new System.Drawing.Point(25, 13);
			this.lbMaThongTin.Name = "lbMaThongTin";
			this.lbMaThongTin.Size = new System.Drawing.Size(268, 28);
			this.lbMaThongTin.TabIndex = 31;
			this.lbMaThongTin.Text = "Mã thông tin người dùng:";
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
			this.txtSDT.Location = new System.Drawing.Point(30, 254);
			this.txtSDT.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtSDT.Name = "txtSDT";
			this.txtSDT.PlaceholderText = "Nhập số điện thoại";
			this.txtSDT.SelectedText = "";
			this.txtSDT.Size = new System.Drawing.Size(240, 40);
			this.txtSDT.TabIndex = 16;
			this.txtSDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSDT_KeyPress);
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(773, 42);
			this.pnlHeader.TabIndex = 10;
			// 
			// btnEdit
			// 
			this.btnEdit.BackColor = System.Drawing.Color.Transparent;
			this.btnEdit.BorderColor = System.Drawing.Color.Yellow;
			this.btnEdit.BorderRadius = 15;
			this.btnEdit.BorderThickness = 2;
			this.btnEdit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnEdit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnEdit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnEdit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnEdit.FillColor = System.Drawing.Color.White;
			this.btnEdit.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnEdit.ForeColor = System.Drawing.Color.Black;
			this.btnEdit.Image = global::Clinic.WinForms.Properties.Resources.pencil;
			this.btnEdit.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnEdit.ImageSize = new System.Drawing.Size(30, 30);
			this.btnEdit.Location = new System.Drawing.Point(520, 550);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(109, 45);
			this.btnEdit.TabIndex = 53;
			this.btnEdit.Text = "  Sửa";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// UpdateBenhNhanForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(773, 607);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "UpdateBenhNhanForm";
			this.Text = "UpdateBenhNhanForm";
			this.Load += new System.EventHandler(this.UpdateBenhNhanForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.picAvt)).EndInit();
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
		private System.Windows.Forms.Label lbGhiChu;
		private Guna.UI2.WinForms.Guna2Button btnDoiAvt;
		private Guna.UI2.WinForms.Guna2PictureBox picAvt;
		private System.Windows.Forms.DateTimePicker dtpNgaySinh;
		private System.Windows.Forms.RadioButton rdoNu;
		private System.Windows.Forms.RadioButton rdoNam;
		private System.Windows.Forms.Label lbNgaySinh;
		private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
		private System.Windows.Forms.Label lbDiaChi;
		private System.Windows.Forms.Label lbGioiTinh;
		private System.Windows.Forms.Label lbThongTin;
		private Guna.UI2.WinForms.Guna2TextBox txtEmail;
		private System.Windows.Forms.Label lbEmail;
		private System.Windows.Forms.Label lbSdt;
		private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
		private System.Windows.Forms.Label lbHoTen;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private System.Windows.Forms.Panel pnlMain;
		private Guna.UI2.WinForms.Guna2TextBox txtSDT;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lbMa;
		private System.Windows.Forms.Label lbMaThongtin_value;
		private System.Windows.Forms.Label lbMaThongTin;
		private Guna.UI2.WinForms.Guna2Button btnEdit;
	}
}