using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IToaThuocRepository
{
	Task<bool> IsToaThuocExits(int phienKhamID);
	Task<int> AddAsync(ToaThuoc toaThuoc);
	Task<ToaThuocReadModel> GetByPhienKhamAsync(int phienKhamID);
	Task<(List<ToaThuocReadModel>, int)> GetPagedAsync(int page, int size);
	Task DeleteAsync(int toaThuocID);
}

public interface IChiTietToaThuocRepository
{
	Task<List<int>> GetThuocIdsAsync(int toaThuocID);
	Task AddAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet);
	Task<List<ChiTietToaThuocReadModel>> GetByToaThuocAsync(int toaThuocID);
	Task UpdateAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet);
	Task DeleteAsync(int toaThuocID, int thuocID);
	Task<int> CountAsync(int toaThuocID);
}
