using Domain.Entities;


namespace Application.Interfaces;

public interface ILieuTrinhDieuTriRepository
{
    Task<LieuTrinhDieuTri?> GetByIdAsync(int lieuTrinhID);
    Task<LieuTrinhDieuTri?> GetByBenhNhanIdAsync(int benhNhanID);
    Task<int?> GetIdByBenhNhanIdAsync(int benhNhanID);
    Task<List<LieuTrinhDieuTri>> GetAllAsync();
    Task<List<LieuTrinhDieuTri>> LocBatDauAsync(DateTime ngay, string trangThai);
    Task<List<LieuTrinhDieuTri>> LocKetThucAsync(DateTime ngay, string trangThai);
    Task<List<LieuTrinhDieuTri>> GetListByBenhNhanAsync(int benhNhanID);
    Task<int> AddAsync(LieuTrinhDieuTri lieuTrinhDieuTri);
    Task UpdateAsync(LieuTrinhDieuTri lieuTrinhDieuTri);
    Task UpdateTrangThaiAsync(LieuTrinhDieuTri lieuTrinh);
}
