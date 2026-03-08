namespace Clinic.WinForms.Forms.PhongChucNang
{
	partial class PhongChucNangForm
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
			this.dgvPhongChucNang = new Guna.UI2.WinForms.Guna2DataGridView();
			this.SearchTimer = new System.Windows.Forms.Timer(this.components);
			this.pnlContent = new System.Windows.Forms.Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlTool = new System.Windows.Forms.Panel();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
			this.btnRefesh = new Guna.UI2.WinForms.Guna2ImageButton();
			((System.ComponentModel.ISupportInitialize)(this.dgvPhongChucNang)).BeginInit();
			this.pnlContent.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvPhongChucNang
			// 
			this.dgvPhongChucNang.AllowUserToAddRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			this.dgvPhongChucNang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvPhongChucNang.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvPhongChucNang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvPhongChucNang.ColumnHeadersHeight = 4;
			this.dgvPhongChucNang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvPhongChucNang.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvPhongChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvPhongChucNang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvPhongChucNang.Location = new System.Drawing.Point(0, 0);
			this.dgvPhongChucNang.MultiSelect = false;
			this.dgvPhongChucNang.Name = "dgvPhongChucNang";
			this.dgvPhongChucNang.ReadOnly = true;
			this.dgvPhongChucNang.RowHeadersVisible = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			this.dgvPhongChucNang.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvPhongChucNang.RowTemplate.Height = 32;
			this.dgvPhongChucNang.Size = new System.Drawing.Size(1093, 553);
			this.dgvPhongChucNang.TabIndex = 0;
			this.dgvPhongChucNang.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvPhongChucNang.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvPhongChucNang.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvPhongChucNang.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvPhongChucNang.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvPhongChucNang.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvPhongChucNang.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvPhongChucNang.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvPhongChucNang.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvPhongChucNang.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvPhongChucNang.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvPhongChucNang.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvPhongChucNang.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvPhongChucNang.ThemeStyle.ReadOnly = true;
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.Height = 32;
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvPhongChucNang.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvPhongChucNang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhongChucNang_CellContentClick);
			this.dgvPhongChucNang.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPhongChucNang_CellFormatting);
			// 
			// SearchTimer
			// 
			this.SearchTimer.Interval = 600;
			this.SearchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.dgvPhongChucNang);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 69);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1093, 553);
			this.pnlContent.TabIndex = 8;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.pnlContent);
			this.pnlMain.Controls.Add(this.pnlTool);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1093, 622);
			this.pnlMain.TabIndex = 2;
			// 
			// pnlTool
			// 
			this.pnlTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(161)))), ((int)(((byte)(234)))));
			this.pnlTool.Controls.Add(this.txtSearch);
			this.pnlTool.Controls.Add(this.btnAdd);
			this.pnlTool.Controls.Add(this.btnRefesh);
			this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTool.Location = new System.Drawing.Point(0, 0);
			this.pnlTool.Name = "pnlTool";
			this.pnlTool.Size = new System.Drawing.Size(1093, 69);
			this.pnlTool.TabIndex = 7;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(161)))), ((int)(((byte)(234)))));
			this.btnAdd.BorderColor = System.Drawing.SystemColors.Control;
			this.btnAdd.BorderRadius = 23;
			this.btnAdd.BorderThickness = 1;
			this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(222)))), ((int)(((byte)(129)))));
			this.btnAdd.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold);
			this.btnAdd.ForeColor = System.Drawing.Color.White;
			this.btnAdd.ImageSize = new System.Drawing.Size(45, 45);
			this.btnAdd.Location = new System.Drawing.Point(847, 13);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnAdd.PressedColor = System.Drawing.Color.LightGray;
			this.btnAdd.Size = new System.Drawing.Size(233, 45);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "Thêm phòng";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.BorderRadius = 15;
			this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSearch.DefaultText = "";
			this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSearch.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSearch.IconLeftOffset = new System.Drawing.Point(20, 0);
			this.txtSearch.IconRight = global::Clinic.WinForms.Properties.Resources.search;
			this.txtSearch.IconRightSize = new System.Drawing.Size(40, 40);
			this.txtSearch.Location = new System.Drawing.Point(12, 15);
			this.txtSearch.Margin = new System.Windows.Forms.Padding(12, 6, 6, 6);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtSearch.PlaceholderText = "Tìm kiếm phòng chức năng...";
			this.txtSearch.SelectedText = "";
			this.txtSearch.Size = new System.Drawing.Size(368, 41);
			this.txtSearch.TabIndex = 8;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
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
			this.btnRefesh.Location = new System.Drawing.Point(794, 8);
			this.btnRefesh.Name = "btnRefesh";
			this.btnRefesh.PressedState.ImageSize = new System.Drawing.Size(45, 45);
			this.btnRefesh.Size = new System.Drawing.Size(55, 55);
			this.btnRefesh.TabIndex = 5;
			this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
			// 
			// PhongChucNangForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1093, 622);
			this.Controls.Add(this.pnlMain);
			this.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "PhongChucNangForm";
			this.Text = "PhongChucNangForm";
			this.Load += new System.EventHandler(this.PhongChucNangForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvPhongChucNang)).EndInit();
			this.pnlContent.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.pnlTool.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2DataGridView dgvPhongChucNang;
		private System.Windows.Forms.Timer SearchTimer;
		private System.Windows.Forms.Panel pnlContent;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Panel pnlTool;
		private Guna.UI2.WinForms.Guna2TextBox txtSearch;
		private Guna.UI2.WinForms.Guna2Button btnAdd;
		private Guna.UI2.WinForms.Guna2ImageButton btnRefesh;
	}
}