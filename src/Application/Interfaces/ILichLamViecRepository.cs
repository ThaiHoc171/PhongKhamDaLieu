using Domain.Entities;

namespace Application.Interfaces;

public interface ILichLamViecRepository
{
	Task<bool> IsNgayNghiAsync(DateTime ngay, int nhanVienID);
	Task <bool> IsExitsAsync(int nhanVienID, DateTime ngay, int caLamViec);
	Task AddAsync(LichLamViec lich);
	Task BeginTransactionAsync();
	Task CommitAsync();
	Task RollbackAsync();
}
