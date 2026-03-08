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

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			while (true)
			{
				var login = new FrmDangNhap();

				if (login.ShowDialog() != DialogResult.OK)
					break;

				var main = new FormMain(login.LoginResult);
				Application.Run(main);
			}
			//Application.Run(new MainFrm());
		}
	}
}
