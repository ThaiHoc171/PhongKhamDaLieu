using System.Collections.Generic;
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
        var tonTai = await _hoSoRepo.GetByBenhNhanIdAsync(dto.BenhNhanID);
        if (tonTai != null)
            throw new Exception("Bệnh nhân đã có hồ sơ bệnh án");
        if (dto.BenhNen?.Length > 500)
            throw new Exception("Bệnh nền quá dài");
        if (dto.DiUng?.Length > 500)
            throw new Exception("Thông tin dị ứng quá dài");
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
    public async Task<bool> CapNhatThongTinAsync(int hoSoBenhAnID, HoSoBenhAnUpdateDTO dto)
    {
        if (dto.BenhNen == null && dto.DiUng == null && dto.TienSuBenh == null && dto.TienSuGiaDinh == null && dto.ThoiQuenSong == null && dto.ThongTinKhac == null)
        {
            throw new Exception("Không có thông tin nào để cập nhật");
        }
        var hoSo = await _hoSoRepo.GetByIdAsync(hoSoBenhAnID);
        if (hoSo == null) return false;
        if (dto.NgayCapNhat < hoSo.NgayTao)
            throw new Exception("Ngày cập nhật không hợp lệ");
        hoSo.CapNhatThongTin(dto.BenhNen,dto.DiUng, dto.TienSuBenh, dto.TienSuGiaDinh, dto.ThoiQuenSong, dto.ThongTinKhac, dto.NgayCapNhat);
        await _hoSoRepo.UpdateAsync(hoSo);
        return true;
    }
    public async Task<HoSoBenhAnResponeDTO?> GetByIdAsync(int hoSoBenhAnID)
    {
        var hs = await _hoSoRepo.GetByIdAsync(hoSoBenhAnID);
        if(hs == null) return null;
        return MapToDto(hs);
    }
    public async Task<HoSoBenhAnResponeDTO?> GetByBenhNhanIdAsync(int benhNhanID)
    {
        var hs = await _hoSoRepo.GetByBenhNhanIdAsync(benhNhanID);
        if (hs == null) return null;
        return MapToDto(hs);
    }
    public async Task<List<HoSoBenhAnResponeDTO>> GetAllAsync()
    {
        var list = await _hoSoRepo.GetAllAsync();
        return list.Select(MapToDto).ToList();
    }
    private static HoSoBenhAnResponeDTO MapToDto(HoSoBenhAn hs)
    {
        return new HoSoBenhAnResponeDTO
        {
            HoSoBenhAnID = hs.HoSoBenhAnID,
            BenhNhanID = hs.BenhNhanID,
            BenhNen = hs.BenhNen,
            DiUng = hs.DiUng,
            TienSuBenh = hs.TienSuBenh,
            TienSuGiaDinh = hs.TienSuGiaDinh,
            ThoiQuenSong = hs.ThoiQuenSong,
            ThongTinKhac = hs.ThongTinKhac,
            NgayTao = hs.NgayTao,
            NgayCapNhat = hs.NgayCapNhat
        };
    }
}
