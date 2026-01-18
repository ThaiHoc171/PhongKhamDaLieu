
namespace Application.ReadModels;

public class PhienKhamBenhReadModel
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public int LoaiBenhID { get; init; }
	public string TenLoaiBenh { get; init; } = default!;
	public string LoaiChuanDoan { get; init; } = default!;
	public string? GhiChu { get; init; }
}
