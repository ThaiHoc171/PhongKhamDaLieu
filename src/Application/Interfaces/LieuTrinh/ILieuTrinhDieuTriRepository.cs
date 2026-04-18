using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ILieuTrinhDieuTriRepository
{
	Task<LieuTrinhDieuTri?> GetByIdAsync(int id);
	Task<int> ExistByPhienKham(int phienKhamID);
	Task<LieuTrinhDieuTri?> GetByBenhNhanIdAsync(int benhNhanID);
	Task<int?> GetIdByBenhNhanIdAsync(int benhNhanID);
	Task<LieuTrinhDieuTriReadModel?> GetDetailAsync(int id);
	Task<(List<LieuTrinhDieuTriListReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai);
	Task<(List<LieuTrinhDieuTriListReadModel>, int)> SearchAsync(string? keyword, int page, int size);
	Task<(List<LieuTrinhDieuTriListReadModel>, int)> GetBenhNhanPagedAsync(int benhNhanID, int page, int size);
	Task<int> AddAsync(LieuTrinhDieuTri entity);
	Task UpdateAsync(LieuTrinhDieuTri entity);
	Task UpdateTrangThaiAsync(LieuTrinhDieuTri entity);
}