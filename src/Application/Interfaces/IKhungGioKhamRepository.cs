using Domain.Entities;

namespace Application.Interfaces;

public interface IKhungGioKhamRepository
{
	Task<List<KhungGioKham>> GetAllAsync();
	Task<KhungGioKham?> GetByIdAsync(int id);
	Task AddAsync(KhungGioKham khungGio);
	Task UpdateAsync(KhungGioKham khungGio);
}
