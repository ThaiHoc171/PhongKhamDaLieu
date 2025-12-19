using System;

namespace DTO 
{ 
	public class ThongTinCaNhanDTO
	{
		public int ThongTinID { get; set; }
		public int? TaiKhoanID { get; set; }
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
		public string Loai { get; set; }
		public DateTime NgayTao { get; set; }
		public DateTime NgayCapNhat { get; set; }
	}
}
