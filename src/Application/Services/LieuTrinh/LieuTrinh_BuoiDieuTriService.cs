using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class LieuTrinh_BuoiDieuTriService
{
    private readonly ILieuTrinh_BuoiDieuTriRepository _repo;
    private readonly ICaKhamRepository _caKhamRepo;
    private readonly ILieuTrinhDieuTriRepository _lieuTrinhRepo;

    public LieuTrinh_BuoiDieuTriService(ILieuTrinh_BuoiDieuTriRepository repo, ICaKhamRepository caKhamRepo, ILieuTrinhDieuTriRepository lieuTrinhRepo)
    {
        _repo = repo;
        _caKhamRepo = caKhamRepo;
        _lieuTrinhRepo = lieuTrinhRepo;
    }

    public async Task TaoBuoiDieuTriAsync(TaoBuoiDieuTriDTO dto)
    {
        // 1. Lấy ca khám
        var caKham = await _caKhamRepo.GetByIdAsync(dto.CaKhamID);
        if (caKham == null || caKham.BenhNhanID == null)
            throw new Exception("Ca khám không hợp lệ");

        int benhNhanID = caKham.BenhNhanID.Value;

        // 2. Lấy liệu trình đang điều trị của bệnh nhân
        var lieuTrinh = await _lieuTrinhRepo.GetByBenhNhanIdAsync(benhNhanID);

        if (lieuTrinh == null)
            throw new Exception("Bệnh nhân không có liệu trình điều trị");

        if (lieuTrinh.TrangThai != "Đang điều trị")
            throw new Exception("Liệu trình không ở trạng thái đang điều trị");

        // 3. Đếm số buổi đã điều trị
        int soBuoi = await _repo.CountBySoBuoiAsync(lieuTrinh.LieuTrinhID) + 1;
        if (soBuoi > lieuTrinh.TongSoBuoi)
            throw new Exception("Liệu trình điều trị đã đủ số buổi");

        // 4. Tính ngày dự kiến
        DateTime ngayDuKien =
            lieuTrinh.NgayBatDau.AddDays((soBuoi - 1) * 7);

        // 5. Tạo buổi điều trị
        var buoi = new LieuTrinh_BuoiDieuTri(
            lieuTrinh.LieuTrinhID,
            dto.CaKhamID,
            soBuoi,
            ngayDuKien,
            caKham.NgayKham
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


    public async Task<List<LieuTrinh_BuoiDieuTri>> LayTheoLieuTrinhAsync(int lieuTrinhID)
    {
        return await _repo.GetByLieuTrinhAsync(lieuTrinhID);
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> LocDuKienAsync(DateTime ngay, string trangThai)
    {
        return await _repo.LocDuKienAsync(ngay, trangThai);
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> LocBatDauAsync(DateTime ngay, string trangThai)
    {
        return await _repo.LocBatDauAsync(ngay, trangThai);
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }
}
