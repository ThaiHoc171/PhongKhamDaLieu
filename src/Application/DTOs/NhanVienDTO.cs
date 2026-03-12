namespace Application.DTOs;
public class NhanVienRequestDTO
{
	public ThongTinRequestDTO ThongTin { get; set; } = default!;
	public int ChucVuID { get; set; }
	public int PhongChucNangID { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string BangCap { get; set; } = default!;
	public string KinhNghiem { get; set; } = default!;
}
public class NhanVienRequestUpdateDTO
{
	public int ChucVuID { get; set; }
	public int PhongChucNangID { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string BangCap { get; set; } = default!;
	public string KinhNghiem { get; set; } = default!;
}
public class NhanVienListReadModel
{
	public int NhanVienID { get; set; }
	public string HoTen { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string TenChucVu { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
}
public class NhanVienDetailReadModel
{
	public int NhanVienID { get; set; }
	public int ThongTinID { get; set; }
	public NameResponseDTO? ChucVu{ get; init; }
	public NameResponseDTO? PhongChucNang { get; set; }
	public string HoTen { get; set; } = default!;
	public DateTime? NgaySinh { get; set; }
	public string? GioiTinh { get; set; }
	public string? SDT { get; set; }
	public string EmailLienHe { get; set; } = default!;
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string? BangCap { get; set; }
	public string? KinhNghiem { get; set; }
	public string TrangThai { get; set; } = default!;
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}