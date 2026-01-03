using System;

namespace Domain.DTO
{
	public class CanLamSangListDTO
	{
		public int CanLamSangID { get; set; }
		public string TenCLS { get; set; }
		public string LoaiXetNghiem { get; set; }
		public decimal Gia { get; set; }
	}
	public class ThemCanLamSangDTO
	{
		public string TenCLS { get; set; }
		public string MoTa { get; set; }
		public decimal Gia { get; set; }
		public string LoaiXetNghiem { get; set; }
	}
	public class CanLamSangDetailDTO
	{
		public int CanLamSangID { get; set; }
		public string TenCLS { get; set; }
		public string MoTa { get; set; }
		public decimal? Gia { get; set; }
		public string LoaiXetNghiem { get; set; }
		public DateTime NgayTao { get; set; }
	}
	public class CapNhatCanLamSangDTO
	{
		public int CanLamSangID { get; set; }
		public string TenCLS { get; set; }
		public string MoTa { get; set; }
		public decimal Gia { get; set; }
		public string LoaiXetNghiem { get; set; }
	}
}
