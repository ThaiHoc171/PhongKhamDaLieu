using Domain.Entities;

namespace Application.Interfaces;

public interface ITaiKhoanRepository
{
	Task<TaiKhoan?> GetByEmailAsync(string email);
	Task<TaiKhoan?> GetByIdAsync(int id);
	Task<List<TaiKhoan>> GetAllAsync();
	Task AddAsync(TaiKhoan taiKhoan);
	Task UpdateAsync(TaiKhoan taiKhoan);
}

