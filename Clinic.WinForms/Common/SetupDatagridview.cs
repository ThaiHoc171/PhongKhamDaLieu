using System.Drawing;
using System.Windows.Forms;

namespace Clinic.WinForms.Common
{
	public static class SetupDatagridview
	{
		public static void ApplyGridStyle(DataGridView dgv)
		{
			dgv.AutoGenerateColumns = false;
			dgv.Columns.Clear();

			dgv.BackgroundColor = Color.White;
			dgv.BorderStyle = BorderStyle.None;
			dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgv.GridColor = Color.FromArgb(230, 230, 230);

			dgv.EnableHeadersVisualStyles = false;
			dgv.ColumnHeadersHeight = 40;
			dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

			dgv.RowTemplate.Height = 40;
			dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgv.MultiSelect = false;
			dgv.ReadOnly = true;

			// enable empty message
			AttachEmptyMessage(dgv);
		}

		public static DataGridViewImageColumn CreateButtonColumn(string name, Image img, string tooltip)
		{
			var col = new DataGridViewImageColumn
			{
				Name = name,
				HeaderText = "",
				Image = helper.ResizeImage(img, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			col.CellTemplate.ToolTipText = tooltip;

			return col;
		}

		private static void AttachEmptyMessage(DataGridView dgv)
		{
			dgv.Paint -= Dgv_Paint;
			dgv.Paint += Dgv_Paint;
		}

		private static void Dgv_Paint(object sender, PaintEventArgs e)
		{
			var dgv = sender as DataGridView;

			if (dgv == null || dgv.Rows.Count > 0)
				return;

			string text = "Không có dữ liệu";

			using (Font font = new Font("Segoe UI", 12, FontStyle.Italic))
			{
				SizeF size = e.Graphics.MeasureString(text, font);

				e.Graphics.DrawString(
					text,
					font,
					Brushes.Gray,
					(dgv.Width - size.Width) / 2,
					(dgv.Height - size.Height) / 2
				);
			}
		}
	}
}