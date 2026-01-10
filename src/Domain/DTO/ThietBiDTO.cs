using System;

namespace Domain.DTO
{
	public class ThietBiDTO
	{
		public int ThietBiID { get; set; }
		public string TenTB { get; set; }
		public string LoaiTB { get; set; }
		public string TinhTrang { get; set; }
		public DateTime NgayNhap { get; set; }
	}
    public class ThemThietBiDTO
    {
        public string TenTB { get; set; }
        public string LoaiTB { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
