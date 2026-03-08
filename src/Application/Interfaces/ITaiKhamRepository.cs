using Domain.Entities;

namespace Application.Interfaces;

public interface ITaiKhamRepository
{
    Task<TaiKham?> GetByIdAsync(int taiKhamID);
    Task<TaiKham?> GetByBenhNhanIdAsync(int benhNhanID);
    Task<int?> GetTaiKhamChoXuLyAsync(int benhNhanID);
    Task<List<TaiKham>> GetAllAsync();
    Task<List<TaiKham>> LocAsync(DateTime ngayDuKien, string trangThai);
    Task<List<TaiKham>> GetListByBenhNhanAsync(int benhNhanID);
    Task<bool> ExistsByPhienKhamAsync(int phienKhamID);
    Task<int> AddAsync(TaiKham taiKham);
    Task UpdateAsync(TaiKham taiKham);
}
