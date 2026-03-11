using Application.DTOs;
using Domain.Entities;
public interface IPhienKhamRepository
{
	Task<PhienKham?> GetByIdAsync(int id);
	Task<PhienKhamReadModel?> GetByCaKhamIdAsync(int id);

    Task<int> AddAsync(PhienKham entity);
	Task UpdateAsync(PhienKham entity);
	Task KetThucAsync(PhienKham entity);
	Task<int?> GetBenhNhanIdByPhienKhamIdAsync(int phienKhamID);
	Task<(List<PhienKhamListReadModel>, int)>	GetPagedAsync(int page, int size, int? nhanVienID, string? trangThai);
	Task<(List<PhienKhamListReadModel>, int)>	GetByBenhNhanPagedAsync(int benhNhanID, int page, int size);
	Task<(List<PhienKhamListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size, int? nhanVienID);
	Task<PhienKhamReadModel?> GetDetailAsync(int id);
}