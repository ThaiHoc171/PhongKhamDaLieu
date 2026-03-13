using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces;

public interface IThuocRepository
{
    Task<List<ThuocReadModel>> GetPagedAsync(int pageNumber, int pageSize);

    Task<int> CountAsync();

    Task<List<ThuocReadModel>> SearchAsync(string keyword);

    Task<List<NameResponseDTO>> GetComboboxAsync();

    Task<Thuoc?> GetByIdAsync(int id);

    Task<List<Thuoc>> GetAllAsync();

    Task AddAsync(Thuoc thuoc);

    Task UpdateAsync(Thuoc thuoc);
}