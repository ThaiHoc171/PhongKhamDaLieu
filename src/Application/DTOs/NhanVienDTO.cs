namespace Application.DTOs;

public class TaoNhanVienDTO
{
	public ThemThongTinCaNhanDTO ThongTin { get; set; } = default!;
	public int ChucVuID { get; set; }
	public int PhongChucNangID { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string BangCap { get; set; } = default!;
	public string KinhNghiem { get; set; } = default!;
}

public class CapNhatNhanVienDTO
{
	public int ChucVuID { get; set; }
	public int PhongChucNangID { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string BangCap { get; set; } = default!;
	public string KinhNghiem { get; set; } = default!;
}

public class NhanVienResponseDTO
{
	public int NhanVienID { get; set; }
	public string? HoTen { get; set; }
	public string? Email { get; set; }
	public string? TenChucVu { get; set; }
	public string? TrangThai { get; set; }
}
