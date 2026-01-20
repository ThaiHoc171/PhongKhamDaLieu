using Domain.Entities;

namespace Application.Interfaces;

public interface ILichLamViecRepository
{
	Task<bool> IsNgayNghiAsync(DateTime ngay, int nhanVienID);
	Task <bool> IsExitsAsync(int nhanVienID, DateTime ngay, int caLamViec);
	Task<bool> IsChucVuExitsAsync(int ChucVuID, DateTime ngay, int caLamViec);
	Task<LichLamViec?> GetByIdAsync(int ID);
	Task<List<LichLamViec>> GetAllAsync();
	Task<List<LichLamViec>> GetByNhanVienIdTheoTuanAsync(int NhanVienID, DateTime tuNgay,DateTime denNgay);
	Task AddAsync(LichLamViec lich);
	Task BeginTransactionAsync();
	Task CommitAsync();
	Task RollbackAsync();
}
