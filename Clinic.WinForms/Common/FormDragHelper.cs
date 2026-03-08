using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Clinic.WinForms.Common
{
	public static class FormDragHelper
	{
		[DllImport("user32.dll")]
		private static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		private static extern int SendMessage(
			IntPtr hWnd, int Msg, int wParam, int lParam);

		private const int WM_NCLBUTTONDOWN = 0xA1;
		private const int HTCAPTION = 0x2;

		public static void EnableDrag(Control control, Form form)
		{
			control.MouseDown += (s, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					ReleaseCapture();
					SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
				}
			};
		}
	}
}
