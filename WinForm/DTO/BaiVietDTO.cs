using System;

namespace DTO
{
	public class BaiVietDTO
	{
		public int BaiVietID { get; set; }
		public string TieuDe { get; set; }
		public string TomTat { get; set; }
		public string NoiDung { get; set; }
		public string HinhAnh { get; set; }
		public int? TacGiaID { get; set; }
		public int? LoaiBenhID { get; set; }
		public int LuotXem { get; set; }
		public DateTime NgayDang { get; set; }
		public DateTime? NgayCapNhat { get; set; }
		public string TrangThai { get; set; }
	}

}
