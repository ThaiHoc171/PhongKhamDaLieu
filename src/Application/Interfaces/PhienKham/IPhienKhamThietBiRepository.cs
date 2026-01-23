using Application.ReadModels;
using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamThietBiRepository
{
	Task<List<PhienKhamThietBiReadModel>> GetByPhienKhamAsync(int phienKhamId);
	Task<PhienKhamThietBi?> GetByPhienKhamAndThietBiAsync(int phienKhamId, int thietBiId);
	Task<PhienKhamThietBi?> GetByIdAsync(int id);
	Task AddAsync(PhienKhamThietBi entity);
	Task UpdateAsync(PhienKhamThietBi entity);
}