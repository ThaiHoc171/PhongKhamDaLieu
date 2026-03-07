using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IToaThuocRepository
{
	Task<int> AddAsync(ToaThuoc toaThuoc);
	Task<ToaThuocReadModel> GetByPhienKhamAsync(int phienKhamID);
	Task<(List<ToaThuocReadModel>, int)> GetPagedAsync(int page, int size);
}

public interface IChiTietToaThuocRepository
{
	Task AddAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet);
	Task<List<ChiTietToaThuocReadModel>> GetByToaThuocAsync(int toaThuocID);
}
