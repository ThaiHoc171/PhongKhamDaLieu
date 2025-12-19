using System;

namespace DTO
{
	public class ToaThuocDTO
	{
		public int ToaThuocID { get; set; }
		public int PhienKhamID { get; set; }
		public int NhanVienKeDonID { get; set; }
		public DateTime NgayLap { get; set; }
		public decimal? TongTien { get; set; }
		public string TrangThai { get; set; }
		public string GhiChu { get; set; }
	}
}
