using Domain.Entities;

namespace Application.Interfaces;

public interface ITaiKhamRepository
{
    Task<List<TaiKham>> GetAllAsync();
    Task<int> AddAsync(TaiKham taiKham);
    Task<TaiKham?> GetByIdAsync(int id);
    Task<List<TaiKham>> GetByBenhNhanAsync(int benhNhanId);
    Task UpdateAsync(TaiKham taiKham);
}
