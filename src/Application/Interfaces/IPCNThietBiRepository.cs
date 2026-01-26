using Domain.Entities;
using Application.ReadModels;

namespace Application.Interfaces;

public interface IPCNThietBiRepository
{
	Task<int> AddAsync(PCN_TB entity);
	Task UpdateAsync(PCN_TB entity);
	Task DeleteAsync(int id);

	Task<PCNThietBiReadModel?> GetByIdAsync(int id);
	Task<List<PCNThietBiReadModel>> GetByPCNAsync(int pcnId);
}
