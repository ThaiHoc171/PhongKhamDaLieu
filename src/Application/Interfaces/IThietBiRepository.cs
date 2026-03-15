using Application.DTOs;
using Domain.Entities;
public interface IThietBiRepository
{
    //-- CUD
    Task<int> AddAsync(ThietBi entity);
    Task UpdateAsync(ThietBi entity);
    Task DeleteAsync(int id);
    //-- READ
    Task<ThietBi?> GetByIdAsync(int id);
    Task<(List<ThietBiListReadModel>, int)> GetPagedAsync(
        int page,
        int size
    );
    Task<(List<ThietBiListReadModel>, int)> SearchPagedAsync(
        string keyword,
        int page,
        int size
    );
    Task<ThietBiReadModel?> GetDetailAsync(int id);
}