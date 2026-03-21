using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IBenhNhanRepository
{
	Task<bool> ExistsByThongTinIdAsync(int thongTinId);
	Task<BenhNhan?> GetByIdAsync(int id);
	Task<BenhNhanDetailReadModel?> GetDetailAsync(int id);
	Task<BenhNhanDetailReadModel?> GetByThongTinIDAsync(int thongTinId);

    Task<(List<BenhNhanReadModel> Data, int TotalCount)> SearchAsync(string? keyword, int pageNumber, int pageSize);
	Task<(List<BenhNhanReadModel> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
    Task AddAsync(BenhNhan benhNhan);
	Task UpdateAsync(BenhNhan benhNhan);
	Task<List<NameResponseDTO>> GetComboboxAsync();
}
