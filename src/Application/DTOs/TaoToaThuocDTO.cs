namespace Application.DTOs;
public class TaoToaThuocDTO
{
	public int PhienKhamID { get; set; }
	public int NhanVienKeDonID { get; set; }
	public string? GhiChu { get; set; }
	public List<TaoChiTietToaThuocDTO> Thuoc { get; set; } = new();
}

public class TaoChiTietToaThuocDTO
{
	public int ThuocID { get; set; }
	public string? LieuDung { get; set; }
	public int SoLuong { get; set; }
}


