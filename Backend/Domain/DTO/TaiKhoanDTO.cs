using System;

namespace Domain.DTO
{
    public class TaiKhoanDTO
    {
        public int TaiKhoanID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
	public class TaiKhoanCreateDTO
    {
        public string Email { get; set; }
        public string MatKhau{ get; set; }
        public string VaiTro { get; set; }
	}
    public class DoiMatKhauDTO
    {
        public int TaiKhoanID { get; set; }
        public string MatKhauCu { get; set; }
        public string MatKhauMoi { get; set; }
	}   
}
