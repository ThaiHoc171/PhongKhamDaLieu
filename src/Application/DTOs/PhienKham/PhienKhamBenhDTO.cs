using Domain.Enums;
namespace Application.DTOs;

public class PhienKhamBenhRequestDTO
{
	public int PhienKhamID { get; set; }
	public int LoaiBenhID { get; set; }
	public LoaiChanDoanEnum LoaiChanDoan { get; set; } = default!;
	public string? GhiChu { get; set; }
}
public class PhienKhamBenhResponseDTO
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public NameResponseDTO LoaiBenh { get; init; } = default!;
	public LoaiChanDoanEnum LoaiChanDoan { get; init; } = default!;
	public string? GhiChu { get; init; }
}
