namespace WPF.Models;

public class ChucVuRequest
{
	public string TenChucVu { get; set; } = "";
	public string MoTa { get; set; } = "";
	public string TrangThai { get; set; } = "";	
}
public class ChucVuReadModel
{
	public int ChucVuID { get; set; }
	public string TenChucVu { get; set; } = "";
	public string MoTa { get; set; } = "";
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
	public string TrangThai { get; set; } = "";
}
public class ChucVuListReadModel
{
	public int ChucVuID { get; set; }
	public string TenChucVu { get; set; } = "";
	public string TrangThai { get; set; } = "";
}
