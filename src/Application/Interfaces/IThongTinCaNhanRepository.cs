using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
namespace Application.Interfaces;
public interface IThongTinCaNhanRepository
{
	Task<ThongTinCaNhan?> GetByIdAsync(int thongTinId);
	Task<int> GetIdByTaiKhoanId(int taiKhoanId);
    Task<ThongTinReadModel?> GetDetailAsync(int id);
	Task<(List<ThongTinReadListModel>, int)> GetPagedAsync(int page, int size);
	Task<(List<ThongTinReadListModel>, int)> SearchPagedAsync(string keyword, int page, int size);
	Task<ThongTinCaNhan?> GetByEmailOrSDTAsync(string? email, string? sdt);
	Task<int> AddAsync(ThongTinCaNhan thongTin);
	Task<int> UpdateAsync(ThongTinCaNhan thongTin);
}
