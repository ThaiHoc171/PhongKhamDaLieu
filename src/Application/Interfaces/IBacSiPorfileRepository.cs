using Domain.Entities;

namespace Application.Repository;

public interface IBacSiProfileRepository
{
	Task<BacSiProfile?> GetByNhanVienIdAsync(int nhanVienID);
    Task<List<BacSiProfile>> GetAllAsync();
    Task AddAsync(BacSiProfile profile);
	Task UpdateAsync(BacSiProfile profile);
}
