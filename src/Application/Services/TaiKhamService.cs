using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TaiKhamService
{
    private readonly ITaiKhamRepository _taiKhamRepo;

    public TaiKhamService(ITaiKhamRepository taiKhamRepo)
    {
        _taiKhamRepo = taiKhamRepo;
    }

    public async Task TaoTaiKhamAsync(TaoTaiKhamDTO dto)
    {
        var tk = new TaiKham(
            dto.PhienKhamID,
            dto.BenhNhanID,
            dto.NgayDuKien,
            dto.LyDo
        );

        await _taiKhamRepo.AddAsync(tk);
    }
    public async Task<bool> CapNhatAsync(int taiKhamID, DateTime ngayDuKien, string? lyDo, string? trangThai, int? caKhamID)
    {
        var caKham = await _taiKhamRepo.GetByIdAsync(taiKhamID);
        if (caKham == null) return false;

        caKham.CapNhat(ngayDuKien, lyDo, trangThai, caKhamID);
        await _taiKhamRepo.UpdateAsync(caKham);
        return true;
    }

    public async Task<List<TaiKham>> GetByBenhNhanAsync(int benhNhanID)
    {
        return await _taiKhamRepo.GetByBenhNhanAsync(benhNhanID);
    }
    public async Task<List<TaiKham>> LocAsync(DateTime ngayDuKien, string trangThai)
    {
        return await _taiKhamRepo.LocAsync(ngayDuKien, trangThai);
    }
    public async Task<List<TaiKham>> GetAllAsync()
    {
        return await _taiKhamRepo.GetAllAsync();
    }
    public async Task<TaiKham?> GetByIdAsync(int taiKhamID)
    {
        return await _taiKhamRepo.GetByIdAsync(taiKhamID);
    }
}
