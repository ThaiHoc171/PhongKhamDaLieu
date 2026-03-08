namespace Clinic.WinForms.Forms.CaKham
{
	partial class CaKhamForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvContent = new Guna.UI2.WinForms.Guna2DataGridView();
			this.SearchTimer = new System.Windows.Forms.Timer(this.components);
			this.pnlMain = new System.Windows.Forms.Panel();
			this.tblcontent = new System.Windows.Forms.TableLayoutPanel();
			this.pnlFooter = new System.Windows.Forms.Panel();
			this.txtSizePage = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbSizePage = new System.Windows.Forms.Label();
			this.lbcurrentPage = new System.Windows.Forms.Label();
			this.pnlTool = new System.Windows.Forms.Panel();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.cbbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
			this.dtpNgayKham = new System.Windows.Forms.DateTimePicker();
			this.cbbLoaiCaKham = new Guna.UI2.WinForms.Guna2ComboBox();
			this.btnEnd = new Guna.UI2.WinForms.Guna2Button();
			this.btnFirst = new Guna.UI2.WinForms.Guna2Button();
			this.btnPrevious = new Guna.UI2.WinForms.Guna2Button();
			this.btnNext = new Guna.UI2.WinForms.Guna2Button();
			this.btnRefesh = new Guna.UI2.WinForms.Guna2ImageButton();
			((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.tblcontent.SuspendLayout();
			this.pnlFooter.SuspendLayout();
			this.pnlTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvContent
			// 
			this.dgvContent.AllowUserToAddRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			this.dgvContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvContent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvContent.ColumnHeadersHeight = 4;
			this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvContent.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvContent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvContent.Location = new System.Drawing.Point(3, 3);
			this.dgvContent.MultiSelect = false;
			this.dgvContent.Name = "dgvContent";
			this.dgvContent.ReadOnly = true;
			this.dgvContent.RowHeadersVisible = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			this.dgvContent.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvContent.RowTemplate.Height = 32;
			this.dgvContent.Size = new System.Drawing.Size(1026, 494);
			this.dgvContent.TabIndex = 12;
			this.dgvContent.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvContent.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvContent.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvContent.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvContent.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvContent.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvContent.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvContent.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvContent.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvContent.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvContent.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvContent.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvContent.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvContent.ThemeStyle.ReadOnly = true;
			this.dgvContent.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvContent.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvContent.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvContent.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvContent.ThemeStyle.RowsStyle.Height = 32;
			this.dgvContent.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvContent.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvContent.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContent_CellClick);
			this.dgvContent.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvContent_Paint);
			// 
			// SearchTimer
			// 
			this.SearchTimer.Interval = 600;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tblcontent);
			this.pnlMain.Controls.Add(this.pnlTool);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1032, 622);
			this.pnlMain.TabIndex = 4;
			// 
			// tblcontent
			// 
			this.tblcontent.ColumnCount = 1;
			this.tblcontent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblcontent.Controls.Add(this.pnlFooter, 0, 1);
			this.tblcontent.Controls.Add(this.dgvContent, 0, 0);
			this.tblcontent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblcontent.Location = new System.Drawing.Point(0, 77);
			this.tblcontent.Name = "tblcontent";
			this.tblcontent.RowCount = 2;
			this.tblcontent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblcontent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tblcontent.Size = new System.Drawing.Size(1032, 545);
			this.tblcontent.TabIndex = 8;
			// 
			// pnlFooter
			// 
			this.pnlFooter.Controls.Add(this.txtSizePage);
			this.pnlFooter.Controls.Add(this.lbSizePage);
			this.pnlFooter.Controls.Add(this.btnEnd);
			this.pnlFooter.Controls.Add(this.btnFirst);
			this.pnlFooter.Controls.Add(this.btnPrevious);
			this.pnlFooter.Controls.Add(this.btnNext);
			this.pnlFooter.Controls.Add(this.lbcurrentPage);
			this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFooter.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlFooter.Location = new System.Drawing.Point(3, 503);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new System.Drawing.Size(1026, 39);
			this.pnlFooter.TabIndex = 14;
			// 
			// txtSizePage
			// 
			this.txtSizePage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.txtSizePage.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSizePage.DefaultText = "";
			this.txtSizePage.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtSizePage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtSizePage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSizePage.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSizePage.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSizePage.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSizePage.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSizePage.Location = new System.Drawing.Point(72, 4);
			this.txtSizePage.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.txtSizePage.Name = "txtSizePage";
			this.txtSizePage.PlaceholderText = "";
			this.txtSizePage.SelectedText = "";
			this.txtSizePage.Size = new System.Drawing.Size(76, 31);
			this.txtSizePage.TabIndex = 6;
			this.txtSizePage.TextChanged += new System.EventHandler(this.txtSizePage_TextChanged);
			// 
			// lbSizePage
			// 
			this.lbSizePage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbSizePage.Location = new System.Drawing.Point(12, 8);
			this.lbSizePage.Name = "lbSizePage";
			this.lbSizePage.Size = new System.Drawing.Size(70, 26);
			this.lbSizePage.TabIndex = 5;
			this.lbSizePage.Text = "Size: ";
			// 
			// lbcurrentPage
			// 
			this.lbcurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbcurrentPage.Location = new System.Drawing.Point(744, 6);
			this.lbcurrentPage.Name = "lbcurrentPage";
			this.lbcurrentPage.Size = new System.Drawing.Size(120, 26);
			this.lbcurrentPage.TabIndex = 0;
			this.lbcurrentPage.Text = "Page";
			// 
			// pnlTool
			// 
			this.pnlTool.BackColor = System.Drawing.Color.Silver;
			this.pnlTool.Controls.Add(this.btnAdd);
			this.pnlTool.Controls.Add(this.btnRefesh);
			this.pnlTool.Controls.Add(this.cbbTrangThai);
			this.pnlTool.Controls.Add(this.dtpNgayKham);
			this.pnlTool.Controls.Add(this.cbbLoaiCaKham);
			this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTool.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlTool.Location = new System.Drawing.Point(0, 0);
			this.pnlTool.Name = "pnlTool";
			this.pnlTool.Size = new System.Drawing.Size(1032, 77);
			this.pnlTool.TabIndex = 7;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.BackColor = System.Drawing.Color.Transparent;
			this.btnAdd.BorderColor = System.Drawing.SystemColors.Control;
			this.btnAdd.BorderRadius = 23;
			this.btnAdd.BorderThickness = 1;
			this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
			this.btnAdd.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold);
			this.btnAdd.ForeColor = System.Drawing.Color.Black;
			this.btnAdd.ImageSize = new System.Drawing.Size(45, 45);
			this.btnAdd.Location = new System.Drawing.Point(787, 14);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnAdd.PressedColor = System.Drawing.Color.LightGray;
			this.btnAdd.Size = new System.Drawing.Size(233, 47);
			this.btnAdd.TabIndex = 37;
			this.btnAdd.Text = "Tạo ca khám";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// cbbTrangThai
			// 
			this.cbbTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.cbbTrangThai.BackColor = System.Drawing.Color.Transparent;
			this.cbbTrangThai.BorderRadius = 15;
			this.cbbTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbTrangThai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbTrangThai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbTrangThai.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbTrangThai.ItemHeight = 30;
			this.cbbTrangThai.Location = new System.Drawing.Point(469, 19);
			this.cbbTrangThai.Name = "cbbTrangThai";
			this.cbbTrangThai.Size = new System.Drawing.Size(235, 36);
			this.cbbTrangThai.TabIndex = 35;
			this.cbbTrangThai.SelectedIndexChanged += new System.EventHandler(this.cbbTrangThai_SelectedIndexChanged);
			// 
			// dtpNgayKham
			// 
			this.dtpNgayKham.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dtpNgayKham.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpNgayKham.Location = new System.Drawing.Point(33, 19);
			this.dtpNgayKham.Name = "dtpNgayKham";
			this.dtpNgayKham.Size = new System.Drawing.Size(176, 36);
			this.dtpNgayKham.TabIndex = 34;
			this.dtpNgayKham.Value = new System.DateTime(2026, 3, 7, 0, 0, 0, 0);
			this.dtpNgayKham.ValueChanged += new System.EventHandler(this.dtpNgayKham_ValueChanged);
			// 
			// cbbLoaiCaKham
			// 
			this.cbbLoaiCaKham.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.cbbLoaiCaKham.BackColor = System.Drawing.Color.Transparent;
			this.cbbLoaiCaKham.BorderRadius = 15;
			this.cbbLoaiCaKham.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbLoaiCaKham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbLoaiCaKham.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbLoaiCaKham.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbLoaiCaKham.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbLoaiCaKham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbLoaiCaKham.ItemHeight = 30;
			this.cbbLoaiCaKham.Location = new System.Drawing.Point(231, 19);
			this.cbbLoaiCaKham.Name = "cbbLoaiCaKham";
			this.cbbLoaiCaKham.Size = new System.Drawing.Size(218, 36);
			this.cbbLoaiCaKham.TabIndex = 33;
			this.cbbLoaiCaKham.SelectedIndexChanged += new System.EventHandler(this.cbbLoaiCaKham_SelectedIndexChanged);
			// 
			// btnEnd
			// 
			this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEnd.BorderRadius = 15;
			this.btnEnd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnEnd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnEnd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnEnd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnEnd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
			this.btnEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnEnd.ForeColor = System.Drawing.Color.White;
			this.btnEnd.Image = global::Clinic.WinForms.Properties.Resources.right_end;
			this.btnEnd.ImageSize = new System.Drawing.Size(30, 30);
			this.btnEnd.Location = new System.Drawing.Point(934, 3);
			this.btnEnd.Name = "btnEnd";
			this.btnEnd.Size = new System.Drawing.Size(58, 32);
			this.btnEnd.TabIndex = 4;
			this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
			// 
			// btnFirst
			// 
			this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFirst.BorderRadius = 15;
			this.btnFirst.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnFirst.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnFirst.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnFirst.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnFirst.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
			this.btnFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnFirst.ForeColor = System.Drawing.Color.White;
			this.btnFirst.Image = global::Clinic.WinForms.Properties.Resources.left_end;
			this.btnFirst.ImageSize = new System.Drawing.Size(30, 30);
			this.btnFirst.Location = new System.Drawing.Point(616, 3);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(58, 32);
			this.btnFirst.TabIndex = 3;
			this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.BorderRadius = 15;
			this.btnPrevious.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnPrevious.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnPrevious.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnPrevious.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnPrevious.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
			this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnPrevious.ForeColor = System.Drawing.Color.White;
			this.btnPrevious.Image = global::Clinic.WinForms.Properties.Resources.left;
			this.btnPrevious.ImageSize = new System.Drawing.Size(30, 30);
			this.btnPrevious.Location = new System.Drawing.Point(680, 3);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(58, 32);
			this.btnPrevious.TabIndex = 2;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.BorderRadius = 15;
			this.btnNext.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnNext.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnNext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnNext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnNext.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
			this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnNext.ForeColor = System.Drawing.Color.White;
			this.btnNext.Image = global::Clinic.WinForms.Properties.Resources.right;
			this.btnNext.ImageSize = new System.Drawing.Size(30, 30);
			this.btnNext.Location = new System.Drawing.Point(870, 3);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(58, 32);
			this.btnNext.TabIndex = 1;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnRefesh
			// 
			this.btnRefesh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefesh.BackColor = System.Drawing.Color.Transparent;
			this.btnRefesh.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.btnRefesh.HoverState.ImageSize = new System.Drawing.Size(45, 45);
			this.btnRefesh.Image = global::Clinic.WinForms.Properties.Resources.refesh;
			this.btnRefesh.ImageOffset = new System.Drawing.Point(0, 0);
			this.btnRefesh.ImageRotate = 0F;
			this.btnRefesh.ImageSize = new System.Drawing.Size(40, 40);
			this.btnRefesh.Location = new System.Drawing.Point(726, 12);
			this.btnRefesh.Name = "btnRefesh";
			this.btnRefesh.PressedState.ImageSize = new System.Drawing.Size(45, 45);
			this.btnRefesh.Size = new System.Drawing.Size(55, 50);
			this.btnRefesh.TabIndex = 36;
			this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
			// 
			// CaKhamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1032, 622);
			this.Controls.Add(this.pnlMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CaKhamForm";
			this.Text = "CaKhamForm";
			this.Load += new System.EventHandler(this.CaKhamForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.tblcontent.ResumeLayout(false);
			this.pnlFooter.ResumeLayout(false);
			this.pnlTool.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2DataGridView dgvContent;
		private System.Windows.Forms.Timer SearchTimer;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.TableLayoutPanel tblcontent;
		private System.Windows.Forms.Panel pnlTool;
		private Guna.UI2.WinForms.Guna2ComboBox cbbLoaiCaKham;
		private System.Windows.Forms.DateTimePicker dtpNgayKham;
		private System.Windows.Forms.Panel pnlFooter;
		private Guna.UI2.WinForms.Guna2TextBox txtSizePage;
		private System.Windows.Forms.Label lbSizePage;
		private Guna.UI2.WinForms.Guna2Button btnEnd;
		private Guna.UI2.WinForms.Guna2Button btnFirst;
		private Guna.UI2.WinForms.Guna2Button btnPrevious;
		private Guna.UI2.WinForms.Guna2Button btnNext;
		private System.Windows.Forms.Label lbcurrentPage;
		private Guna.UI2.WinForms.Guna2ComboBox cbbTrangThai;
		private Guna.UI2.WinForms.Guna2Button btnAdd;
		private Guna.UI2.WinForms.Guna2ImageButton btnRefesh;
	}
}