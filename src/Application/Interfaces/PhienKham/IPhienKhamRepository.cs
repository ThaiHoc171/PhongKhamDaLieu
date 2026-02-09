using Application.ReadModels;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPhienKhamRepository
{
	Task<PhienKham?> GetByIdAsync(int id);
	Task<int?> GetBenhNhanIdByPhienKhamIdAsync(int phienKhamID);

    Task<int> AddAsync(PhienKham phienKham);
	Task UpdateAsync(PhienKham phienKham);
	Task KetThucAsync(PhienKham phienKham);
	Task<List<PhienKham>> GetAllAsync();
	Task<List<PhienKham>> FilterAsync(DateTime? tuNgay,DateTime? denNgay,string? trangThai,int? nhanVienID);
}

