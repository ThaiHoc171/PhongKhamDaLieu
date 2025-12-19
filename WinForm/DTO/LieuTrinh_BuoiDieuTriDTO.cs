using System;

namespace DTO
{
	public class LieuTrinh_BuoiDieuTriDTO
	{
		public int BuoiDieuTriID { get; set; }
		public int LieuTrinhID { get; set; }
		public int SoBuoi { get; set; }
		public DateTime? NgayDuKien { get; set; }
		public DateTime? NgayThucHien { get; set; }
		public int? NhanVienID { get; set; }
		public string TrangThai { get; set; }
		public string GhiChu { get; set; }
		public string HinhAnhJSON { get; set; }
	}
}
