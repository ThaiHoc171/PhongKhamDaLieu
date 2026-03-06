using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamRepository
{
	Task<PhienKham?> GetByIdAsync(int id);
	Task<int?> GetBenhNhanIdByPhienKhamIdAsync(int phienKhamID);
	Task<(List<PhienKham> Data, int TotalCount)> GetByBenhNhanPagedAsync(int benhNhanID, int pageNumber, int pageSize);
	Task<int> AddAsync(PhienKham phienKham);
	Task UpdateAsync(PhienKham phienKham);
	Task KetThucAsync(PhienKham phienKham);
	Task<(List<PhienKham> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, int? nhanVienID, string? trangThai);
	Task<List<PhienKham>> SearchAsync(string keyword, int? nhanVienID);
}

