using Application.ReadModels;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPhienKhamBenhRepository
{
	Task<int> CountPKBenhAsync(int phienKhamID);
	Task<bool> PrimaryPKBenhExitsAsync(int phienKhamID);
	Task<List<PhienKhamBenhReadModel>> GetByIdAsync (int phienKhamID);
	Task AddAsync (PhienKhamBenh phienKhamBenh);
	Task UpdateAsync (PhienKhamBenh phienKhamBenh);
}
