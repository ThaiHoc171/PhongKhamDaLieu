using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ILoaiBenhRepository
{
    //--CUD
    Task<int> AddAsync(LoaiBenh entity);
    Task UpdateAsync(LoaiBenh entity);
    //--Read
    Task<IEnumerable<LoaiBenh>> GetAllAsync();
    Task<LoaiBenh?> GetByIdAsync(int id);
    Task<LoaiBenhReadModel?> GetDetailAsync(int id);
    Task<(List<LoaiBenhListReadModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<LoaiBenhListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
    Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
    Task<string?> GetTenBenhByIdAsync(int id);
}
