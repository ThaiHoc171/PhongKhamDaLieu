using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services;
public class HoSoBenhAnService
{
    private readonly IHoSoBenhAnRepository _hoSoRepo;

    public HoSoBenhAnService(IHoSoBenhAnRepository hoSoRepo)
    {
        _hoSoRepo = hoSoRepo;
    }

    public async Task<int> TaoHoSoBenhAn(TaoHoSoBenhAnDTO dto)
    {
        if(dto.BenhNhanID == 0)
            throw new Exception("BenhNhanID không hợp lệ");

        var hs = new HoSoBenhAn(
            dto.BenhNhanID,
            dto.BenhNen,
            dto.DiUng,
            dto.TienSuBenh,
            dto.TienSuGiaDinh,
            dto.ThoiQuenSong,
            dto.ThongTinKhac,
            dto.NgayTao,
            dto.NgayCapNhat);

        var HoSo = await _hoSoRepo.AddAsync(hs);
        return HoSo;
    }
    public async Task<bool> CapNhatThongTinAsync(int hoSoBenhAnID, string? benhNen, string? diUng, string? tienSuBenh, string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac, DateTime ngayCapNhat)
    {
        var hoSo = await _hoSoRepo.GetByIdAsync(hoSoBenhAnID);
        if (hoSo == null) return false;

        hoSo.CapNhatThongTin(benhNen, diUng, tienSuBenh, tienSuGiaDinh, thoiQuenSong, thongTinKhac, ngayCapNhat);
        await _hoSoRepo.UpdateAsync(hoSo);
        return true;
    }

    public async Task<HoSoBenhAn?> GetByIdAsync(int hoSoBenhAnID)
    {
        return await _hoSoRepo.GetByIdAsync(hoSoBenhAnID);
    }
    public async Task<HoSoBenhAn?> GetByBenhNhanIdAsync(int benhNhanID)
    {
        return await _hoSoRepo.GetByBenhNhanIdAsync(benhNhanID);
    }
    public async Task<List<HoSoBenhAn>> GetAllAsync()
    {
        return await _hoSoRepo.GetAllAsync();
    }
}

