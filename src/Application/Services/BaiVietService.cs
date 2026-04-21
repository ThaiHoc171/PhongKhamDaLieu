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
    public async Task<ApiResponse<int>> CreateAsync(ThemBaiVietDTO dto)
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
        var id = await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(id, "Tạo bài viết thành công");
    }
    public async Task<ApiResponse<bool>> UpdateAsync(int id, CapNhatBaiVietDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        entity.Update(
            dto.TieuDe,
            dto.TomTat,
            dto.NoiDung,
            dto.HinhAnh,
            dto.LoaiBenhID);
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true, "Cập nhật bài viết thành công");
    }
    public async Task<ApiResponse<bool>> PostAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        entity.Post();
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true, "Đã đăng bài viết!");
    }
    public async Task<ApiResponse<bool>> HideAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        entity.Hide();
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true, "Đã ẩn bài viết!");
    }
    public async Task<ApiResponse<bool>> SaveAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        entity.Save();
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true, "Đã lưu bài viết!");
    }


    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Bài viết không tồn tại");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(true, "Xóa bài viết thành công");
    }
    public async Task<ApiResponse<BaiVietReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<BaiVietReadModel>.Fail("Bài viết không tồn tại");
        return ApiResponse<BaiVietReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<BaiVietListReadModel>>> GetPagedAsync(int page, int size, string? trangThai)
    {
        var (items, total) = await _repo.GetPagedAsync(page, size, trangThai);
        return ApiResponse<PagedResult<BaiVietListReadModel>>.SuccessResponse(
            new PagedResult<BaiVietListReadModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size
            });
    }
    public async Task<ApiResponse<PagedResult<BaiVietListReadModel>>> SearchPagedAsync(string keyword, int page, int size, string? trangThai)
    {
        var (items, total) = await _repo.SearchPagedAsync(keyword, page, size, trangThai);
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