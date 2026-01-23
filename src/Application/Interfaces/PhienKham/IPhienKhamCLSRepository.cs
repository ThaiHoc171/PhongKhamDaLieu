using Domain.Entities;
using Application.ReadModels;

namespace Application.Interfaces;
public interface IPhienKhamCLSRepository
{
	Task<List<PhienKhamCLSReadModel>> GetByPhienKhamAsync(int phienKhamID);
	Task<PhienKhamCLS?> GetByIdAsync(int id);

	Task AddAsync(PhienKhamCLS phienKhamCLS);
	Task UpdateAsync(PhienKhamCLS phienKhamCLS);
}
