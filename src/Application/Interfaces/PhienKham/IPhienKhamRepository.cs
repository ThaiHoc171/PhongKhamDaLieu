using Application.DTOs;
using Domain.Entities;
public interface IPhienKhamRepository
{
	Task<PhienKham?> GetByIdAsync(int id);
	Task<int> AddAsync(PhienKham entity);
	Task UpdateAsync(PhienKham entity);
	Task KetThucAsync(PhienKham entity);
	Task<int?> GetBenhNhanIdByPhienKhamIdAsync(int phienKhamID);
	Task<(List<PhienKhamResponseLiteDTO>, int)>	GetPagedAsync(int page, int size, int? nhanVienID, string? trangThai);
	Task<(List<PhienKhamResponseLiteDTO>, int)>	GetByBenhNhanPagedAsync(int benhNhanID, int page, int size);
	Task<List<PhienKhamResponseLiteDTO>> SearchAsync(string keyword, int? nhanVienID);
	Task<PhienKhamResponseDTO?> GetDetailAsync(int id);
}