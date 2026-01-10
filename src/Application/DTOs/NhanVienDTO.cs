using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class ThemNhanVienDTO
	{
		public string HoTen { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string GioiTinh { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string DiaChi { get; set; }
		public string Avatar { get; set; }

		public int ChucVuID { get; set; }
		public DateTime? NgayVaoLam { get; set; }
		public string BangCap { get; set; }
		public string KinhNghiem { get; set; }
	}

	public class ListNhanVienDTO
	{
		public int NhanVienID { get; set; }
		public string HoTen { get; set; }
		public string SDT { get; set; }
		public string EmailLienHe { get; set; }
		public string TenChucVu { get; set; }
		public string TrangThai { get; set; }
	}
}
