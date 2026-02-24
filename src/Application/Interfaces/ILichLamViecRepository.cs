using Domain.Entities;

namespace Application.Interfaces;

public interface ILichLamViecRepository
{
	Task <bool> IsExitsAsync(int nhanVienID, DateTime ngay, int caLamViec);
	Task<int> CountNhanVienTheoChucVuAsync(int chucVuId, DateTime ngay, int caLamViec);
	Task<int?> GetChucVuIdByLichLamViecIdAsync(int lichLamViecId);
	Task<List<LichLamViec>> GetByWeekAsync(DateTime tuNgay, DateTime denNgay);
    Task<LichLamViec?> GetByIdAsync(int ID);
	Task<List<LichLamViec>> GetAllAsync();
	Task<List<LichLamViec>> GetByNhanVienIdTheoTuanAsync(int NhanVienID, DateTime tuNgay,DateTime denNgay);
	Task AddAsync(LichLamViec lich);
	Task BeginTransactionAsync();
	Task CommitAsync();
	Task RollbackAsync();
}
