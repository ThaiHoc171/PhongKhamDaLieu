using System;

namespace Domain.DTO
{
	public class NhanVienDetailDTO
	{
		public int NhanVienID { get; set; }
		public int ThongTinID { get; set; }
		public int ChucVuID { get; set; }
		public string TenChucVu { get; set; }

		public decimal? Luong { get; set; }
		public DateTime? NgayVaoLam { get; set; }
		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }

		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }

		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
		public string Loai { get; set; }

		public int? TaiKhoanID { get; set; }
		public DateTime NgayTao { get; set; }
		public DateTime NgayCapNhat { get; set; }
	}
	public class NhanVienCreateDTO
	{
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }

		public int ChucVuID { get; set; }
		public decimal? Luong { get; set; }
		public DateTime? NgayVaoLam { get; set; }
		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }


	}
	public class NhanVienListDTO
	{
		public int NhanVienID { get; set; }
		public string HoTen { get; set; }
		public string TenChucVu { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public DateTime? NgayVaoLam { get; set; }
	}
	public class NhanVienUpdateDTO
	{
		public int NhanVienID { get; set; }
		public string HoTen { get; set; }
		public DateTime NgaySinh { get; set; }
		public string GioiTinh { get; set; }

		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }

		public int ChucVuID { get; set; }
		public decimal Luong { get; set; }
		public DateTime NgayVaoLam { get; set; }
		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }
	}

}
