using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IPhongChucNangRepository
{
    //CUD
    Task<int> AddAsync(PhongChucNang phong);
    Task UpdateAsync(PhongChucNang phong);
    //Read
    Task<PhongChucNang?> GetByIdAsync(int id);
	Task<(List<PhongChucNangListReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai);
	Task<(List<PhongChucNangListReadModel>, int)> SearchPagedAsync(string? keyword, int page, int size);
	Task<PhongChucNangReadModel?> GetDetailAsync(int id);
	Task<List<(int Id, string Ten)>> GetComboboxAsync();
}