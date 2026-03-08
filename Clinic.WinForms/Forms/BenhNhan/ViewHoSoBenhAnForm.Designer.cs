namespace Clinic.WinForms.Forms.BenhNhan
{
	partial class ViewHoSoBenhAnForm
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
			this.lbMa = new System.Windows.Forms.Label();
			this.lbMaHS_value = new System.Windows.Forms.Label();
			this.lbMaHS = new System.Windows.Forms.Label();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
			this.txtBenhNen = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbBenhNen = new System.Windows.Forms.Label();
			this.lbThongTin = new System.Windows.Forms.Label();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.lbNgayCapNhat_value = new System.Windows.Forms.Label();
			this.lbNgayCapNhat = new System.Windows.Forms.Label();
			this.lbNgayTao_value = new System.Windows.Forms.Label();
			this.lbNgayTao = new System.Windows.Forms.Label();
			this.txtThongTinKhac = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbKhac = new System.Windows.Forms.Label();
			this.txtThoiQuenSong = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbThoiQuenSong = new System.Windows.Forms.Label();
			this.txtTienSuGiaDinh = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbTienSuGiaDinh = new System.Windows.Forms.Label();
			this.txtTienSuBenh = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbTienSuBenh = new System.Windows.Forms.Label();
			this.txtDiUng = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbDiUng = new System.Windows.Forms.Label();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbMa
			// 
			this.lbMa.AutoSize = true;
			this.lbMa.Location = new System.Drawing.Point(538, 46);
			this.lbMa.Name = "lbMa";
			this.lbMa.Size = new System.Drawing.Size(66, 28);
			this.lbMa.TabIndex = 51;
			this.lbMa.Text = "value";
			// 
			// lbMaHS_value
			// 
			this.lbMaHS_value.AutoSize = true;
			this.lbMaHS_value.Location = new System.Drawing.Point(224, 13);
			this.lbMaHS_value.Name = "lbMaHS_value";
			this.lbMaHS_value.Size = new System.Drawing.Size(66, 28);
			this.lbMaHS_value.TabIndex = 52;
			this.lbMaHS_value.Text = "value";
			// 
			// lbMaHS
			// 
			this.lbMaHS.AutoSize = true;
			this.lbMaHS.Location = new System.Drawing.Point(25, 13);
			this.lbMaHS.Name = "lbMaHS";
			this.lbMaHS.Size = new System.Drawing.Size(193, 28);
			this.lbMaHS.TabIndex = 31;
			this.lbMaHS.Text = "Mã hồ sơ bệnh án:";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(774, 42);
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
			this.btnExit.Location = new System.Drawing.Point(722, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
			this.btnEdit.Location = new System.Drawing.Point(512, 513);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(109, 45);
			this.btnEdit.TabIndex = 53;
			this.btnEdit.Text = "  Sửa";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// txtBenhNen
			// 
			this.txtBenhNen.BorderRadius = 15;
			this.txtBenhNen.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtBenhNen.DefaultText = "";
			this.txtBenhNen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtBenhNen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtBenhNen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBenhNen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtBenhNen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBenhNen.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBenhNen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtBenhNen.Location = new System.Drawing.Point(203, 47);
			this.txtBenhNen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtBenhNen.Name = "txtBenhNen";
			this.txtBenhNen.PlaceholderText = "";
			this.txtBenhNen.SelectedText = "";
			this.txtBenhNen.Size = new System.Drawing.Size(521, 40);
			this.txtBenhNen.TabIndex = 14;
			// 
			// lbBenhNen
			// 
			this.lbBenhNen.AutoSize = true;
			this.lbBenhNen.Location = new System.Drawing.Point(25, 56);
			this.lbBenhNen.Name = "lbBenhNen";
			this.lbBenhNen.Size = new System.Drawing.Size(114, 28);
			this.lbBenhNen.TabIndex = 13;
			this.lbBenhNen.Text = "Bệnh nền:";
			// 
			// lbThongTin
			// 
			this.lbThongTin.AutoSize = true;
			this.lbThongTin.ForeColor = System.Drawing.Color.Black;
			this.lbThongTin.Location = new System.Drawing.Point(188, 46);
			this.lbThongTin.Name = "lbThongTin";
			this.lbThongTin.Size = new System.Drawing.Size(344, 28);
			this.lbThongTin.TabIndex = 12;
			this.lbThongTin.Text = "HỒ SƠ BỆNH ÁN BỆNH NHÂN: ";
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
			this.btnLuu.Location = new System.Drawing.Point(640, 513);
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
			this.pnlContent.Size = new System.Drawing.Size(774, 577);
			this.pnlContent.TabIndex = 7;
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlMain.Controls.Add(this.lbNgayCapNhat_value);
			this.pnlMain.Controls.Add(this.lbNgayCapNhat);
			this.pnlMain.Controls.Add(this.lbNgayTao_value);
			this.pnlMain.Controls.Add(this.lbNgayTao);
			this.pnlMain.Controls.Add(this.txtThongTinKhac);
			this.pnlMain.Controls.Add(this.lbKhac);
			this.pnlMain.Controls.Add(this.txtThoiQuenSong);
			this.pnlMain.Controls.Add(this.lbThoiQuenSong);
			this.pnlMain.Controls.Add(this.txtTienSuGiaDinh);
			this.pnlMain.Controls.Add(this.lbTienSuGiaDinh);
			this.pnlMain.Controls.Add(this.txtTienSuBenh);
			this.pnlMain.Controls.Add(this.lbTienSuBenh);
			this.pnlMain.Controls.Add(this.txtDiUng);
			this.pnlMain.Controls.Add(this.lbDiUng);
			this.pnlMain.Controls.Add(this.lbMaHS_value);
			this.pnlMain.Controls.Add(this.lbMaHS);
			this.pnlMain.Controls.Add(this.txtBenhNen);
			this.pnlMain.Controls.Add(this.lbBenhNen);
			this.pnlMain.Location = new System.Drawing.Point(12, 77);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(750, 430);
			this.pnlMain.TabIndex = 12;
			// 
			// lbNgayCapNhat_value
			// 
			this.lbNgayCapNhat_value.AutoSize = true;
			this.lbNgayCapNhat_value.Location = new System.Drawing.Point(582, 380);
			this.lbNgayCapNhat_value.Name = "lbNgayCapNhat_value";
			this.lbNgayCapNhat_value.Size = new System.Drawing.Size(66, 28);
			this.lbNgayCapNhat_value.TabIndex = 66;
			this.lbNgayCapNhat_value.Text = "value";
			// 
			// lbNgayCapNhat
			// 
			this.lbNgayCapNhat.AutoSize = true;
			this.lbNgayCapNhat.Location = new System.Drawing.Point(416, 380);
			this.lbNgayCapNhat.Name = "lbNgayCapNhat";
			this.lbNgayCapNhat.Size = new System.Drawing.Size(160, 28);
			this.lbNgayCapNhat.TabIndex = 65;
			this.lbNgayCapNhat.Text = "Ngày cập nhật:";
			// 
			// lbNgayTao_value
			// 
			this.lbNgayTao_value.AutoSize = true;
			this.lbNgayTao_value.Location = new System.Drawing.Point(138, 380);
			this.lbNgayTao_value.Name = "lbNgayTao_value";
			this.lbNgayTao_value.Size = new System.Drawing.Size(66, 28);
			this.lbNgayTao_value.TabIndex = 64;
			this.lbNgayTao_value.Text = "value";
			// 
			// lbNgayTao
			// 
			this.lbNgayTao.AutoSize = true;
			this.lbNgayTao.Location = new System.Drawing.Point(25, 380);
			this.lbNgayTao.Name = "lbNgayTao";
			this.lbNgayTao.Size = new System.Drawing.Size(105, 28);
			this.lbNgayTao.TabIndex = 63;
			this.lbNgayTao.Text = "Ngày tạo:";
			// 
			// txtThongTinKhac
			// 
			this.txtThongTinKhac.BorderRadius = 15;
			this.txtThongTinKhac.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtThongTinKhac.DefaultText = "";
			this.txtThongTinKhac.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtThongTinKhac.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtThongTinKhac.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtThongTinKhac.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtThongTinKhac.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtThongTinKhac.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtThongTinKhac.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtThongTinKhac.Location = new System.Drawing.Point(203, 307);
			this.txtThongTinKhac.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtThongTinKhac.Name = "txtThongTinKhac";
			this.txtThongTinKhac.PlaceholderText = "";
			this.txtThongTinKhac.SelectedText = "";
			this.txtThongTinKhac.Size = new System.Drawing.Size(521, 40);
			this.txtThongTinKhac.TabIndex = 62;
			// 
			// lbKhac
			// 
			this.lbKhac.AutoSize = true;
			this.lbKhac.Location = new System.Drawing.Point(25, 316);
			this.lbKhac.Name = "lbKhac";
			this.lbKhac.Size = new System.Drawing.Size(68, 28);
			this.lbKhac.TabIndex = 61;
			this.lbKhac.Text = "Khác:";
			// 
			// txtThoiQuenSong
			// 
			this.txtThoiQuenSong.BorderRadius = 15;
			this.txtThoiQuenSong.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtThoiQuenSong.DefaultText = "";
			this.txtThoiQuenSong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtThoiQuenSong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtThoiQuenSong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtThoiQuenSong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtThoiQuenSong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtThoiQuenSong.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtThoiQuenSong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtThoiQuenSong.Location = new System.Drawing.Point(203, 255);
			this.txtThoiQuenSong.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtThoiQuenSong.Name = "txtThoiQuenSong";
			this.txtThoiQuenSong.PlaceholderText = "";
			this.txtThoiQuenSong.SelectedText = "";
			this.txtThoiQuenSong.Size = new System.Drawing.Size(521, 40);
			this.txtThoiQuenSong.TabIndex = 60;
			// 
			// lbThoiQuenSong
			// 
			this.lbThoiQuenSong.AutoSize = true;
			this.lbThoiQuenSong.Location = new System.Drawing.Point(25, 264);
			this.lbThoiQuenSong.Name = "lbThoiQuenSong";
			this.lbThoiQuenSong.Size = new System.Drawing.Size(173, 28);
			this.lbThoiQuenSong.TabIndex = 59;
			this.lbThoiQuenSong.Text = "Thói quen sống:";
			// 
			// txtTienSuGiaDinh
			// 
			this.txtTienSuGiaDinh.BorderRadius = 15;
			this.txtTienSuGiaDinh.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTienSuGiaDinh.DefaultText = "";
			this.txtTienSuGiaDinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtTienSuGiaDinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtTienSuGiaDinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTienSuGiaDinh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTienSuGiaDinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTienSuGiaDinh.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTienSuGiaDinh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTienSuGiaDinh.Location = new System.Drawing.Point(203, 203);
			this.txtTienSuGiaDinh.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtTienSuGiaDinh.Name = "txtTienSuGiaDinh";
			this.txtTienSuGiaDinh.PlaceholderText = "";
			this.txtTienSuGiaDinh.SelectedText = "";
			this.txtTienSuGiaDinh.Size = new System.Drawing.Size(521, 40);
			this.txtTienSuGiaDinh.TabIndex = 58;
			// 
			// lbTienSuGiaDinh
			// 
			this.lbTienSuGiaDinh.AutoSize = true;
			this.lbTienSuGiaDinh.Location = new System.Drawing.Point(25, 212);
			this.lbTienSuGiaDinh.Name = "lbTienSuGiaDinh";
			this.lbTienSuGiaDinh.Size = new System.Drawing.Size(179, 28);
			this.lbTienSuGiaDinh.TabIndex = 57;
			this.lbTienSuGiaDinh.Text = "Tiền sử gia đình:";
			// 
			// txtTienSuBenh
			// 
			this.txtTienSuBenh.BorderRadius = 15;
			this.txtTienSuBenh.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTienSuBenh.DefaultText = "";
			this.txtTienSuBenh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtTienSuBenh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtTienSuBenh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTienSuBenh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTienSuBenh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTienSuBenh.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTienSuBenh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTienSuBenh.Location = new System.Drawing.Point(203, 151);
			this.txtTienSuBenh.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtTienSuBenh.Name = "txtTienSuBenh";
			this.txtTienSuBenh.PlaceholderText = "";
			this.txtTienSuBenh.SelectedText = "";
			this.txtTienSuBenh.Size = new System.Drawing.Size(521, 40);
			this.txtTienSuBenh.TabIndex = 56;
			// 
			// lbTienSuBenh
			// 
			this.lbTienSuBenh.AutoSize = true;
			this.lbTienSuBenh.Location = new System.Drawing.Point(25, 160);
			this.lbTienSuBenh.Name = "lbTienSuBenh";
			this.lbTienSuBenh.Size = new System.Drawing.Size(148, 28);
			this.lbTienSuBenh.TabIndex = 55;
			this.lbTienSuBenh.Text = "Tiền sử bệnh:";
			// 
			// txtDiUng
			// 
			this.txtDiUng.BorderRadius = 15;
			this.txtDiUng.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDiUng.DefaultText = "";
			this.txtDiUng.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtDiUng.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtDiUng.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtDiUng.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtDiUng.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtDiUng.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDiUng.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtDiUng.Location = new System.Drawing.Point(203, 99);
			this.txtDiUng.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtDiUng.Name = "txtDiUng";
			this.txtDiUng.PlaceholderText = "";
			this.txtDiUng.SelectedText = "";
			this.txtDiUng.Size = new System.Drawing.Size(521, 40);
			this.txtDiUng.TabIndex = 54;
			// 
			// lbDiUng
			// 
			this.lbDiUng.AutoSize = true;
			this.lbDiUng.Location = new System.Drawing.Point(25, 108);
			this.lbDiUng.Name = "lbDiUng";
			this.lbDiUng.Size = new System.Drawing.Size(86, 28);
			this.lbDiUng.TabIndex = 53;
			this.lbDiUng.Text = "Di ứng:";
			// 
			// ViewHoSoBenhAnForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(774, 577);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ViewHoSoBenhAnForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ViewHoSoBenhAnForm";
			this.Load += new System.EventHandler(this.ViewHoSoBenhAnForm_Load);
			this.pnlHeader.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbMa;
		private System.Windows.Forms.Label lbMaHS_value;
		private System.Windows.Forms.Label lbMaHS;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2Button btnEdit;
		private Guna.UI2.WinForms.Guna2TextBox txtBenhNen;
		private System.Windows.Forms.Label lbBenhNen;
		private System.Windows.Forms.Label lbThongTin;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private System.Windows.Forms.Panel pnlMain;
		private Guna.UI2.WinForms.Guna2TextBox txtThongTinKhac;
		private System.Windows.Forms.Label lbKhac;
		private Guna.UI2.WinForms.Guna2TextBox txtThoiQuenSong;
		private System.Windows.Forms.Label lbThoiQuenSong;
		private Guna.UI2.WinForms.Guna2TextBox txtTienSuGiaDinh;
		private System.Windows.Forms.Label lbTienSuGiaDinh;
		private Guna.UI2.WinForms.Guna2TextBox txtTienSuBenh;
		private System.Windows.Forms.Label lbTienSuBenh;
		private Guna.UI2.WinForms.Guna2TextBox txtDiUng;
		private System.Windows.Forms.Label lbDiUng;
		private System.Windows.Forms.Label lbNgayCapNhat_value;
		private System.Windows.Forms.Label lbNgayCapNhat;
		private System.Windows.Forms.Label lbNgayTao_value;
		private System.Windows.Forms.Label lbNgayTao;
	}
}