using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Common
{
	public static class Session
	{
		public static string Token { get; set; }
		public static int UserId { get; set; }
		public static int? NhanVienId { get; set; }

		public static void Clear()
		{
			Token = null;
			UserId = 0;
			NhanVienId = 0;
		}
	}
}
