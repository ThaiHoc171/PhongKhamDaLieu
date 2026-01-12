using Domain.Entities;

namespace Application.Interfaces;
public interface IBenhNhanRepository
{
	Task<BenhNhan?> GetByIdAsync(int id);
	Task<List<BenhNhan>> GetNhanViens(string keyword);
	Task<List<BenhNhan>> GetAllAsync();
	Task<int> AddAsync(BenhNhan benhNhan);
	Task UpdateAsync(BenhNhan benhNhan);
}

