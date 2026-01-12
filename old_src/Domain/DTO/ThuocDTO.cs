using System;

namespace Domain.DTO
{
	public class ThuocDTO
	{
		public int ThuocID { get; set; }
		public string TenThuoc { get; set; }
		public string HoatChat { get; set; }
	}
	public class ThuocCreateDTO
	{
		public string TenThuoc { get; set; }
		public string HoatChat { get; set; }
	}
}
