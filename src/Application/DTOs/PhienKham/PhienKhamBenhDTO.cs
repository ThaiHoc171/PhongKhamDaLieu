namespace Application.DTOs;

public class PhienKhamBenhRequestDTO
{
	public int PhienKhamID { get; set; }
	public int LoaiBenhID { get; set; }
	public string LoaiChanDoan { get; set; } = default!;
	public string? GhiChu { get; set; }
}
public class PhienKhamBenhResponseDTO
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public int LoaiBenhID { get; init; }
	public string LoaiChanDoan { get; init; } = default!;
	public string? GhiChu { get; init; }
}

public class PhienKhamBenhReadModel
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public NameResponseDTO? LoaiBenh { get; init; }
	public string LoaiChanDoan { get; init; } = default!;
	public string? GhiChu { get; init; }
}
