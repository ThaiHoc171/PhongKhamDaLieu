using Domain.Entities;

namespace Application.Interfaces;

public interface ICanLamSangRepository
{
	Task<List<CanLamSang>> GetAllAsync();
	Task<CanLamSang?> GetByIdAsync(int id);
	Task<List<(int Id, string Ten)>> GetIdAndNameAsync();

    Task AddAsync(CanLamSang cls);
	Task UpdateAsync(CanLamSang cls);
}
