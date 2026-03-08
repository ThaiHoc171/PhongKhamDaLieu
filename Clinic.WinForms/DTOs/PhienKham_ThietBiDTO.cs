using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class PhienKham_ThietBiRequestDTO
	{
		public int PhienKhamID { get; set; }
		public int ChiTietID { get; set; }
		public string GhiChu { get; set; }
	}
	public class PhienKham_ThietBiReadModel
	{
		public int PhienKhamThietBiID { get; set; }
		public int ChiTietID { get; set; }
		public string TenThietBi { get; set; } = default;
		public string TenPhong { get; set; }
		public string GhiChu { get; set; }
	}
}
