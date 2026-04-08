using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IPhienKhamCLSRepository
{
	Task<PhienKhamCLS?> GetByIdAsync(int id);
	Task<List<PhienKhamClsReadListModel>> GetByPhienKhamAsync(int phienKhamID);
	Task<PhienKhamClsReadModel?> GetDetailAsync(int id);
	Task<(List<PhienKhamClsReadListModel>, int)> GetPagedAsync(string? trangThai, int page, int size);
	Task<(List<PhienKhamClsReadListModel>, int)> SearchPagedAsync(string keyword, string? trangThai, int page, int size);
	Task<int> AddAsync(PhienKhamCLS phienKhamCLS);
	Task<int> UpdateAsync(PhienKhamCLS phienKhamCLS);
}