namespace WPF.Models;
public class ChiTietPCNThietBiUpdate
{
	public string MaTaiSan { get; set; } = "";
	public string TinhTrang { get; set; }  ="";
	public string? GhiChu { get; set; }
}

public class ChiTietPCNThietBiRequest
{
	public int PhongChucNangID { get; set; }
	public int ThietBiID { get; set; }
	public string MaTaiSan { get; set; } = default!;
	public string? GhiChu { get; set; }
}
public class ChiTietPCNThietBiReadModel
{
	public int ChiTietID { get; set; }
	public string MaTaiSan { get; set; } = default!;
	public DateTime NgayNhap { get; set; }
	public string TinhTrang { get; set; } = default!;
	public string? GhiChu { get; set; }
	public string? PhongChucNang { get; init; }
	public string? ThietBi { get; init; }
}
public class ChiTietPCNThietBiListReadModel
{
	public int ChiTietID { get; set; }
	public string MaTaiSan { get; set; } = default!;
	public string TinhTrang { get; set; } = default!;
	public DateTime NgayNhap { get; set; }
}