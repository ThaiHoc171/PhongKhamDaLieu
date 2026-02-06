using Domain.Entities;

namespace Application.Interfaces;

public interface IChiTietPCNThietBiRepository
{
	Task<List<ChiTietPCNThietBi>> GetByPCNTBIdAsync(int pcnTbId);
	Task<ChiTietPCNThietBi?> GetByIdAsync(int chiTietId);

	Task AddAsync(ChiTietPCNThietBi chiTiet);
	Task UpdateAsync(ChiTietPCNThietBi chiTiet);
	Task DeleteAsync(int chiTietId);
}
