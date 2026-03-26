using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ICanLamSangRepository
{
    Task<int> AddAsync(CanLamSang entity);
	Task BulkInsertAsync(List<CanLamSang> list);
	Task<int> UpdateAsync(CanLamSang entity);
    Task<CanLamSang?> GetByIdAsync(int id);
    Task<(List<CanLamSangListReadModel>, int)>GetPagedAsync(int page, int size);
    Task<(List<CanLamSangListReadModel>, int)>SearchPagedAsync(string keyword, int page, int size);
    Task<List<CanLamSangListReadModel>>GetByLoaiXetNghiemAsync(string loaiXetNghiem);
    Task<CanLamSangReadModel?>GetDetailAsync(int id);
    Task<List<NameResponseDTO>> GetComboboxAsync();
}