namespace WPF.Models;

public class ToaThuocRequest
{
	public int PhienKhamID { get; set; }
	public int NhanVienKeDonID { get; set; }
	public string? GhiChu { get; set; }
	public List<ChiTietToaThuocRequest> Thuoc { get; set; } = new();
}
public class ToaThuocUpdateRequestDTO
{
	public string? GhiChu { get; set; }
}
public class ChiTietToaThuocRequest
{
	public int ThuocID { get; set; }
	public string? LieuDung { get; set; }
	public int SoLuong { get; set; }
}
public class ToaThuocReadModel
{
	public int ToaThuocID { get; set; }
	public int PhienKhamID { get; set; }
	public DateTime NgayLap { get; set; }
	public NameHelper NguoiLap { get; init; } = default!;
	public string? GhiChu { get; set; }
	public List<ChiTietToaThuocReadModel> Thuoc { get; set; } = new();
}
public class ChiTietToaThuocReadModel
{
	public int ThuocID { get; set; }
	public string? TenThuoc { get; set; }
	public string? LieuDung { get; set; }
	public int SoLuong { get; set; }
}
public class ToaThuocListReadModel
{
	public int ToaThuocID { get; set; }
	public DateTime NgayLap { get; set; }
	public string? NguoiLap { get; set; }
	public string? GhiChu { get; set; }
}
