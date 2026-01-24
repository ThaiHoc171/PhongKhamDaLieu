using Application.ReadModels;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPhienKhamRepository
{
	Task<PhienKham?> GetByIdAsync(int id);
	Task<int> AddAsync(PhienKham phienKham);
	Task UpdateAsync(PhienKham phienKham);
	Task KetThucAsync(PhienKham phienKham);
	Task<PhienKhamReadModel?> GetReadModelByIdAsync(int phienKhamID);
	Task<List<PhienKhamReadModel>> GetAllAsync();
	Task<List<PhienKhamReadModel>> FilterAsync(DateTime? tuNgay,DateTime? denNgay,string? trangThai,int? nhanVienID);
}

