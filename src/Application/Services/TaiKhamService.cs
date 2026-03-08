using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TaiKhamService
{
    private readonly ITaiKhamRepository _taiKhamRepo;
    private readonly IPhienKhamRepository _phienKhamRepo;
    private readonly ICaKhamRepository _caKhamRepo;

    public TaiKhamService(ITaiKhamRepository taiKhamRepo, IPhienKhamRepository phienKhamRepo, ICaKhamRepository caKhamRepo)
    {
        _taiKhamRepo = taiKhamRepo;
        _phienKhamRepo = phienKhamRepo;
        _caKhamRepo = caKhamRepo;
    }

    public async Task TaoTaiKhamAsync(TaoTaiKhamDTO dto)
    {
        var pk = await _phienKhamRepo.GetBenhNhanIdByPhienKhamIdAsync(dto.PhienKhamID);
        
        if (pk == null)
            throw new Exception("Phiên khám không tồn tại, không thể tạo");

        var tt = await _taiKhamRepo.GetByBenhNhanIdAsync(dto.PhienKhamID);
        if (tt != null && tt.TrangThai == "Chờ xử lý")
            throw new Exception("Bệnh nhân còn lịch tái khám chưa xử lý xong, không thể tạo thêm.");
        var daCoTaiKham = await _taiKhamRepo
            .ExistsByPhienKhamAsync(dto.PhienKhamID);
        if (daCoTaiKham)
            throw new Exception("Phiên khám này đã được tạo tái khám");
        if (dto.NgayDuKien.Date < DateTime.Today)
            throw new Exception("Ngày tái khám không hợp lệ, không thể tạo");
        int BenhNhanID = pk.Value;  
        var tk = new TaiKham(
            dto.PhienKhamID,
            BenhNhanID,
            dto.NgayDuKien,
            dto.LyDo
        );

        await _taiKhamRepo.AddAsync(tk);
    }
    public async Task<bool> CapNhatAsync(int taiKhamID, CapNhatTaiKhamDTO dto)
    {
        var taiKham = await _taiKhamRepo.GetByIdAsync(taiKhamID);
        if (taiKham == null) return false;
        if (taiKham.TrangThai == "Hoàn thành")
            throw new Exception("Tái khám đã hoàn thành, không thể chỉnh sửa.");
        if (taiKham.CaKhamID != null && dto.CaKhamID != taiKham.CaKhamID && taiKham.TrangThai == "Chờ xử lý")
            throw new Exception("Bệnh nhân đang có lịch tái khám, không thể chỉnh sửa.");
        if (taiKham.CaKhamID != null && dto.CaKhamID == null)
            throw new Exception("Không thể hủy gán ca khám");
        taiKham.CapNhat(dto.TrangThai, dto.CaKhamID);
        await _taiKhamRepo.UpdateAsync(taiKham);
        return true;
    }

    public async Task<List<TaiKhamResponeDTO>> GetListByBenhNhanAsync(int benhNhanID)
    {
        var list = await _taiKhamRepo.GetListByBenhNhanAsync(benhNhanID);
        return list.Select(MapToDto).ToList();
    }
    public async Task<List<TaiKhamResponeDTO>> LocAsync(DateTime ngayDuKien, string trangThai)
    {
        var list = await _taiKhamRepo.LocAsync(ngayDuKien, trangThai);
        return list.Select(MapToDto).ToList();
    }
    public async Task<List<TaiKhamResponeDTO>> GetAllAsync()
    {
        var list = await _taiKhamRepo.GetAllAsync();
        return list.Select(MapToDto).ToList();
    }
    public async Task<TaiKhamResponeDTO?> GetByIdAsync(int taiKhamID)
    {
        var tk = await _taiKhamRepo.GetByIdAsync(taiKhamID);
        if(tk == null) return null;

        return MapToDto(tk);
    }
    public async Task<TaiKhamResponeDTO?> GetByBenhNhanID(int benhNhanID)
    {
        var tk = await _taiKhamRepo.GetByBenhNhanIdAsync(benhNhanID);
        if (tk == null) return null;

        return MapToDto(tk);
    }
    public async Task<TaiKhamResponeDTO?> GetIdByBenhNhanIdAsync(int benhNhanID)
    {
        var tk = await _taiKhamRepo
        .GetTaiKhamChoXuLyAsync(benhNhanID);
        if (tk == null) return null;

        return MapToDto(tk);
    }

    private static TaiKhamResponeDTO MapToDto(TaiKham tk)
    {
        return new TaiKhamResponeDTO
        {
            TaiKhamID = tk.TaiKhamID,
            PhienKhamID = tk.PhienKhamID,
            BenhNhanID = tk.BenhNhanID,
            NgayDuKien = tk.NgayDuKien,
            LyDo = tk.LyDo,
            TrangThai = tk.TrangThai,
            CaKhamID = tk.CaKhamID,
            NgayTao = tk.NgayTao
        };
    }

}
