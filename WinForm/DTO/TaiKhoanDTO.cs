using System;

namespace DTO
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
}
