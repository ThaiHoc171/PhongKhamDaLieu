namespace Clinic.WinForms.Forms.LichLamViec
{
	partial class AddNgayNghiForm
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
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.dtpNgay = new System.Windows.Forms.DateTimePicker();
			this.lbNgay = new System.Windows.Forms.Label();
			this.lbChucVu = new System.Windows.Forms.Label();
			this.cbbChucVu = new Guna.UI2.WinForms.Guna2ComboBox();
			this.cbbNhanVien = new Guna.UI2.WinForms.Guna2ComboBox();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbLyDo = new System.Windows.Forms.Label();
			this.lbTenChucVu = new System.Windows.Forms.Label();
			this.lbHeader = new System.Windows.Forms.Label();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.pnlContent.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.Black;
			this.pnlContent.BorderRadius = 15;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.dtpNgay);
			this.pnlContent.Controls.Add(this.lbNgay);
			this.pnlContent.Controls.Add(this.lbChucVu);
			this.pnlContent.Controls.Add(this.cbbChucVu);
			this.pnlContent.Controls.Add(this.cbbNhanVien);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.txtLyDo);
			this.pnlContent.Controls.Add(this.lbLyDo);
			this.pnlContent.Controls.Add(this.lbTenChucVu);
			this.pnlContent.Controls.Add(this.lbHeader);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(495, 546);
			this.pnlContent.TabIndex = 1;
			// 
			// dtpNgay
			// 
			this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgay.Location = new System.Drawing.Point(230, 276);
			this.dtpNgay.Name = "dtpNgay";
			this.dtpNgay.Size = new System.Drawing.Size(226, 36);
			this.dtpNgay.TabIndex = 51;
			this.dtpNgay.Value = new System.DateTime(2026, 1, 1, 0, 0, 0, 0);
			// 
			// lbNgay
			// 
			this.lbNgay.AutoSize = true;
			this.lbNgay.Location = new System.Drawing.Point(39, 276);
			this.lbNgay.Name = "lbNgay";
			this.lbNgay.Size = new System.Drawing.Size(122, 28);
			this.lbNgay.TabIndex = 50;
			this.lbNgay.Text = "Ngày nghỉ:";
			this.lbNgay.Click += new System.EventHandler(this.lbNgay_Click);
			// 
			// lbChucVu
			// 
			this.lbChucVu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbChucVu.AutoSize = true;
			this.lbChucVu.Location = new System.Drawing.Point(39, 95);
			this.lbChucVu.Name = "lbChucVu";
			this.lbChucVu.Size = new System.Drawing.Size(156, 28);
			this.lbChucVu.TabIndex = 49;
			this.lbChucVu.Text = "Chọn chức vụ:";
			// 
			// cbbChucVu
			// 
			this.cbbChucVu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.cbbChucVu.BackColor = System.Drawing.Color.Transparent;
			this.cbbChucVu.BorderRadius = 15;
			this.cbbChucVu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbChucVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbChucVu.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbChucVu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbChucVu.ItemHeight = 30;
			this.cbbChucVu.Location = new System.Drawing.Point(70, 135);
			this.cbbChucVu.Name = "cbbChucVu";
			this.cbbChucVu.Size = new System.Drawing.Size(386, 36);
			this.cbbChucVu.TabIndex = 48;
			this.cbbChucVu.SelectedIndexChanged += new System.EventHandler(this.cbbChucVu_SelectedIndexChanged);
			// 
			// cbbNhanVien
			// 
			this.cbbNhanVien.BackColor = System.Drawing.Color.Transparent;
			this.cbbNhanVien.BorderRadius = 15;
			this.cbbNhanVien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbNhanVien.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbNhanVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbNhanVien.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbNhanVien.ItemHeight = 30;
			this.cbbNhanVien.Location = new System.Drawing.Point(70, 223);
			this.cbbNhanVien.Name = "cbbNhanVien";
			this.cbbNhanVien.Size = new System.Drawing.Size(386, 36);
			this.cbbNhanVien.TabIndex = 36;
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
			this.btnLuu.Location = new System.Drawing.Point(334, 467);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// txtLyDo
			// 
			this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtLyDo.DefaultText = "";
			this.txtLyDo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtLyDo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtLyDo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLyDo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLyDo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLyDo.Font = new System.Drawing.Font("Palatino Linotype", 15.75F);
			this.txtLyDo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLyDo.Location = new System.Drawing.Point(70, 370);
			this.txtLyDo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtLyDo.Multiline = true;
			this.txtLyDo.Name = "txtLyDo";
			this.txtLyDo.PlaceholderText = "Nhập lý do";
			this.txtLyDo.SelectedText = "";
			this.txtLyDo.Size = new System.Drawing.Size(386, 79);
			this.txtLyDo.TabIndex = 7;
			// 
			// lbLyDo
			// 
			this.lbLyDo.AutoSize = true;
			this.lbLyDo.Location = new System.Drawing.Point(39, 336);
			this.lbLyDo.Name = "lbLyDo";
			this.lbLyDo.Size = new System.Drawing.Size(71, 28);
			this.lbLyDo.TabIndex = 4;
			this.lbLyDo.Text = "Lý Do";
			// 
			// lbTenChucVu
			// 
			this.lbTenChucVu.AutoSize = true;
			this.lbTenChucVu.Location = new System.Drawing.Point(35, 182);
			this.lbTenChucVu.Name = "lbTenChucVu";
			this.lbTenChucVu.Size = new System.Drawing.Size(179, 28);
			this.lbTenChucVu.TabIndex = 3;
			this.lbTenChucVu.Text = "Chọn nhân viên:";
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(65, 45);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(351, 28);
			this.lbHeader.TabIndex = 2;
			this.lbHeader.Text = "THÊM NGÀY NGHỈ NHÂN VIÊN";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(495, 42);
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
			this.btnExit.Location = new System.Drawing.Point(443, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// AddNgayNghiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(495, 546);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddNgayNghiForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddNgayNghi";
			this.Load += new System.EventHandler(this.AddNgayNghiForm_Load);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2TextBox txtLyDo;
		private System.Windows.Forms.Label lbLyDo;
		private System.Windows.Forms.Label lbTenChucVu;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2ComboBox cbbNhanVien;
		private System.Windows.Forms.Label lbChucVu;
		private Guna.UI2.WinForms.Guna2ComboBox cbbChucVu;
		private System.Windows.Forms.DateTimePicker dtpNgay;
		private System.Windows.Forms.Label lbNgay;
	}
}