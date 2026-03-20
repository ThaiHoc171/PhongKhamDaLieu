using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IChiTietPCNThietBiRepository
{
    //CUD
    Task<int> AddAsync(ChiTietPCNThietBi entity);
    Task UpdateAsync(ChiTietPCNThietBi entity);
    Task DeleteAsync(int chiTietId);
    //READ
    Task<ChiTietPCNThietBi?> GetByIdAsync(int chiTietId);
    Task<ChiTietPCNThietBiReadModel?> GetDetailAsync(int chiTietId);
    Task<(List<ChiTietPCNThietBiListReadModel>, int)> GetPagedAsync(int pcnTbId, int page, int size);
    Task<(List<ChiTietPCNThietBiListReadModel>, int)> SearchPagedAsync(int pcnTbId, string keyword, int page, int size);
    Task<List<(int Id, string Ten)>> GetComboboxAsync(int pcnTbId);
}