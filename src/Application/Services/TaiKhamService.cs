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
        
        if (id == null)
            throw new Exception("Phiên khám không tồn tại, không thể tạo");
        var tt = await _taiKhamRepo.GetByBenhNhanIdAsync(id.Value);
        if (tt != null && tt.TrangThai == "Chờ xử lý")
            throw new Exception("Bệnh nhân còn lịch tái khám chưa xử lý xong, không thể tạo thêm.");
        var daCoTaiKham = await _taiKhamRepo
            .ExistsByPhienKhamAsync(dto.PhienKhamID);
        if (daCoTaiKham)
            throw new Exception("Phiên khám này đã được tạo tái khám");
        if (dto.NgayDuKien.Date < DateTime.Today)
            throw new Exception("Ngày tái khám không hợp lệ, không thể tạo");
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
        if (taiKham.TrangThai == "Hoàn thành")
            throw new Exception("Tái khám đã hoàn thành, không thể chỉnh sửa.");
        if (taiKham.CaKhamID != null && caKhamID != taiKham.CaKhamID && taiKham.TrangThai == "Chờ xử lý")
            throw new Exception("Bệnh nhân đang có lịch tái khám, không thể chỉnh sửa.");
        if (taiKham.CaKhamID != null && caKhamID == null)
            throw new Exception("Không thể hủy gán ca khám");
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
