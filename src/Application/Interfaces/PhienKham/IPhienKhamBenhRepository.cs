using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPhienKhamBenhRepository
{
	// CUD
	Task AddAsync (PhienKhamBenh phienKhamBenh);
	Task UpdateAsync (PhienKhamBenh phienKhamBenh);
	// Query
	Task<int> CountAsync(int phienKhamID);
	Task<bool> PrimaryExistsAsync(int phienKhamID);
	Task<PhienKhamBenh?> GetByIdAsync(int id);
	Task<List<PhienKhamBenhReadModel>> GetByPhienKhamIdAsync (int phienKhamID);
}
