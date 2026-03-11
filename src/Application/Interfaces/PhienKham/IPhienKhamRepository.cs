using Application.DTOs;
using Domain.Entities;
public interface IPhienKhamRepository
{
	//--CRUD
    Task<int> AddAsync(PhienKham entity);
	Task UpdateAsync(PhienKham entity);
	Task KetThucAsync(PhienKham entity);
	//--Read-only
	Task<PhienKham?> GetByIdAsync(int id);
	Task<PhienKhamReadModel?> GetByCaKhamIdAsync(int id);
	Task<int?> GetBenhNhanByIdAsync(int phienKhamID);
	Task<(List<PhienKhamListReadModel>, int)>	GetPagedAsync(int page, int size, int? nhanVienID, string? trangThai);
	Task<(List<PhienKhamListReadModel>, int)>	GetBenhNhanPagedAsync(int benhNhanID, int page, int size);
	Task<(List<PhienKhamListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size, int? nhanVienID);
	Task<PhienKhamReadModel?> GetDetailAsync(int id);
}