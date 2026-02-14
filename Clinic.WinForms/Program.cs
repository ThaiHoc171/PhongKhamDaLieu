using Clinic.WinForms.Forms;
using GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			using (var loginForm = new FrmDangNhap())
			{
				if (loginForm.ShowDialog() == DialogResult.OK)
				{
					var mainForm = new MainFrm(loginForm.LoginResult);
					Application.Run(mainForm);
				}
			}

		}
	}
}
