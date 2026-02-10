namespace Application.DTOs;

public class PhienKhamThietBiRequestDTO
{
	public int PhienKhamID { get; set; }
	public int ChiTietID { get; set; }
	public string? GhiChu { get; set; }
}
public class PhienKhamThietBiResponseDTO
{
	public int PhienKhamThietBiID { get; set; }
	public int PhienKhamID { get; set; }
	public int ChiTietID { get; set; }
	public string? GhiChu { get; set; }
}