using Domain.Entities;

namespace Application.Interfaces
{
	public interface IChucVuRepository
	{
		Task<List<ChucVu>> GetAllAsync();
		Task<ChucVu?> GetByIdAsync(int id);
		Task<string?> GetNameByIdAsync(int id);
		Task<string?> GetByNhanVienIdAsync(int nhanVienId);
		Task AddAsync(ChucVu chucVu);
		Task UpdateAsync(ChucVu chucVu);
		Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
	}
}
