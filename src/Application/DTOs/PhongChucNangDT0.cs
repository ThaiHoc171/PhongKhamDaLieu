namespace Application.DTOs;

public class PhongChucNangRequestDTO
{
	public string TenPhong { get; set; }
	public string? LoaiPhong { get; set; }
	public string? MoTa { get; set; }
}

public class PhongChucNangResponseDTO
{
	public int Id { get; set; }
	public string TenPhong { get; set; }
	public string? LoaiPhong { get; set; }
	public string? MoTa { get; set; }
	public string TrangThai { get; set; }
	public DateTime NgayTao { get; set; }
}
