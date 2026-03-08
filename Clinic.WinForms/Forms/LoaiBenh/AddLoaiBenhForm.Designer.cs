namespace Clinic.WinForms.Forms.LoaiBenh
{
	partial class AddLoaiBenhForm
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
			this.pnlThietBi = new Guna.UI2.WinForms.Guna2Panel();
			this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbMoTa = new System.Windows.Forms.Label();
			this.txtTen = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbTen = new System.Windows.Forms.Label();
			this.lbHeader = new System.Windows.Forms.Label();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.txtTenKhoaHoc = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbTenKhoaHoc = new System.Windows.Forms.Label();
			this.cbbNhomBenh = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lbNhomBenh = new System.Windows.Forms.Label();
			this.cbbMucDo = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lbMucDo = new System.Windows.Forms.Label();
			this.cbbDoPhoBien = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lbDoPhoBien = new System.Windows.Forms.Label();
			this.pnlThietBi.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlThietBi
			// 
			this.pnlThietBi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlThietBi.Controls.Add(this.cbbDoPhoBien);
			this.pnlThietBi.Controls.Add(this.lbDoPhoBien);
			this.pnlThietBi.Controls.Add(this.cbbMucDo);
			this.pnlThietBi.Controls.Add(this.lbMucDo);
			this.pnlThietBi.Controls.Add(this.cbbNhomBenh);
			this.pnlThietBi.Controls.Add(this.lbNhomBenh);
			this.pnlThietBi.Controls.Add(this.txtTenKhoaHoc);
			this.pnlThietBi.Controls.Add(this.lbTenKhoaHoc);
			this.pnlThietBi.Controls.Add(this.txtMoTa);
			this.pnlThietBi.Controls.Add(this.lbMoTa);
			this.pnlThietBi.Controls.Add(this.txtTen);
			this.pnlThietBi.Controls.Add(this.lbTen);
			this.pnlThietBi.Controls.Add(this.lbHeader);
			this.pnlThietBi.Location = new System.Drawing.Point(32, 48);
			this.pnlThietBi.Name = "pnlThietBi";
			this.pnlThietBi.Size = new System.Drawing.Size(862, 329);
			this.pnlThietBi.TabIndex = 13;
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
			this.txtMoTa.Location = new System.Drawing.Point(26, 169);
			this.txtMoTa.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtMoTa.Name = "txtMoTa";
			this.txtMoTa.PlaceholderText = "Nhập mô tả";
			this.txtMoTa.SelectedText = "";
			this.txtMoTa.Size = new System.Drawing.Size(361, 40);
			this.txtMoTa.TabIndex = 40;
			// 
			// lbMoTa
			// 
			this.lbMoTa.AutoSize = true;
			this.lbMoTa.Location = new System.Drawing.Point(14, 135);
			this.lbMoTa.Name = "lbMoTa";
			this.lbMoTa.Size = new System.Drawing.Size(73, 28);
			this.lbMoTa.TabIndex = 39;
			this.lbMoTa.Text = "Mô tả:";
			// 
			// txtTen
			// 
			this.txtTen.BorderRadius = 15;
			this.txtTen.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTen.DefaultText = "";
			this.txtTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTen.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTen.Location = new System.Drawing.Point(26, 82);
			this.txtTen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtTen.Name = "txtTen";
			this.txtTen.PlaceholderText = "Nhập tên bệnh";
			this.txtTen.SelectedText = "";
			this.txtTen.Size = new System.Drawing.Size(361, 40);
			this.txtTen.TabIndex = 37;
			// 
			// lbTen
			// 
			this.lbTen.AutoSize = true;
			this.lbTen.Location = new System.Drawing.Point(14, 48);
			this.lbTen.Name = "lbTen";
			this.lbTen.Size = new System.Drawing.Size(111, 28);
			this.lbTen.TabIndex = 34;
			this.lbTen.Text = "Tên bệnh:";
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(322, 11);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(206, 28);
			this.lbHeader.TabIndex = 29;
			this.lbHeader.Text = "THÊM LOẠI BỆNH";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(928, 42);
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
			this.btnExit.Location = new System.Drawing.Point(876, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.Black;
			this.pnlContent.BorderRadius = 15;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.pnlThietBi);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(928, 442);
			this.pnlContent.TabIndex = 4;
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
			this.btnLuu.Location = new System.Drawing.Point(772, 385);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// txtTenKhoaHoc
			// 
			this.txtTenKhoaHoc.BorderRadius = 15;
			this.txtTenKhoaHoc.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTenKhoaHoc.DefaultText = "";
			this.txtTenKhoaHoc.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtTenKhoaHoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtTenKhoaHoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTenKhoaHoc.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTenKhoaHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTenKhoaHoc.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTenKhoaHoc.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTenKhoaHoc.Location = new System.Drawing.Point(471, 82);
			this.txtTenKhoaHoc.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtTenKhoaHoc.Name = "txtTenKhoaHoc";
			this.txtTenKhoaHoc.PlaceholderText = "Nhập tên khoa học";
			this.txtTenKhoaHoc.SelectedText = "";
			this.txtTenKhoaHoc.Size = new System.Drawing.Size(361, 40);
			this.txtTenKhoaHoc.TabIndex = 42;
			// 
			// lbTenKhoaHoc
			// 
			this.lbTenKhoaHoc.AutoSize = true;
			this.lbTenKhoaHoc.Location = new System.Drawing.Point(459, 48);
			this.lbTenKhoaHoc.Name = "lbTenKhoaHoc";
			this.lbTenKhoaHoc.Size = new System.Drawing.Size(150, 28);
			this.lbTenKhoaHoc.TabIndex = 41;
			this.lbTenKhoaHoc.Text = "Tên khoa học:";
			// 
			// cbbNhomBenh
			// 
			this.cbbNhomBenh.BackColor = System.Drawing.Color.Transparent;
			this.cbbNhomBenh.BorderRadius = 15;
			this.cbbNhomBenh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbNhomBenh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbNhomBenh.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbNhomBenh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbNhomBenh.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbNhomBenh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbNhomBenh.ItemHeight = 30;
			this.cbbNhomBenh.Location = new System.Drawing.Point(471, 173);
			this.cbbNhomBenh.Name = "cbbNhomBenh";
			this.cbbNhomBenh.Size = new System.Drawing.Size(361, 36);
			this.cbbNhomBenh.TabIndex = 44;
			// 
			// lbNhomBenh
			// 
			this.lbNhomBenh.AutoSize = true;
			this.lbNhomBenh.Location = new System.Drawing.Point(459, 135);
			this.lbNhomBenh.Name = "lbNhomBenh";
			this.lbNhomBenh.Size = new System.Drawing.Size(137, 28);
			this.lbNhomBenh.TabIndex = 43;
			this.lbNhomBenh.Text = "Nhóm bệnh:";
			// 
			// cbbMucDo
			// 
			this.cbbMucDo.BackColor = System.Drawing.Color.Transparent;
			this.cbbMucDo.BorderRadius = 15;
			this.cbbMucDo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbMucDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbMucDo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbMucDo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbMucDo.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbMucDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbMucDo.ItemHeight = 30;
			this.cbbMucDo.Location = new System.Drawing.Point(26, 264);
			this.cbbMucDo.Name = "cbbMucDo";
			this.cbbMucDo.Size = new System.Drawing.Size(361, 36);
			this.cbbMucDo.TabIndex = 46;
			// 
			// lbMucDo
			// 
			this.lbMucDo.AutoSize = true;
			this.lbMucDo.Location = new System.Drawing.Point(14, 226);
			this.lbMucDo.Name = "lbMucDo";
			this.lbMucDo.Size = new System.Drawing.Size(232, 28);
			this.lbMucDo.TabIndex = 45;
			this.lbMucDo.Text = "Mức độ nghiêm trọng:";
			// 
			// cbbDoPhoBien
			// 
			this.cbbDoPhoBien.BackColor = System.Drawing.Color.Transparent;
			this.cbbDoPhoBien.BorderRadius = 15;
			this.cbbDoPhoBien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbDoPhoBien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbDoPhoBien.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbDoPhoBien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbDoPhoBien.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbDoPhoBien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbDoPhoBien.ItemHeight = 30;
			this.cbbDoPhoBien.Location = new System.Drawing.Point(471, 264);
			this.cbbDoPhoBien.Name = "cbbDoPhoBien";
			this.cbbDoPhoBien.Size = new System.Drawing.Size(361, 36);
			this.cbbDoPhoBien.TabIndex = 48;
			// 
			// lbDoPhoBien
			// 
			this.lbDoPhoBien.AutoSize = true;
			this.lbDoPhoBien.Location = new System.Drawing.Point(459, 226);
			this.lbDoPhoBien.Name = "lbDoPhoBien";
			this.lbDoPhoBien.Size = new System.Drawing.Size(140, 28);
			this.lbDoPhoBien.TabIndex = 47;
			this.lbDoPhoBien.Text = "Độ phổ biến:";
			// 
			// AddLoaiBenhForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(928, 442);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddLoaiBenhForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddLoaiBenhForm";
			this.pnlThietBi.ResumeLayout(false);
			this.pnlThietBi.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlThietBi;
		private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
		private System.Windows.Forms.Label lbMoTa;
		private Guna.UI2.WinForms.Guna2TextBox txtTen;
		private System.Windows.Forms.Label lbTen;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2TextBox txtTenKhoaHoc;
		private System.Windows.Forms.Label lbTenKhoaHoc;
		private Guna.UI2.WinForms.Guna2ComboBox cbbDoPhoBien;
		private System.Windows.Forms.Label lbDoPhoBien;
		private Guna.UI2.WinForms.Guna2ComboBox cbbMucDo;
		private System.Windows.Forms.Label lbMucDo;
		private Guna.UI2.WinForms.Guna2ComboBox cbbNhomBenh;
		private System.Windows.Forms.Label lbNhomBenh;
	}
}