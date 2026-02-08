using Domain.Entities;

public interface ILieuTrinh_BuoiDieuTriRepository
{
    Task<LieuTrinh_BuoiDieuTri?> GetByIdAsync(int buoiDieuTriID);
    Task<List<LieuTrinh_BuoiDieuTri>> GetAllAsync();
    Task<List<LieuTrinh_BuoiDieuTri>> GetByLieuTrinhAsync(int lieuTrinhID);
    Task<List<LieuTrinh_BuoiDieuTri>> LocDuKienAsync(DateTime ngay, string trangThai);
    Task<List<LieuTrinh_BuoiDieuTri>> LocBatDauAsync(DateTime ngay, string trangThai);
    Task<bool> ExistsByCaKhamAsync(int caKhamID);
    Task<int> CountBySoBuoiAsync(int lieuTrinhID);
    Task<int> AddAsync(LieuTrinh_BuoiDieuTri buoiDieuTri);
    Task UpdateTrangThaiAsync(LieuTrinh_BuoiDieuTri buoiDieuTri);
}
