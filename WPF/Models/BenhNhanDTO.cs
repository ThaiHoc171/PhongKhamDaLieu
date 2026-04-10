namespace WPF.Models;
public class BenhNhanRequest
{
	public int? TaiKhoanID { get; set; }
	public string HoTen { get; set; } = "";
	public DateTime NgaySinh { get; set; }
	public string GioiTinh { get; set; } = "";
	public string SDT { get; set; } = "";
	public string? EmailLienHe { get; set; }
	public string DiaChi { get; set; } = "";
	public string? Avatar { get; set; }
	public string? GhiChu { get; set; }
}
public class BenhNhanReadListModel
{
	public int BenhNhanID { get; init; }
	public int ThongTinID { get; init; }
	public string HoTen { get; init; } = "";
	public DateTime NgaySinh { get; init; }
	public string GioiTinh { get; init; } = "";
	public string SDT { get; init; } = "";
	public string? EmailLienHe { get; init; }
}
public class BenhNhanReadModel
{
	public int BenhNhanID { get; init; }
	public int ThongTinID { get; init; }
	public int? TaiKhoanID { get; init; }
	public string HoTen { get; init; } = "";
	public DateTime NgaySinh { get; init; }
	public string GioiTinh { get; init; } = "";
	public string SDT { get; init; } = "";
	public string? EmailLienHe { get; init; }
	public string DiaChi { get; init; } = "";
	public string? Avatar { get; init; }
	public string GhiChu { get; init; } = "";
	public DateTime NgayTao { get; init; }
	public DateTime NgayCapNhat { get; init; }
}
public class BenhNhanUpdateRequest
{
	public string HoTen { get; set; } = "";
	public DateTime NgaySinh { get; set; }
	public string GioiTinh { get; set; } = "";
	public string SDT { get; set; } = "";
	public string? EmailLienHe { get; set; }
	public string DiaChi { get; set; } = "";
	public string? Avatar { get; set; }
	public string? GhiChu { get; set; }
}
