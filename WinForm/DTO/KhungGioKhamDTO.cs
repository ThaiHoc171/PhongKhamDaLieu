using System;

namespace DTO
{
	public class KhungGioKhamDTO
	{
		public int KhungGioID { get; set; }
		public TimeSpan GioBatDau { get; set; }
		public TimeSpan GioKetThuc { get; set; }
		public string TenKhung { get; set; }
		public int MaxSlot { get; set; }
	}
}
