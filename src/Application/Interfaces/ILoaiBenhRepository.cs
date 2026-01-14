using Domain.Entities;

namespace Application.Interfaces;

public interface ILoaiBenhRepository
{
	Task<List<LoaiBenh>> GetAllAsync();
	Task<LoaiBenh?> GetByIdAsync(int id);
	Task<List<LoaiBenh>> SearchByTenAsync(string keyword);
	Task AddAsync(LoaiBenh loaiBenh);
	Task UpdateAsync(LoaiBenh loaiBenh);
}
