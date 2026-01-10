using System;

namespace Domain.DTO
{
	public class PhongChucNang_ThietBiDTO
	{
		public int PCN_TB_ID { get; set; }
		public int PhongChucNangID { get; set; }
		public int ThietBiID { get; set; }
		public int SoLuong { get; set; }
		public string GhiChu { get; set; }
	}
    public class ThemPhongChucNang_ThietBiDTO
    {
        public int PhongChucNangID { get; set; }
        public int ThietBiID { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
