using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class BaiVietService
{
    private readonly IBaiVietRepository _repository;

    public BaiVietService(IBaiVietRepository repository)
    {
        _repository = repository;
    }
    public async Task<ApiResponse<int>> ThemBaiVietAsync(ThemBaiVietDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TieuDe))
            return ApiResponse<int>.Fail("Tiêu đề không hợp lệ");
        if (string.IsNullOrWhiteSpace(dto.NoiDung))
            return ApiResponse<int>.Fail("Nội dung không được để trống");
        var entity = new BaiViet
        {
            TieuDe = dto.TieuDe.Trim(),
            TomTat = dto.TomTat,
            NoiDung = dto.NoiDung,
            HinhAnh = dto.HinhAnh,
            TacGiaID = dto.TacGiaID,
            LoaiBenhID = dto.LoaiBenhID,
            LuotXem = 0,
            NgayDang = DateTime.Now,
            TrangThai = "Bản nháp"
        };
        var id = await _repository.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(id, "Tạo bài viết thành công");
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, CapNhatBaiVietDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Không tìm thấy bài viết");
        entity.CapNhat(
            dto.TieuDe,
            dto.TomTat,
            dto.NoiDung,
            dto.HinhAnh,
            dto.LoaiBenhID);
        await _repository.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true, "Cập nhật bài viết thành công");
    }
    public async Task<ApiResponse<bool>> XoaAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        await _repository.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(true, "Xóa bài viết thành công");
    }
    public async Task<ApiResponse<BaiVietReadModel>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<BaiVietReadModel>.Fail("ID không hợp lệ");
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<BaiVietReadModel>.Fail("Không tìm thấy bài viết");
        var result = new BaiVietReadModel
        {
            BaiVietID = entity.BaiVietID,
            TieuDe = entity.TieuDe,
            TomTat = entity.TomTat,
            NoiDung = entity.NoiDung,
            HinhAnh = entity.HinhAnh,
            TacGiaID = entity.TacGiaID,
            LoaiBenhID = entity.LoaiBenhID,
            LuotXem = entity.LuotXem,
            NgayDang = entity.NgayDang,
            NgayCapNhat = entity.NgayCapNhat,
            TrangThai = entity.TrangThai
        };

        return ApiResponse<BaiVietReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<BaiVietListReadModel>>> GetPagedAsync(int page, int size)
    {
        if (page < 1) page = 1;
        if (size <= 0) size = 10;
        var (items, total) = await _repository.GetPagedAsync(page, size);
        var result = new PagedResult<BaiVietListReadModel>
        {
            Items = items,
            TotalCount = total,
            PageNumber = page,
            PageSize = size
        };
        return ApiResponse<PagedResult<BaiVietListReadModel>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<List<BaiVietListReadModel>>> GetByLoaiBenhAsync(int loaiBenhId)
    {
        if (loaiBenhId <= 0)
            return ApiResponse<List<BaiVietListReadModel>>.Fail("LoaiBenhID không hợp lệ");

        var data = await _repository.GetByLoaiBenhAsync(loaiBenhId);

        var result = data.Select(x => new BaiVietListReadModel
        {
            BaiVietID = x.BaiVietID,
            TieuDe = x.TieuDe,
            TomTat = x.TomTat,
            HinhAnh = x.HinhAnh,
            LuotXem = x.LuotXem,
            NgayDang = x.NgayDang
        }).ToList();

        return ApiResponse<List<BaiVietListReadModel>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<List<BaiVietListReadModel>>> GetTopLuotXemAsync(int top)
    {
        if (top <= 0) top = 5;

        var data = await _repository.GetTopLuotXemAsync(top);

        var result = data.Select(x => new BaiVietListReadModel
        {
            BaiVietID = x.BaiVietID,
            TieuDe = x.TieuDe,
            TomTat = x.TomTat,
            HinhAnh = x.HinhAnh,
            LuotXem = x.LuotXem,
            NgayDang = x.NgayDang
        }).ToList();

        return ApiResponse<List<BaiVietListReadModel>>.SuccessResponse(result);
    }
}