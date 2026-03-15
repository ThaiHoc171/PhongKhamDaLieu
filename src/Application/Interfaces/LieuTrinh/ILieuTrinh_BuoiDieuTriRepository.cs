using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface IBuoiDieuTriRepository
{
	Task<BuoiDieuTri?> GetByIdAsync(int id);
	Task<List<BuoiDieuTriListReadModel>> GetByLieuTrinhAsync(int lieuTrinhID);
	Task<BuoiDieuTriReadModel?> GetDetailAsync(int id);
	Task<int> GetMaxSoBuoiAsync(int lieuTrinhID);
	Task<int> CountHoanThanhAsync(int lieuTrinhID);
	Task<bool> ExistsByCaKhamAsync(int caKhamID);
	Task<BuoiDieuTri?> GetLastAsync(int lieuTrinhID);
	Task<int> AddAsync(BuoiDieuTri buoi);
	Task UpdateAsync(BuoiDieuTri buoi);
}