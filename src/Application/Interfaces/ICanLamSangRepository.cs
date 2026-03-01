using Domain.Entities;

namespace Application.Interfaces;

public interface ICanLamSangRepository
{
	Task<(List<CanLamSang> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
	Task<CanLamSang?> GetByIdAsync(int id);
	Task<List<(int Id, string Ten)>> GetIdAndNameAsync();

    Task AddAsync(CanLamSang cls);
	Task UpdateAsync(CanLamSang cls);
	Task<List<CanLamSang>> SearchByTenAsync(string tenCLS);
}
