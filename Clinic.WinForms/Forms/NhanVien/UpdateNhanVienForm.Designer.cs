namespace Clinic.WinForms.Forms.NhanVien
{
	partial class UpdateNhanVienForm
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
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.lbHeader = new System.Windows.Forms.Label();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.btnDoiAvt = new Guna.UI2.WinForms.Guna2Button();
			this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
			this.rdoNu = new System.Windows.Forms.RadioButton();
			this.pnlThongTin = new System.Windows.Forms.Panel();
			this.lbMaTTCN_value = new System.Windows.Forms.Label();
			this.lbMaTTCN = new System.Windows.Forms.Label();
			this.picAvt = new Guna.UI2.WinForms.Guna2PictureBox();
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
			this.lbTenChucVu = new System.Windows.Forms.Label();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
			this.pnlNhanVien = new Guna.UI2.WinForms.Guna2Panel();
			this.lbMaNV_value = new System.Windows.Forms.Label();
			this.lbMaNV = new System.Windows.Forms.Label();
			this.txtKinhNghiem = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtBangCap = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbKiinhNghiem = new System.Windows.Forms.Label();
			this.lbBangCap = new System.Windows.Forms.Label();
			this.dtpNgayVaoLam = new System.Windows.Forms.DateTimePicker();
			this.lbNgayVaoLam = new System.Windows.Forms.Label();
			this.cbbPhong = new Guna.UI2.WinForms.Guna2ComboBox();
			this.cbbChucVu = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lbHeaderNhanVien = new System.Windows.Forms.Label();
			this.lbPhong = new System.Windows.Forms.Label();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.pnlHeader.SuspendLayout();
			this.pnlThongTin.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAvt)).BeginInit();
			this.pnlContent.SuspendLayout();
			this.pnlNhanVien.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(415, 45);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(419, 28);
			this.lbHeader.TabIndex = 2;
			this.lbHeader.Text = "ĐIỀU CHỈNH THÔNG TIN NHÂN VIÊN";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(1221, 42);
			this.pnlHeader.TabIndex = 10;
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
			this.btnExit.Location = new System.Drawing.Point(1169, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
			this.btnDoiAvt.Location = new System.Drawing.Point(95, 193);
			this.btnDoiAvt.Name = "btnDoiAvt";
			this.btnDoiAvt.Size = new System.Drawing.Size(140, 40);
			this.btnDoiAvt.TabIndex = 12;
			this.btnDoiAvt.Text = "Đổi ảnh";
			this.btnDoiAvt.Click += new System.EventHandler(this.btnDoiAvt_Click);
			// 
			// dtpNgaySinh
			// 
			this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgaySinh.Location = new System.Drawing.Point(462, 197);
			this.dtpNgaySinh.Name = "dtpNgaySinh";
			this.dtpNgaySinh.Size = new System.Drawing.Size(192, 36);
			this.dtpNgaySinh.TabIndex = 27;
			this.dtpNgaySinh.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			// 
			// rdoNu
			// 
			this.rdoNu.AutoSize = true;
			this.rdoNu.Location = new System.Drawing.Point(564, 147);
			this.rdoNu.Name = "rdoNu";
			this.rdoNu.Size = new System.Drawing.Size(62, 32);
			this.rdoNu.TabIndex = 26;
			this.rdoNu.TabStop = true;
			this.rdoNu.Text = "Nữ";
			this.rdoNu.UseVisualStyleBackColor = true;
			// 
			// pnlThongTin
			// 
			this.pnlThongTin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlThongTin.Controls.Add(this.lbMaTTCN_value);
			this.pnlThongTin.Controls.Add(this.lbMaTTCN);
			this.pnlThongTin.Controls.Add(this.btnDoiAvt);
			this.pnlThongTin.Controls.Add(this.picAvt);
			this.pnlThongTin.Controls.Add(this.dtpNgaySinh);
			this.pnlThongTin.Controls.Add(this.rdoNu);
			this.pnlThongTin.Controls.Add(this.rdoNam);
			this.pnlThongTin.Controls.Add(this.lbNgaySinh);
			this.pnlThongTin.Controls.Add(this.txtDiaChi);
			this.pnlThongTin.Controls.Add(this.lbDiaChi);
			this.pnlThongTin.Controls.Add(this.lbGioiTinh);
			this.pnlThongTin.Controls.Add(this.txtEmail);
			this.pnlThongTin.Controls.Add(this.lbEmail);
			this.pnlThongTin.Controls.Add(this.txtSDT);
			this.pnlThongTin.Controls.Add(this.lbSdt);
			this.pnlThongTin.Controls.Add(this.txtHoTen);
			this.pnlThongTin.Controls.Add(this.lbHoTen);
			this.pnlThongTin.Controls.Add(this.lbThongTin);
			this.pnlThongTin.Location = new System.Drawing.Point(32, 86);
			this.pnlThongTin.Name = "pnlThongTin";
			this.pnlThongTin.Size = new System.Drawing.Size(750, 421);
			this.pnlThongTin.TabIndex = 11;
			// 
			// lbMaTTCN_value
			// 
			this.lbMaTTCN_value.AutoSize = true;
			this.lbMaTTCN_value.Location = new System.Drawing.Point(459, 59);
			this.lbMaTTCN_value.Name = "lbMaTTCN_value";
			this.lbMaTTCN_value.Size = new System.Drawing.Size(66, 28);
			this.lbMaTTCN_value.TabIndex = 30;
			this.lbMaTTCN_value.Text = "value";
			// 
			// lbMaTTCN
			// 
			this.lbMaTTCN.AutoSize = true;
			this.lbMaTTCN.Location = new System.Drawing.Point(307, 59);
			this.lbMaTTCN.Name = "lbMaTTCN";
			this.lbMaTTCN.Size = new System.Drawing.Size(146, 28);
			this.lbMaTTCN.TabIndex = 29;
			this.lbMaTTCN.Text = "Mã thông tin:";
			// 
			// picAvt
			// 
			this.picAvt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.picAvt.Image = global::Clinic.WinForms.Properties.Resources.profile_picture;
			this.picAvt.ImageFlip = Guna.UI2.WinForms.Enums.FlipOrientation.Horizontal;
			this.picAvt.ImageRotate = 0F;
			this.picAvt.Location = new System.Drawing.Point(95, 48);
			this.picAvt.Name = "picAvt";
			this.picAvt.Size = new System.Drawing.Size(143, 133);
			this.picAvt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picAvt.TabIndex = 28;
			this.picAvt.TabStop = false;
			// 
			// rdoNam
			// 
			this.rdoNam.AutoSize = true;
			this.rdoNam.Location = new System.Drawing.Point(462, 147);
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
			this.lbNgaySinh.Location = new System.Drawing.Point(309, 197);
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
			this.txtDiaChi.Location = new System.Drawing.Point(39, 368);
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
			this.lbDiaChi.Location = new System.Drawing.Point(34, 335);
			this.lbDiaChi.Name = "lbDiaChi";
			this.lbDiaChi.Size = new System.Drawing.Size(88, 28);
			this.lbDiaChi.TabIndex = 20;
			this.lbDiaChi.Text = "Địa chỉ:";
			// 
			// lbGioiTinh
			// 
			this.lbGioiTinh.AutoSize = true;
			this.lbGioiTinh.Location = new System.Drawing.Point(309, 149);
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
			this.txtEmail.Location = new System.Drawing.Point(314, 281);
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
			this.lbEmail.Location = new System.Drawing.Point(309, 247);
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
			this.txtSDT.Location = new System.Drawing.Point(39, 281);
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
			this.lbSdt.Location = new System.Drawing.Point(34, 247);
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
			this.txtHoTen.Location = new System.Drawing.Point(435, 103);
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
			this.lbHoTen.Location = new System.Drawing.Point(307, 104);
			this.lbHoTen.Name = "lbHoTen";
			this.lbHoTen.Size = new System.Drawing.Size(117, 28);
			this.lbHoTen.TabIndex = 13;
			this.lbHoTen.Text = "Họ và Tên:";
			// 
			// lbThongTin
			// 
			this.lbThongTin.AutoSize = true;
			this.lbThongTin.ForeColor = System.Drawing.Color.Black;
			this.lbThongTin.Location = new System.Drawing.Point(281, 9);
			this.lbThongTin.Name = "lbThongTin";
			this.lbThongTin.Size = new System.Drawing.Size(195, 28);
			this.lbThongTin.TabIndex = 12;
			this.lbThongTin.Text = "Thông tin cá nhân";
			// 
			// lbTenChucVu
			// 
			this.lbTenChucVu.AutoSize = true;
			this.lbTenChucVu.Location = new System.Drawing.Point(18, 78);
			this.lbTenChucVu.Name = "lbTenChucVu";
			this.lbTenChucVu.Size = new System.Drawing.Size(105, 28);
			this.lbTenChucVu.TabIndex = 10;
			this.lbTenChucVu.Text = "Chức Vụ:";
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pnlContent.BorderColor = System.Drawing.Color.Silver;
			this.pnlContent.BorderRadius = 15;
			this.pnlContent.BorderThickness = 4;
			this.pnlContent.Controls.Add(this.btnEdit);
			this.pnlContent.Controls.Add(this.pnlNhanVien);
			this.pnlContent.Controls.Add(this.pnlThongTin);
			this.pnlContent.Controls.Add(this.lbHeader);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.CustomBorderColor = System.Drawing.Color.Transparent;
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.FillColor = System.Drawing.Color.Transparent;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1221, 584);
			this.pnlContent.TabIndex = 2;
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
			this.btnEdit.Location = new System.Drawing.Point(930, 523);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(109, 45);
			this.btnEdit.TabIndex = 16;
			this.btnEdit.Text = "  Sửa";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// pnlNhanVien
			// 
			this.pnlNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlNhanVien.Controls.Add(this.lbMaNV_value);
			this.pnlNhanVien.Controls.Add(this.lbMaNV);
			this.pnlNhanVien.Controls.Add(this.txtKinhNghiem);
			this.pnlNhanVien.Controls.Add(this.txtBangCap);
			this.pnlNhanVien.Controls.Add(this.lbKiinhNghiem);
			this.pnlNhanVien.Controls.Add(this.lbBangCap);
			this.pnlNhanVien.Controls.Add(this.dtpNgayVaoLam);
			this.pnlNhanVien.Controls.Add(this.lbNgayVaoLam);
			this.pnlNhanVien.Controls.Add(this.cbbPhong);
			this.pnlNhanVien.Controls.Add(this.cbbChucVu);
			this.pnlNhanVien.Controls.Add(this.lbHeaderNhanVien);
			this.pnlNhanVien.Controls.Add(this.lbPhong);
			this.pnlNhanVien.Controls.Add(this.lbTenChucVu);
			this.pnlNhanVien.Location = new System.Drawing.Point(788, 86);
			this.pnlNhanVien.Name = "pnlNhanVien";
			this.pnlNhanVien.Size = new System.Drawing.Size(410, 421);
			this.pnlNhanVien.TabIndex = 12;
			// 
			// lbMaNV_value
			// 
			this.lbMaNV_value.AutoSize = true;
			this.lbMaNV_value.Location = new System.Drawing.Point(185, 38);
			this.lbMaNV_value.Name = "lbMaNV_value";
			this.lbMaNV_value.Size = new System.Drawing.Size(66, 28);
			this.lbMaNV_value.TabIndex = 40;
			this.lbMaNV_value.Text = "value";
			// 
			// lbMaNV
			// 
			this.lbMaNV.AutoSize = true;
			this.lbMaNV.Location = new System.Drawing.Point(18, 38);
			this.lbMaNV.Name = "lbMaNV";
			this.lbMaNV.Size = new System.Drawing.Size(156, 28);
			this.lbMaNV.TabIndex = 39;
			this.lbMaNV.Text = "Mã nhân viên:";
			// 
			// txtKinhNghiem
			// 
			this.txtKinhNghiem.BorderRadius = 15;
			this.txtKinhNghiem.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtKinhNghiem.DefaultText = "";
			this.txtKinhNghiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtKinhNghiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtKinhNghiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtKinhNghiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtKinhNghiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtKinhNghiem.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtKinhNghiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtKinhNghiem.Location = new System.Drawing.Point(30, 368);
			this.txtKinhNghiem.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtKinhNghiem.Name = "txtKinhNghiem";
			this.txtKinhNghiem.PlaceholderText = "Nhập kinh nghiệm";
			this.txtKinhNghiem.SelectedText = "";
			this.txtKinhNghiem.Size = new System.Drawing.Size(361, 40);
			this.txtKinhNghiem.TabIndex = 38;
			// 
			// txtBangCap
			// 
			this.txtBangCap.BorderRadius = 15;
			this.txtBangCap.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtBangCap.DefaultText = "";
			this.txtBangCap.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtBangCap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtBangCap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBangCap.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBangCap.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBangCap.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBangCap.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBangCap.Location = new System.Drawing.Point(30, 259);
			this.txtBangCap.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtBangCap.Name = "txtBangCap";
			this.txtBangCap.PlaceholderText = "Nhập bằng cấp";
			this.txtBangCap.SelectedText = "";
			this.txtBangCap.Size = new System.Drawing.Size(361, 40);
			this.txtBangCap.TabIndex = 37;
			// 
			// lbKiinhNghiem
			// 
			this.lbKiinhNghiem.AutoSize = true;
			this.lbKiinhNghiem.Location = new System.Drawing.Point(18, 325);
			this.lbKiinhNghiem.Name = "lbKiinhNghiem";
			this.lbKiinhNghiem.Size = new System.Drawing.Size(154, 28);
			this.lbKiinhNghiem.TabIndex = 36;
			this.lbKiinhNghiem.Text = "Kinh Nghiệm:";
			// 
			// lbBangCap
			// 
			this.lbBangCap.AutoSize = true;
			this.lbBangCap.Location = new System.Drawing.Point(18, 225);
			this.lbBangCap.Name = "lbBangCap";
			this.lbBangCap.Size = new System.Drawing.Size(112, 28);
			this.lbBangCap.TabIndex = 34;
			this.lbBangCap.Text = "Bằng Cấp:";
			// 
			// dtpNgayVaoLam
			// 
			this.dtpNgayVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgayVaoLam.Location = new System.Drawing.Point(190, 176);
			this.dtpNgayVaoLam.Name = "dtpNgayVaoLam";
			this.dtpNgayVaoLam.Size = new System.Drawing.Size(201, 36);
			this.dtpNgayVaoLam.TabIndex = 33;
			this.dtpNgayVaoLam.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
			// 
			// lbNgayVaoLam
			// 
			this.lbNgayVaoLam.AutoSize = true;
			this.lbNgayVaoLam.Location = new System.Drawing.Point(18, 182);
			this.lbNgayVaoLam.Name = "lbNgayVaoLam";
			this.lbNgayVaoLam.Size = new System.Drawing.Size(152, 28);
			this.lbNgayVaoLam.TabIndex = 32;
			this.lbNgayVaoLam.Text = "Ngày vào làm:";
			// 
			// cbbPhong
			// 
			this.cbbPhong.BackColor = System.Drawing.Color.Transparent;
			this.cbbPhong.BorderRadius = 15;
			this.cbbPhong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbPhong.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbPhong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbPhong.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbPhong.ItemHeight = 30;
			this.cbbPhong.Location = new System.Drawing.Point(150, 128);
			this.cbbPhong.Name = "cbbPhong";
			this.cbbPhong.Size = new System.Drawing.Size(257, 36);
			this.cbbPhong.TabIndex = 31;
			// 
			// cbbChucVu
			// 
			this.cbbChucVu.BackColor = System.Drawing.Color.Transparent;
			this.cbbChucVu.BorderRadius = 15;
			this.cbbChucVu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbChucVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbChucVu.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbChucVu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbChucVu.ItemHeight = 30;
			this.cbbChucVu.Location = new System.Drawing.Point(150, 78);
			this.cbbChucVu.Name = "cbbChucVu";
			this.cbbChucVu.Size = new System.Drawing.Size(257, 36);
			this.cbbChucVu.TabIndex = 30;
			// 
			// lbHeaderNhanVien
			// 
			this.lbHeaderNhanVien.AutoSize = true;
			this.lbHeaderNhanVien.ForeColor = System.Drawing.Color.Black;
			this.lbHeaderNhanVien.Location = new System.Drawing.Point(119, 9);
			this.lbHeaderNhanVien.Name = "lbHeaderNhanVien";
			this.lbHeaderNhanVien.Size = new System.Drawing.Size(175, 28);
			this.lbHeaderNhanVien.TabIndex = 29;
			this.lbHeaderNhanVien.Text = "Hồ sơ nhân viên";
			// 
			// lbPhong
			// 
			this.lbPhong.AutoSize = true;
			this.lbPhong.Location = new System.Drawing.Point(18, 128);
			this.lbPhong.Name = "lbPhong";
			this.lbPhong.Size = new System.Drawing.Size(82, 28);
			this.lbPhong.TabIndex = 11;
			this.lbPhong.Text = "Phòng:";
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
			this.btnLuu.Location = new System.Drawing.Point(1070, 523);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(109, 45);
			this.btnLuu.TabIndex = 14;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// UpdateNhanVienForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1221, 584);
			this.Controls.Add(this.pnlContent);
			this.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "UpdateNhanVienForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UpdateNhanVienForm";
			this.Load += new System.EventHandler(this.UpdateNhanVienForm_Load);
			this.pnlHeader.ResumeLayout(false);
			this.pnlThongTin.ResumeLayout(false);
			this.pnlThongTin.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAvt)).EndInit();
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlNhanVien.ResumeLayout(false);
			this.pnlNhanVien.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2PictureBox picAvt;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2Button btnDoiAvt;
		private System.Windows.Forms.DateTimePicker dtpNgaySinh;
		private System.Windows.Forms.RadioButton rdoNu;
		private System.Windows.Forms.Panel pnlThongTin;
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
		private System.Windows.Forms.Label lbTenChucVu;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Panel pnlNhanVien;
		private Guna.UI2.WinForms.Guna2TextBox txtKinhNghiem;
		private Guna.UI2.WinForms.Guna2TextBox txtBangCap;
		private System.Windows.Forms.Label lbKiinhNghiem;
		private System.Windows.Forms.Label lbBangCap;
		private System.Windows.Forms.DateTimePicker dtpNgayVaoLam;
		private System.Windows.Forms.Label lbNgayVaoLam;
		private Guna.UI2.WinForms.Guna2ComboBox cbbPhong;
		private Guna.UI2.WinForms.Guna2ComboBox cbbChucVu;
		private System.Windows.Forms.Label lbHeaderNhanVien;
		private System.Windows.Forms.Label lbPhong;
		private System.Windows.Forms.Label lbMaTTCN_value;
		private System.Windows.Forms.Label lbMaTTCN;
		private System.Windows.Forms.Label lbMaNV_value;
		private System.Windows.Forms.Label lbMaNV;
		private Guna.UI2.WinForms.Guna2Button btnEdit;
	}
}