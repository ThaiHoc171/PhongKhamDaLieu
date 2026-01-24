namespace Application.DTOs;

public class PhienKhamBenhRequestDTO
{
	public int PhienKhamID { get; set; }
	public int LoaiBenhID { get; set; }
	public string LoaiChanDoan { get; set; } = default!;
	public string? GhiChu { get; set; }
}
