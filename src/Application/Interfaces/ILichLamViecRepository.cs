using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ILichLamViecRepository
{
	Task<int> AddAsync(LichLamViec entity);
	Task UpdateAsync(LichLamViec entity);
	Task<LichLamViec?> GetByIdAsync(int id);
	Task<List<LichLamViecReadModel>> GetWeekByNhanVienAsync(int nhanVienID, DateTime tuNgay, DateTime denNgay);
	Task<List<LichLamViecChucVuReadModel>> GetWeekAsync(DateTime tuNgay, DateTime denNgay);
	Task<bool> ExistsAsync(int nhanVienID, DateTime ngay, int caLamViec);
	Task<int> CountByChucVuAsync(int chucVuId, DateTime ngay, int caLamViec);
}