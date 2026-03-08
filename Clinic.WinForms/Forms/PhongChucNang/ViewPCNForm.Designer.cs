namespace Clinic.WinForms.Forms.PhongChucNang
{
	partial class ViewPCNForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.btnExit = new Guna.UI2.WinForms.Guna2CircleButton();
			this.dgvThietBiPhong = new Guna.UI2.WinForms.Guna2DataGridView();
			this.dgvChiTiet = new Guna.UI2.WinForms.Guna2DataGridView();
			this.tblView = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnlControl = new System.Windows.Forms.Panel();
			this.txtMaTaiSan = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbMaTaiSan = new System.Windows.Forms.Label();
			this.lbValuePhong = new System.Windows.Forms.Label();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbGhiChu = new System.Windows.Forms.Label();
			this.lbChucVu = new System.Windows.Forms.Label();
			this.lbPhong = new System.Windows.Forms.Label();
			this.cbbThietBi = new Guna.UI2.WinForms.Guna2ComboBox();
			this.pnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvThietBiPhong)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
			this.tblView.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Gainsboro;
			this.pnlHeader.Controls.Add(this.btnExit);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(1375, 42);
			this.pnlHeader.TabIndex = 11;
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
			this.btnExit.Location = new System.Drawing.Point(1323, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
			this.btnExit.Size = new System.Drawing.Size(52, 42);
			this.btnExit.TabIndex = 1;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// dgvThietBiPhong
			// 
			this.dgvThietBiPhong.AllowUserToAddRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			this.dgvThietBiPhong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvThietBiPhong.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvThietBiPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvThietBiPhong.ColumnHeadersHeight = 4;
			this.dgvThietBiPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvThietBiPhong.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvThietBiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvThietBiPhong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvThietBiPhong.Location = new System.Drawing.Point(3, 3);
			this.dgvThietBiPhong.MultiSelect = false;
			this.dgvThietBiPhong.Name = "dgvThietBiPhong";
			this.dgvThietBiPhong.ReadOnly = true;
			this.dgvThietBiPhong.RowHeadersVisible = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			this.dgvThietBiPhong.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvThietBiPhong.RowTemplate.Height = 32;
			this.dgvThietBiPhong.Size = new System.Drawing.Size(509, 442);
			this.dgvThietBiPhong.TabIndex = 3;
			this.dgvThietBiPhong.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvThietBiPhong.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvThietBiPhong.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvThietBiPhong.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvThietBiPhong.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvThietBiPhong.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvThietBiPhong.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvThietBiPhong.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvThietBiPhong.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvThietBiPhong.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvThietBiPhong.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvThietBiPhong.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvThietBiPhong.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvThietBiPhong.ThemeStyle.ReadOnly = true;
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.Height = 32;
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvThietBiPhong.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvThietBiPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThietBiPhong_CellContentClick);
			// 
			// dgvChiTiet
			// 
			this.dgvChiTiet.AllowUserToAddRows = false;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			this.dgvChiTiet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dgvChiTiet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvChiTiet.ColumnHeadersHeight = 4;
			this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle7;
			this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvChiTiet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvChiTiet.Location = new System.Drawing.Point(518, 3);
			this.dgvChiTiet.MultiSelect = false;
			this.dgvChiTiet.Name = "dgvChiTiet";
			this.dgvChiTiet.ReadOnly = true;
			this.dgvChiTiet.RowHeadersVisible = false;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			this.dgvChiTiet.RowsDefaultCellStyle = dataGridViewCellStyle8;
			this.dgvChiTiet.RowTemplate.Height = 32;
			this.dgvChiTiet.Size = new System.Drawing.Size(854, 442);
			this.dgvChiTiet.TabIndex = 4;
			this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvChiTiet.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvChiTiet.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvChiTiet.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvChiTiet.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvChiTiet.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvChiTiet.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvChiTiet.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvChiTiet.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvChiTiet.ThemeStyle.ReadOnly = true;
			this.dgvChiTiet.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvChiTiet.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvChiTiet.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvChiTiet.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvChiTiet.ThemeStyle.RowsStyle.Height = 32;
			this.dgvChiTiet.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvChiTiet.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvChiTiet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellClick);
			// 
			// tblView
			// 
			this.tblView.ColumnCount = 2;
			this.tblView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
			this.tblView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
			this.tblView.Controls.Add(this.dgvChiTiet, 1, 0);
			this.tblView.Controls.Add(this.dgvThietBiPhong, 0, 0);
			this.tblView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblView.Location = new System.Drawing.Point(0, 0);
			this.tblView.Name = "tblView";
			this.tblView.RowCount = 1;
			this.tblView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblView.Size = new System.Drawing.Size(1375, 448);
			this.tblView.TabIndex = 13;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tblView);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 174);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1375, 448);
			this.panel1.TabIndex = 14;
			// 
			// pnlControl
			// 
			this.pnlControl.Controls.Add(this.txtMaTaiSan);
			this.pnlControl.Controls.Add(this.lbMaTaiSan);
			this.pnlControl.Controls.Add(this.lbValuePhong);
			this.pnlControl.Controls.Add(this.btnLuu);
			this.pnlControl.Controls.Add(this.txtGhiChu);
			this.pnlControl.Controls.Add(this.lbGhiChu);
			this.pnlControl.Controls.Add(this.lbChucVu);
			this.pnlControl.Controls.Add(this.lbPhong);
			this.pnlControl.Controls.Add(this.cbbThietBi);
			this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlControl.Location = new System.Drawing.Point(0, 42);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Size = new System.Drawing.Size(1375, 132);
			this.pnlControl.TabIndex = 12;
			// 
			// txtMaTaiSan
			// 
			this.txtMaTaiSan.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtMaTaiSan.BorderRadius = 15;
			this.txtMaTaiSan.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMaTaiSan.DefaultText = "";
			this.txtMaTaiSan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtMaTaiSan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtMaTaiSan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMaTaiSan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtMaTaiSan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMaTaiSan.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMaTaiSan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtMaTaiSan.Location = new System.Drawing.Point(795, 49);
			this.txtMaTaiSan.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtMaTaiSan.Name = "txtMaTaiSan";
			this.txtMaTaiSan.PlaceholderText = "";
			this.txtMaTaiSan.SelectedText = "";
			this.txtMaTaiSan.Size = new System.Drawing.Size(438, 32);
			this.txtMaTaiSan.TabIndex = 66;
			// 
			// lbMaTaiSan
			// 
			this.lbMaTaiSan.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbMaTaiSan.AutoSize = true;
			this.lbMaTaiSan.Location = new System.Drawing.Point(668, 52);
			this.lbMaTaiSan.Name = "lbMaTaiSan";
			this.lbMaTaiSan.Size = new System.Drawing.Size(118, 28);
			this.lbMaTaiSan.TabIndex = 65;
			this.lbMaTaiSan.Text = "Mã tài sản:";
			// 
			// lbValuePhong
			// 
			this.lbValuePhong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbValuePhong.AutoSize = true;
			this.lbValuePhong.Location = new System.Drawing.Point(92, 6);
			this.lbValuePhong.Name = "lbValuePhong";
			this.lbValuePhong.Size = new System.Drawing.Size(66, 28);
			this.lbValuePhong.TabIndex = 64;
			this.lbValuePhong.Text = "value";
			// 
			// btnLuu
			// 
			this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
			this.btnLuu.Location = new System.Drawing.Point(1258, 18);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(109, 39);
			this.btnLuu.TabIndex = 63;
			this.btnLuu.Text = "  Lưu";
			// 
			// txtGhiChu
			// 
			this.txtGhiChu.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtGhiChu.BorderRadius = 15;
			this.txtGhiChu.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtGhiChu.DefaultText = "";
			this.txtGhiChu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtGhiChu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtGhiChu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtGhiChu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtGhiChu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtGhiChu.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGhiChu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtGhiChu.Location = new System.Drawing.Point(178, 94);
			this.txtGhiChu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtGhiChu.Name = "txtGhiChu";
			this.txtGhiChu.PlaceholderText = "";
			this.txtGhiChu.SelectedText = "";
			this.txtGhiChu.Size = new System.Drawing.Size(438, 34);
			this.txtGhiChu.TabIndex = 62;
			// 
			// lbGhiChu
			// 
			this.lbGhiChu.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbGhiChu.AutoSize = true;
			this.lbGhiChu.Location = new System.Drawing.Point(62, 97);
			this.lbGhiChu.Name = "lbGhiChu";
			this.lbGhiChu.Size = new System.Drawing.Size(99, 28);
			this.lbGhiChu.TabIndex = 61;
			this.lbGhiChu.Text = "Ghi chú:";
			// 
			// lbChucVu
			// 
			this.lbChucVu.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbChucVu.AutoSize = true;
			this.lbChucVu.Location = new System.Drawing.Point(30, 52);
			this.lbChucVu.Name = "lbChucVu";
			this.lbChucVu.Size = new System.Drawing.Size(148, 28);
			this.lbChucVu.TabIndex = 60;
			this.lbChucVu.Text = "Chọn thiết bị:";
			// 
			// lbPhong
			// 
			this.lbPhong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbPhong.AutoSize = true;
			this.lbPhong.Location = new System.Drawing.Point(7, 5);
			this.lbPhong.Name = "lbPhong";
			this.lbPhong.Size = new System.Drawing.Size(82, 28);
			this.lbPhong.TabIndex = 59;
			this.lbPhong.Text = "Phòng:";
			// 
			// cbbThietBi
			// 
			this.cbbThietBi.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cbbThietBi.BackColor = System.Drawing.Color.Transparent;
			this.cbbThietBi.BorderRadius = 15;
			this.cbbThietBi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbThietBi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbThietBi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbThietBi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbThietBi.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbThietBi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbThietBi.ItemHeight = 30;
			this.cbbThietBi.Location = new System.Drawing.Point(178, 50);
			this.cbbThietBi.Name = "cbbThietBi";
			this.cbbThietBi.Size = new System.Drawing.Size(438, 36);
			this.cbbThietBi.TabIndex = 58;
			// 
			// ViewPCNForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1375, 622);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pnlControl);
			this.Controls.Add(this.pnlHeader);
			this.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.Name = "ViewPCNForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ViewPCNForm";
			this.Load += new System.EventHandler(this.ViewPCNForm_Load);
			this.pnlHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvThietBiPhong)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
			this.tblView.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
		private Guna.UI2.WinForms.Guna2CircleButton btnExit;
		private Guna.UI2.WinForms.Guna2DataGridView dgvThietBiPhong;
		private Guna.UI2.WinForms.Guna2DataGridView dgvChiTiet;
		private System.Windows.Forms.TableLayoutPanel tblView;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlControl;
		private Guna.UI2.WinForms.Guna2TextBox txtMaTaiSan;
		private System.Windows.Forms.Label lbMaTaiSan;
		private System.Windows.Forms.Label lbValuePhong;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
		private System.Windows.Forms.Label lbGhiChu;
		private System.Windows.Forms.Label lbChucVu;
		private System.Windows.Forms.Label lbPhong;
		private Guna.UI2.WinForms.Guna2ComboBox cbbThietBi;
	}
}