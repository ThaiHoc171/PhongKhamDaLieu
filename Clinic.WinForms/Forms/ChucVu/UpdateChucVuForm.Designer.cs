namespace Clinic.WinForms.Forms.ChucVu
{
	partial class UpdateChucVuForm
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
			this.txtNgayTao = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbNgayTao = new System.Windows.Forms.Label();
			this.txtChucVuID = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbChucVuID = new System.Windows.Forms.Label();
			this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtTenChucVu = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbMoTa = new System.Windows.Forms.Label();
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
			this.pnlContent.Controls.Add(this.txtNgayTao);
			this.pnlContent.Controls.Add(this.lbNgayTao);
			this.pnlContent.Controls.Add(this.txtChucVuID);
			this.pnlContent.Controls.Add(this.lbChucVuID);
			this.pnlContent.Controls.Add(this.btnHuy);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.txtMoTa);
			this.pnlContent.Controls.Add(this.txtTenChucVu);
			this.pnlContent.Controls.Add(this.lbMoTa);
			this.pnlContent.Controls.Add(this.lbTenChucVu);
			this.pnlContent.Controls.Add(this.lbHeader);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(496, 536);
			this.pnlContent.TabIndex = 1;
			// 
			// txtNgayTao
			// 
			this.txtNgayTao.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtNgayTao.DefaultText = "";
			this.txtNgayTao.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtNgayTao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtNgayTao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtNgayTao.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtNgayTao.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtNgayTao.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNgayTao.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtNgayTao.Location = new System.Drawing.Point(71, 407);
			this.txtNgayTao.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtNgayTao.Name = "txtNgayTao";
			this.txtNgayTao.PlaceholderText = "ReadOnly";
			this.txtNgayTao.ReadOnly = true;
			this.txtNgayTao.SelectedText = "";
			this.txtNgayTao.Size = new System.Drawing.Size(386, 40);
			this.txtNgayTao.TabIndex = 14;
			this.txtNgayTao.TabStop = false;
			this.txtNgayTao.Enter += new System.EventHandler(this.txtNgayTao_Enter);
			// 
			// lbNgayTao
			// 
			this.lbNgayTao.AutoSize = true;
			this.lbNgayTao.Location = new System.Drawing.Point(36, 373);
			this.lbNgayTao.Name = "lbNgayTao";
			this.lbNgayTao.Size = new System.Drawing.Size(112, 28);
			this.lbNgayTao.TabIndex = 13;
			this.lbNgayTao.Text = "Ngày Tạo:";
			// 
			// txtChucVuID
			// 
			this.txtChucVuID.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtChucVuID.DefaultText = "";
			this.txtChucVuID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtChucVuID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtChucVuID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtChucVuID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtChucVuID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtChucVuID.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtChucVuID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtChucVuID.Location = new System.Drawing.Point(71, 124);
			this.txtChucVuID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtChucVuID.Name = "txtChucVuID";
			this.txtChucVuID.PlaceholderText = "ReadOnly";
			this.txtChucVuID.ReadOnly = true;
			this.txtChucVuID.SelectedText = "";
			this.txtChucVuID.Size = new System.Drawing.Size(386, 40);
			this.txtChucVuID.TabIndex = 12;
			this.txtChucVuID.TabStop = false;
			this.txtChucVuID.Enter += new System.EventHandler(this.txtChucVuID_Enter);
			// 
			// lbChucVuID
			// 
			this.lbChucVuID.AutoSize = true;
			this.lbChucVuID.Location = new System.Drawing.Point(36, 90);
			this.lbChucVuID.Name = "lbChucVuID";
			this.lbChucVuID.Size = new System.Drawing.Size(135, 28);
			this.lbChucVuID.TabIndex = 11;
			this.lbChucVuID.Text = "Chức Vụ ID:";
			// 
			// btnHuy
			// 
			this.btnHuy.BackColor = System.Drawing.Color.Transparent;
			this.btnHuy.BorderColor = System.Drawing.Color.Red;
			this.btnHuy.BorderRadius = 15;
			this.btnHuy.BorderThickness = 2;
			this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnHuy.FillColor = System.Drawing.Color.White;
			this.btnHuy.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHuy.ForeColor = System.Drawing.Color.Black;
			this.btnHuy.Image = global::Clinic.WinForms.Properties.Resources.letter_x;
			this.btnHuy.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnHuy.ImageSize = new System.Drawing.Size(30, 30);
			this.btnHuy.Location = new System.Drawing.Point(107, 469);
			this.btnHuy.Name = "btnHuy";
			this.btnHuy.Size = new System.Drawing.Size(122, 45);
			this.btnHuy.TabIndex = 9;
			this.btnHuy.Text = "  Hủy";
			this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
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
			this.btnLuu.Location = new System.Drawing.Point(293, 469);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// txtMoTa
			// 
			this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMoTa.DefaultText = "";
			this.txtMoTa.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtMoTa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtMoTa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMoTa.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMoTa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMoTa.Font = new System.Drawing.Font("Palatino Linotype", 15.75F);
			this.txtMoTa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMoTa.Location = new System.Drawing.Point(71, 285);
			this.txtMoTa.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtMoTa.Multiline = true;
			this.txtMoTa.Name = "txtMoTa";
			this.txtMoTa.PlaceholderText = "Nhập mô tả chức vụ";
			this.txtMoTa.SelectedText = "";
			this.txtMoTa.Size = new System.Drawing.Size(386, 79);
			this.txtMoTa.TabIndex = 7;
			// 
			// txtTenChucVu
			// 
			this.txtTenChucVu.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTenChucVu.DefaultText = "";
			this.txtTenChucVu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtTenChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtTenChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTenChucVu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtTenChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTenChucVu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTenChucVu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtTenChucVu.Location = new System.Drawing.Point(71, 205);
			this.txtTenChucVu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtTenChucVu.Name = "txtTenChucVu";
			this.txtTenChucVu.PlaceholderText = "Nhập tên chức vụ";
			this.txtTenChucVu.SelectedText = "";
			this.txtTenChucVu.Size = new System.Drawing.Size(386, 40);
			this.txtTenChucVu.TabIndex = 6;
			// 
			// lbMoTa
			// 
			this.lbMoTa.AutoSize = true;
			this.lbMoTa.Location = new System.Drawing.Point(36, 251);
			this.lbMoTa.Name = "lbMoTa";
			this.lbMoTa.Size = new System.Drawing.Size(73, 28);
			this.lbMoTa.TabIndex = 4;
			this.lbMoTa.Text = "Mô tả:";
			// 
			// lbTenChucVu
			// 
			this.lbTenChucVu.AutoSize = true;
			this.lbTenChucVu.Location = new System.Drawing.Point(36, 171);
			this.lbTenChucVu.Name = "lbTenChucVu";
			this.lbTenChucVu.Size = new System.Drawing.Size(147, 28);
			this.lbTenChucVu.TabIndex = 3;
			this.lbTenChucVu.Text = "Tên Chức Vụ:";
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(126, 45);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(255, 28);
			this.lbHeader.TabIndex = 2;
			this.lbHeader.Text = "ĐIỀU CHỈNH CHỨC VỤ";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(496, 42);
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
			this.btnExit.Location = new System.Drawing.Point(444, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// UpdateChucVuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 536);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "UpdateChucVuForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UpdateChucVuForm";
			this.Load += new System.EventHandler(this.UpdateChucVuForm_Load);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Button btnHuy;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
		private Guna.UI2.WinForms.Guna2TextBox txtTenChucVu;
		private System.Windows.Forms.Label lbMoTa;
		private System.Windows.Forms.Label lbTenChucVu;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2TextBox txtChucVuID;
		private System.Windows.Forms.Label lbChucVuID;
		private Guna.UI2.WinForms.Guna2TextBox txtNgayTao;
		private System.Windows.Forms.Label lbNgayTao;
	}
}