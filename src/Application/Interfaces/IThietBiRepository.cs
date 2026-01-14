using Domain.Entities;

namespace Application.Interfaces;

public interface IThietBiRepository
{
	Task<List<ThietBi>> GetAllAsync();
	Task<ThietBi?> GetByIdAsync(int id);
	Task<List<ThietBi>> SearchByTenAsync(string tenTB);
	Task AddAsync(ThietBi thietBi);
	Task UpdateAsync(ThietBi thietBi);
}
