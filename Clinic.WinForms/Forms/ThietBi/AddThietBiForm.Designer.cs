namespace Clinic.WinForms.Forms.ThietBi
{
	partial class AddThietBiForm
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
			this.txtLoai = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtTen = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbLoai = new System.Windows.Forms.Label();
			this.lbTen = new System.Windows.Forms.Label();
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
			this.pnlMain.Controls.Add(this.txtLoai);
			this.pnlMain.Controls.Add(this.txtTen);
			this.pnlMain.Controls.Add(this.lbLoai);
			this.pnlMain.Controls.Add(this.lbTen);
			this.pnlMain.Controls.Add(this.lbHeader);
			this.pnlMain.Location = new System.Drawing.Point(32, 48);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(410, 260);
			this.pnlMain.TabIndex = 13;
			// 
			// txtLoai
			// 
			this.txtLoai.BorderRadius = 15;
			this.txtLoai.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtLoai.DefaultText = "";
			this.txtLoai.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtLoai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtLoai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLoai.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtLoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLoai.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLoai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtLoai.Location = new System.Drawing.Point(26, 173);
			this.txtLoai.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtLoai.Name = "txtLoai";
			this.txtLoai.PlaceholderText = "Nhập loại thiết bị";
			this.txtLoai.SelectedText = "";
			this.txtLoai.Size = new System.Drawing.Size(361, 40);
			this.txtLoai.TabIndex = 38;
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
			this.txtTen.PlaceholderText = "Nhập tên thiết bị";
			this.txtTen.SelectedText = "";
			this.txtTen.Size = new System.Drawing.Size(361, 40);
			this.txtTen.TabIndex = 37;
			// 
			// lbLoai
			// 
			this.lbLoai.AutoSize = true;
			this.lbLoai.Location = new System.Drawing.Point(14, 139);
			this.lbLoai.Name = "lbLoai";
			this.lbLoai.Size = new System.Drawing.Size(60, 28);
			this.lbLoai.TabIndex = 36;
			this.lbLoai.Text = "Loại:";
			// 
			// lbTen
			// 
			this.lbTen.AutoSize = true;
			this.lbTen.Location = new System.Drawing.Point(14, 48);
			this.lbTen.Name = "lbTen";
			this.lbTen.Size = new System.Drawing.Size(130, 28);
			this.lbTen.TabIndex = 34;
			this.lbTen.Text = "Tên thiết bị:";
			// 
			// lbHeader
			// 
			this.lbHeader.AutoSize = true;
			this.lbHeader.ForeColor = System.Drawing.Color.Black;
			this.lbHeader.Location = new System.Drawing.Point(110, 9);
			this.lbHeader.Name = "lbHeader";
			this.lbHeader.Size = new System.Drawing.Size(177, 28);
			this.lbHeader.TabIndex = 29;
			this.lbHeader.Text = "THÊM THIẾT BỊ";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(474, 42);
			this.pnlHeader.TabIndex = 10;
			// 
			// pnlContent
			// 
			this.pnlContent.BackColor = System.Drawing.Color.White;
			this.pnlContent.BorderColor = System.Drawing.Color.Black;
			this.pnlContent.BorderRadius = 15;
			this.pnlContent.BorderThickness = 3;
			this.pnlContent.Controls.Add(this.pnlMain);
			this.pnlContent.Controls.Add(this.btnLuu);
			this.pnlContent.Controls.Add(this.pnlHeader);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(474, 378);
			this.pnlContent.TabIndex = 2;
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
			this.btnLuu.Location = new System.Drawing.Point(331, 314);
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
			this.btnExit.Location = new System.Drawing.Point(422, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// AddThietBiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 378);
			this.Controls.Add(this.pnlContent);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddThietBiForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddThietBiForm";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2Panel pnlMain;
		private Guna.UI2.WinForms.Guna2TextBox txtLoai;
		private Guna.UI2.WinForms.Guna2TextBox txtTen;
		private System.Windows.Forms.Label lbLoai;
		private System.Windows.Forms.Label lbTen;
		private System.Windows.Forms.Label lbHeader;
		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
	}
}