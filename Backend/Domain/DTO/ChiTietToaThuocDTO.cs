using System;

namespace Domain.DTO
{
	public class ChiTietToaThuocDTO
	{
		public int ChiTietToaThuocID { get; set; }
		public int ToaThuocID { get; set; }
		public int ThuocID { get; set; }
		public string LieuDung { get; set; }
		public int SoLuong { get; set; }
	}
    public class ChiTietToaThuocCreateDTO
    {
        public int ToaThuocID { get; set; }
        public int ThuocID { get; set; }
        public string LieuDung { get; set; }
        public int SoLuong { get; set; }
    }
}
