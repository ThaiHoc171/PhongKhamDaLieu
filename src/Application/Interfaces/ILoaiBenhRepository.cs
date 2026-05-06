using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ILoaiBenhRepository
{
    //--CUD
    Task<int> AddAsync(LoaiBenh entity);
    Task BulkInsertAsync(List<LoaiBenh> list);
	Task<int> DeleteAsync(int id);
	Task<int> UpdateAsync(LoaiBenh entity);
	//--Read
	Task<bool> ExistsTenBenhAsync(string tenBenh);
	Task<bool> ExistsTenKhoaHocAsync(string tenKhoaHoc);
	Task<LoaiBenh?> GetByIdAsync(int id);
    Task<LoaiBenhReadModel?> GetDetailAsync(int id);
    Task<(List<LoaiBenhListReadModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<LoaiBenhListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
    Task<List<NameResponseDTO>> GetComboboxAsync();
	Task<string?> GetTenBenhByIdAsync(int id);
}
