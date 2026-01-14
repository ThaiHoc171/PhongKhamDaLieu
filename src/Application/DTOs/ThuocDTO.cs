namespace Application.DTOs;

public class ThuocRequestDTO
{
	public string TenThuoc { get; set; } = default!;
	public string? HoatChat { get; set; }
}
public class ThuocResponseDTO
{
	public int ThuocID { get; set; }
	public string TenThuoc { get; set; } = default!;
	public string? HoatChat { get; set; }
}