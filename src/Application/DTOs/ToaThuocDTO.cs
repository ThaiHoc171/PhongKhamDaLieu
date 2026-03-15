namespace Application.DTOs;
public class ToaThuocRequestDTO
{
	public int PhienKhamID { get; set; }
	public int NhanVienKeDonID { get; set; }
	public string? GhiChu { get; set; }
	public List<ChiTietToaThuocRequestDTO> Thuoc { get; set; } = new();
}
public class ToaThuocUpdateRequestDTO
{
	public string? GhiChu { get; set; }
}
public class ChiTietToaThuocRequestDTO
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
	public NameResponseDTO NguoiLap { get; init; } = default!;
	public string? GhiChu { get; set; }
	public List<ChiTietToaThuocReadModel> Thuoc { get; set; } = new();
}
public class ChiTietToaThuocReadModel
{
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