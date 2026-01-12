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
		public string Loai { get; set; }

		// ---- Meta ----
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
		public string TrangThaiTheoDoi { get; set; }
	}
	public class ThemBenhNhanDTO
	{
		public int ThongTinID { get; set; }
		public string LoaiDa { get; set; }
		public string TrangThaiTheoDoi { get; set; }
		public string GhiChu { get; set; }
	}
	public class HoSoBenhNhanDTO
	{
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
	}
	public class BenhNhanUpdateDTO
	{
		// ---- BenhNhan ----
		public int BenhNhanID { get; set; }
		public string LoaiDa { get; set; }
		public string TrangThaiTheoDoi { get; set; }
		public string GhiChu { get; set; }
		// ---- ThongTinCaNhan ----
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
	}
}
