using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class LoginRequestDto
	{
		public string Email { get; set; }
		public string MatKhau { get; set; }
	}

	public class TaiKhoanCreateDto
	{
		public string Email { get; set; }
		public string MatKhau { get; set; }
		public string VaiTro { get; set; }
	}
	public class DoiMatKhauDto
	{
		public string MatKhauCu { get; set; }
		public string MatKhauMoi { get; set; }
	}
	public class TaiKhoanResponseDto
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string Status { get; set; }
	}
}
