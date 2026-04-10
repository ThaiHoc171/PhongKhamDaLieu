namespace WPF.Models;

public class LoginRequestDTO
{
	public string Email { get; set; } = "";
	public string MatKhau { get; set; } = "";
}
public class LoginResponseDTO
{
	public int Id { get; set; }
	public string Email { get; set; } = "";
	public string VaiTro { get; set; } = "";
	public string AccessToken { get; set; } = "";
	public string RefreshToken { get; set; } = "";
	public NameHelper? HoTen { get; set; } 
	public int? NhanVienId { get; set; }
	public int? BenhNhanId { get; set; }
	public string ChucVu { get; set; } = "";
}
public class ChangePasswordRequestDTO
{
	public string MatKhauCu { get; set; } = "";
	public string MatKhauMoi { get; set; } = "";
}
public class RefreshTokenRequestDTO
{
	public string RefreshToken { get; set; } = null!;
}
//---TaiKhoan---
public class TaiKhoanRequestDTO
{
	public string Email { get; set; } = "";
	public string MatKhau { get; set; } = "";
	public string VaiTro { get; set; } = "";
}
public class TaiKhoanUpdateRequestDTO
{
	public string? TrangThai { get; set; }
}
public class UpdateFcmTokenDto
{
	public string FCMToken { get; set; } = null!;
}
public class TaiKhoanListReadModel
{
	public int Id { get; set; }
	public string Email { get; set; } = "";
	public string VaiTro { get; set; } = "";
	public string TrangThai { get; set; } = "";
}
public class TaiKhoanReadModel
{
	public int TaiKhoanID { get; set; }
	public string Email { get; set; } = "";
	public string VaiTro { get; set; } = "";
	public string TrangThai { get; set; } = "";
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}
