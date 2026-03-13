using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class LieuTrinh_BuoiDieuTriService
{
    private readonly ILieuTrinh_BuoiDieuTriRepository _repo;
    private readonly ICaKhamRepository _caKhamRepo;
    private readonly ILieuTrinhDieuTriRepository _lieuTrinhRepo;
    private readonly ILichLamViecRepository _lichLamViecRepo;

    public LieuTrinh_BuoiDieuTriService(ILieuTrinh_BuoiDieuTriRepository repo, ICaKhamRepository caKhamRepo, ILieuTrinhDieuTriRepository lieuTrinhRepo, ILichLamViecRepository lichLamViecRepo)
    {
        _repo = repo;
        _caKhamRepo = caKhamRepo;
        _lieuTrinhRepo = lieuTrinhRepo;
        _lichLamViecRepo = lichLamViecRepo;
    }

    public async Task TaoBuoiDieuTriAsync(TaoBuoiDieuTriDTO dto)
    {
        var caKham = await _caKhamRepo.GetByIdAsync(dto.CaKhamID);
        if (caKham == null || caKham.LichLamViecID == null)
            throw new Exception("Ca khám không hợp lệ");

        var lich = await _lichLamViecRepo.GetByIdAsync(caKham.LichLamViecID.Value);
        if (lich == null)
            throw new Exception("Lịch làm việc không tồn tại");

        var lieuTrinh = await _lieuTrinhRepo.GetByIdAsync(dto.LieuTrinhID);
        if (lieuTrinh == null)
            throw new Exception("Không tìm thấy liệu trình");

        if (lieuTrinh.TrangThai != "Đang điều trị")
            throw new Exception("Liệu trình không ở trạng thái đang điều trị");

        int maxSoBuoi = await _repo.GetMaxSoBuoiAsync(lieuTrinh.LieuTrinhID);
        int soBuoi = maxSoBuoi + 1;

        if (soBuoi > lieuTrinh.TongSoBuoi)
            throw new Exception("Liệu trình đã đủ số buổi");

        DateTime ngayDuKien =
            lieuTrinh.NgayBatDau.AddDays((soBuoi - 1) * 7);

        var buoi = new LieuTrinh_BuoiDieuTri(
            lieuTrinh.LieuTrinhID,
            dto.CaKhamID,
            soBuoi,
            ngayDuKien,
            caKham.NgayKham,
            lich.NhanVienID
        );

        await _repo.AddAsync(buoi);
    }

    public async Task<bool> CapNhatTrangThaiAsync(
    int buoiDieuTriID,
    CapNhatTrangThaiBuoiDieuTriDTO dto)
    {
        var buoi = await _repo.GetByIdAsync(buoiDieuTriID);
        if (buoi == null) return false;

        if (buoi.TrangThai == "Hoàn thành" || buoi.TrangThai == "Đã hủy")
            throw new Exception("Buổi điều trị đã kết thúc");

        var hopLe = new[] { "Chờ xử lý", "Đang xử lý", "Hoàn thành", "Đã hủy" };
        if (!hopLe.Contains(dto.TrangThai))
            throw new Exception("Trạng thái không hợp lệ");

        buoi.CapNhatTrangThai(
            dto.TrangThai,
            dto.NhanVienID,
            dto.NgayThucHien,
            dto.GhiChu
        );

        await _repo.UpdateTrangThaiAsync(buoi);

        if (dto.TrangThai == "Hoàn thành")
        {
            var lieuTrinh = await _lieuTrinhRepo.GetByIdAsync(buoi.LieuTrinhID);
            if (lieuTrinh == null)
                throw new Exception("Liệu trình không tồn tại");

            int soBuoiHoanThanh =
                await _repo.CountBySoBuoiAsync(buoi.LieuTrinhID);

            if (soBuoiHoanThanh >= lieuTrinh.TongSoBuoi)
            {
                lieuTrinh.CapNhatTrangThai("Hoàn thành", "Đã hoàn tất đủ số buổi");
                await _lieuTrinhRepo.UpdateTrangThaiAsync(lieuTrinh);
            }
        }

        return true;
    }


    public async Task<List<BuoiDieuTriResponeDTO>> LayTheoLieuTrinhAsync(int lieuTrinhID)
    {
        var list = await _repo.GetByLieuTrinhAsync(lieuTrinhID);

        return list.Select(MapToDto).ToList();
    }
    public async Task<List<BuoiDieuTriResponeDTO>> LocDuKienAsync(DateTime ngay, string trangThai)
    {
        var list = await _repo.LocDuKienAsync(ngay, trangThai);
        return list.Select(MapToDto).ToList();
    }
    public async Task<List<BuoiDieuTriResponeDTO>> LocBatDauAsync(DateTime ngay, string trangThai)
    {
        var list = await _repo.LocBatDauAsync(ngay, trangThai);
        return list.Select(MapToDto).ToList();
    }
    public async Task<List<BuoiDieuTriResponeDTO>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(MapToDto).ToList();
    }

    private static BuoiDieuTriResponeDTO MapToDto(LieuTrinh_BuoiDieuTri buoi)
    {
        return new BuoiDieuTriResponeDTO
        {
            BuoiDieuTriID = buoi.BuoiDieuTriID,
            LieuTrinhID = buoi.LieuTrinhID,
            CaKhamID = buoi.CaKhamID,
            SoBuoi = buoi.SoBuoi,
            NgayDuKien = buoi.NgayDuKien,
            NgayThucHien = buoi.NgayThucHien,
            NhanVienID = buoi.NhanVienID,
            TrangThai = buoi.TrangThai,
            GhiChu = buoi.GhiChu,
            HinhAnhJSON = buoi.HinhAnhJSON
        };
    }
}
