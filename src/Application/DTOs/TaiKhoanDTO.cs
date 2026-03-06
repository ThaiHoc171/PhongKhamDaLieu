namespace Application.DTOs;

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
	public int? ThongTinID { get; set; }
    public int? NhanVienId { get; set; }
	public int? BenhNhanId { get; set; }
	public string? ChucVu { get; set; }
	public string? HoTen { get; set; }
}

public class ThemTaiKhoanDTO
{
	public string Email { get; set; } = default!;
	public string MatKhau { get; set; } = default!;
	public string VaiTro { get; set; } = default!;
}

public class DoiMatKhauDTO
{
	public string MatKhauCu { get; set; } = default!;
	public string MatKhauMoi { get; set; } = default!;
}

public class TaiKhoanResponseDTO
{
	public int Id { get; set; }
	public string Email { get; set; } = default!;
	public string VaiTro { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
}
public class RefreshTokenRequestDTO
{
    public string RefreshToken { get; set; } = null!;
}
