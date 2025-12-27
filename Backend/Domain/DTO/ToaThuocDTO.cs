using System;

namespace Domain.DTO
{
	public class ToaThuocDTO
	{
		public int ToaThuocID { get; set; }
		public int PhienKhamID { get; set; }
		public int NhanVienKeDonID { get; set; }
		public DateTime NgayLap { get; set; }
		public string GhiChu { get; set; }
	}
    public class ToaThuocCreateDTO
    {
        public int PhienKhamID { get; set; }
        public int NhanVienKeDonID { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
    }
}
