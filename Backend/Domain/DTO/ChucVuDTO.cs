using System;

namespace Domain.DTO
{
	public class ChucVuDTO
	{
		public int ChucVuID { get; set; }
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
		public DateTime NgayTao { get; set; }
		public string TrangThai { get; set; }
	}
	public class ThemChucVuDTO
	{
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
	}
	public class CapNhatChucVuDTO
	{
		public int ChucVuID { get; set; }
		public string TenChucVu { get; set; }
		public string MoTa { get; set; }
	}
}
