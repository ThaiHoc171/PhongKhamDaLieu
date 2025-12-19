namespace UI
{
	partial class FrmMain
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
			Guna.UI2.WinForms.Guna2CircleButton btnLogo;
			this.tlMain = new System.Windows.Forms.TableLayoutPanel();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
			this.flowSidebar = new System.Windows.Forms.FlowLayoutPanel();
			this.btnTaiKhoan = new Guna.UI2.WinForms.Guna2Button();
			this.btnBenhNhan = new Guna.UI2.WinForms.Guna2Button();
			this.btnNhanVien = new Guna.UI2.WinForms.Guna2Button();
			this.btnCSVC = new Guna.UI2.WinForms.Guna2Button();
			this.btnLichLamViec = new Guna.UI2.WinForms.Guna2Button();
			this.btnLichKhamBenh = new Guna.UI2.WinForms.Guna2Button();
			this.btnKhamBenh = new Guna.UI2.WinForms.Guna2Button();
			this.btnThuoc = new Guna.UI2.WinForms.Guna2Button();
			btnLogo = new Guna.UI2.WinForms.Guna2CircleButton();
			this.tlMain.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlSidebar.SuspendLayout();
			this.flowSidebar.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLogo
			// 
			btnLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
			btnLogo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			btnLogo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			btnLogo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			btnLogo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			btnLogo.FillColor = System.Drawing.Color.Transparent;
			btnLogo.Font = new System.Drawing.Font("Segoe UI", 9F);
			btnLogo.ForeColor = System.Drawing.Color.White;
			btnLogo.Location = new System.Drawing.Point(4, 3);
			btnLogo.Name = "btnLogo";
			btnLogo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			btnLogo.Size = new System.Drawing.Size(200, 70);
			btnLogo.TabIndex = 0;
			btnLogo.Text = "LogoImg";
			btnLogo.Click += new System.EventHandler(this.btnLogo_Click);
			// 
			// tlMain
			// 
			this.tlMain.ColumnCount = 2;
			this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
			this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlMain.Controls.Add(this.pnlContent, 1, 1);
			this.tlMain.Controls.Add(this.pnlHeader, 1, 0);
			this.tlMain.Controls.Add(this.pnlSidebar, 0, 0);
			this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlMain.Location = new System.Drawing.Point(0, 0);
			this.tlMain.Name = "tlMain";
			this.tlMain.RowCount = 2;
			this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlMain.Size = new System.Drawing.Size(1600, 900);
			this.tlMain.TabIndex = 0;
			// 
			// pnlContent
			// 
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Location = new System.Drawing.Point(223, 63);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1374, 834);
			this.pnlContent.TabIndex = 1;
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeader.Location = new System.Drawing.Point(223, 3);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(1374, 54);
			this.pnlHeader.TabIndex = 1;
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
			this.btnExit.Location = new System.Drawing.Point(1335, 9);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(30, 30);
			this.btnExit.TabIndex = 0;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// pnlSidebar
			// 
			this.pnlSidebar.Controls.Add(this.flowSidebar);
			this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSidebar.Location = new System.Drawing.Point(3, 3);
			this.pnlSidebar.Name = "pnlSidebar";
			this.tlMain.SetRowSpan(this.pnlSidebar, 2);
			this.pnlSidebar.Size = new System.Drawing.Size(214, 894);
			this.pnlSidebar.TabIndex = 0;
			// 
			// flowSidebar
			// 
			this.flowSidebar.AutoScroll = true;
			this.flowSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(44)))), ((int)(((byte)(106)))));
			this.flowSidebar.Controls.Add(btnLogo);
			this.flowSidebar.Controls.Add(this.btnTaiKhoan);
			this.flowSidebar.Controls.Add(this.btnBenhNhan);
			this.flowSidebar.Controls.Add(this.btnNhanVien);
			this.flowSidebar.Controls.Add(this.btnCSVC);
			this.flowSidebar.Controls.Add(this.btnLichLamViec);
			this.flowSidebar.Controls.Add(this.btnLichKhamBenh);
			this.flowSidebar.Controls.Add(this.btnKhamBenh);
			this.flowSidebar.Controls.Add(this.btnThuoc);
			this.flowSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowSidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowSidebar.Location = new System.Drawing.Point(0, 0);
			this.flowSidebar.Name = "flowSidebar";
			this.flowSidebar.Size = new System.Drawing.Size(214, 894);
			this.flowSidebar.TabIndex = 0;
			this.flowSidebar.WrapContents = false;
			// 
			// btnTaiKhoan
			// 
			this.btnTaiKhoan.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnTaiKhoan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnTaiKhoan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnTaiKhoan.FillColor = System.Drawing.Color.Transparent;
			this.btnTaiKhoan.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTaiKhoan.ForeColor = System.Drawing.Color.White;
			this.btnTaiKhoan.Location = new System.Drawing.Point(3, 79);
			this.btnTaiKhoan.Name = "btnTaiKhoan";
			this.btnTaiKhoan.Size = new System.Drawing.Size(203, 70);
			this.btnTaiKhoan.TabIndex = 0;
			this.btnTaiKhoan.Text = "Tài Khoản";
			this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
			// 
			// btnBenhNhan
			// 
			this.btnBenhNhan.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnBenhNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnBenhNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnBenhNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnBenhNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnBenhNhan.FillColor = System.Drawing.Color.Transparent;
			this.btnBenhNhan.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBenhNhan.ForeColor = System.Drawing.Color.White;
			this.btnBenhNhan.Location = new System.Drawing.Point(3, 155);
			this.btnBenhNhan.Name = "btnBenhNhan";
			this.btnBenhNhan.Size = new System.Drawing.Size(203, 70);
			this.btnBenhNhan.TabIndex = 1;
			this.btnBenhNhan.Text = "Bệnh Nhân";
			// 
			// btnNhanVien
			// 
			this.btnNhanVien.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnNhanVien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnNhanVien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnNhanVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnNhanVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnNhanVien.FillColor = System.Drawing.Color.Transparent;
			this.btnNhanVien.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNhanVien.ForeColor = System.Drawing.Color.White;
			this.btnNhanVien.Location = new System.Drawing.Point(3, 231);
			this.btnNhanVien.Name = "btnNhanVien";
			this.btnNhanVien.Size = new System.Drawing.Size(203, 70);
			this.btnNhanVien.TabIndex = 2;
			this.btnNhanVien.Text = "Nhân Viên";
			// 
			// btnCSVC
			// 
			this.btnCSVC.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnCSVC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnCSVC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnCSVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnCSVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnCSVC.FillColor = System.Drawing.Color.Transparent;
			this.btnCSVC.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCSVC.ForeColor = System.Drawing.Color.White;
			this.btnCSVC.Location = new System.Drawing.Point(3, 307);
			this.btnCSVC.Name = "btnCSVC";
			this.btnCSVC.Size = new System.Drawing.Size(203, 70);
			this.btnCSVC.TabIndex = 3;
			this.btnCSVC.Text = "Cơ Sở Vật Chất";
			// 
			// btnLichLamViec
			// 
			this.btnLichLamViec.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnLichLamViec.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLichLamViec.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLichLamViec.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLichLamViec.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLichLamViec.FillColor = System.Drawing.Color.Transparent;
			this.btnLichLamViec.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLichLamViec.ForeColor = System.Drawing.Color.White;
			this.btnLichLamViec.Location = new System.Drawing.Point(3, 383);
			this.btnLichLamViec.Name = "btnLichLamViec";
			this.btnLichLamViec.Size = new System.Drawing.Size(203, 70);
			this.btnLichLamViec.TabIndex = 4;
			this.btnLichLamViec.Text = "Lịch Làm Việc";
			// 
			// btnLichKhamBenh
			// 
			this.btnLichKhamBenh.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnLichKhamBenh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLichKhamBenh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnLichKhamBenh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnLichKhamBenh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnLichKhamBenh.FillColor = System.Drawing.Color.Transparent;
			this.btnLichKhamBenh.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLichKhamBenh.ForeColor = System.Drawing.Color.White;
			this.btnLichKhamBenh.Location = new System.Drawing.Point(3, 459);
			this.btnLichKhamBenh.Name = "btnLichKhamBenh";
			this.btnLichKhamBenh.Size = new System.Drawing.Size(203, 70);
			this.btnLichKhamBenh.TabIndex = 5;
			this.btnLichKhamBenh.Text = "Lịch Khám Bệnh";
			// 
			// btnKhamBenh
			// 
			this.btnKhamBenh.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnKhamBenh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnKhamBenh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnKhamBenh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnKhamBenh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnKhamBenh.FillColor = System.Drawing.Color.Transparent;
			this.btnKhamBenh.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnKhamBenh.ForeColor = System.Drawing.Color.White;
			this.btnKhamBenh.Location = new System.Drawing.Point(3, 535);
			this.btnKhamBenh.Name = "btnKhamBenh";
			this.btnKhamBenh.Size = new System.Drawing.Size(203, 70);
			this.btnKhamBenh.TabIndex = 6;
			this.btnKhamBenh.Text = "Khám Bệnh";
			// 
			// btnThuoc
			// 
			this.btnThuoc.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnThuoc.BackColor = System.Drawing.Color.Transparent;
			this.btnThuoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnThuoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnThuoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnThuoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnThuoc.FillColor = System.Drawing.Color.Transparent;
			this.btnThuoc.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnThuoc.ForeColor = System.Drawing.Color.White;
			this.btnThuoc.Location = new System.Drawing.Point(3, 611);
			this.btnThuoc.Name = "btnThuoc";
			this.btnThuoc.Size = new System.Drawing.Size(203, 70);
			this.btnThuoc.TabIndex = 7;
			this.btnThuoc.Text = "Thuốc";
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1600, 900);
			this.Controls.Add(this.tlMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(1024, 768);
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form Main";
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.tlMain.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlSidebar.ResumeLayout(false);
			this.flowSidebar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlMain;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private System.Windows.Forms.FlowLayoutPanel flowSidebar;
		private Guna.UI2.WinForms.Guna2Button btnTaiKhoan;
		private Guna.UI2.WinForms.Guna2Button btnBenhNhan;
		private Guna.UI2.WinForms.Guna2Button btnNhanVien;
		private Guna.UI2.WinForms.Guna2Button btnCSVC;
		private Guna.UI2.WinForms.Guna2Button btnLichLamViec;
		private Guna.UI2.WinForms.Guna2Button btnLichKhamBenh;
		private Guna.UI2.WinForms.Guna2Button btnKhamBenh;
		private Guna.UI2.WinForms.Guna2Button btnThuoc;
	}
}

