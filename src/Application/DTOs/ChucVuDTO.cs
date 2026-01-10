using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class ChucVuDTO
	{
		public int ChucVuID { get; set; }
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
		public DateTime NgayTao { get; set; }
	}

	public class ThemChucVuDTO
	{
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
	}
	public class CapNhatChucVuDTO
	{
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
	}
}
