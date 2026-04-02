using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPCNThietBiRepository
{
    // ---- CUD
    Task<int> AddAsync(PCNThietBi entity);
    Task<int> UpdateAsync(PCNThietBi entity);
    Task<int> DeleteAsync(int pcnTbId);
    // ---- Read
    Task<PCNThietBi?> GetByIdAsync(int pcnTbId);
    Task<PCNThietBi?> GetByPhongAndThietBiAsync(int phongChucNangId, int thietBiId);
    Task<(List<PCNThietBiReadListModel>, int)> GetPagedAsync(int page, int size, int? phongChucNangID);
    Task<(List<PCNThietBiReadListModel>, int)> SearchPagedAsync(string keyword, int page,int size, int? phongChucNangID);
    Task<List<PCNThietBiReadModel>> GetByPhongAsync(int phongChucNangID);
    Task<PCNThietBiReadModel?> GetDetailAsync(int pcnTbId);
}