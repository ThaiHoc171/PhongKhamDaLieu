using Domain.Entities;
using Application.ReadModels;

namespace Application.Interfaces;
public interface IPhienKhamCLSRepository
{
	Task<PhienKhamCLS?> GetByIdAsync(int id);
	Task<List<PhienKhamCLS>> GetByPhienKhamAsync(int phienKhamID);
	Task AddAsync(PhienKhamCLS phienKhamCLS);
	Task UpdateAsync(PhienKhamCLS phienKhamCLS);
}