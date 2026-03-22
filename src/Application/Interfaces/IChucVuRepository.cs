using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces
{
	public interface IChucVuRepository
	{
        //CUD
        Task<int> AddAsync(ChucVu chucVu);
        Task UpdateAsync(ChucVu chucVu);
        //Read
        Task<(List<ChucVuListReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai);
        Task<(List<ChucVuListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
        Task<ChucVuReadModel?> GetDetailAsync(int id);
        Task<ChucVu?> GetByIdAsync(int id);
		Task<string?> GetNameByIdAsync(int id);
		Task<string?> GetByNhanVienIdAsync(int nhanVienId);
		Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
	}
}
