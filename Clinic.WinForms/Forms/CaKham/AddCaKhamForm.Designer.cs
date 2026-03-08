namespace Clinic.WinForms.Forms.CaKham
{
	partial class AddCaKhamForm
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
			this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
			this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
			this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
			this.lbNgayKetThuc = new System.Windows.Forms.Label();
			this.lbNgayBatDau = new System.Windows.Forms.Label();
			this.lbHeader = new System.Windows.Forms.Label();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.pnlMain.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlMain.Controls.Add(this.dtpNgayKetThuc);
			this.pnlMain.Controls.Add(this.dtpNgayBatDau);
			this.pnlMain.Controls.Add(this.lbNgayKetThuc);
			this.pnlMain.Controls.Add(this.lbNgayBatDau);
			this.pnlMain.Location = new System.Drawing.Point(27, 94);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(410, 120);
			this.pnlMain.TabIndex = 13;
			// 
			// dtpNgayKetThuc
			// 
			this.dtpNgayKetThuc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgayKetThuc.Location = new System.Drawing.Point(198, 66);
			this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
			this.dtpNgayKetThuc.Size = new System.Drawing.Size(176, 36);
			this.dtpNgayKetThuc.TabIndex = 42;
			this.dtpNgayKetThuc.Value = new System.DateTime(2026, 3, 7, 0, 0, 0, 0);
			// 
			// dtpNgayBatDau
			// 
			this.dtpNgayBatDau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgayBatDau.Location = new System.Drawing.Point(198, 14);
			this.dtpNgayBatDau.Name = "dtpNgayBatDau";
			this.dtpNgayBatDau.Size = new System.Drawing.Size(176, 36);
			this.dtpNgayBatDau.TabIndex = 41;
			this.dtpNgayBatDau.Value = new System.DateTime(2026, 3, 7, 0, 0, 0, 0);
			// 
			// lbNgayKetThuc
			// 
			this.lbNgayKetThuc.AutoSize = true;
			this.lbNgayKetThuc.Location = new System.Drawing.Point(22, 66);
			this.lbNgayKetThuc.Name = "lbNgayKetThuc";
			this.lbNgayKetThuc.Size = new System.Drawing.Size(156, 28);
			this.lbNgayKetThuc.TabIndex = 39;
			this.lbNgayKetThuc.Text = "Ngày kết thúc:";
			// 
			// lbNgayBatDau
			// 
			this.lbNgayBatDau.AutoSize = true;
			this.lbNgayBatDau.Location = new System.Drawing.Point(22, 20);
			this.lbNgayBatDau.Name = "lbNgayBatDau";
			this.lbNgayBatDau.Size = new System.Drawing.Size(149, 28);
			this.lbNgayBatDau.TabIndex = 34;
			this.lbNgayBatDau.Text = "Ngày bắt đầu:";
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(84, 51);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(260, 28);
			this.lbHeader.TabIndex = 29;
			this.lbHeader.Text = "TẠO CA KHÁM TRỐNG";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.LightGray;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(452, 42);
			this.pnlHeader.TabIndex = 10;
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.LightGray;
			this.pnlContent.BorderRadius = 3;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.pnlMain);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Controls.Add(this.lbHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(452, 275);
			this.pnlContent.TabIndex = 5;
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
			this.btnLuu.Location = new System.Drawing.Point(315, 220);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(122, 45);
			this.btnLuu.TabIndex = 8;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
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
			this.btnExit.Location = new System.Drawing.Point(400, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// AddCaKhamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(452, 275);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddCaKhamForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddCaKhamForm";
			this.Load += new System.EventHandler(this.AddCaKhamForm_Load);
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.pnlContent.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlMain;
		private System.Windows.Forms.Label lbNgayKetThuc;
		private System.Windows.Forms.Label lbNgayBatDau;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
		private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
	}
}