using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IBacSiProfileRepository
{
    //CUD 
    Task AddAsync(BacSiProfile entity);
    Task UpdateAsync(BacSiProfile entity);
    //READ
    Task<BacSiProfile?> GetByIdAsync(int id);
    Task<BacSiProfileReadModel?> GetDetailAsync(int id);
    Task<BacSiProfileReadModel?> GetByNhanVienIdAsync(int nhanVienId);
    Task<(List<BacSiProfileListReadModel>, int)> GetPagedAsync(int page, int size);
    Task<(List<BacSiProfileListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
}