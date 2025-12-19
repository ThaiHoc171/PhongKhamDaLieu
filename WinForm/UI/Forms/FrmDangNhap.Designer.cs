namespace GUI
{
	partial class FrmDangNhap
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
			this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.lblErrorRpt = new System.Windows.Forms.Label();
			this.btnSubmit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.chkHienMK = new Guna.UI2.WinForms.Guna2CheckBox();
			this.lblMatKhau = new System.Windows.Forms.Label();
			this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
			this.lblTenDangNhap = new System.Windows.Forms.Label();
			this.txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
			this.lblTitleSiginIn = new System.Windows.Forms.Label();
			this.lblNamePhongKham = new System.Windows.Forms.Label();
			this.pnlBackGround = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(1100, 33);
			this.pnlHeader.TabIndex = 2;
			this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
			// 
			// btnExit
			// 
			this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnExit.FillColor = System.Drawing.Color.Transparent;
			this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnExit.ForeColor = System.Drawing.Color.White;
			this.btnExit.Image = global::UI.Properties.Resources.exit;
			this.btnExit.ImageSize = new System.Drawing.Size(30, 30);
			this.btnExit.Location = new System.Drawing.Point(1058, 1);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(30, 30);
			this.btnExit.TabIndex = 0;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.lblErrorRpt);
			this.pnlContent.Controls.Add(this.btnSubmit);
			this.pnlContent.Controls.Add(this.chkHienMK);
			this.pnlContent.Controls.Add(this.lblMatKhau);
			this.pnlContent.Controls.Add(this.txtMatKhau);
			this.pnlContent.Controls.Add(this.lblTenDangNhap);
			this.pnlContent.Controls.Add(this.txtUsername);
			this.pnlContent.Controls.Add(this.lblTitleSiginIn);
			this.pnlContent.Controls.Add(this.lblNamePhongKham);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlContent.Location = new System.Drawing.Point(0, 33);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(432, 667);
			this.pnlContent.TabIndex = 3;
			// 
			// lblErrorRpt
			// 
			this.lblErrorRpt.AutoSize = true;
			this.lblErrorRpt.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblErrorRpt.Location = new System.Drawing.Point(45, 481);
			this.lblErrorRpt.Name = "lblErrorRpt";
			this.lblErrorRpt.Size = new System.Drawing.Size(0, 28);
			this.lblErrorRpt.TabIndex = 8;
			// 
			// btnSubmit
			// 
			this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
			this.btnSubmit.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
			this.btnSubmit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnSubmit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnSubmit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnSubmit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnSubmit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSubmit.Font = new System.Drawing.Font("Palatino Linotype", 36F, System.Drawing.FontStyle.Bold);
			this.btnSubmit.ForeColor = System.Drawing.Color.Black;
			this.btnSubmit.Location = new System.Drawing.Point(156, 547);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnSubmit.Size = new System.Drawing.Size(90, 90);
			this.btnSubmit.TabIndex = 7;
			this.btnSubmit.Text = "➜";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// chkHienMK
			// 
			this.chkHienMK.AutoSize = true;
			this.chkHienMK.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.chkHienMK.CheckedState.BorderRadius = 7;
			this.chkHienMK.CheckedState.BorderThickness = 0;
			this.chkHienMK.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.chkHienMK.CheckMarkColor = System.Drawing.Color.Black;
			this.chkHienMK.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold);
			this.chkHienMK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.chkHienMK.Location = new System.Drawing.Point(50, 422);
			this.chkHienMK.Name = "chkHienMK";
			this.chkHienMK.Size = new System.Drawing.Size(166, 30);
			this.chkHienMK.TabIndex = 6;
			this.chkHienMK.Text = "Hiện mật khẩu";
			this.chkHienMK.UncheckedState.BorderColor = System.Drawing.Color.White;
			this.chkHienMK.UncheckedState.BorderRadius = 7;
			this.chkHienMK.UncheckedState.BorderThickness = 0;
			this.chkHienMK.UncheckedState.FillColor = System.Drawing.Color.Silver;
			this.chkHienMK.CheckedChanged += new System.EventHandler(this.chkHienMK_CheckedChanged);
			// 
			// lblMatKhau
			// 
			this.lblMatKhau.AutoSize = true;
			this.lblMatKhau.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMatKhau.Location = new System.Drawing.Point(45, 313);
			this.lblMatKhau.Name = "lblMatKhau";
			this.lblMatKhau.Size = new System.Drawing.Size(111, 28);
			this.lblMatKhau.TabIndex = 5;
			this.lblMatKhau.Text = "Mật Khẩu";
			// 
			// txtMatKhau
			// 
			this.txtMatKhau.BorderRadius = 15;
			this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMatKhau.DefaultText = "";
			this.txtMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMatKhau.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold);
			this.txtMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMatKhau.Location = new System.Drawing.Point(50, 359);
			this.txtMatKhau.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtMatKhau.Name = "txtMatKhau";
			this.txtMatKhau.PlaceholderText = "Nhập mật khẩu";
			this.txtMatKhau.SelectedText = "";
			this.txtMatKhau.Size = new System.Drawing.Size(332, 40);
			this.txtMatKhau.TabIndex = 4;
			this.txtMatKhau.UseSystemPasswordChar = true;
			this.txtMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatKhau_KeyDown);
			// 
			// lblTenDangNhap
			// 
			this.lblTenDangNhap.AutoSize = true;
			this.lblTenDangNhap.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTenDangNhap.Location = new System.Drawing.Point(45, 209);
			this.lblTenDangNhap.Name = "lblTenDangNhap";
			this.lblTenDangNhap.Size = new System.Drawing.Size(169, 28);
			this.lblTenDangNhap.TabIndex = 3;
			this.lblTenDangNhap.Text = "Tên Đăng Nhập";
			// 
			// txtUsername
			// 
			this.txtUsername.BorderRadius = 15;
			this.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtUsername.DefaultText = "";
			this.txtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtUsername.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold);
			this.txtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtUsername.Location = new System.Drawing.Point(50, 255);
			this.txtUsername.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.PlaceholderText = "Nhập tên đăng nhập";
			this.txtUsername.SelectedText = "";
			this.txtUsername.Size = new System.Drawing.Size(332, 40);
			this.txtUsername.TabIndex = 2;
			// 
			// lblTitleSiginIn
			// 
			this.lblTitleSiginIn.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitleSiginIn.Location = new System.Drawing.Point(0, 140);
			this.lblTitleSiginIn.Name = "lblTitleSiginIn";
			this.lblTitleSiginIn.Size = new System.Drawing.Size(432, 37);
			this.lblTitleSiginIn.TabIndex = 1;
			this.lblTitleSiginIn.Text = "Đăng Nhập";
			this.lblTitleSiginIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblNamePhongKham
			// 
			this.lblNamePhongKham.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNamePhongKham.Location = new System.Drawing.Point(0, 32);
			this.lblNamePhongKham.Name = "lblNamePhongKham";
			this.lblNamePhongKham.Size = new System.Drawing.Size(432, 37);
			this.lblNamePhongKham.TabIndex = 0;
			this.lblNamePhongKham.Text = "Tên Phòng Khám";
			this.lblNamePhongKham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlBackGround
			// 
			this.pnlBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBackGround.Location = new System.Drawing.Point(432, 33);
			this.pnlBackGround.Name = "pnlBackGround";
			this.pnlBackGround.Size = new System.Drawing.Size(668, 667);
			this.pnlBackGround.TabIndex = 4;
			// 
			// FrmDangNhap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1100, 700);
			this.Controls.Add(this.pnlBackGround);
			this.Controls.Add(this.pnlContent);
			this.Controls.Add(this.pnlHeader);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmDangNhap";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmDangNhap";
			this.pnlHeader.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Panel pnlBackGround;
		private System.Windows.Forms.Label lblNamePhongKham;
		private System.Windows.Forms.Label lblTitleSiginIn;
		private Guna.UI2.WinForms.Guna2TextBox txtUsername;
		private System.Windows.Forms.Label lblTenDangNhap;
		private System.Windows.Forms.Label lblMatKhau;
		private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
		private Guna.UI2.WinForms.Guna2CheckBox chkHienMK;
		private Guna.UI2.WinForms.Guna2CircleButton btnSubmit;
		private System.Windows.Forms.Label lblErrorRpt;
	}
}