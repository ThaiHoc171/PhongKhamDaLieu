using Domain.Entities;
namespace Application.Interfaces;
public interface IToaThuocRepository
{
	Task<int> AddAsync(ToaThuoc toaThuoc);
	Task<ToaThuoc?> GetByPhienKhamIdAsync(int phienKhamID);
}

public interface IChiTietToaThuocRepository
{
	Task AddAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet);
	Task<List<ChiTietToaThuoc>> GetByToaThuocIdAsync(int toaThuocID);
}
