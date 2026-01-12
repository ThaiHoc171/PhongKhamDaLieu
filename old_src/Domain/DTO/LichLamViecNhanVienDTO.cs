using System;

namespace Domain.DTO
{
	public class LichLamViecNhanVienDTO
	{
		public int LichLamViecID { get; set; }
		public int NhanVienID { get; set; }
		public DateTime Ngay { get; set; }
		public int CaLamViec { get; set; }
		public string GhiChu { get; set; }
	}
    public class ThemLichLamViecNhanVienDTO
    {
        public int NhanVienID { get; set; }
        public DateTime Ngay { get; set; }
        public int CaLamViec { get; set; }
        public string GhiChu { get; set; }
    }
}
