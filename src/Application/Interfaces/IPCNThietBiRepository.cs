using Domain.Entities;
namespace Application.Interfaces;
public interface IPCNThietBiRepository
{
	Task<List<PCNThietBi>> GetByPhongAsync(int pcnId);
	Task<PCNThietBi?> GetByIdAsync(int pcnTbId);
	Task<PCNThietBi?> GetByPhongAndThietBiAsync(int phongChucNangId, int thietBiId);
	Task AddAsync(PCNThietBi entity);
	Task UpdateAsync(PCNThietBi entity);
	Task DeleteAsync(int pcnTbId);
}
