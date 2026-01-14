namespace Application.DTOs;

public class ThietBiRequestDTO
{
	public string TenTB { get; set; }
	public string? LoaiTB { get; set; }
}

public class ThietBiResponseDTO
{
	public int Id { get; set; }
	public string? TenTB { get; set; }
	public string? LoaiTB { get; set; }
	public string? TinhTrang { get; set; }
	public DateTime NgayNhap { get; set; }
}
