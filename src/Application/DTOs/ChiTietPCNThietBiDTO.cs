using Domain.Enums;
namespace Application.DTOs;
public class ChiTietPCNThietBiResponseDTO
{
	public int ChiTietID { get; set; }
	public string MaTaiSan { get; set; } = default!;
	public DateTime NgayNhap { get; set; }
	public TinhTrang TinhTrang { get; set; }
	public string? GhiChu { get; set; }
}
public class ChiTietPCNThietBiCreateDTO
{
	public int PhongChucNangID { get; set; }
	public int ThietBiID { get; set; }
	public string MaTaiSan { get; set; } = default!;
	public string? GhiChu { get; set; }
}
public class ChiTietPCNThietBiUpdateDTO
{
	public TinhTrang TinhTrang { get; set; }
	public string? GhiChu { get; set; }
}
