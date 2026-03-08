namespace Clinic.WinForms
{
	partial class ChucVuForm
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
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlContent = new System.Windows.Forms.Panel();
			this.dgvChucVu = new Guna.UI2.WinForms.Guna2DataGridView();
			this.pnlTool = new System.Windows.Forms.Panel();
			this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.btnRefesh = new Guna.UI2.WinForms.Guna2ImageButton();
			this.pnlMain.SuspendLayout();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvChucVu)).BeginInit();
			this.pnlTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.pnlContent);
			this.pnlMain.Controls.Add(this.pnlTool);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1093, 662);
			this.pnlMain.TabIndex = 0;
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.dgvChucVu);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 69);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1093, 593);
			this.pnlContent.TabIndex = 8;
			// 
			// dgvChucVu
			// 
			this.dgvChucVu.AllowUserToAddRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			this.dgvChucVu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvChucVu.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvChucVu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvChucVu.ColumnHeadersHeight = 4;
			this.dgvChucVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvChucVu.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvChucVu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvChucVu.Location = new System.Drawing.Point(0, 0);
			this.dgvChucVu.MultiSelect = false;
			this.dgvChucVu.Name = "dgvChucVu";
			this.dgvChucVu.ReadOnly = true;
			this.dgvChucVu.RowHeadersVisible = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			this.dgvChucVu.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvChucVu.Size = new System.Drawing.Size(1093, 593);
			this.dgvChucVu.TabIndex = 0;
			this.dgvChucVu.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvChucVu.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvChucVu.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvChucVu.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvChucVu.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvChucVu.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvChucVu.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvChucVu.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvChucVu.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvChucVu.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvChucVu.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvChucVu.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvChucVu.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvChucVu.ThemeStyle.ReadOnly = true;
			this.dgvChucVu.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvChucVu.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvChucVu.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvChucVu.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvChucVu.ThemeStyle.RowsStyle.Height = 22;
			this.dgvChucVu.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvChucVu.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvChucVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChucVu_CellClick);
			this.dgvChucVu.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvChucVu_CellFormatting);
			this.dgvChucVu.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvChucVu_CellPainting);
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
			// txtSearch
			// 
			this.txtSearch.BorderRadius = 15;
			this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSearch.DefaultText = "";
			this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSearch.Enabled = false;
			this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSearch.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSearch.IconLeft = global::Clinic.WinForms.Properties.Resources.search;
			this.txtSearch.IconLeftOffset = new System.Drawing.Point(20, 0);
			this.txtSearch.Location = new System.Drawing.Point(12, 15);
			this.txtSearch.Margin = new System.Windows.Forms.Padding(12, 6, 6, 6);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtSearch.PlaceholderText = "Search...";
			this.txtSearch.SelectedText = "";
			this.txtSearch.Size = new System.Drawing.Size(368, 41);
			this.txtSearch.TabIndex = 8;
			this.txtSearch.Visible = false;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
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
			this.btnAdd.Image = global::Clinic.WinForms.Properties.Resources.add_button;
			this.btnAdd.ImageSize = new System.Drawing.Size(45, 45);
			this.btnAdd.Location = new System.Drawing.Point(868, 13);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnAdd.PressedColor = System.Drawing.Color.LightGray;
			this.btnAdd.Size = new System.Drawing.Size(212, 45);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "Thêm chức vụ";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
			this.btnRefesh.Location = new System.Drawing.Point(807, 8);
			this.btnRefesh.Name = "btnRefesh";
			this.btnRefesh.PressedState.ImageSize = new System.Drawing.Size(45, 45);
			this.btnRefesh.Size = new System.Drawing.Size(55, 55);
			this.btnRefesh.TabIndex = 5;
			this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
			// 
			// ChucVuForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.ClientSize = new System.Drawing.Size(1093, 662);
			this.Controls.Add(this.pnlMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ChucVuForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.ChucVuForm_Load);
			this.pnlMain.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvChucVu)).EndInit();
			this.pnlTool.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlMain;
		private Guna.UI2.WinForms.Guna2DataGridView dgvChucVu;
		private Guna.UI2.WinForms.Guna2ImageButton btnRefesh;
		private System.Windows.Forms.Panel pnlTool;
		private System.Windows.Forms.Panel pnlContent;
		private Guna.UI2.WinForms.Guna2Button btnAdd;
		private Guna.UI2.WinForms.Guna2TextBox txtSearch;
	}
}

