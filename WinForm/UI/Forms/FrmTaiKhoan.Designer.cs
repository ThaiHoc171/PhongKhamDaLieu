namespace GUI
{
	partial class FrmTaiKhoan
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.pnlContent = new System.Windows.Forms.Panel();
			this.tabCtrlQuanLiTaiKhoan = new System.Windows.Forms.TabControl();
			this.tpDSTaiKhoan = new System.Windows.Forms.TabPage();
			this.dgvDSTaiKhoan = new System.Windows.Forms.DataGridView();
			this.pnlAction = new System.Windows.Forms.Panel();
			this.txtNgayCapNhat = new System.Windows.Forms.TextBox();
			this.txtNgayTao = new System.Windows.Forms.TextBox();
			this.lblNgayCapNhat = new System.Windows.Forms.Label();
			this.lblNgayTao = new System.Windows.Forms.Label();
			this.cboStatus = new System.Windows.Forms.ComboBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.cboRole = new System.Windows.Forms.ComboBox();
			this.lblRole = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.lblInfo = new System.Windows.Forms.Label();
			this.tpThemTaiKhoan = new System.Windows.Forms.TabPage();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.tabCtrlQuanLiTaiKhoan.SuspendLayout();
			this.tpDSTaiKhoan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDSTaiKhoan)).BeginInit();
			this.pnlAction.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.lblTitle);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(1300, 44);
			this.pnlHeader.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblTitle.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(0, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(238, 37);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Quản lí tài khoản";
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.tabCtrlQuanLiTaiKhoan);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Location = new System.Drawing.Point(0, 44);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1300, 756);
			this.pnlContent.TabIndex = 1;
			// 
			// tabCtrlQuanLiTaiKhoan
			// 
			this.tabCtrlQuanLiTaiKhoan.Controls.Add(this.tpDSTaiKhoan);
			this.tabCtrlQuanLiTaiKhoan.Controls.Add(this.tpThemTaiKhoan);
			this.tabCtrlQuanLiTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabCtrlQuanLiTaiKhoan.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabCtrlQuanLiTaiKhoan.Location = new System.Drawing.Point(0, 0);
			this.tabCtrlQuanLiTaiKhoan.Name = "tabCtrlQuanLiTaiKhoan";
			this.tabCtrlQuanLiTaiKhoan.SelectedIndex = 0;
			this.tabCtrlQuanLiTaiKhoan.Size = new System.Drawing.Size(1300, 756);
			this.tabCtrlQuanLiTaiKhoan.TabIndex = 0;
			// 
			// tpDSTaiKhoan
			// 
			this.tpDSTaiKhoan.Controls.Add(this.dgvDSTaiKhoan);
			this.tpDSTaiKhoan.Controls.Add(this.pnlAction);
			this.tpDSTaiKhoan.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tpDSTaiKhoan.Location = new System.Drawing.Point(4, 35);
			this.tpDSTaiKhoan.Name = "tpDSTaiKhoan";
			this.tpDSTaiKhoan.Padding = new System.Windows.Forms.Padding(3);
			this.tpDSTaiKhoan.Size = new System.Drawing.Size(1292, 717);
			this.tpDSTaiKhoan.TabIndex = 0;
			this.tpDSTaiKhoan.Text = "Danh Sách Tài Khoản";
			this.tpDSTaiKhoan.UseVisualStyleBackColor = true;
			// 
			// dgvDSTaiKhoan
			// 
			this.dgvDSTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvDSTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvDSTaiKhoan.Location = new System.Drawing.Point(3, 3);
			this.dgvDSTaiKhoan.Name = "dgvDSTaiKhoan";
			this.dgvDSTaiKhoan.Size = new System.Drawing.Size(896, 711);
			this.dgvDSTaiKhoan.TabIndex = 2;
			// 
			// pnlAction
			// 
			this.pnlAction.Controls.Add(this.txtNgayCapNhat);
			this.pnlAction.Controls.Add(this.txtNgayTao);
			this.pnlAction.Controls.Add(this.lblNgayCapNhat);
			this.pnlAction.Controls.Add(this.lblNgayTao);
			this.pnlAction.Controls.Add(this.cboStatus);
			this.pnlAction.Controls.Add(this.lblStatus);
			this.pnlAction.Controls.Add(this.cboRole);
			this.pnlAction.Controls.Add(this.lblRole);
			this.pnlAction.Controls.Add(this.txtEmail);
			this.pnlAction.Controls.Add(this.lblEmail);
			this.pnlAction.Controls.Add(this.lblInfo);
			this.pnlAction.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlAction.Location = new System.Drawing.Point(899, 3);
			this.pnlAction.Name = "pnlAction";
			this.pnlAction.Size = new System.Drawing.Size(390, 711);
			this.pnlAction.TabIndex = 1;
			// 
			// txtNgayCapNhat
			// 
			this.txtNgayCapNhat.Location = new System.Drawing.Point(33, 450);
			this.txtNgayCapNhat.Name = "txtNgayCapNhat";
			this.txtNgayCapNhat.ReadOnly = true;
			this.txtNgayCapNhat.Size = new System.Drawing.Size(224, 36);
			this.txtNgayCapNhat.TabIndex = 15;
			// 
			// txtNgayTao
			// 
			this.txtNgayTao.Location = new System.Drawing.Point(33, 348);
			this.txtNgayTao.Name = "txtNgayTao";
			this.txtNgayTao.ReadOnly = true;
			this.txtNgayTao.Size = new System.Drawing.Size(224, 36);
			this.txtNgayTao.TabIndex = 14;
			// 
			// lblNgayCapNhat
			// 
			this.lblNgayCapNhat.AutoSize = true;
			this.lblNgayCapNhat.Location = new System.Drawing.Point(28, 419);
			this.lblNgayCapNhat.Name = "lblNgayCapNhat";
			this.lblNgayCapNhat.Size = new System.Drawing.Size(155, 28);
			this.lblNgayCapNhat.TabIndex = 12;
			this.lblNgayCapNhat.Text = "Ngày cập nhật";
			// 
			// lblNgayTao
			// 
			this.lblNgayTao.AutoSize = true;
			this.lblNgayTao.Location = new System.Drawing.Point(28, 317);
			this.lblNgayTao.Name = "lblNgayTao";
			this.lblNgayTao.Size = new System.Drawing.Size(107, 28);
			this.lblNgayTao.TabIndex = 9;
			this.lblNgayTao.Text = "Ngày Tạo";
			// 
			// cboStatus
			// 
			this.cboStatus.FormattingEnabled = true;
			this.cboStatus.Location = new System.Drawing.Point(33, 258);
			this.cboStatus.Name = "cboStatus";
			this.cboStatus.Size = new System.Drawing.Size(224, 36);
			this.cboStatus.TabIndex = 8;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(28, 227);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(115, 28);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "Trạng thái";
			// 
			// cboRole
			// 
			this.cboRole.FormattingEnabled = true;
			this.cboRole.Location = new System.Drawing.Point(33, 169);
			this.cboRole.Name = "cboRole";
			this.cboRole.Size = new System.Drawing.Size(224, 36);
			this.cboRole.TabIndex = 6;
			// 
			// lblRole
			// 
			this.lblRole.AutoSize = true;
			this.lblRole.Location = new System.Drawing.Point(28, 138);
			this.lblRole.Name = "lblRole";
			this.lblRole.Size = new System.Drawing.Size(81, 28);
			this.lblRole.TabIndex = 5;
			this.lblRole.Text = "Quyền";
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(33, 91);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(333, 36);
			this.txtEmail.TabIndex = 2;
			// 
			// lblEmail
			// 
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new System.Drawing.Point(28, 60);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(69, 28);
			this.lblEmail.TabIndex = 1;
			this.lblEmail.Text = "Email";
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.Location = new System.Drawing.Point(94, 14);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(210, 28);
			this.lblInfo.TabIndex = 0;
			this.lblInfo.Text = "Thông tin tài khoản";
			// 
			// tpThemTaiKhoan
			// 
			this.tpThemTaiKhoan.Location = new System.Drawing.Point(4, 35);
			this.tpThemTaiKhoan.Name = "tpThemTaiKhoan";
			this.tpThemTaiKhoan.Padding = new System.Windows.Forms.Padding(3);
			this.tpThemTaiKhoan.Size = new System.Drawing.Size(1292, 717);
			this.tpThemTaiKhoan.TabIndex = 1;
			this.tpThemTaiKhoan.Text = "Thêm Tài Khoản";
			this.tpThemTaiKhoan.UseVisualStyleBackColor = true;
			// 
			// FrmTaiKhoan
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1300, 800);
			this.Controls.Add(this.pnlContent);
			this.Controls.Add(this.pnlHeader);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmTaiKhoan";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FrmTaiKhoan";
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.pnlContent.ResumeLayout(false);
			this.tabCtrlQuanLiTaiKhoan.ResumeLayout(false);
			this.tpDSTaiKhoan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvDSTaiKhoan)).EndInit();
			this.pnlAction.ResumeLayout(false);
			this.pnlAction.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Panel pnlContent;
		private System.Windows.Forms.TabControl tabCtrlQuanLiTaiKhoan;
		private System.Windows.Forms.TabPage tpDSTaiKhoan;
		private System.Windows.Forms.TabPage tpThemTaiKhoan;
		private System.Windows.Forms.DataGridView dgvDSTaiKhoan;
		private System.Windows.Forms.Panel pnlAction;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.ComboBox cboRole;
		private System.Windows.Forms.Label lblRole;
		private System.Windows.Forms.ComboBox cboStatus;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TextBox txtNgayTao;
		private System.Windows.Forms.Label lblNgayCapNhat;
		private System.Windows.Forms.Label lblNgayTao;
		private System.Windows.Forms.TextBox txtNgayCapNhat;
	}
}