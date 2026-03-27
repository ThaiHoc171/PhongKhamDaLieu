using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces
{
	public interface IChucVuRepository
	{
        Task<int> AddAsync(ChucVu chucVu);
		Task BulkInsertAsync(List<ChucVu> list);
		Task<int> UpdateAsync(ChucVu chucVu);
        Task<(List<ChucVuListReadModel>, int)> GetPagedAsync(int page, int size);
        Task<(List<ChucVuListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
        Task<ChucVuReadModel?> GetDetailAsync(int id);
        Task<ChucVu?> GetByIdAsync(int id);
		Task<string?> GetByNhanVienIdAsync(int nhanVienId);
		Task<List<NameResponseDTO>> GetComboboxAsync();
	}
}
