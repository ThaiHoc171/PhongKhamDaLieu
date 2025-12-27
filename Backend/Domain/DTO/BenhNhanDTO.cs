using System;

namespace Domain.DTO
{
	public class BenhNhanDetailDTO
	{
		// ---- BenhNhan ----
		public int BenhNhanID { get; set; }
		public string LoaiDa { get; set; }
		public string TrangThaiTheoDoi { get; set; }
		public string GhiChu { get; set; }

		// ---- ThongTinCaNhan ----
		public int ThongTinID { get; set; }
		public int? TaiKhoanID { get; set; }

		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }

		// ---- Meta ----
		public string TrangThai { get; set; }
		public DateTime NgayTao { get; set; }
		public DateTime NgayCapNhat { get; set; }
	}
	public class BenhNhanListDTO
	{
		public int BenhNhanID { get; set; }
		public int ThongTinID { get; set; }
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
	}
	public class BenhNhanCreateDTO
	{
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
		
		public string LoaiDa { get; set; }
		public string TrangThaiTheoDoi { get; set; }
		public string GhiChu { get; set; }
	}
}
