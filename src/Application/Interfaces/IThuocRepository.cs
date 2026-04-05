using Application.DTOs;
using Domain.Entities;
public interface IThuocRepository
{
    //--CUD
    Task<int> AddAsync(Thuoc entity);
    Task<int> UpdateAsync(Thuoc entity);
    Task BulkInsertAsync(List<Thuoc> list);
    Task DeleteAsync(int id);
	//--READ
	Task<Thuoc?> GetByIdAsync(int id);
    Task<(List<ThuocReadModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<ThuocReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
    Task<ThuocReadModel?> GetDetailAsync(int id);
    Task<bool> ExistsTenThuocAsync(string tenThuoc);
    Task<List<NameResponseDTO>> GetComboboxAsync();

}