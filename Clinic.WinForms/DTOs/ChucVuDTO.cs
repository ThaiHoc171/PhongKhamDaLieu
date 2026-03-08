using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class ChucVuResponseDTO
	{
		public int ChucVuID { get; set; }
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
		public DateTime NgayTao { get; set; }
		public string TrangThai { get; set; }
	}
	public class ChucVuRequestDTO
	{
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
	}
}
