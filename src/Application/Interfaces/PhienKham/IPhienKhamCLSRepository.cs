using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamCLSRepository
{
	Task<PhienKhamCLS?> GetByIdAsync(int id);
	Task<List<PhienKhamClsListReadModel>> GetByPhienKhamAsync(int phienKhamID);
	Task<PhienKhamClsReadModel?> GetDetailAsync(int id);
	Task<List<PhienKhamClsListReadModel>> GetListAsync();
	Task AddAsync(PhienKhamCLS phienKhamCLS);
	Task UpdateAsync(PhienKhamCLS phienKhamCLS);
}