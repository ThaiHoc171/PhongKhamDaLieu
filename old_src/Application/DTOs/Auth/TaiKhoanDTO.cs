namespace Application.DTOs;

public class LoginRequestDTO
{
	public string Email { get; set; } = default!;
	public string MatKhau { get; set; } = default!;
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
