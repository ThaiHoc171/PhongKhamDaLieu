using Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IThongTinCaNhanRepository
{
	Task<ThongTinCaNhan?> GetByIdAsync(int thongTinId);
	Task<ThongTinFullReadModel?> GetDetailAsync(int id);
	Task<List<ThongTinLiteReadModel>> GetAllByLoaiAsync(LoaiThongTinEnum loai);
	Task<List<NameResponseDTO>> GetComboboxAsync();
	Task<int> AddAsync(ThongTinCaNhan thongTin);
	Task UpdateAsync(ThongTinCaNhan thongTin);
}
