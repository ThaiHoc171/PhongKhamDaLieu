using Domain.Entities;
namespace Application.Interfaces;
public interface ILoaiBenhRepository
{
	Task<List<LoaiBenh>> GetAllAsync();
	Task<(List<LoaiBenh> Data, int TotalCount)> GetPageAsync(int pageNumber, int pageSize);
	Task<LoaiBenh?> GetByIdAsync(int id);
	Task<List<LoaiBenh>> SearchByTenAsync(string keyword);
    Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
    Task AddAsync(LoaiBenh loaiBenh);
	Task UpdateAsync(LoaiBenh loaiBenh);
	Task<string?> GetNameByIdAsync(int id);
}
