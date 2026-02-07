using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TaiKhamService
{
    private readonly ITaiKhamRepository _taiKhamRepo;
    private readonly IPhienKhamRepository _phienKhamRepo;

    public TaiKhamService(ITaiKhamRepository taiKhamRepo, IPhienKhamRepository phienKhamRepo)
    {
        _taiKhamRepo = taiKhamRepo;
        _phienKhamRepo = phienKhamRepo;
    }

    public async Task TaoTaiKhamAsync(TaoTaiKhamDTO dto)
    {
        int? id = await _phienKhamRepo.GetBenhNhanIdByPhienKhamIdAsync(dto.PhienKhamID);
        int BenhNhanID = id.Value;
        var tk = new TaiKham(
            dto.PhienKhamID,
            BenhNhanID,
            dto.NgayDuKien,
            dto.LyDo
        );

        await _taiKhamRepo.AddAsync(tk);
    }
    public async Task<bool> CapNhatAsync(int taiKhamID, string? trangThai, int? caKhamID)
    {
        var taiKham = await _taiKhamRepo.GetByIdAsync(taiKhamID);
        if (taiKham == null) return false;

        taiKham.CapNhat(trangThai, caKhamID);
        await _taiKhamRepo.UpdateAsync(taiKham);
        return true;
    }

    public async Task<List<TaiKham>> GetListByBenhNhanAsync(int benhNhanID)
    {
        return await _taiKhamRepo.GetListByBenhNhanAsync(benhNhanID);
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
    public async Task<TaiKham?> GetByBenhNhanID(int benhNhanID)
    {
        return await _taiKhamRepo.GetByBenhNhanIdAsync(benhNhanID);
    }
    public async Task<int?> GetIdByBenhNhanIdAsync(int benhNhanID)
    {
        return await _taiKhamRepo.GetIdByBenhNhanIdAsync(benhNhanID);
    }
}
