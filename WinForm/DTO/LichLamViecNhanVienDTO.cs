using System;

namespace DTO
{
	public class LichLamViecNhanVienDTO
	{
		public int LichLamViecID { get; set; }
		public int NhanVienID { get; set; }
		public int PhongChucNangID { get; set; }
		public DateTime Ngay { get; set; }
		public int CaLamViec { get; set; }
		public string GhiChu { get; set; }
	}
}
