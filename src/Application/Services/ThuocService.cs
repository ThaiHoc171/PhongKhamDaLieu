using Application.Common;
using Application.DTOs;
using Domain.Entities;
namespace Application.Services;
public class ThuocService
{
    private readonly IThuocRepository _repo;
    public ThuocService(IThuocRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<int>> TaoMoiAsync(ThuocRequestDTO dto)
    {
        var entity = new Thuoc(dto.TenThuoc, dto.HoatChat);

        var id = await _repo.AddAsync(entity);

        return ApiResponse<int>.SuccessResponse(id);
    }

    public async Task<ApiResponse<bool>> CapNhatAsync(int id, ThuocUpdateDTO dto)

    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Không tìm thấy thuốc");

        entity.CapNhat(dto.TenThuoc, dto.HoatChat);

        await _repo.UpdateAsync(entity);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<PagedResult<ThuocListReadModel>>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var (items, totalCount) =
            await _repo.GetPagedAsync(pageNumber, pageSize);
        return ApiResponse<PagedResult<ThuocListReadModel>>.SuccessResponse(
            new PagedResult<ThuocListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }

    public async Task<ApiResponse<ThuocReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);

        return ApiResponse<ThuocReadModel>.SuccessResponse(result);
    }

    public async Task<ApiResponse<PagedResult<ThuocListReadModel>>> SearchAsync(string keyword, int pageNumber, int pageSize)
    {
        var (items, totalCount) = await _repo.SearchPagedAsync(keyword, pageNumber, pageSize);
        return ApiResponse<PagedResult<ThuocListReadModel>>.SuccessResponse(
            new PagedResult<ThuocListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
}