using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ChucVuService
{
    private readonly IChucVuRepository _repo;

    public ChucVuService(IChucVuRepository repo)
    {
        _repo = repo;
    }
    public async Task<ApiResponse<int>> ThemAsync(ChucVuRequestDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TenChucVu))
            return ApiResponse<int>.Fail("Tên chức vụ không hợp lệ");
        var entity = new ChucVu(
            dto.TenChucVu.Trim(),
            dto.MoTa
        );
        await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(1);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, ChucVuRequestDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        if (string.IsNullOrWhiteSpace(dto.TenChucVu))
            return ApiResponse<bool>.Fail("Tên chức vụ không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Chức vụ không tồn tại");
        entity.CapNhat(dto.TenChucVu, dto.MoTa);
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<bool>> CapNhatTrangThaiAsync(int id, string trangThai)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Chức vụ không tồn tại");
        entity.CapNhatTrangThai(trangThai);
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<ChucVuReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<ChucVuReadModel>.Fail("Chức vụ không tồn tại");
        return ApiResponse<ChucVuReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<ChucVuListReadModel>>> GetPagedAsync(int page, int size, string? trangThai)
    {
        var (items, total) = await _repo.GetPagedAsync(page, size, trangThai);

        return ApiResponse<PagedResult<ChucVuListReadModel>>.SuccessResponse(
            new PagedResult<ChucVuListReadModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size
            });
    }
    public async Task<ApiResponse<PagedResult<ChucVuListReadModel>>> SearchAsync(string keyword, int page, int size)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return ApiResponse<PagedResult<ChucVuListReadModel>>.Fail("Từ khóa không hợp lệ");
        var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);
        return ApiResponse<PagedResult<ChucVuListReadModel>>.SuccessResponse(
            new PagedResult<ChucVuListReadModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size
            });
    }
    public async Task<ApiResponse<string?>> GetNameByIdAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<string?>.Fail("ID không hợp lệ");
        var result = await _repo.GetNameByIdAsync(id);
        return ApiResponse<string?>.SuccessResponse(result);
    }
    public async Task<ApiResponse<string?>> GetByNhanVienIdAsync(int nhanVienId)
    {
        if (nhanVienId <= 0)
            return ApiResponse<string?>.Fail("ID nhân viên không hợp lệ");
        var result = await _repo.GetByNhanVienIdAsync(nhanVienId);
        return ApiResponse<string?>.SuccessResponse(result);
    }
    public async Task<ApiResponse<List<NameResponseDTO>>> GetIdAndNameAsync()
    {
        var list = await _repo.GetIdAndNameAsync();
        var result = list.Select(x => new NameResponseDTO
        {
            Id = x.Id,
            Name = x.Ten
        }).ToList();
        return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
    }
}