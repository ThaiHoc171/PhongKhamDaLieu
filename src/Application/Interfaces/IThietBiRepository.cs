using Application.DTOs;
using Domain.Entities;
public interface IThietBiRepository
{
    Task<int> AddAsync(ThietBi entity);
    Task BulkInsertAsync(List<ThietBi> list);
	Task<int> UpdateAsync(ThietBi entity);
    Task<ThietBi?> GetByIdAsync(int id);
    Task<(List<ThietBiReadListModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<ThietBiReadListModel>, int)> SearchPagedAsync(string keyword, int page, int size);
    Task<ThietBiReadModel?> GetDetailAsync(int id);
    Task<List<NameResponseDTO>> GetComboboxAsync();
}