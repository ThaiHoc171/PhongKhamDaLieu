namespace Application.DTOs;

public class KhungGioKhamRequestDTO
{
	public TimeSpan GioBatDau { get; set; }
	public TimeSpan GioKetThuc { get; set; }
	public string? TenKhung { get; set; }
}
public class KhungGioKhamResponseDTO
{
	public int KhungGioID { get; set; }
	public TimeSpan GioBatDau { get; set; }
	public TimeSpan GioKetThuc { get; set; }
	public string? TenKhung { get; set; }
	public int MaxSlot { get; set; }
}