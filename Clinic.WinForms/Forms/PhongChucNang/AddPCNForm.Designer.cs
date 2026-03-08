namespace Clinic.WinForms.Forms.PhongChucNang
{
	partial class AddPCNForm
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
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.pnlNhanVien = new Guna.UI2.WinForms.Guna2Panel();
			this.lbHeader = new System.Windows.Forms.Label();
			this.lbTenPhong = new System.Windows.Forms.Label();
			this.lbLoaiPhong = new System.Windows.Forms.Label();
			this.txtTenPhong = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtLoaiPhong = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbMoTa = new System.Windows.Forms.Label();
			this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlHeader.SuspendLayout();
			this.pnlNhanVien.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(507, 42);
			this.pnlHeader.TabIndex = 10;
			this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
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
			this.btnExit.Location = new System.Drawing.Point(455, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
			this.btnLuu.Location = new System.Drawing.Point(331, 401);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// pnlNhanVien
			// 
			this.pnlNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlNhanVien.Controls.Add(this.txtMoTa);
			this.pnlNhanVien.Controls.Add(this.lbMoTa);
			this.pnlNhanVien.Controls.Add(this.txtLoaiPhong);
			this.pnlNhanVien.Controls.Add(this.txtTenPhong);
			this.pnlNhanVien.Controls.Add(this.lbLoaiPhong);
			this.pnlNhanVien.Controls.Add(this.lbTenPhong);
			this.pnlNhanVien.Controls.Add(this.lbHeader);
			this.pnlNhanVien.Location = new System.Drawing.Point(43, 48);
			this.pnlNhanVien.Name = "pnlNhanVien";
			this.pnlNhanVien.Size = new System.Drawing.Size(410, 337);
			this.pnlNhanVien.TabIndex = 13;
			this.pnlNhanVien.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNhanVien_Paint);
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(62, 10);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(313, 28);
			this.lbHeader.TabIndex = 29;
			this.lbHeader.Text = "THÊM PHÒNG CHỨC NĂNG";
			this.lbHeader.Click += new System.EventHandler(this.lbHeader_Click);
			// 
			// lbTenPhong
			// 
			this.lbTenPhong.AutoSize = true;
			this.lbTenPhong.Location = new System.Drawing.Point(14, 48);
			this.lbTenPhong.Name = "lbTenPhong";
			this.lbTenPhong.Size = new System.Drawing.Size(124, 28);
			this.lbTenPhong.TabIndex = 34;
			this.lbTenPhong.Text = "Tên phòng:";
			this.lbTenPhong.Click += new System.EventHandler(this.lbTenPhong_Click);
			// 
			// lbLoaiPhong
			// 
			this.lbLoaiPhong.AutoSize = true;
			this.lbLoaiPhong.Location = new System.Drawing.Point(14, 139);
			this.lbLoaiPhong.Name = "lbLoaiPhong";
			this.lbLoaiPhong.Size = new System.Drawing.Size(130, 28);
			this.lbLoaiPhong.TabIndex = 36;
			this.lbLoaiPhong.Text = "Loại Phòng:";
			this.lbLoaiPhong.Click += new System.EventHandler(this.lbLoaiPhong_Click);
			// 
			// txtTenPhong
			// 
			this.txtTenPhong.BorderRadius = 15;
			this.txtTenPhong.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTenPhong.DefaultText = "";
			this.txtTenPhong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtTenPhong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtTenPhong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTenPhong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTenPhong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTenPhong.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTenPhong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTenPhong.Location = new System.Drawing.Point(26, 82);
			this.txtTenPhong.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtTenPhong.Name = "txtTenPhong";
			this.txtTenPhong.PlaceholderText = "Nhập tên phòng";
			this.txtTenPhong.SelectedText = "";
			this.txtTenPhong.Size = new System.Drawing.Size(361, 40);
			this.txtTenPhong.TabIndex = 37;
			this.txtTenPhong.TextChanged += new System.EventHandler(this.txtTenPhong_TextChanged);
			// 
			// txtLoaiPhong
			// 
			this.txtLoaiPhong.BorderRadius = 15;
			this.txtLoaiPhong.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtLoaiPhong.DefaultText = "";
			this.txtLoaiPhong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtLoaiPhong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtLoaiPhong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLoaiPhong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLoaiPhong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLoaiPhong.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLoaiPhong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLoaiPhong.Location = new System.Drawing.Point(26, 173);
			this.txtLoaiPhong.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtLoaiPhong.Name = "txtLoaiPhong";
			this.txtLoaiPhong.PlaceholderText = "Nhập loại phòng";
			this.txtLoaiPhong.SelectedText = "";
			this.txtLoaiPhong.Size = new System.Drawing.Size(361, 40);
			this.txtLoaiPhong.TabIndex = 38;
			this.txtLoaiPhong.TextChanged += new System.EventHandler(this.txtLoaiPhong_TextChanged);
			// 
			// lbMoTa
			// 
			this.lbMoTa.AutoSize = true;
			this.lbMoTa.Location = new System.Drawing.Point(14, 234);
			this.lbMoTa.Name = "lbMoTa";
			this.lbMoTa.Size = new System.Drawing.Size(73, 28);
			this.lbMoTa.TabIndex = 39;
			this.lbMoTa.Text = "Mô tả:";
			this.lbMoTa.Click += new System.EventHandler(this.lbMoTa_Click);
			// 
			// txtMoTa
			// 
			this.txtMoTa.BorderRadius = 15;
			this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMoTa.DefaultText = "";
			this.txtMoTa.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtMoTa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtMoTa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMoTa.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMoTa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMoTa.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMoTa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMoTa.Location = new System.Drawing.Point(26, 268);
			this.txtMoTa.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtMoTa.Name = "txtMoTa";
			this.txtMoTa.PlaceholderText = "Nhập mô tả";
			this.txtMoTa.SelectedText = "";
			this.txtMoTa.Size = new System.Drawing.Size(361, 40);
			this.txtMoTa.TabIndex = 40;
			this.txtMoTa.TextChanged += new System.EventHandler(this.txtMoTa_TextChanged);
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.Black;
			this.pnlContent.BorderRadius = 15;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.pnlNhanVien);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(507, 486);
			this.pnlContent.TabIndex = 1;
			this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
			// 
			// AddPCNForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(507, 486);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddPCNForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddPCNForm";
			this.pnlHeader.ResumeLayout(false);
			this.pnlNhanVien.ResumeLayout(false);
			this.pnlNhanVien.PerformLayout();
			this.pnlContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2Panel pnlNhanVien;
		private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
		private System.Windows.Forms.Label lbMoTa;
		private Guna.UI2.WinForms.Guna2TextBox txtLoaiPhong;
		private Guna.UI2.WinForms.Guna2TextBox txtTenPhong;
		private System.Windows.Forms.Label lbLoaiPhong;
		private System.Windows.Forms.Label lbTenPhong;
		private System.Windows.Forms.Label lbHeader;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
	}
}