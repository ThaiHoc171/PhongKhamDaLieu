using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IChiTietPCNThietBiRepository
{
    //CUD
    Task<int> AddAsync(ChiTietPCNThietBi entity);
    Task BulkInsertAsync(List<ChiTietPCNThietBi> list);
    Task<int> UpdateAsync(ChiTietPCNThietBi entity);
    Task<int> DeleteAsync(int chiTietId);

    //READ
    Task<bool> ExistsMaTaiSanAsync(string maTaiSan);
	Task<ChiTietPCNThietBi?> GetByIdAsync(int chiTietId);
    Task<ChiTietPCNThietBiReadModel?> GetDetailAsync(int chiTietId);
    Task<List<ChiTietPCNThietBiListReadModel>> GetListAsync(int pcnTbId);
    Task<List<NameResponseDTO>> GetComboboxAsync(int pcnId, int tbId);
}