namespace Application.DTOs;
public class ThongTinRequestDTO
{
    public int? TaiKhoanID { get; set; }
    public string HoTen { get; set; } = null!;
	public DateTime? NgaySinh { get; set; }
	public string GioiTinh { get; set; } = null!;   // "Nam" | "Nữ" | "Khác"
	public string SDT { get; set; } = null!;
	public string EmailLienHe { get; set; } = null!;
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }
}
public class ThongTinUpdateRequestDTO
{
	public string HoTen { get; set; } = null!;
	public DateTime? NgaySinh { get; set; }
	public string GioiTinh { get; set; } = null!;
	public string SDT { get; set; } = null!;
	public string EmailLienHe { get; set; } = null!;
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }
}
public class ThongTinCaNhanResponseDTO
{
	public int ThongTinID { get; set; }
	public int? TaiKhoanID { get; set; }
	public string HoTen { get; set; } = null!;
    public DateTime? NgaySinh { get; set; }
    public string GioiTinh { get; set; } = null!;
    public string SDT { get; set; } = null!;
	public string EmailLienHe { get; set; } = null!;
    public string? DiaChi { get; set; }
    public string? Avatar { get; set; }
	public string Loai { get; set; } = null!;
}
public class ThongTinLiteReadModel
{
	public int ThongTinID { get; set; }
	public int? TaiKhoanID { get; set; }
	public string HoTen { get; set; } = null!;
	public string SDT { get; set; } = null!;
	public string EmailLienHe { get; set; } = null!;
	public string Loai { get; set; } = null!;
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}
public class ThongTinFullReadModel
{
	public int ThongTinID { get; set; }
	public int? TaiKhoanID { get; set; }
	public string HoTen { get; set; } = null!;
	public DateTime? NgaySinh { get; set; }
	public string? GioiTinh { get; set; }
	public string SDT { get; set; } = null!;
	public string EmailLienHe { get; set; } = null!;
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }
	public string Loai { get; set; } = null!;
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}