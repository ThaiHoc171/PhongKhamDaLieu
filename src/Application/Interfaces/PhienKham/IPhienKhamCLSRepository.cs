using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamCLSRepository
{
	Task<PhienKhamCLS?> GetByIdAsync(int id);
	Task<List<PhienKhamClsReadListModel>> GetByPhienKhamAsync(int phienKhamID);
	Task<PhienKhamClsReadModel?> GetDetailAsync(int id);
	Task<List<PhienKhamClsReadListModel>> GetListAsync();
	Task<int> AddAsync(PhienKhamCLS phienKhamCLS);
	Task<int> UpdateAsync(PhienKhamCLS phienKhamCLS);
}