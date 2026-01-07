using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class LoginRequestDTO
	{
		public string Email { get; set; }
		public string MatKhau { get; set; }
	}

	public class ThemTaiKhoanDTO
	{
		public string Email { get; set; }
		public string MatKhau { get; set; }
		public string VaiTro { get; set; }
	}
	public class DoiMatKhauDTO
	{
		public string MatKhauCu { get; set; }
		public string MatKhauMoi { get; set; }
	}
	public class TaiKhoanResponseDTO
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string VaiTro { get; set; }
		public string TrangThai { get; set; }
	}
}
