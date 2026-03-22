using Application.DTOs;
using Domain.Entities;
public interface IThuocRepository
{
    //--CUD
    Task<int> AddAsync(Thuoc entity);
    Task UpdateAsync(Thuoc entity);
    Task DeleteAsync(int id);
    //--READ
    Task<Thuoc?> GetByIdAsync(int id);
    Task<(List<ThuocListReadModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<ThuocListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
    Task<ThuocReadModel?> GetDetailAsync(int id);
    Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
}