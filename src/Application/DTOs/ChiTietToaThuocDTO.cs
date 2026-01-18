namespace Application.DTOs;

public class ToaThuocChiTietDTO
{
	public int ToaThuocID { get; set; }
	public DateTime NgayLap { get; set; }
	public string? GhiChu { get; set; }
	public List<ChiTietToaThuocDTO> Thuoc { get; set; } = new();
}

public class ChiTietToaThuocDTO
{
	public int ThuocID { get; set; }
	public string TenThuoc { get; set; } = "";
	public string? LieuDung { get; set; }
	public int SoLuong { get; set; }
}
