using Domain.Entities;

namespace Application.Interfaces;

public interface IPhongChucNangRepository
{
	Task<List<PhongChucNang>> GetAllAsync();
	Task<List<PhongChucNang>> SearchAsync(string keyword);
	Task<PhongChucNang?> GetByIdAsync(int id);
	Task AddAsync(PhongChucNang phong);
	Task UpdateAsync(PhongChucNang phong);
}
