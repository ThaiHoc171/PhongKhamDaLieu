using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class ThuocRequestDTO
	{
		public string TenThuoc { get; set; } = default;
		public string HoatChat { get; set; }
	}
	public class ThuocResponseDTO
	{
		public int ThuocID { get; set; }
		public string TenThuoc { get; set; } = default;
		public string HoatChat { get; set; }
	}
}
