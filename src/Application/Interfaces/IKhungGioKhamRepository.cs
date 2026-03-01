using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Application.Interfaces;

public interface IKhungGioKhamRepository
{
	Task<List<KhungGioKham>> GetAllAsync();
	Task<KhungGioKham?> GetByIdAsync(int id);
	Task<int> CountKhungGioKhamAsync();
    Task<List<int>> GetKhungGioIdsByCaLamViecAsync(int caLamViec);
    Task AddAsync(KhungGioKham khungGio);
	Task UpdateAsync(KhungGioKham khungGio);
	Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
}
