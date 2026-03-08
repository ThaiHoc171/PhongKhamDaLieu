namespace Clinic.WinForms.Forms.ThietBi
{
	partial class ThietBiForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvThietBi = new Guna.UI2.WinForms.Guna2DataGridView();
			this.SearchTimer = new System.Windows.Forms.Timer(this.components);
			this.pnlContent = new System.Windows.Forms.Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlTool = new System.Windows.Forms.Panel();
			this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.btnRefesh = new Guna.UI2.WinForms.Guna2ImageButton();
			((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
			this.pnlContent.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlTool.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvThietBi
			// 
			this.dgvThietBi.AllowUserToAddRows = false;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			this.dgvThietBi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvThietBi.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvThietBi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.dgvThietBi.ColumnHeadersHeight = 4;
			this.dgvThietBi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvThietBi.DefaultCellStyle = dataGridViewCellStyle11;
			this.dgvThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvThietBi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvThietBi.Location = new System.Drawing.Point(0, 0);
			this.dgvThietBi.MultiSelect = false;
			this.dgvThietBi.Name = "dgvThietBi";
			this.dgvThietBi.ReadOnly = true;
			this.dgvThietBi.RowHeadersVisible = false;
			dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
			this.dgvThietBi.RowsDefaultCellStyle = dataGridViewCellStyle12;
			this.dgvThietBi.RowTemplate.Height = 32;
			this.dgvThietBi.Size = new System.Drawing.Size(1032, 553);
			this.dgvThietBi.TabIndex = 0;
			this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.Font = null;
			this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
			this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
			this.dgvThietBi.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
			this.dgvThietBi.ThemeStyle.BackColor = System.Drawing.Color.White;
			this.dgvThietBi.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
			this.dgvThietBi.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvThietBi.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvThietBi.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.dgvThietBi.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.dgvThietBi.ThemeStyle.HeaderStyle.Height = 4;
			this.dgvThietBi.ThemeStyle.ReadOnly = true;
			this.dgvThietBi.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.dgvThietBi.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvThietBi.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvThietBi.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvThietBi.ThemeStyle.RowsStyle.Height = 32;
			this.dgvThietBi.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
			this.dgvThietBi.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
			this.dgvThietBi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThietBi_CellContentClick);
			// 
			// SearchTimer
			// 
			this.SearchTimer.Interval = 600;
			this.SearchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.dgvThietBi);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlContent.Location = new System.Drawing.Point(0, 69);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(1032, 553);
			this.pnlContent.TabIndex = 8;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.pnlContent);
			this.pnlMain.Controls.Add(this.pnlTool);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1032, 622);
			this.pnlMain.TabIndex = 3;
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
			this.pnlTool.Size = new System.Drawing.Size(1032, 69);
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
			this.txtSearch.PlaceholderText = "TÌm kiếm thiết bị...";
			this.txtSearch.SelectedText = "";
			this.txtSearch.Size = new System.Drawing.Size(368, 41);
			this.txtSearch.TabIndex = 8;
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
			this.btnAdd.ImageSize = new System.Drawing.Size(45, 45);
			this.btnAdd.Location = new System.Drawing.Point(786, 13);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnAdd.PressedColor = System.Drawing.Color.LightGray;
			this.btnAdd.Size = new System.Drawing.Size(233, 45);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "Thêm thiết bị";
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
			this.btnRefesh.Location = new System.Drawing.Point(733, 8);
			this.btnRefesh.Name = "btnRefesh";
			this.btnRefesh.PressedState.ImageSize = new System.Drawing.Size(45, 45);
			this.btnRefesh.Size = new System.Drawing.Size(55, 55);
			this.btnRefesh.TabIndex = 5;
			this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
			// 
			// ThietBiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1032, 622);
			this.Controls.Add(this.pnlMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ThietBiForm";
			this.Text = "ThietBiForm";
			this.Load += new System.EventHandler(this.ThietBiForm1_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
			this.pnlContent.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.pnlTool.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Guna.UI2.WinForms.Guna2DataGridView dgvThietBi;
		private Guna.UI2.WinForms.Guna2TextBox txtSearch;
		private System.Windows.Forms.Timer SearchTimer;
		private System.Windows.Forms.Panel pnlContent;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Panel pnlTool;
		private Guna.UI2.WinForms.Guna2Button btnAdd;
		private Guna.UI2.WinForms.Guna2ImageButton btnRefesh;
	}
}