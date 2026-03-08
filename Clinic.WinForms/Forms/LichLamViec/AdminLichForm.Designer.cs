namespace Clinic.WinForms.Forms.LichLamViec
{
	partial class AdminLichForm
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tabAdminLich = new System.Windows.Forms.TabControl();
			this.pageThemLich = new System.Windows.Forms.TabPage();
			this.pnlContent = new System.Windows.Forms.Panel();
			this.dgvLichTam = new Guna.UI2.WinForms.Guna2DataGridView();
			this.pnlControl = new System.Windows.Forms.Panel();
			this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbGhiChu = new System.Windows.Forms.Label();
			this.lbCaLam = new System.Windows.Forms.Label();
			this.cbbCaLamViec = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lbChucVu = new System.Windows.Forms.Label();
			this.lbNgayTaoLich = new System.Windows.Forms.Label();
			this.dtpNgayTaoLich = new System.Windows.Forms.DateTimePicker();
			this.lbNhanVien = new System.Windows.Forms.Label();
			this.cbbChucVu = new Guna.UI2.WinForms.Guna2ComboBox();
			this.cbbNhanVien = new Guna.UI2.WinForms.Guna2ComboBox();
			this.pageNgayNghi = new System.Windows.Forms.TabPage();
			this.dgvNgayNghiNhanVien = new Guna.UI2.WinForms.Guna2DataGridView();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.lbHeaderdgv = new System.Windows.Forms.Label();
			this.cbbMonth = new Guna.UI2.WinForms.Guna2ComboBox();
			this.SearchTimer = new System.Windows.Forms.Timer(this.components);
			this.cbbYear = new Guna.UI2.WinForms.Guna2ComboBox();
			this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
			this.btnAddNgayNghi = new Guna.UI2.WinForms.Guna2Button();
			this.tabAdminLich.SuspendLayout();
			this.pageThemLich.SuspendLayout();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvLichTam)).BeginInit();
			this.pnlControl.SuspendLayout();
			this.pageNgayNghi.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvNgayNghiNhanVien)).BeginInit();
			this.pnlTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabAdminLich
			// 
			this.tabAdminLich.Controls.Add(this.pageThemLich);
			this.tabAdminLich.Controls.Add(this.pageNgayNghi);
			this.tabAdminLich.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabAdminLich.Location = new System.Drawing.Point(0, 0);
			this.tabAdminLich.Margin = new System.Windows.Forms.Padding(6);
			this.tabAdminLich.Name = "tabAdminLich";
			this.tabAdminLich.SelectedIndex = 0;
			this.tabAdminLich.Size = new System.Drawing.Size(1093, 662);
			this.tabAdminLich.TabIndex = 0;
			// 
			// pageThemLich
			// 
			this.pageThemLich.Controls.Add(this.pnlContent);
			this.pageThemLich.Controls.Add(this.pnlControl);
			this.pageThemLich.Location = new System.Drawing.Point(4, 37);
			this.pageThemLich.Margin = new System.Windows.Forms.Padding(6);
			this.pageThemLich.Name = "pageThemLich";
			this.pageThemLich.Padding = new System.Windows.Forms.Padding(6);
			this.pageThemLich.Size = new System.Drawing.Size(1085, 621);
			this.pageThemLich.TabIndex = 0;
			this.pageThemLich.Text = "Thêm lịch làm việc";
			this.pageThemLich.UseVisualStyleBackColor = true;
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.dgvLichTam);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Location = new System.Drawing.Point(6, 198);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1073, 417);
			this.pnlContent.TabIndex = 1;
			// 
			// dgvLichTam
			// 
			this.dgvLichTam.AllowUserToAddRows = false;
			dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
			this.dgvLichTam.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
			this.dgvLichTam.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle22.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle22.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvLichTam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
			this.dgvLichTam.ColumnHeadersHeight = 4;
			this.dgvLichTam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle23.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle23.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvLichTam.DefaultCellStyle = dataGridViewCellStyle23;
			this.dgvLichTam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvLichTam.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvLichTam.Location = new System.Drawing.Point(0, 0);
			this.dgvLichTam.MultiSelect = false;
			this.dgvLichTam.Name = "dgvLichTam";
			this.dgvLichTam.ReadOnly = true;
			this.dgvLichTam.RowHeadersVisible = false;
			dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
			this.dgvLichTam.RowsDefaultCellStyle = dataGridViewCellStyle24;
			this.dgvLichTam.RowTemplate.Height = 32;
			this.dgvLichTam.Size = new System.Drawing.Size(1073, 417);
			this.dgvLichTam.TabIndex = 1;
			this.dgvLichTam.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvLichTam.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvLichTam.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvLichTam.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvLichTam.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvLichTam.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvLichTam.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvLichTam.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvLichTam.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvLichTam.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvLichTam.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvLichTam.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvLichTam.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvLichTam.ThemeStyle.ReadOnly = true;
			this.dgvLichTam.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvLichTam.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvLichTam.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvLichTam.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvLichTam.ThemeStyle.RowsStyle.Height = 32;
			this.dgvLichTam.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvLichTam.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvLichTam.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichTam_CellClick);
			// 
			// pnlControl
			// 
			this.pnlControl.Controls.Add(this.btnXoa);
			this.pnlControl.Controls.Add(this.btnAdd);
			this.pnlControl.Controls.Add(this.btnLuu);
			this.pnlControl.Controls.Add(this.txtGhiChu);
			this.pnlControl.Controls.Add(this.lbGhiChu);
			this.pnlControl.Controls.Add(this.lbCaLam);
			this.pnlControl.Controls.Add(this.cbbCaLamViec);
			this.pnlControl.Controls.Add(this.lbChucVu);
			this.pnlControl.Controls.Add(this.lbNgayTaoLich);
			this.pnlControl.Controls.Add(this.dtpNgayTaoLich);
			this.pnlControl.Controls.Add(this.lbNhanVien);
			this.pnlControl.Controls.Add(this.cbbChucVu);
			this.pnlControl.Controls.Add(this.cbbNhanVien);
			this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlControl.Location = new System.Drawing.Point(6, 6);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Size = new System.Drawing.Size(1073, 192);
			this.pnlControl.TabIndex = 0;
			// 
			// txtGhiChu
			// 
			this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
			this.txtGhiChu.Location = new System.Drawing.Point(145, 131);
			this.txtGhiChu.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.txtGhiChu.Name = "txtGhiChu";
			this.txtGhiChu.PlaceholderText = "";
			this.txtGhiChu.SelectedText = "";
			this.txtGhiChu.Size = new System.Drawing.Size(470, 40);
			this.txtGhiChu.TabIndex = 51;
			// 
			// lbGhiChu
			// 
			this.lbGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbGhiChu.AutoSize = true;
			this.lbGhiChu.Location = new System.Drawing.Point(21, 137);
			this.lbGhiChu.Name = "lbGhiChu";
			this.lbGhiChu.Size = new System.Drawing.Size(99, 28);
			this.lbGhiChu.TabIndex = 50;
			this.lbGhiChu.Text = "Ghi chú:";
			// 
			// lbCaLam
			// 
			this.lbCaLam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbCaLam.AutoSize = true;
			this.lbCaLam.Location = new System.Drawing.Point(521, 73);
			this.lbCaLam.Name = "lbCaLam";
			this.lbCaLam.Size = new System.Drawing.Size(140, 28);
			this.lbCaLam.TabIndex = 49;
			this.lbCaLam.Text = "Chọn ca làm:";
			// 
			// cbbCaLamViec
			// 
			this.cbbCaLamViec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.cbbCaLamViec.BackColor = System.Drawing.Color.Transparent;
			this.cbbCaLamViec.BorderRadius = 15;
			this.cbbCaLamViec.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbCaLamViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbCaLamViec.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbCaLamViec.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbCaLamViec.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbCaLamViec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbCaLamViec.ItemHeight = 30;
			this.cbbCaLamViec.Location = new System.Drawing.Point(667, 73);
			this.cbbCaLamViec.Name = "cbbCaLamViec";
			this.cbbCaLamViec.Size = new System.Drawing.Size(252, 36);
			this.cbbCaLamViec.TabIndex = 48;
			// 
			// lbChucVu
			// 
			this.lbChucVu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbChucVu.AutoSize = true;
			this.lbChucVu.Location = new System.Drawing.Point(437, 16);
			this.lbChucVu.Name = "lbChucVu";
			this.lbChucVu.Size = new System.Drawing.Size(156, 28);
			this.lbChucVu.TabIndex = 47;
			this.lbChucVu.Text = "Chọn chức vụ:";
			// 
			// lbNgayTaoLich
			// 
			this.lbNgayTaoLich.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbNgayTaoLich.AutoSize = true;
			this.lbNgayTaoLich.Location = new System.Drawing.Point(21, 16);
			this.lbNgayTaoLich.Name = "lbNgayTaoLich";
			this.lbNgayTaoLich.Size = new System.Drawing.Size(126, 28);
			this.lbNgayTaoLich.TabIndex = 46;
			this.lbNgayTaoLich.Text = "Chọn ngày:";
			// 
			// dtpNgayTaoLich
			// 
			this.dtpNgayTaoLich.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dtpNgayTaoLich.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgayTaoLich.Location = new System.Drawing.Point(167, 16);
			this.dtpNgayTaoLich.Name = "dtpNgayTaoLich";
			this.dtpNgayTaoLich.Size = new System.Drawing.Size(225, 36);
			this.dtpNgayTaoLich.TabIndex = 45;
			this.dtpNgayTaoLich.Value = new System.DateTime(2026, 2, 25, 0, 0, 0, 0);
			// 
			// lbNhanVien
			// 
			this.lbNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbNhanVien.AutoSize = true;
			this.lbNhanVien.Location = new System.Drawing.Point(21, 73);
			this.lbNhanVien.Name = "lbNhanVien";
			this.lbNhanVien.Size = new System.Drawing.Size(179, 28);
			this.lbNhanVien.TabIndex = 44;
			this.lbNhanVien.Text = "Chọn nhân viên:";
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
			this.cbbChucVu.Location = new System.Drawing.Point(619, 16);
			this.cbbChucVu.Name = "cbbChucVu";
			this.cbbChucVu.Size = new System.Drawing.Size(300, 36);
			this.cbbChucVu.TabIndex = 43;
			this.cbbChucVu.SelectedIndexChanged += new System.EventHandler(this.cbbChucVu_SelectedIndexChanged);
			// 
			// cbbNhanVien
			// 
			this.cbbNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.cbbNhanVien.BackColor = System.Drawing.Color.Transparent;
			this.cbbNhanVien.BorderRadius = 15;
			this.cbbNhanVien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbNhanVien.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbNhanVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbNhanVien.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbNhanVien.ItemHeight = 30;
			this.cbbNhanVien.Location = new System.Drawing.Point(217, 73);
			this.cbbNhanVien.Name = "cbbNhanVien";
			this.cbbNhanVien.Size = new System.Drawing.Size(298, 36);
			this.cbbNhanVien.TabIndex = 42;
			// 
			// pageNgayNghi
			// 
			this.pageNgayNghi.Controls.Add(this.dgvNgayNghiNhanVien);
			this.pageNgayNghi.Controls.Add(this.pnlTop);
			this.pageNgayNghi.Location = new System.Drawing.Point(4, 37);
			this.pageNgayNghi.Margin = new System.Windows.Forms.Padding(6);
			this.pageNgayNghi.Name = "pageNgayNghi";
			this.pageNgayNghi.Padding = new System.Windows.Forms.Padding(6);
			this.pageNgayNghi.Size = new System.Drawing.Size(1085, 621);
			this.pageNgayNghi.TabIndex = 1;
			this.pageNgayNghi.Text = "Ngày nghỉ nhân viên";
			this.pageNgayNghi.UseVisualStyleBackColor = true;
			this.pageNgayNghi.Click += new System.EventHandler(this.pageNgayNghi_Click);
			// 
			// dgvNgayNghiNhanVien
			// 
			this.dgvNgayNghiNhanVien.AllowUserToAddRows = false;
			dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
			this.dgvNgayNghiNhanVien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
			this.dgvNgayNghiNhanVien.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvNgayNghiNhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
			this.dgvNgayNghiNhanVien.ColumnHeadersHeight = 4;
			this.dgvNgayNghiNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle19.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvNgayNghiNhanVien.DefaultCellStyle = dataGridViewCellStyle19;
			this.dgvNgayNghiNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvNgayNghiNhanVien.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvNgayNghiNhanVien.Location = new System.Drawing.Point(6, 102);
			this.dgvNgayNghiNhanVien.Margin = new System.Windows.Forms.Padding(2);
			this.dgvNgayNghiNhanVien.MultiSelect = false;
			this.dgvNgayNghiNhanVien.Name = "dgvNgayNghiNhanVien";
			this.dgvNgayNghiNhanVien.ReadOnly = true;
			this.dgvNgayNghiNhanVien.RowHeadersVisible = false;
			this.dgvNgayNghiNhanVien.RowHeadersWidth = 51;
			dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
			this.dgvNgayNghiNhanVien.RowsDefaultCellStyle = dataGridViewCellStyle20;
			this.dgvNgayNghiNhanVien.Size = new System.Drawing.Size(1073, 513);
			this.dgvNgayNghiNhanVien.TabIndex = 3;
			this.dgvNgayNghiNhanVien.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvNgayNghiNhanVien.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvNgayNghiNhanVien.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvNgayNghiNhanVien.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvNgayNghiNhanVien.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvNgayNghiNhanVien.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvNgayNghiNhanVien.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvNgayNghiNhanVien.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvNgayNghiNhanVien.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvNgayNghiNhanVien.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvNgayNghiNhanVien.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvNgayNghiNhanVien.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvNgayNghiNhanVien.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvNgayNghiNhanVien.ThemeStyle.ReadOnly = true;
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.Height = 22;
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvNgayNghiNhanVien.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.cbbYear);
			this.pnlTop.Controls.Add(this.btnAddNgayNghi);
			this.pnlTop.Controls.Add(this.lbHeaderdgv);
			this.pnlTop.Controls.Add(this.cbbMonth);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(6, 6);
			this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(1073, 96);
			this.pnlTop.TabIndex = 2;
			// 
			// lbHeaderdgv
			// 
			this.lbHeaderdgv.AutoSize = true;
			this.lbHeaderdgv.Location = new System.Drawing.Point(29, 34);
			this.lbHeaderdgv.Name = "lbHeaderdgv";
			this.lbHeaderdgv.Size = new System.Drawing.Size(330, 28);
			this.lbHeaderdgv.TabIndex = 33;
			this.lbHeaderdgv.Text = "Danh sách ngày nghỉ nhân viên";
			// 
			// cbbMonth
			// 
			this.cbbMonth.BackColor = System.Drawing.Color.Transparent;
			this.cbbMonth.BorderRadius = 15;
			this.cbbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbMonth.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbMonth.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbMonth.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbMonth.ItemHeight = 30;
			this.cbbMonth.Location = new System.Drawing.Point(386, 34);
			this.cbbMonth.Name = "cbbMonth";
			this.cbbMonth.Size = new System.Drawing.Size(156, 36);
			this.cbbMonth.TabIndex = 32;
			this.cbbMonth.SelectedIndexChanged += new System.EventHandler(this.cbbMonth_SelectedIndexChanged);
			// 
			// SearchTimer
			// 
			this.SearchTimer.Interval = 600;
			// 
			// cbbYear
			// 
			this.cbbYear.BackColor = System.Drawing.Color.Transparent;
			this.cbbYear.BorderRadius = 15;
			this.cbbYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbYear.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbYear.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbYear.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbYear.ItemHeight = 30;
			this.cbbYear.Location = new System.Drawing.Point(566, 34);
			this.cbbYear.Name = "cbbYear";
			this.cbbYear.Size = new System.Drawing.Size(185, 36);
			this.cbbYear.TabIndex = 35;
			this.cbbYear.SelectedIndexChanged += new System.EventHandler(this.cbbYear_SelectedIndexChanged);
			// 
			// btnXoa
			// 
			this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnXoa.BackColor = System.Drawing.Color.Transparent;
			this.btnXoa.BorderColor = System.Drawing.Color.Red;
			this.btnXoa.BorderRadius = 15;
			this.btnXoa.BorderThickness = 2;
			this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnXoa.FillColor = System.Drawing.Color.White;
			this.btnXoa.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnXoa.ForeColor = System.Drawing.Color.Black;
			this.btnXoa.Image = global::Clinic.WinForms.Properties.Resources.letter_x;
			this.btnXoa.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnXoa.ImageSize = new System.Drawing.Size(30, 30);
			this.btnXoa.Location = new System.Drawing.Point(951, 73);
			this.btnXoa.Name = "btnXoa";
			this.btnXoa.Size = new System.Drawing.Size(109, 45);
			this.btnXoa.TabIndex = 54;
			this.btnXoa.Text = "  Xóa";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.BackColor = System.Drawing.Color.Transparent;
			this.btnAdd.BorderColor = System.Drawing.Color.Blue;
			this.btnAdd.BorderRadius = 15;
			this.btnAdd.BorderThickness = 2;
			this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAdd.FillColor = System.Drawing.Color.White;
			this.btnAdd.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAdd.ForeColor = System.Drawing.Color.Black;
			this.btnAdd.Image = global::Clinic.WinForms.Properties.Resources.add;
			this.btnAdd.ImageOffset = new System.Drawing.Point(5, 0);
			this.btnAdd.Location = new System.Drawing.Point(951, 16);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(109, 45);
			this.btnAdd.TabIndex = 53;
			this.btnAdd.Text = "  Thêm";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
			this.btnLuu.Location = new System.Drawing.Point(951, 131);
			this.btnLuu.Name = "btnLuu";
			this.btnLuu.Size = new System.Drawing.Size(109, 45);
			this.btnLuu.TabIndex = 52;
			this.btnLuu.Text = "  Lưu";
			this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
			// 
			// btnAddNgayNghi
			// 
			this.btnAddNgayNghi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddNgayNghi.BackColor = System.Drawing.Color.Transparent;
			this.btnAddNgayNghi.BorderColor = System.Drawing.SystemColors.Control;
			this.btnAddNgayNghi.BorderRadius = 23;
			this.btnAddNgayNghi.BorderThickness = 1;
			this.btnAddNgayNghi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAddNgayNghi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAddNgayNghi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAddNgayNghi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAddNgayNghi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(222)))), ((int)(((byte)(129)))));
			this.btnAddNgayNghi.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold);
			this.btnAddNgayNghi.ForeColor = System.Drawing.Color.White;
			this.btnAddNgayNghi.Image = global::Clinic.WinForms.Properties.Resources.add_button;
			this.btnAddNgayNghi.ImageSize = new System.Drawing.Size(30, 30);
			this.btnAddNgayNghi.Location = new System.Drawing.Point(808, 31);
			this.btnAddNgayNghi.Name = "btnAddNgayNghi";
			this.btnAddNgayNghi.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnAddNgayNghi.PressedColor = System.Drawing.Color.LightGray;
			this.btnAddNgayNghi.Size = new System.Drawing.Size(248, 41);
			this.btnAddNgayNghi.TabIndex = 34;
			this.btnAddNgayNghi.Text = "Thêm ngày nghỉ";
			this.btnAddNgayNghi.Click += new System.EventHandler(this.btnAddNgayNghi_Click);
			// 
			// AdminLichForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1093, 662);
			this.Controls.Add(this.tabAdminLich);
			this.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "AdminLichForm";
			this.Text = "AdminLichForm";
			this.Load += new System.EventHandler(this.AdminLichForm_Load);
			this.tabAdminLich.ResumeLayout(false);
			this.pageThemLich.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvLichTam)).EndInit();
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.pageNgayNghi.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvNgayNghiNhanVien)).EndInit();
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabAdminLich;
		private System.Windows.Forms.TabPage pageThemLich;
		private System.Windows.Forms.TabPage pageNgayNghi;
		private System.Windows.Forms.Panel pnlContent;
		private System.Windows.Forms.Panel pnlControl;
		private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
		private System.Windows.Forms.Label lbGhiChu;
		private System.Windows.Forms.Label lbCaLam;
		private Guna.UI2.WinForms.Guna2ComboBox cbbCaLamViec;
		private System.Windows.Forms.Label lbChucVu;
		private System.Windows.Forms.Label lbNgayTaoLich;
		private System.Windows.Forms.DateTimePicker dtpNgayTaoLich;
		private System.Windows.Forms.Label lbNhanVien;
		private Guna.UI2.WinForms.Guna2ComboBox cbbChucVu;
		private Guna.UI2.WinForms.Guna2ComboBox cbbNhanVien;
		private Guna.UI2.WinForms.Guna2DataGridView dgvLichTam;
		private Guna.UI2.WinForms.Guna2Button btnXoa;
		private Guna.UI2.WinForms.Guna2Button btnAdd;
		private Guna.UI2.WinForms.Guna2Button btnLuu;
		private System.Windows.Forms.Timer SearchTimer;
		private Guna.UI2.WinForms.Guna2DataGridView dgvNgayNghiNhanVien;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Label lbHeaderdgv;
		private Guna.UI2.WinForms.Guna2ComboBox cbbMonth;
		private Guna.UI2.WinForms.Guna2Button btnAddNgayNghi;
		private Guna.UI2.WinForms.Guna2ComboBox cbbYear;
	}
}