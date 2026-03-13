using Application.Common;
using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public class ThietBiService
{
    private readonly IThietBiRepository _repo;

    public ThietBiService(IThietBiRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<int>> TaoMoiAsync(ThietBiRequestDTO dto)
    {
        var entity = new ThietBi(dto.TenTB, dto.LoaiTB);

        var id = await _repo.AddAsync(entity);

        return ApiResponse<int>.SuccessResponse(id);
    }

    public async Task<ApiResponse<bool>> CapNhatAsync(int id, ThietBiUpdateDTO dto)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<bool>.Fail("Không tìm thấy thiết bị");

        entity.CapNhat(dto.TenTB, dto.LoaiTB);

        await _repo.UpdateAsync(entity);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<PagedResult<ThietBiListReadModel>>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var (items, totalCount) =
            await _repo.GetPagedAsync(pageNumber, pageSize);
        return ApiResponse<PagedResult<ThietBiListReadModel>>.SuccessResponse(
            new PagedResult<ThietBiListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }

    public async Task<ApiResponse<ThietBiReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);

        return ApiResponse<ThietBiReadModel>.SuccessResponse(result);
    }

    public async Task<ApiResponse<PagedResult<ThietBiListReadModel>>> SearchAsync(string keyword, int pageNumber, int pageSize)
    {
        var (items, totalCount) =
            await _repo.SearchPagedAsync(keyword, pageNumber, pageSize);
        return ApiResponse<PagedResult<ThietBiListReadModel>>.SuccessResponse(
            new PagedResult<ThietBiListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
}