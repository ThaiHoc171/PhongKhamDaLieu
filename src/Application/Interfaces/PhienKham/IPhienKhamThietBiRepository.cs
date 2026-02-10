using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamThietBiRepository
{
	Task AddAsync(PhienKhamThietBi entity);
	Task<List<PhienKhamThietBi>> GetByPhienKhamAsync(int phienKhamID);
	Task<PhienKhamThietBi?> GetByIdAsync(int id);
	Task<PhienKhamThietBi?> GetByPhienKhamAndChiTietAsync(int phienKhamID, int chiTietID);
	Task UpdateAsync(PhienKhamThietBi entity);
}