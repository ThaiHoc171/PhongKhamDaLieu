using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class CanLamSangRequestDTO
	{
		public string TenCLS { get; set; } = default;
		public string MoTa { get; set; }
		public string LoaiXetNghiem { get; set; } = default;
	}

	public class CanLamSangResponseDTO
	{
		public int CanLamSangID { get; set; }
		public string TenCLS { get; set; } = default;
		public string MoTa { get; set; }
		public string LoaiXetNghiem { get; set; } = default;
		public DateTime NgayTao { get; set; }
		public string TrangThai { get; set; } = default;
	}

}
