using System;

namespace DTO
{
	public class TaiKhamDTO
	{
		public int TaiKhamID { get; set; }
		public int PhienKhamID { get; set; }
		public int BenhNhanID { get; set; }
		public DateTime NgayDuKien { get; set; }
		public string LyDo { get; set; }
		public string TrangThai { get; set; }
		public int? CaKhamID { get; set; }
		public DateTime NgayTao { get; set; }
	}
}
