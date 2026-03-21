using Application.Common;
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
    public async Task<ApiResponse<int>> ThemAsync(ThemBaiVietDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TieuDe))
            return ApiResponse<int>.Fail("Tiêu đề không hợp lệ");
        if (string.IsNullOrWhiteSpace(dto.NoiDung))
            return ApiResponse<int>.Fail("Nội dung không được để trống");
        var entity = new BaiViet(
            dto.TieuDe.Trim(),
            dto.TomTat,
            dto.NoiDung,
            dto.HinhAnh,
            dto.TacGiaID,
            dto.LoaiBenhID);
        await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(1);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, CapNhatBaiVietDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        entity.CapNhat(
            dto.TieuDe,
            dto.TomTat,
            dto.NoiDung,
            dto.HinhAnh,
            dto.LoaiBenhID);
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<bool>> XoaAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<BaiVietReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<BaiVietReadModel>.Fail("Bài viết không tồn tại");
        return ApiResponse<BaiVietReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<BaiVietListReadModel>>> GetPagedAsync(int page, int size)
    {
        var (items, total) = await _repo.GetPagedAsync(page, size);
        return ApiResponse<PagedResult<BaiVietListReadModel>>.SuccessResponse(
            new PagedResult<BaiVietListReadModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size
            });
    }
    public async Task<ApiResponse<List<BaiVietListReadModel>>> GetByLoaiBenhAsync(int loaiBenhID)
    {
        var result = await _repo.GetByLoaiBenhAsync(loaiBenhID);
        return ApiResponse<List<BaiVietListReadModel>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<List<BaiVietListReadModel>>> GetTopLuotXemAsync(int top)
    {
        var result = await _repo.GetTopLuotXemAsync(top);
        return ApiResponse<List<BaiVietListReadModel>>.SuccessResponse(result);
    }
}