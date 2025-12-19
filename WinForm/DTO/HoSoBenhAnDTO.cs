using System;

namespace DTO
{
	public class HoSoBenhAnDTO
	{
		public int HoSoBenhAnID { get; set; }
		public int BenhNhanID { get; set; }
		public string BenhNen { get; set; }
		public string DiUng { get; set; }
		public string TienSuBenh { get; set; }
		public string TienSuGiaDinh { get; set; }
		public string ThoiQuenSong { get; set; }
		public string ThongTinKhac { get; set; }
		public DateTime NgayTao { get; set; }
		public DateTime NgayCapNhat { get; set; }
	}
}
