using Application.Common;
using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IBaiVietRepository
{
    //CUD
    Task<int> AddAsync(BaiViet entity);
    Task UpdateAsync(BaiViet entity);
    Task DeleteAsync(int id);

    //Read
    Task<BaiViet?> GetByIdAsync(int id);
    Task<(List<BaiVietListReadModel>, int)> GetPagedAsync(int page, int size);
    Task<List<BaiViet>> GetByLoaiBenhAsync(int loaiBenhID);
    Task<List<BaiViet>> GetTopLuotXemAsync(int top);
}