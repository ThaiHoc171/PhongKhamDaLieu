using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ILichLamViecRepository
{
	Task BulkInsertAsync(List<LichLamViec> list);
	Task<LichLamViec?> GetByIdAsync(int id);
	Task<List<LichLamViecReadListModel>> GetWeekByNhanVienAsync(int nhanVienID, DateTime tuNgay, DateTime denNgay);
	Task<List<LichLamViecReadListModel>> GetWeekAsync(DateTime tuNgay, DateTime denNgay);
	Task<bool> ExistsAsync(int nhanVienID, DateTime ngay, int caLamViec);
	Task<int> CountByChucVuAsync(int chucVuId, DateTime ngay, int caLamViec);
}