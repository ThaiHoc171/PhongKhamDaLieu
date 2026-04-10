namespace WPF.Models;
public class PhienKhamThietBiRequestDTO
{
	public int PhienKhamID { get; set; }
	public int ChiTietID { get; set; }
	public string? GhiChu { get; set; }
}
public class PhienKhamThietBiReadModel
{
	public int PhienKhamThietBiID { get; set; }
	public string TenThietBi { get; set; } = default!;
	public string? TenPhong { get; set; }
	public string? GhiChu { get; set; }
}