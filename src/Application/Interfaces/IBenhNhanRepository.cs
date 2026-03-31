using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IBenhNhanRepository
{
	//CUD
    Task<int> AddAsync(BenhNhan benhNhan);
    Task<int> UpdateAsync(BenhNhan benhNhan);
	//Read
    Task<bool> ExistsByThongTinIdAsync(int thongTinId);
	Task<BenhNhan?> GetByIdAsync(int id);
	Task<BenhNhanReadModel?> GetDetailAsync(int id);
	Task<BenhNhanReadModel?> GetByThongTinIDAsync(int thongTinId);
    Task<(List<BenhNhanReadListModel> Data, int TotalCount)> SearchAsync(string keyword, int pageNumber, int pageSize);
	Task<(List<BenhNhanReadListModel> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
	Task<List<NameResponseDTO>> GetComboboxAsync();
}
