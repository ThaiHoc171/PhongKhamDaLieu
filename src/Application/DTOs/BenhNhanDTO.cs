namespace Application.DTOs;

public class BenhNhanRequestDTO
{

	public int? ThongTinID { get; set; }
    public int? TaiKhoanID { get; set; }
    public string? HoTen { get; set; }
	public DateTime? NgaySinh { get; set; }
	public string? GioiTinh { get; set; }
	public string? SDT { get; set; }
	public string? EmailLienHe { get; set; }
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }
	public string? GhiChu { get; set; }
}

public class BenhNhanResponseDTO
{
	public int BenhNhanID { get; set; }
	public int ThongTinID { get; set; }
	public string? HoTen { get; set; }
	public string? SDT { get; set; }
	public string? EmailLienHe { get; set; }
	public string? GhiChu { get; set; }
}
public class BenhNhanIdResponseDTO
{
	public int BenhNhanID { get; set; }
	public int ThongTinID { get; set; }	
	public string? GhiChu { get; set; }
	public string? HoTen { get; set; }
	public DateTime? NgaySinh { get; set; }
	public string? GioiTinh { get; set; }
	public string? SDT { get; set; }
	public string? EmailLienHe { get; set; }
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }

	public DateTime? NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}
public class CapNhatBenhNhanDTO
{
	public string? GhiChu { get; set; }
}
