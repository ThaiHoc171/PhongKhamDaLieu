using System;

namespace Application.DTOs
{
	public class ThemThongTinCaNhanDTO
	{
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
	}

	public class CapNhatThongTinCaNhanDTO
	{
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
	}
	public class ThongTinCaNhanResponseDTO
	{
		public int ThongTinID { get; set; }
		public int? TaiKhoanID { get; set; }
		public string HoTen { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string Avatar { get; set; }
		public string Loai { get; set; }
	}
}
