using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using Application.ReadModels;
using System.Collections.Generic;

namespace Application.Services;
public class LieuTrinhDieuTriService
{
    private readonly ILieuTrinhDieuTriRepository _lieuTrinhRepo;
    private readonly IPhienKhamRepository _phienKhamRepo;
    private readonly ILieuTrinh_BuoiDieuTriRepository _lieuTrinh_BuoiDieuTriRepo;
    private readonly ITaiKhamRepository _taiKhamRepo;


    public LieuTrinhDieuTriService(ILieuTrinhDieuTriRepository lieuTrinhRepo, IPhienKhamRepository phienKhamRepo, ILieuTrinh_BuoiDieuTriRepository lieuTrinh_BuoiDieuTriRepo, ITaiKhamRepository taiKhamRepo)
    {
        _lieuTrinhRepo = lieuTrinhRepo;
        _phienKhamRepo = phienKhamRepo;
        _lieuTrinh_BuoiDieuTriRepo = lieuTrinh_BuoiDieuTriRepo;
        _taiKhamRepo = taiKhamRepo;
    }

    public async Task TaoLieuTrinhAsync(TaoLieuTrinhDieuTriDTO dto)
    {
        var benhNhanId = await _phienKhamRepo.GetBenhNhanIdByPhienKhamIdAsync(dto.PhienKhamID);
        if (!benhNhanId.HasValue)
            throw new Exception("Phiên khám không tồn tại hoặc không hợp lệ");
        int benhNhanID = benhNhanId.Value;

        var tontai = await _taiKhamRepo.GetByBenhNhanIdAsync(benhNhanID);
        if (tontai != null && tontai.TrangThai == "Chờ xử lý")
            throw new Exception("Bệnh nhân đang có lịch tái khám, không thể tạo liệu trình.");

        var dangDieuTri = await _lieuTrinhRepo.GetByBenhNhanIdAsync(benhNhanID);
        if (dangDieuTri != null && dangDieuTri.TrangThai == "Đang điều trị")
            throw new Exception("Bệnh nhân đang có liệu trình điều trị, không thể tạo mới.");

        if (dto.TongSoBuoi <= 0)
            throw new Exception("Tổng số buổi phải lớn hơn 0");
        int tongSoBuoi = dto.TongSoBuoi;

        if (dto.NgayBatDau.Date < DateTime.Today)
            throw new Exception("Ngày bắt đầu không được nhỏ hơn ngày hiện tại");
        DateTime ngayKetThuc = dto.NgayBatDau.AddDays((tongSoBuoi - 1) * 7);
        var lt = new LieuTrinhDieuTri(
            benhNhanID,
            dto.PhienKhamID,
            dto.TenLieuTrinh,
            dto.TongSoBuoi,
            dto.GhiChu,
            dto.NgayBatDau,
            ngayKetThuc
        );

        await _lieuTrinhRepo.AddAsync(lt);
    }

    public async Task<bool> CapNhatAsync(int lieuTrinhID, CapNhatLieuTrinhDieuTriDTO dto)
    {
        var lieuTrinh = await _lieuTrinhRepo.GetByIdAsync(lieuTrinhID);

        if (lieuTrinh == null) return false;

        if (lieuTrinh.TrangThai == "Hoàn thành")
            throw new Exception("Không thể cập nhật liệu trình đã hoàn thành");

        lieuTrinh.CapNhat(dto.TenLieuTrinh, dto.TongSoBuoi, dto.NgayKetThuc);
        await _lieuTrinhRepo.UpdateAsync(lieuTrinh);
        return true;
    }

    public async Task<bool> CapNhatTrangThaiAsync(int lieuTrinhID, CapNhatTrangThaiLieuTrinhDieuTriDTO dto)
    {
        var lieuTrinh = await _lieuTrinhRepo.GetByIdAsync(lieuTrinhID);
        if (lieuTrinh == null) return false;

        if (lieuTrinh.TrangThai == "Hoàn thành")
            throw new Exception("Không thể cập nhật liệu trình đã hoàn thành");

        var trangThaiHopLe = new[] { "Đang điều trị", "Đã hủy", "Hoàn thành" };
        if (!trangThaiHopLe.Contains(dto.TrangThai))
            throw new Exception("Trạng thái không hợp lệ");

        if (dto.TrangThai == "Hoàn thành")
        {
            var soBuoiDaDieuTri =
                await _lieuTrinh_BuoiDieuTriRepo.CountBySoBuoiAsync(lieuTrinhID);

            if (soBuoiDaDieuTri < lieuTrinh.TongSoBuoi)
                throw new Exception("Chưa đủ số buổi để hoàn thành liệu trình");
        }

        lieuTrinh.CapNhatTrangThai(dto.TrangThai, dto.GhiChu);
        await _lieuTrinhRepo.UpdateTrangThaiAsync(lieuTrinh);

        return true;
    }

    public async Task<LieuTrinhDieuTriResponeDTO?> LayTheoIdAsync(int lieuTrinhID)
    {
        var lt = await _lieuTrinhRepo.GetByIdAsync(lieuTrinhID);
        if (lt == null) return null;

        return MapToDto(lt);
    }

    public async Task<LieuTrinhDieuTriResponeDTO?> LayTheoBenhNhanAsync(int benhNhanID)
    {
        var lt = await _lieuTrinhRepo.GetByBenhNhanIdAsync(benhNhanID);
        if (lt == null) return null;

        return MapToDto(lt);
    }

    public async Task<int?> LayIdTheoBenhNhanAsync(int benhNhanID)
    {
        return await _lieuTrinhRepo.GetIdByBenhNhanIdAsync(benhNhanID);
    }

    public async Task<List<LieuTrinhDieuTriResponeDTO>> DanhSachAsync()
    {
        var list = await _lieuTrinhRepo.GetAllAsync();

        return list.Select(MapToDto).ToList();
    }

    public async Task<List<LieuTrinhDieuTriResponeDTO>> LocBatDauAsync(DateTime ngay, string trangThai)
    {
        var list = await _lieuTrinhRepo.LocBatDauAsync(ngay, trangThai);

        return list.Select(MapToDto).ToList();
    }
    public async Task<List<LieuTrinhDieuTriResponeDTO>> LocKetThucAsync(DateTime ngay, string trangThai)
    {
        var list = await _lieuTrinhRepo.LocKetThucAsync(ngay, trangThai);
        return list.Select(MapToDto).ToList();
    }
    public async Task<List<LieuTrinhDieuTriResponeDTO>> DanhSachTheoBenhNhanAsync(int benhNhanID)
    {
        var list = await _lieuTrinhRepo.GetListByBenhNhanAsync(benhNhanID);
        return list.Select(MapToDto).ToList();
    }

    private static LieuTrinhDieuTriResponeDTO MapToDto(LieuTrinhDieuTri lt)
    {
        return new LieuTrinhDieuTriResponeDTO
        {
            LieuTrinhID = lt.LieuTrinhID,
            BenhNhanID = lt.BenhNhanID,
            PhienKhamID = lt.PhienKhamID,
            TenLieuTrinh = lt.TenLieuTrinh,
            TongSoBuoi = lt.TongSoBuoi,
            TrangThai = lt.TrangThai,
            GhiChu = lt.GhiChu,
            NgayBatDau = lt.NgayBatDau,
            NgayKetThuc = lt.NgayKetThuc
        };
    }
}

