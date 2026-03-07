using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPhienKhamBenhRepository
{
	Task<int> CountPKBenhAsync(int phienKhamID);
	Task<bool> PrimaryPKBenhExitsAsync(int phienKhamID);
	Task<PhienKhamBenh?> GetByIdAsync(int id);
	Task<List<PhienKhamBenhReadModel>> GetByPhienKhamAsync (int phienKhamID);
	Task AddAsync (PhienKhamBenh phienKhamBenh);
	Task UpdateAsync (PhienKhamBenh phienKhamBenh);
}
