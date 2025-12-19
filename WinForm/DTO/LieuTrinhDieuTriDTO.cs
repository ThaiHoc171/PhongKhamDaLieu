using System;

namespace DTO
{
	public class LieuTrinhDieuTriDTO
	{
		public int LieuTrinhID { get; set; }
		public int BenhNhanID { get; set; }
		public int PhienKhamID { get; set; }
		public string TenLieuTrinh { get; set; }
		public int TongSoBuoi { get; set; }
		public string TrangThai { get; set; }
		public string GhiChu { get; set; }
		public DateTime NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
	}
}
