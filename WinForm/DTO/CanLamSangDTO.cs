using System;

namespace DTO
{
	public class CanLamSangDTO
	{
		public int CanLamSangID { get; set; }
		public string TenCLS { get; set; }
		public string MoTa { get; set; }
		public decimal? Gia { get; set; }
		public string LoaiXetNghiem { get; set; }
		public bool ChiDinhTuBacSi { get; set; }
		public DateTime NgayTao { get; set; }
	}
}
