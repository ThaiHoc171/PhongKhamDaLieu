using Domain.Entities;

namespace Application.Repository;

public interface IBacSiProfileRepository
{
	Task<BacSiProfile?> GetByNhanVienIdAsync(int nhanVienID);
	Task AddAsync(BacSiProfile profile);
	Task UpdateAsync(BacSiProfile profile);
}
