using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
namespace Application.Interfaces;
public interface IThongTinCaNhanRepository
{
	Task<ThongTinCaNhan?> GetByIdAsync(int thongTinId);
	Task<int> GetIdByTaiKhoanId(int taiKhoanId);

	Task<bool> ExistsByEmailAsync(string email, string sdt);
    Task<ThongTinReadModel?> GetDetailAsync(int id);
	Task<List<ThongTinReadListModel>> GetAllByLoaiAsync(LoaiThongTinEnum loai);
	Task<ThongTinCaNhan?> GetByEmailOrSDTAsync(string? email, string? sdt);
    Task<List<NameResponseDTO>> GetComboboxAsync();
	Task<int> AddAsync(ThongTinCaNhan thongTin);
	Task UpdateAsync(ThongTinCaNhan thongTin);
}
