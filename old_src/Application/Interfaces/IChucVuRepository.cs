using Domain.Entities;

namespace Application.Interfaces
{
	public interface IChucVuRepository
	{
		Task<List<ChucVu>> GetAllAsync();
		Task<ChucVu?> GetByIdAsync(int id);
		Task AddAsync(ChucVu chucVu);
		Task UpdateAsync(ChucVu chucVu);
	}
}
