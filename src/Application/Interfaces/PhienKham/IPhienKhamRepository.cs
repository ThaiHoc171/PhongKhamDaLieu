using Domain.Entities;

namespace Application.Interfaces;

public interface IPhienKhamRepository
{
	Task<PhienKham?> GetByIdAsync(int id);
	Task<int> AddAsync(PhienKham phienKham);
	Task UpdateAsync(PhienKham phienKham);
}

