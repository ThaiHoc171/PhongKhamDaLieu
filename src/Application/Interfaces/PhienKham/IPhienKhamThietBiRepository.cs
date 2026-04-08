using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamThietBiRepository
{
	Task<List<PhienKhamThietBiReadModel>> GetByPhienKhamAsync(int phienKhamID);
	Task<PhienKhamThietBi?> GetByIdAsync(int id);
	Task<PhienKhamThietBi?> GetByPhienKhamAndChiTietAsync(int phienKhamID, int chiTietID);
	Task<int> AddAsync(PhienKhamThietBi entity);
	Task<int> UpdateAsync(PhienKhamThietBi entity);
	Task<int> DeleteAsync(int id);
}