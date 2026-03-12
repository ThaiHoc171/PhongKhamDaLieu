using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces;

public interface IThuocRepository
{
    Task<List<ThuocListReadModel>> GetPagedAsync(int pageNumber, int pageSize);

    Task<int> CountAsync();

    Task<List<ThuocListReadModel>> SearchAsync(string keyword);

    Task<List<ThuocComboboxReadModel>> GetComboboxAsync();

    Task<Thuoc?> GetByIdAsync(int id);

    Task<List<Thuoc>> GetAllAsync();

    Task AddAsync(Thuoc thuoc);

    Task UpdateAsync(Thuoc thuoc);
}