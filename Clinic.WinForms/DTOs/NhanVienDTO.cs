using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class NhanVienResponseDTO
	{
		public int NhanVienID { get; set; }
		public string HoTen { get; set; }
		public string Email { get; set; }
		public string TenChucVu { get; set; }
		public string TrangThai { get; set; }
	}
	public class ChiTietNhanVienResponseDTO
	{
		public int NhanVienID { get; set; }
		public int ThongTinID { get; set; }
		public int ChucVuID { get; set; }
		public int PhongChucNangID { get; set; }

		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }
		public DateTime? NgayVaoLam { get; set; }
		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }
		public string TrangThai { get; set; }

		public DateTime? NgayTao { get; set; }
		public DateTime? NgayCapNhat { get; set; }
	}
	public class NhanVienRequestDTO
	{
		public ThongTinRequestDTO ThongTin { get; set; }

		public int ChucVuID { get; set; }
		public int PhongChucNangID { get; set; }
		public DateTime NgayVaoLam { get; set; }

		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }
	}
	public class CapNhatNhanVienDTO
	{
		public int ChucVuID { get; set; }
		public int PhongChucNangID { get; set; }
		public DateTime? NgayVaoLam { get; set; }
		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }
	}
}