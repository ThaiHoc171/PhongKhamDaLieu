using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class LoginDTO
	{
		public string Email { get; set; } = "";
		public string Password { get; set; } = "";
	}
	public class LoginResponseDTO
	{
		public int Id { get; set; }
		public string Email { get; set; } = "";
		public string VaiTro { get; set; } = "";
		public string AccessToken { get; set; } = "";
		public string RefreshToken { get; set; } = "";
		public int? NhanVienId { get; set; }
		public int? BenhNhanId { get; set; }
		public string ChucVu { get; set; }
	}
}
