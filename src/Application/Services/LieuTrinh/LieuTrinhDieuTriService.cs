using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using Application.ReadModels;

namespace Application.Services;
public class LieuTrinhDieuTriService
{
    private readonly ILieuTrinhDieuTriRepository _lieuTrinhRepo;
    private readonly IPhienKhamRepository _phienKhamRepo;
    private readonly ILieuTrinh_BuoiDieuTriRepository _repo;

    public LieuTrinhDieuTriService(ILieuTrinhDieuTriRepository lieuTrinhRepo, IPhienKhamRepository phienKhamRepo, ILieuTrinh_BuoiDieuTriRepository repo)
    {
        _lieuTrinhRepo = lieuTrinhRepo;
        _phienKhamRepo = phienKhamRepo;
        _repo = repo;
    }

    public async Task TaoLieuTrinhAsync(TaoLieuTrinhDieuTriDTO dto)
    {
        var benhNhanId = await _phienKhamRepo.GetBenhNhanIdByPhienKhamIdAsync(dto.PhienKhamID);
        if (!benhNhanId.HasValue)
            throw new Exception("Phiên khám không tồn tại hoặc không hợp lệ");
        int benhNhanID = benhNhanId.Value;

        var dangDieuTri = await _lieuTrinhRepo.GetByBenhNhanIdAsync(benhNhanID);
        if (dangDieuTri != null && dangDieuTri.TrangThai == "Đang điều trị")
            throw new Exception("Bệnh nhân đang có liệu trình điều trị, không thể tạo mới");

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

    public async Task<bool> CapNhatAsync(int lieuTrinhID, string tenLieuTrinh, int tongSoBuoi, DateTime ngayBatDau, DateTime ngayKetThuc)
    {
        var lieuTrinh = await _lieuTrinhRepo.GetByIdAsync(lieuTrinhID);

        if (lieuTrinh == null) return false;

        if (lieuTrinh.TrangThai == "Hoàn thành")
            throw new Exception("Không thể cập nhật liệu trình đã hoàn thành");

        lieuTrinh.CapNhat(tenLieuTrinh, tongSoBuoi, ngayBatDau, ngayKetThuc);
        await _lieuTrinhRepo.UpdateAsync(lieuTrinh);
        return true;
    }

    public async Task<bool> CapNhatTrangThaiAsync(
        int lieuTrinhID,
        string trangThai,
        string? ghiChu)
    {
        var lieuTrinh = await _lieuTrinhRepo.GetByIdAsync(lieuTrinhID);
        if (lieuTrinh == null) return false;

        if (lieuTrinh.TrangThai == "Hoàn thành")
            throw new Exception("Không thể cập nhật liệu trình đã hoàn thành");

        var trangThaiHopLe = new[] { "Đang điều trị", "Đã hủy", "Hoàn thành" };
        if (!trangThaiHopLe.Contains(trangThai))
            throw new Exception("Trạng thái không hợp lệ");

        if (trangThai == "Hoàn thành")
            throw new Exception("Không thể tự kết thúc liệu trình");

        lieuTrinh.CapNhatTrangThai(trangThai, ghiChu);
        await _lieuTrinhRepo.UpdateTrangThaiAsync(lieuTrinh);

        return true;
    }

    public async Task<LieuTrinhDieuTri?> LayTheoIdAsync(int lieuTrinhID)
    {
        return await _lieuTrinhRepo.GetByIdAsync(lieuTrinhID);
    }

    public async Task<LieuTrinhDieuTri?> LayTheoBenhNhanAsync(int benhNhanID)
    {
        return await _lieuTrinhRepo.GetByBenhNhanIdAsync(benhNhanID);
    }

    public async Task<int?> LayIdTheoBenhNhanAsync(int benhNhanID)
    {
        return await _lieuTrinhRepo.GetIdByBenhNhanIdAsync(benhNhanID);
    }

    public async Task<List<LieuTrinhDieuTri>> DanhSachAsync()
    {
        return await _lieuTrinhRepo.GetAllAsync();
    }

    public async Task<List<LieuTrinhDieuTri>> LocBatDauAsync(DateTime ngay, string trangThai)
    {
        return await _lieuTrinhRepo.LocBatDauAsync(ngay, trangThai);
    }
    public async Task<List<LieuTrinhDieuTri>> LocKetThucAsync(DateTime ngay, string trangThai)
    {
        return await _lieuTrinhRepo.LocKetThucAsync(ngay, trangThai);
    }
    public async Task<List<LieuTrinhDieuTri>> DanhSachTheoBenhNhanAsync(int benhNhanID)
    {
        return await _lieuTrinhRepo.GetListByBenhNhanAsync(benhNhanID);
    }
}

