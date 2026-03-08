namespace Clinic.WinForms.Forms.CaKham
{
	partial class RegisterCaKhamForm
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
			this.lbMa = new System.Windows.Forms.Label();
			this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
			this.txtGhiGhu = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbGhiChu = new System.Windows.Forms.Label();
			this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbNgayKetThuc = new System.Windows.Forms.Label();
			this.lbName = new System.Windows.Forms.Label();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.lbHeader = new System.Windows.Forms.Label();
			this.cbbBenhNhan = new System.Windows.Forms.ComboBox();
			this.pnlContent.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.LightGray;
			this.pnlContent.BorderRadius = 3;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.lbMa);
			this.pnlContent.Controls.Add(this.pnlMain);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Controls.Add(this.lbHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(642, 380);
			this.pnlContent.TabIndex = 6;
			// 
			// lbMa
			// 
			this.lbMa.AutoSize = true;
			this.lbMa.Location = new System.Drawing.Point(351, 55);
			this.lbMa.Name = "lbMa";
			this.lbMa.Size = new System.Drawing.Size(66, 28);
			this.lbMa.TabIndex = 51;
			this.lbMa.Text = "value";
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlMain.Controls.Add(this.cbbBenhNhan);
			this.pnlMain.Controls.Add(this.txtGhiGhu);
			this.pnlMain.Controls.Add(this.lbGhiChu);
			this.pnlMain.Controls.Add(this.txtLyDo);
			this.pnlMain.Controls.Add(this.lbNgayKetThuc);
			this.pnlMain.Controls.Add(this.lbName);
			this.pnlMain.Location = new System.Drawing.Point(27, 94);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(591, 226);
			this.pnlMain.TabIndex = 13;
			// 
			// txtGhiGhu
			// 
			this.txtGhiGhu.BorderRadius = 15;
			this.txtGhiGhu.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtGhiGhu.DefaultText = "";
			this.txtGhiGhu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtGhiGhu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtGhiGhu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtGhiGhu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtGhiGhu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtGhiGhu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGhiGhu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtGhiGhu.Location = new System.Drawing.Point(27, 172);
			this.txtGhiGhu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtGhiGhu.Name = "txtGhiGhu";
			this.txtGhiGhu.PlaceholderText = "Nhập ghi chú";
			this.txtGhiGhu.SelectedText = "";
			this.txtGhiGhu.Size = new System.Drawing.Size(542, 40);
			this.txtGhiGhu.TabIndex = 46;
			// 
			// lbGhiChu
			// 
			this.lbGhiChu.AutoSize = true;
			this.lbGhiChu.Location = new System.Drawing.Point(22, 138);
			this.lbGhiChu.Name = "lbGhiChu";
			this.lbGhiChu.Size = new System.Drawing.Size(99, 28);
			this.lbGhiChu.TabIndex = 45;
			this.lbGhiChu.Text = "Ghi chú:";
			// 
			// txtLyDo
			// 
			this.txtLyDo.BorderRadius = 15;
			this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtLyDo.DefaultText = "";
			this.txtLyDo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtLyDo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtLyDo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLyDo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLyDo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLyDo.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLyDo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLyDo.Location = new System.Drawing.Point(27, 92);
			this.txtLyDo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtLyDo.Name = "txtLyDo";
			this.txtLyDo.PlaceholderText = "Nhập lý do";
			this.txtLyDo.SelectedText = "";
			this.txtLyDo.Size = new System.Drawing.Size(542, 40);
			this.txtLyDo.TabIndex = 44;
			// 
			// lbNgayKetThuc
			// 
			this.lbNgayKetThuc.AutoSize = true;
			this.lbNgayKetThuc.Location = new System.Drawing.Point(22, 58);
			this.lbNgayKetThuc.Name = "lbNgayKetThuc";
			this.lbNgayKetThuc.Size = new System.Drawing.Size(134, 28);
			this.lbNgayKetThuc.TabIndex = 39;
			this.lbNgayKetThuc.Text = "Lý do khám:";
			// 
			// lbName
			// 
			this.lbName.AutoSize = true;
			this.lbName.Location = new System.Drawing.Point(22, 20);
			this.lbName.Name = "lbName";
			this.lbName.Size = new System.Drawing.Size(128, 28);
			this.lbName.TabIndex = 34;
			this.lbName.Text = "Bệnh nhân:";
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
			this.btnLuu.Location = new System.Drawing.Point(496, 326);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.LightGray;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(642, 42);
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
			this.btnExit.Location = new System.Drawing.Point(590, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(107, 55);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(238, 28);
			this.lbHeader.TabIndex = 29;
			this.lbHeader.Text = "ĐĂNG KÝ CA KHÁM: ";
			// 
			// cbbBenhNhan
			// 
			this.cbbBenhNhan.DropDownHeight = 200;
			this.cbbBenhNhan.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbBenhNhan.FormattingEnabled = true;
			this.cbbBenhNhan.IntegralHeight = false;
			this.cbbBenhNhan.ItemHeight = 28;
			this.cbbBenhNhan.Location = new System.Drawing.Point(172, 17);
			this.cbbBenhNhan.MaxLength = 6;
			this.cbbBenhNhan.Name = "cbbBenhNhan";
			this.cbbBenhNhan.Size = new System.Drawing.Size(397, 36);
			this.cbbBenhNhan.TabIndex = 47;
			// 
			// RegisterCaKhamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.ClientSize = new System.Drawing.Size(642, 380);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "RegisterCaKhamForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RegisterCaKham";
			this.Load += new System.EventHandler(this.RegisterCaKhamForm_Load);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Panel pnlMain;
		private System.Windows.Forms.Label lbNgayKetThuc;
		private System.Windows.Forms.Label lbName;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Label lbMa;
		private Guna.UI2.WinForms.Guna2TextBox txtLyDo;
		private Guna.UI2.WinForms.Guna2TextBox txtGhiGhu;
		private System.Windows.Forms.Label lbGhiChu;
		private System.Windows.Forms.ComboBox cbbBenhNhan;
	}
}