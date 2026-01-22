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
        if (dto.NgayKham.Date < DateTime.Today)
            throw new Exception("Ngày khám không hợp lệ");

        var danhSachLich = await _lichLamViecRepo.GetByNgayAsync(dto.NgayKham);

        if (!danhSachLich.Any())
            throw new Exception("Không có lịch làm việc trong ngày này");

        const int MaxCa = 5;
        int tongCaDaTao = 0;

        foreach (var lich in danhSachLich)
        {
            var chucVuId = await _lichLamViecRepo
                .GetChucVuIdByLichLamViecIdAsync(lich.LichLamViecID);

            if (chucVuId != 1) continue; 

            int KhungGioBatDat;
            int KhungGioKetThuc;

            if (lich.CaLamViec == 1)
            {
                KhungGioBatDat = 1;
                KhungGioKetThuc = 6;
            }
            else if (lich.CaLamViec == 2)
            {
                KhungGioBatDat = 7;
                KhungGioKetThuc = 13;
            }
            else
            {
                continue; 
            }

            for (int khungGioId = KhungGioBatDat; khungGioId <= KhungGioKetThuc; khungGioId++)
            {
                if (await _caKhamRepo.ExistsAsync(dto.NgayKham, khungGioId, lich.LichLamViecID))
                    continue;

                var soCaHienTai = await _caKhamRepo
                    .CountByNgayAndKhungGioAsync(dto.NgayKham, khungGioId);

                if (soCaHienTai >= MaxCa) continue;

                int soCanTao = MaxCa - soCaHienTai;

                for (int i = 0; i < soCanTao; i++)
                {
                    var ca = new CaKham(
                        lichLamViecID: lich.LichLamViecID,
                        phongChucNangID: dto.PhongChucNangID,
                        ngayKham: dto.NgayKham,
                        khungGioID: khungGioId,
                        trangThai: "Trống"
                    );

                    await _caKhamRepo.AddAsync(ca);
                    tongCaDaTao++;
                }
            }
        }

        return tongCaDaTao;
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
    public async Task<List<CaKham>> DanhSachCaKhamTheoNgayAsync(DateTime ngay, string trangThai)
    {
        return await _caKhamRepo.LocAsync(ngay, trangThai);
    }
    public async Task<List<CaKham>> GetByBenhNhanAsync(int benhNhanID)
    {
        return await _caKhamRepo.GetByBenhNhanAsync(benhNhanID);
    }
    public async Task<List<CaKham>> GetAllAsync()
    {
        return await _caKhamRepo.GetAllAsync();
    }

}
