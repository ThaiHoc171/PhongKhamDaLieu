using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TaiKhamService
{
    private readonly ITaiKhamRepository _repo;

    public TaiKhamService(ITaiKhamRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TaiKhamResponseDTO>> LayDanhSachAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(MapToResponse).ToList();
    }
    public async Task<int> TaoTaiKhamAsync(ThemTaiKhamDTO dto)
    {
        var taiKham = new TaiKham(
            dto.PhienKhamId,
            dto.BenhNhanId,
            dto.NgayDuKien,
            dto.LyDo,
            dto.CaKhamId
        );

        return await _repo.AddAsync(taiKham);
    }

    public async Task<bool> CapNhatAsync(int id, CapNhatTaiKhamDTO dto)
    {
        var taiKham = await _repo.GetByIdAsync(id);
        if (taiKham == null) return false;

        taiKham.CapNhatNgayTaiKham(dto.NgayDuKien, dto.CaKhamId);
        taiKham.CapNhatLyDo(dto.LyDo);

        await _repo.UpdateAsync(taiKham);
        return true;
    }

    public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThai)
    {
        var taiKham = await _repo.GetByIdAsync(id);
        if (taiKham == null) return false;

        taiKham.CapNhatTrangThai(trangThai);
        await _repo.UpdateAsync(taiKham);
        return true;
    }

    public async Task<List<TaiKhamResponseDTO>> LayTheoBenhNhanAsync(int benhNhanId)
    {
        var list = await _repo.GetByBenhNhanAsync(benhNhanId);

        return list.Select(t => new TaiKhamResponseDTO
        {
            TaiKhamId = t.TaiKhamId,
            PhienKhamId = t.PhienKhamId,
            BenhNhanId = t.BenhNhanId,
            NgayDuKien = t.NgayDuKien,
            LyDo = t.LyDo,
            TrangThai = t.TrangThai,
            CaKhamId = t.CaKhamId,
            NgayTao = t.NgayTao
        }).ToList();
    }

    private static TaiKhamResponseDTO MapToResponse(TaiKham t)
        => new()
        {
            TaiKhamId = t.TaiKhamId,
            PhienKhamId = t.PhienKhamId,
            BenhNhanId = t.BenhNhanId,
            NgayDuKien = t.NgayDuKien,
            LyDo = t.LyDo,
            TrangThai = t.TrangThai,
            CaKhamId = t.CaKhamId,
            NgayTao = t.NgayTao
        };
}
