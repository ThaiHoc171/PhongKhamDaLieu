using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ITaiKhamRepository
{
    Task<TaiKham?> GetByIdAsync(int taiKhamID);
    Task<TaiKhamReadModel?> GetDetailAsync(int taiKhamID);
    Task<TaiKham?> GetTaiKhamDangChoAsync(int benhNhanID);
    Task<(List<TaiKhamReadListModel>, int)> GetPagedAsync(int page, int size, string? trangThai);
    Task<(List<TaiKhamReadListModel>, int)> SearchAsync(string? keyword, int page, int size);
    Task<(List<TaiKhamReadListModel>, int)> GetPagedByBenhNhanAsync(int benhNhanID, int page, int size);
	Task<bool> ExistsByPhienKhamAsync(int phienKhamID);
    Task<int> AddAsync(TaiKham taiKham);
    Task<int> UpdateAsync(TaiKham taiKham);
}
