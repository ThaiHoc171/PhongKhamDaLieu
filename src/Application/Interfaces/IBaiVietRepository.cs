using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IBaiVietRepository
{
    //--CUD
    Task<int> AddAsync(BaiViet entity);
    Task UpdateAsync(BaiViet entity);
    Task DeleteAsync(int id);
    //--R
    Task<BaiViet?> GetByIdAsync(int id);
    Task<(List<BaiVietListReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai);
    Task<(List<BaiVietListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size, string? trangThai);

	Task<List<BaiVietListReadModel>> GetByLoaiBenhAsync(int loaiBenhID);
    Task<List<BaiVietListReadModel>> GetTopLuotXemAsync(int top);
    Task<BaiVietReadModel?> GetDetailAsync(int id);
}