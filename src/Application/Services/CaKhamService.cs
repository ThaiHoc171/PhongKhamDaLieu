using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CaKhamService
{
    private readonly ICaKhamRepository _caKhamRepo;
    private readonly ILichLamViecRepository _lichLamViecRepo;

    public CaKhamService(
        ICaKhamRepository caKhamRepo,
        ILichLamViecRepository lichLamViecRepo)
    {
        _caKhamRepo = caKhamRepo;
        _lichLamViecRepo = lichLamViecRepo;
    }
    public async Task<int> TaoCaKhamAsync(TaoCaKhamDTO dto)
    {
        var lichLamViec = await _lichLamViecRepo.GetByIdAsync(dto.LichLamViecID);
        if (lichLamViec == null)
            throw new Exception("Lịch làm việc không tồn tại, không thể tạo ca khám");

        if (dto.NgayKham.Date < DateTime.Today)
            throw new Exception("Ngày khám không hợp lệ");

        var caKham = new CaKham(
            lichLamViecID: dto.LichLamViecID,
            phongChucNangID: dto.PhongChucNangID,
            ngayKham: dto.NgayKham,
            khungGioID: dto.KhungGioID,
            trangThai: "Trống"
        );
        return await _caKhamRepo.AddAsync(caKham);
    }
    public async Task<bool> DangKyKhamAsync(int caKhamID, int benhNhanID, string lyDoKham, DateTime ngayDat, string ghiChu)
    {
        var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
        if (caKham == null) return false;

        caKham.DangKyKham(benhNhanID, lyDoKham, ngayDat, ghiChu);
        await _caKhamRepo.UpdateAsync(caKham);
        return true;
    }
    public async Task<bool> UpdateTrangThaiAsync(int caKhamID, string trangThai)
    {
        var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
        if (caKham == null) return false;

        caKham.CapNhatTrangThai(trangThai);
        await _caKhamRepo.UpdateAsync(caKham);
        return true;
    }
    public async Task<CaKham?> LayCaKhamTheoIdAsync(int caKhamId)
    {
        return await _caKhamRepo.GetByIdAsync(caKhamId);
    }
    public async Task<List<CaKham>> DanhSachCaKhamTheoNgayAsync(DateTime ngay)
    {
        return await _caKhamRepo.GetByNgayAsync(ngay);
    }
    public async Task<List<CaKham>> GetByBenhNhanAsync(int benhNhanID)
    {
        return await _caKhamRepo.GetByBenhNhanAsync(benhNhanID);
    }
    public async Task<List<CaKham>> GetAllAsync()
    {
        return await _caKhamRepo.GetAllAsync();
    }
    public async Task<List<CaKham>> GetCaKhamConTrongAsync()
    {
        return await _caKhamRepo.GetCaKhamConTrongAsync();
    }

}
