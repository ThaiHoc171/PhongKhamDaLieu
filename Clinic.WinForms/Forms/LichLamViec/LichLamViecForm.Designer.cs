namespace Clinic.WinForms.Forms.LichLamViec
{
	partial class LichLamViecForm
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
			this.pnlTop = new System.Windows.Forms.Panel();
			this.cbbWeek = new Guna.UI2.WinForms.Guna2ComboBox();
			this.cbbViewMode = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lbTue = new System.Windows.Forms.Label();
			this.pnlCommonView = new System.Windows.Forms.Panel();
			this.dgvLichLamViec = new Guna.UI2.WinForms.Guna2DataGridView();
			this.pnlPersonalView = new System.Windows.Forms.Panel();
			this.tblCalendar = new System.Windows.Forms.TableLayoutPanel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.lbSun = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.lbSat = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.lbFri = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.lbThu = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lbWed = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lbTues = new System.Windows.Forms.Label();
			this.pnlSun2 = new System.Windows.Forms.Panel();
			this.pnlSat2 = new System.Windows.Forms.Panel();
			this.pnlFri2 = new System.Windows.Forms.Panel();
			this.pnlThu2 = new System.Windows.Forms.Panel();
			this.pnlWed2 = new System.Windows.Forms.Panel();
			this.pnlTue2 = new System.Windows.Forms.Panel();
			this.pnlSun1 = new System.Windows.Forms.Panel();
			this.pnlSat1 = new System.Windows.Forms.Panel();
			this.pnlFri1 = new System.Windows.Forms.Panel();
			this.pnlThu1 = new System.Windows.Forms.Panel();
			this.pnlWed1 = new System.Windows.Forms.Panel();
			this.pnlTue1 = new System.Windows.Forms.Panel();
			this.pnlMon1 = new System.Windows.Forms.Panel();
			this.pnlMon2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lbMon = new System.Windows.Forms.Label();
			this.panel8 = new System.Windows.Forms.Panel();
			this.lbCaSang = new System.Windows.Forms.Label();
			this.panel9 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlTop.SuspendLayout();
			this.pnlCommonView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvLichLamViec)).BeginInit();
			this.pnlPersonalView.SuspendLayout();
			this.tblCalendar.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel9.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.cbbWeek);
			this.pnlTop.Controls.Add(this.cbbViewMode);
			this.pnlTop.Controls.Add(this.lbTue);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(1093, 88);
			this.pnlTop.TabIndex = 0;
			this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTop_Paint);
			// 
			// cbbWeek
			// 
			this.cbbWeek.BackColor = System.Drawing.Color.Transparent;
			this.cbbWeek.BorderRadius = 15;
			this.cbbWeek.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbWeek.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbWeek.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbWeek.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbWeek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbWeek.ItemHeight = 30;
			this.cbbWeek.Location = new System.Drawing.Point(237, 11);
			this.cbbWeek.Name = "cbbWeek";
			this.cbbWeek.Size = new System.Drawing.Size(289, 36);
			this.cbbWeek.TabIndex = 32;
			this.cbbWeek.SelectedIndexChanged += new System.EventHandler(this.cbbWeek_SelectedIndexChanged);
			// 
			// cbbViewMode
			// 
			this.cbbViewMode.BackColor = System.Drawing.Color.Transparent;
			this.cbbViewMode.BorderRadius = 15;
			this.cbbViewMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbViewMode.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbViewMode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cbbViewMode.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbbViewMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cbbViewMode.ItemHeight = 30;
			this.cbbViewMode.Location = new System.Drawing.Point(22, 11);
			this.cbbViewMode.Name = "cbbViewMode";
			this.cbbViewMode.Size = new System.Drawing.Size(186, 36);
			this.cbbViewMode.TabIndex = 31;
			this.cbbViewMode.SelectedIndexChanged += new System.EventHandler(this.cbbViewMode_SelectedIndexChanged);
			// 
			// lbTue
			// 
			this.lbTue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbTue.AutoSize = true;
			this.lbTue.Location = new System.Drawing.Point(139, 98);
			this.lbTue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbTue.Name = "lbTue";
			this.lbTue.Size = new System.Drawing.Size(42, 13);
			this.lbTue.TabIndex = 1;
			this.lbTue.Text = "Thứ Ba";
			// 
			// pnlCommonView
			// 
			this.pnlCommonView.Controls.Add(this.dgvLichLamViec);
			this.pnlCommonView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCommonView.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlCommonView.Location = new System.Drawing.Point(0, 88);
			this.pnlCommonView.Margin = new System.Windows.Forms.Padding(2);
			this.pnlCommonView.Name = "pnlCommonView";
			this.pnlCommonView.Size = new System.Drawing.Size(1093, 574);
			this.pnlCommonView.TabIndex = 2;
			// 
			// dgvLichLamViec
			// 
			this.dgvLichLamViec.AllowUserToAddRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			this.dgvLichLamViec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvLichLamViec.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvLichLamViec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvLichLamViec.ColumnHeadersHeight = 4;
			this.dgvLichLamViec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvLichLamViec.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvLichLamViec.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvLichLamViec.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvLichLamViec.Location = new System.Drawing.Point(0, 0);
			this.dgvLichLamViec.Margin = new System.Windows.Forms.Padding(2);
			this.dgvLichLamViec.MultiSelect = false;
			this.dgvLichLamViec.Name = "dgvLichLamViec";
			this.dgvLichLamViec.ReadOnly = true;
			this.dgvLichLamViec.RowHeadersVisible = false;
			this.dgvLichLamViec.RowHeadersWidth = 51;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			this.dgvLichLamViec.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvLichLamViec.Size = new System.Drawing.Size(1093, 574);
			this.dgvLichLamViec.TabIndex = 1;
			this.dgvLichLamViec.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvLichLamViec.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvLichLamViec.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvLichLamViec.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvLichLamViec.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvLichLamViec.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvLichLamViec.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvLichLamViec.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvLichLamViec.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvLichLamViec.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvLichLamViec.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvLichLamViec.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvLichLamViec.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvLichLamViec.ThemeStyle.ReadOnly = true;
			this.dgvLichLamViec.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvLichLamViec.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvLichLamViec.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvLichLamViec.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvLichLamViec.ThemeStyle.RowsStyle.Height = 22;
			this.dgvLichLamViec.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvLichLamViec.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvLichLamViec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichLamViec_CellContentClick);
			// 
			// pnlPersonalView
			// 
			this.pnlPersonalView.Controls.Add(this.tblCalendar);
			this.pnlPersonalView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPersonalView.Location = new System.Drawing.Point(0, 0);
			this.pnlPersonalView.Margin = new System.Windows.Forms.Padding(2);
			this.pnlPersonalView.Name = "pnlPersonalView";
			this.pnlPersonalView.Size = new System.Drawing.Size(1093, 662);
			this.pnlPersonalView.TabIndex = 3;
			// 
			// tblCalendar
			// 
			this.tblCalendar.BackColor = System.Drawing.Color.Gainsboro;
			this.tblCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tblCalendar.ColumnCount = 8;
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tblCalendar.Controls.Add(this.panel7, 7, 0);
			this.tblCalendar.Controls.Add(this.panel6, 6, 0);
			this.tblCalendar.Controls.Add(this.panel5, 5, 0);
			this.tblCalendar.Controls.Add(this.panel4, 4, 0);
			this.tblCalendar.Controls.Add(this.panel3, 3, 0);
			this.tblCalendar.Controls.Add(this.panel2, 2, 0);
			this.tblCalendar.Controls.Add(this.pnlSun2, 7, 2);
			this.tblCalendar.Controls.Add(this.pnlSat2, 6, 2);
			this.tblCalendar.Controls.Add(this.pnlFri2, 5, 2);
			this.tblCalendar.Controls.Add(this.pnlThu2, 4, 2);
			this.tblCalendar.Controls.Add(this.pnlWed2, 3, 2);
			this.tblCalendar.Controls.Add(this.pnlTue2, 2, 2);
			this.tblCalendar.Controls.Add(this.pnlSun1, 7, 1);
			this.tblCalendar.Controls.Add(this.pnlSat1, 6, 1);
			this.tblCalendar.Controls.Add(this.pnlFri1, 5, 1);
			this.tblCalendar.Controls.Add(this.pnlThu1, 4, 1);
			this.tblCalendar.Controls.Add(this.pnlWed1, 3, 1);
			this.tblCalendar.Controls.Add(this.pnlTue1, 2, 1);
			this.tblCalendar.Controls.Add(this.pnlMon1, 1, 1);
			this.tblCalendar.Controls.Add(this.pnlMon2, 1, 2);
			this.tblCalendar.Controls.Add(this.panel1, 1, 0);
			this.tblCalendar.Controls.Add(this.panel8, 0, 1);
			this.tblCalendar.Controls.Add(this.panel9, 0, 2);
			this.tblCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblCalendar.Location = new System.Drawing.Point(0, 0);
			this.tblCalendar.Margin = new System.Windows.Forms.Padding(2);
			this.tblCalendar.Name = "tblCalendar";
			this.tblCalendar.RowCount = 3;
			this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tblCalendar.Size = new System.Drawing.Size(1093, 662);
			this.tblCalendar.TabIndex = 1;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.lbSun);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel7.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel7.Location = new System.Drawing.Point(955, 3);
			this.panel7.Margin = new System.Windows.Forms.Padding(2);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(135, 127);
			this.panel7.TabIndex = 20;
			// 
			// lbSun
			// 
			this.lbSun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbSun.AutoSize = true;
			this.lbSun.Location = new System.Drawing.Point(-2, 30);
			this.lbSun.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbSun.Name = "lbSun";
			this.lbSun.Size = new System.Drawing.Size(113, 29);
			this.lbSun.TabIndex = 2;
			this.lbSun.Text = "Chủ Nhật";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.lbSat);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel6.Location = new System.Drawing.Point(819, 3);
			this.panel6.Margin = new System.Windows.Forms.Padding(2);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(131, 127);
			this.panel6.TabIndex = 19;
			// 
			// lbSat
			// 
			this.lbSat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbSat.AutoSize = true;
			this.lbSat.Location = new System.Drawing.Point(2, 30);
			this.lbSat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbSat.Name = "lbSat";
			this.lbSat.Size = new System.Drawing.Size(100, 29);
			this.lbSat.TabIndex = 2;
			this.lbSat.Text = "Thứ Bảy";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.lbFri);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel5.Location = new System.Drawing.Point(683, 3);
			this.panel5.Margin = new System.Windows.Forms.Padding(2);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(131, 127);
			this.panel5.TabIndex = 18;
			// 
			// lbFri
			// 
			this.lbFri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbFri.AutoSize = true;
			this.lbFri.Location = new System.Drawing.Point(5, 30);
			this.lbFri.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbFri.Name = "lbFri";
			this.lbFri.Size = new System.Drawing.Size(100, 29);
			this.lbFri.TabIndex = 2;
			this.lbFri.Text = "Thứ Sáu";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.lbThu);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel4.Location = new System.Drawing.Point(547, 3);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(131, 127);
			this.panel4.TabIndex = 17;
			// 
			// lbThu
			// 
			this.lbThu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbThu.AutoSize = true;
			this.lbThu.Location = new System.Drawing.Point(-3, 30);
			this.lbThu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbThu.Name = "lbThu";
			this.lbThu.Size = new System.Drawing.Size(110, 29);
			this.lbThu.TabIndex = 2;
			this.lbThu.Text = "Thứ Năm";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.lbWed);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel3.Location = new System.Drawing.Point(411, 3);
			this.panel3.Margin = new System.Windows.Forms.Padding(2);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(131, 127);
			this.panel3.TabIndex = 16;
			// 
			// lbWed
			// 
			this.lbWed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbWed.AutoSize = true;
			this.lbWed.Location = new System.Drawing.Point(7, 30);
			this.lbWed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbWed.Name = "lbWed";
			this.lbWed.Size = new System.Drawing.Size(91, 29);
			this.lbWed.TabIndex = 2;
			this.lbWed.Text = "Thứ Tư";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lbTues);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel2.Location = new System.Drawing.Point(275, 3);
			this.panel2.Margin = new System.Windows.Forms.Padding(2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(131, 127);
			this.panel2.TabIndex = 15;
			// 
			// lbTues
			// 
			this.lbTues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbTues.AutoSize = true;
			this.lbTues.Location = new System.Drawing.Point(9, 30);
			this.lbTues.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbTues.Name = "lbTues";
			this.lbTues.Size = new System.Drawing.Size(88, 29);
			this.lbTues.TabIndex = 2;
			this.lbTues.Text = "Thứ Ba";
			// 
			// pnlSun2
			// 
			this.pnlSun2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSun2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlSun2.Location = new System.Drawing.Point(955, 399);
			this.pnlSun2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSun2.Name = "pnlSun2";
			this.pnlSun2.Size = new System.Drawing.Size(135, 174);
			this.pnlSun2.TabIndex = 13;
			// 
			// pnlSat2
			// 
			this.pnlSat2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSat2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlSat2.Location = new System.Drawing.Point(819, 399);
			this.pnlSat2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSat2.Name = "pnlSat2";
			this.pnlSat2.Size = new System.Drawing.Size(131, 174);
			this.pnlSat2.TabIndex = 12;
			// 
			// pnlFri2
			// 
			this.pnlFri2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFri2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlFri2.Location = new System.Drawing.Point(683, 399);
			this.pnlFri2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlFri2.Name = "pnlFri2";
			this.pnlFri2.Size = new System.Drawing.Size(131, 174);
			this.pnlFri2.TabIndex = 11;
			// 
			// pnlThu2
			// 
			this.pnlThu2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlThu2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlThu2.Location = new System.Drawing.Point(547, 399);
			this.pnlThu2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlThu2.Name = "pnlThu2";
			this.pnlThu2.Size = new System.Drawing.Size(131, 174);
			this.pnlThu2.TabIndex = 10;
			// 
			// pnlWed2
			// 
			this.pnlWed2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlWed2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlWed2.Location = new System.Drawing.Point(411, 399);
			this.pnlWed2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlWed2.Name = "pnlWed2";
			this.pnlWed2.Size = new System.Drawing.Size(131, 174);
			this.pnlWed2.TabIndex = 9;
			// 
			// pnlTue2
			// 
			this.pnlTue2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTue2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlTue2.Location = new System.Drawing.Point(275, 399);
			this.pnlTue2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlTue2.Name = "pnlTue2";
			this.pnlTue2.Size = new System.Drawing.Size(131, 174);
			this.pnlTue2.TabIndex = 8;
			// 
			// pnlSun1
			// 
			this.pnlSun1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSun1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlSun1.Location = new System.Drawing.Point(955, 135);
			this.pnlSun1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSun1.Name = "pnlSun1";
			this.pnlSun1.Size = new System.Drawing.Size(135, 174);
			this.pnlSun1.TabIndex = 7;
			// 
			// pnlSat1
			// 
			this.pnlSat1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSat1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlSat1.Location = new System.Drawing.Point(819, 135);
			this.pnlSat1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSat1.Name = "pnlSat1";
			this.pnlSat1.Size = new System.Drawing.Size(131, 174);
			this.pnlSat1.TabIndex = 6;
			// 
			// pnlFri1
			// 
			this.pnlFri1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFri1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlFri1.Location = new System.Drawing.Point(683, 135);
			this.pnlFri1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlFri1.Name = "pnlFri1";
			this.pnlFri1.Size = new System.Drawing.Size(131, 174);
			this.pnlFri1.TabIndex = 5;
			// 
			// pnlThu1
			// 
			this.pnlThu1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlThu1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlThu1.Location = new System.Drawing.Point(547, 135);
			this.pnlThu1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlThu1.Name = "pnlThu1";
			this.pnlThu1.Size = new System.Drawing.Size(131, 174);
			this.pnlThu1.TabIndex = 4;
			// 
			// pnlWed1
			// 
			this.pnlWed1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlWed1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlWed1.Location = new System.Drawing.Point(411, 135);
			this.pnlWed1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlWed1.Name = "pnlWed1";
			this.pnlWed1.Size = new System.Drawing.Size(131, 174);
			this.pnlWed1.TabIndex = 3;
			// 
			// pnlTue1
			// 
			this.pnlTue1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTue1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlTue1.Location = new System.Drawing.Point(275, 135);
			this.pnlTue1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlTue1.Name = "pnlTue1";
			this.pnlTue1.Size = new System.Drawing.Size(131, 174);
			this.pnlTue1.TabIndex = 2;
			// 
			// pnlMon1
			// 
			this.pnlMon1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlMon1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlMon1.Location = new System.Drawing.Point(139, 135);
			this.pnlMon1.Margin = new System.Windows.Forms.Padding(2);
			this.pnlMon1.Name = "pnlMon1";
			this.pnlMon1.Size = new System.Drawing.Size(131, 174);
			this.pnlMon1.TabIndex = 0;
			// 
			// pnlMon2
			// 
			this.pnlMon2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlMon2.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlMon2.Location = new System.Drawing.Point(139, 399);
			this.pnlMon2.Margin = new System.Windows.Forms.Padding(2);
			this.pnlMon2.Name = "pnlMon2";
			this.pnlMon2.Size = new System.Drawing.Size(131, 174);
			this.pnlMon2.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lbMon);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel1.Location = new System.Drawing.Point(139, 3);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(131, 127);
			this.panel1.TabIndex = 14;
			// 
			// lbMon
			// 
			this.lbMon.AutoSize = true;
			this.lbMon.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbMon.Location = new System.Drawing.Point(3, 30);
			this.lbMon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbMon.Name = "lbMon";
			this.lbMon.Size = new System.Drawing.Size(98, 29);
			this.lbMon.TabIndex = 1;
			this.lbMon.Text = "Thứ Hai";
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.lbCaSang);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel8.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel8.Location = new System.Drawing.Point(3, 135);
			this.panel8.Margin = new System.Windows.Forms.Padding(2);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(131, 259);
			this.panel8.TabIndex = 21;
			// 
			// lbCaSang
			// 
			this.lbCaSang.AutoSize = true;
			this.lbCaSang.BackColor = System.Drawing.Color.Transparent;
			this.lbCaSang.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbCaSang.Location = new System.Drawing.Point(0, 0);
			this.lbCaSang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbCaSang.Name = "lbCaSang";
			this.lbCaSang.Size = new System.Drawing.Size(96, 29);
			this.lbCaSang.TabIndex = 3;
			this.lbCaSang.Text = "Ca Sáng";
			this.lbCaSang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.label1);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel9.Font = new System.Drawing.Font("Palatino Linotype", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel9.Location = new System.Drawing.Point(3, 399);
			this.panel9.Margin = new System.Windows.Forms.Padding(2);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(131, 260);
			this.panel9.TabIndex = 22;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 29);
			this.label1.TabIndex = 4;
			this.label1.Text = "Ca Chiều";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LichLamViecForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1093, 662);
			this.Controls.Add(this.pnlCommonView);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.pnlPersonalView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "LichLamViecForm";
			this.Text = "LichLamViecForm";
			this.Load += new System.EventHandler(this.LichLamViecForm_Load);
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.pnlCommonView.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvLichLamViec)).EndInit();
			this.pnlPersonalView.ResumeLayout(false);
			this.tblCalendar.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.panel7.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel8.ResumeLayout(false);
			this.panel8.PerformLayout();
			this.panel9.ResumeLayout(false);
			this.panel9.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlCommonView;
		private Guna.UI2.WinForms.Guna2ComboBox cbbWeek;
		private Guna.UI2.WinForms.Guna2ComboBox cbbViewMode;
		private Guna.UI2.WinForms.Guna2DataGridView dgvLichLamViec;
		private System.Windows.Forms.Label lbTue;
		private System.Windows.Forms.Panel pnlPersonalView;
		private System.Windows.Forms.TableLayoutPanel tblCalendar;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label lbSun;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label lbSat;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label lbFri;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label lbThu;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label lbWed;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lbTues;
		private System.Windows.Forms.Panel pnlSun2;
		private System.Windows.Forms.Panel pnlSat2;
		private System.Windows.Forms.Panel pnlFri2;
		private System.Windows.Forms.Panel pnlThu2;
		private System.Windows.Forms.Panel pnlWed2;
		private System.Windows.Forms.Panel pnlTue2;
		private System.Windows.Forms.Panel pnlSun1;
		private System.Windows.Forms.Panel pnlSat1;
		private System.Windows.Forms.Panel pnlFri1;
		private System.Windows.Forms.Panel pnlThu1;
		private System.Windows.Forms.Panel pnlWed1;
		private System.Windows.Forms.Panel pnlTue1;
		private System.Windows.Forms.Panel pnlMon1;
		private System.Windows.Forms.Panel pnlMon2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lbMon;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Label lbCaSang;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label label1;
	}
}