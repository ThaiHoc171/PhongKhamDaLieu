using Domain.Entities;

namespace Application.Interfaces;

public interface ITaiKhamRepository
{
    Task<TaiKham?> GetByIdAsync(int taiKhamID);
    Task<List<TaiKham>> GetAllAsync();
    Task<List<TaiKham>> LocAsync(DateTime ngayDuKien, string trangThai);
    Task<List<TaiKham>> GetByBenhNhanAsync(int benhNhanID);
    Task<int> AddAsync(TaiKham taiKham);
    Task UpdateAsync(TaiKham taiKham);
}
