namespace Application.DTOs;
//--Auth--
public class LoginRequestDTO
{
	public string Email { get; set; } = default!;
	public string MatKhau { get; set; } = default!;
}
public class LoginResponseDTO
{
	public int Id { get; set; }
	public string Email { get; set; } = default!;
	public string VaiTro { get; set; } = default!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
	public NameResponseDTO HoTen { get; set; } = default!;
    public int? NhanVienId { get; set; }
	public int? BenhNhanId { get; set; }
	public string? ChucVu { get; set; }
}
public class ChangePasswordRequestDTO
{
	public string MatKhauCu { get; set; } = default!;
	public string MatKhauMoi { get; set; } = default!;
}
public class RefreshTokenRequestDTO
{
	public string RefreshToken { get; set; } = null!;
}
//---TaiKhoan---
public class TaiKhoanRequestDTO
{
	public string Email { get; set; } = default!;
	public string MatKhau { get; set; } = default!;
	public string VaiTro { get; set; } = default!;
}
public class TaiKhoanUpdateRequestDTO
{
	public string? TrangThai { get; set; }
}
public class TaiKhoanListReadModel
{
	public int Id { get; set; }
	public string Email { get; set; } = default!;
	public string VaiTro { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
}
public class TaiKhoanReadModel
{
	public int TaiKhoanID { get; set; }
	public string Email { get; set; } = default!;
	public string VaiTro { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
	public DateTime NgayTao { get; set; }
}
