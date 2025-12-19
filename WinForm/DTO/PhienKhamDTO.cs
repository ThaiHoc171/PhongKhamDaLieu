using System;

namespace DTO
{
	public class PhienKhamDTO
	{
		public int PhienKhamID { get; set; }
		public int CaKhamID { get; set; }
		public int BenhNhanID { get; set; }
		public int NhanVienID { get; set; }
		public int? PhongChucNangID { get; set; }
		public string TrieuChung { get; set; }
		public string GhiChu { get; set; }
		public string HinhAnhJSON { get; set; }
		public string AI_KetQuaJSON { get; set; }
		public string ChuanDoanCuoi { get; set; }
		public DateTime NgayKham { get; set; }
		public string TrangThai { get; set; }
	}
}
