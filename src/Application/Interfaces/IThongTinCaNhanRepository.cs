using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IThongTinCaNhanRepository
{
	Task<ThongTinCaNhan?> GetByIdAsync(int thongTinId);
	Task<List<ThongTinCaNhan>> GetAllByLoaiAsync(LoaiThongTinEnum loai);
	Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
	Task<int> AddAsync(ThongTinCaNhan thongTin);
	Task UpdateAsync(ThongTinCaNhan thongTin);
}
