using System;

namespace Domain.DTO
{
	public class PhienKham_ThietBiDTO
	{
		public int PhienKham_ThietBiID { get; set; }
		public int PhienKhamID { get; set; }
		public int ThietBiID { get; set; }
		public int SoLuong { get; set; }
		public string GhiChu { get; set; }
	}
}
