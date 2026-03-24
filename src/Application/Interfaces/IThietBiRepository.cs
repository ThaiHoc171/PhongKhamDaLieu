using Application.DTOs;
using Domain.Entities;
public interface IThietBiRepository
{
    Task<int> AddAsync(ThietBi entity);
    Task<int> UpdateAsync(ThietBi entity);
    Task<ThietBi?> GetByIdAsync(int id);
    Task<(List<ThietBiReadModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<ThietBiReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
    Task<ThietBiReadModel?> GetDetailAsync(int id);
}