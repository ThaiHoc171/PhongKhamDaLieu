using Domain.Entities;

namespace Application.Interfaces;

public interface IPCNThietBiRepository
{
	// Command
	Task<int> AddAsync(PCNThietBi entity);
	Task UpdateAsync(PCNThietBi entity);
	Task DeleteAsync(int id);
	Task<bool> ExistsAsync(int pcnId, int thietBiId);
	// Query
	Task<PCNThietBi?> GetByIdAsync(int id);
	Task<List<PCNThietBi>> GetByPCNAsync(int phongChucNangId);
	Task<List<ThietBiNhapRaw>> GetChiTietNhapAsync(int phongId);
	Task<TongTheoPhongRaw?> GetPhongTongAsync(int phongId);

	// raw data records
	public record ThietBiNhapRaw(
		int ThietBiId,
		string TenThietBi,
		DateTime NgayNhap,
		int SoLuong
	);

	public record TongTheoPhongRaw(
		int PhongChucNangId,
		string TenPhong,
		int TongSoLuong
	);
}
