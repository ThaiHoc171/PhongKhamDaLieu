using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ITaiKhoanRepository
{
	Task<TaiKhoan?> GetByEmailAsync(string email);
	Task<TaiKhoan?> GetByIdAsync(int id);
	Task<bool> ExistsByEmailAsync(string email);

    Task<TaiKhoanReadModel?> GetDetailAsync(int id);
	Task<(List<TaiKhoanListReadModel>, int)>
		GetPagedAsync(int page, int size, string? vaiTro, string? trangThai);
	Task<int> AddAsync(TaiKhoan taiKhoan);
	Task<int> UpdateAsync(TaiKhoan taiKhoan);
    Task UpdateFcmTokenAsync(int taiKhoanId, string? fcmToken);
}
