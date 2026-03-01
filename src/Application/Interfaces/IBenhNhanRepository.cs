using Domain.Entities;

namespace Application.Interfaces;
public interface IBenhNhanRepository
{
	Task<BenhNhan?> GetByIdAsync(int id);
	Task<List<BenhNhan>> GetBenhNhans(string keyword);
	Task<(List<BenhNhan> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
	Task<string?> GetNameByIdAsync(int id);
	Task<int> GetForAuthAsync(int taiKhoanID);
	Task<int> AddAsync(BenhNhan benhNhan);
	Task UpdateAsync(BenhNhan benhNhan);
	Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
}

