using System;

namespace DTO
{
	public class ChiTietToaThuocDTO
	{
		public int ChiTietToaThuocID { get; set; }
		public int ToaThuocID { get; set; }
		public int ThuocID { get; set; }
		public string LieuDung { get; set; }
		public int SoLuong { get; set; }
		public decimal? DonGia { get; set; }
		public decimal? ThanhTien { get; set; }
	}
}
