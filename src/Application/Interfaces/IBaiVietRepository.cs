using Domain.Entities;

namespace Application.Interfaces;

public interface IBaiVietRepository
{
    Task<int> AddAsync(BaiViet baiViet);
    Task UpdateAsync(BaiViet baiViet);
    Task<BaiViet?> GetByIdAsync(int id);
    Task<List<BaiViet>> GetAllAsync();
    Task<List<BaiViet>> GetByLuotXemAsync();
    Task<List<BaiViet>> GetByLoaiBenhAsync(int loaiBenhID);
}