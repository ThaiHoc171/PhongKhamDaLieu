using Application.DTOs;
using Domain.Entities;
public interface IPhienKhamRepository
{
	//--CUD
    Task<int> AddAsync(PhienKham entity);
	Task<int> UpdateAsync(PhienKham entity);
	Task<int> KetThucAsync(PhienKham entity);
	//--R
	Task<PhienKham?> GetByIdAsync(int id);
	Task<PhienKhamReadModel?> GetByCaKhamIdAsync(int id);
	Task<int?> GetBenhNhanByIdAsync(int phienKhamID);
	Task<(List<PhienKhamReadListModel>, int)>	GetPagedAsync(int page, int size, int? nhanVienID, string? trangThai);
	Task<(List<PhienKhamReadListModel>, int)>	GetBenhNhanPagedAsync(int benhNhanID, int page, int size);
	Task<(List<PhienKhamReadListModel>, int)> SearchPagedAsync(string keyword, int page, int size, int? nhanVienID);
	Task<PhienKhamReadModel?> GetDetailAsync(int id);
}