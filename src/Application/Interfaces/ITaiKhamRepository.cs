using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ITaiKhamRepository
{
    Task<TaiKham?> GetByIdAsync(int taiKhamID);
    Task<TaiKhamDetailReadModel?> GetDetailAsync(int taiKhamID);
	Task<TaiKham?> GetByBenhNhanIdAsync(int benhNhanID);
    Task<TaiKham?> GetTaiKhamDangChoAsync(int benhNhanID);
    Task<(List<TaiKhamReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai);
    Task<(List<TaiKhamReadModel>, int)> SearchAsync(string? keyword, int page, int size);
    Task<(List<TaiKhamReadModel>, int)> GetListByBenhNhanAsync(int benhNhanID, int page, int size);
	Task<bool> ExistsByPhienKhamAsync(int phienKhamID);
    Task<int> AddAsync(TaiKham taiKham);
    Task UpdateAsync(TaiKham taiKham);
}
