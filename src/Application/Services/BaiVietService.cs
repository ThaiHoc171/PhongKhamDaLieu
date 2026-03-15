using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
namespace Application.Services;
public class BaiVietService
{
    private readonly IBaiVietRepository _repo;
    public BaiVietService(IBaiVietRepository repo)
    {
        _repo = repo;
    }
    public async Task<int> ThemBaiVietAsync(ThemBaiVietDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TieuDe))
            throw new Exception("Tiêu đề bài viết không được để trống");
        if (string.IsNullOrWhiteSpace(dto.NoiDung))
            throw new Exception("Nội dung bài viết không được để trống");
        if (dto.TacGiaID <= 0)
            throw new Exception("Tác giả không hợp lệ");
        if (dto.LoaiBenhID <= 0)
            throw new Exception("Loại bệnh không hợp lệ");
        var bv = new BaiViet(
            dto.TieuDe.Trim(),
            dto.TomTat?.Trim() ?? "",
            dto.NoiDung.Trim(),
            dto.HinhAnh ?? "",
            dto.TacGiaID,
            dto.LoaiBenhID
        );
        return await _repo.AddAsync(bv);
    }
    public async Task<List<BaiVietResponseDTO>> DanhSachAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(MapToDto).ToList();
    }
    public async Task<BaiVietResponseDTO?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new Exception("ID bài viết không hợp lệ");
        var bv = await _repo.GetByIdAsync(id);
        if (bv == null) return null;
        return MapToDto(bv);
    }
    public async Task<List<BaiVietResponseDTO>> GetByLuotXemAsync()
    {
        var list = await _repo.GetByLuotXemAsync();
        return list.Select(MapToDto).ToList();
    }
    public async Task<List<BaiVietResponseDTO>> GetByLoaiBenhAsync(int loaiBenhID)
    {
        if (loaiBenhID <= 0)
            throw new Exception("Loại bệnh không hợp lệ");
        var list = await _repo.GetByLoaiBenhAsync(loaiBenhID);
        return list.Select(MapToDto).ToList();
    }
    public async Task<bool> CapNhatBaiVietAsync(int id, CapNhatBaiVietDTO dto)
    {
        if (id <= 0)
            throw new Exception("ID bài viết không hợp lệ");
        if (string.IsNullOrWhiteSpace(dto.TieuDe))
            throw new Exception("Tiêu đề không được để trống");
        if (string.IsNullOrWhiteSpace(dto.NoiDung))
            throw new Exception("Nội dung không được để trống");
        if (dto.LoaiBenhID <= 0)
            throw new Exception("Loại bệnh không hợp lệ");
        var bv = await _repo.GetByIdAsync(id);
        if (bv == null) return false;
        bv.CapNhat(
            dto.TieuDe.Trim(),
            dto.TomTat?.Trim() ?? "",
            dto.NoiDung.Trim(),
            dto.HinhAnh ?? "",
            dto.LoaiBenhID
        );
        await _repo.UpdateAsync(bv);
        return true;
    }
    public async Task<bool> TangLuotXemAsync(int id)
    {
        if (id <= 0)
            throw new Exception("ID bài viết không hợp lệ");
        var bv = await _repo.GetByIdAsync(id);
        if (bv == null) return false;
        bv.TangLuotXem();
        await _repo.UpdateAsync(bv);
        return true;
    }
    private static BaiVietResponseDTO MapToDto(BaiViet bv)
    {
        return new BaiVietResponseDTO
        {
            BaiVietID = bv.BaiVietID,
            TieuDe = bv.TieuDe,
            TomTat = bv.TomTat,
            NoiDung = bv.NoiDung,
            HinhAnh = bv.HinhAnh,
            TacGiaID = bv.TacGiaID,
            LoaiBenhID = bv.LoaiBenhID,
            LuotXem = bv.LuotXem,
            NgayDang = bv.NgayDang,
            NgayCapNhat = bv.NgayCapNhat
        };
    }
}
