using Domain.Entities;

namespace Application.Interfaces;

public interface IThuocRepository
{
	Task<List<Thuoc>> GetAllAsync();
	Task<List<Thuoc>> SearchAsync(string keyword);
	Task<List<(int Id, string Ten)>> GetIdAndNameAsync();

    Task<Thuoc?> GetByIdAsync(int id);
	Task AddAsync(Thuoc thuoc);
	Task UpdateAsync(Thuoc thuoc);
}
