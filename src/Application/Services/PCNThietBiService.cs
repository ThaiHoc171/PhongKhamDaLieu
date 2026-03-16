using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
namespace Application.Services;
public class PCNThietBiService
{
    private readonly IPCNThietBiRepository _repo;
    public PCNThietBiService(IPCNThietBiRepository repo)
    {
        _repo = repo;
    }
    public async Task<ApiResponse<int>> TaoMoiAsync(PCNThietBiRequestDTO dto)
    {
        var existed = await _repo.GetByPhongAndThietBiAsync(
            dto.PhongChucNangID,
            dto.ThietBiID);
        if (existed != null)
            return ApiResponse<int>.Fail("Thiết bị đã tồn tại trong phòng");
        var entity = new PCNThietBi(
            dto.PhongChucNangID,
            dto.ThietBiID
        );
        var id = await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(id);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, PCNThietBiUpdateDTO dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Thiết bị không tồn tại");
        try
        {
            entity.CapNhatSoLuong(dto.TongSoLuong);
        }
        catch (InvalidOperationException ex)
        {
            return ApiResponse<bool>.Fail(ex.Message);
        }
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<bool>> XoaAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Thiết bị không tồn tại");
        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<PCNThietBiReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<PCNThietBiReadModel>.Fail("Thiết bị không tồn tại");
        return ApiResponse<PCNThietBiReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<List<PCNThietBiReadModel>>> GetByPhongAsync(int phongId)
    {
        var result = await _repo.GetByPhongAsync(phongId);
        return ApiResponse<List<PCNThietBiReadModel>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<PCNThietBiListReadModel>>> GetPagedAsync(int pageNumber, int pageSize, int? phongChucNangID)
    {
        var (items, totalCount) = await _repo.GetPagedAsync(pageNumber, pageSize, phongChucNangID);

        return ApiResponse<PagedResult<PCNThietBiListReadModel>>.SuccessResponse(
            new PagedResult<PCNThietBiListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
    public async Task<ApiResponse<PagedResult<PCNThietBiListReadModel>>> SearchAsync(string keyword, int pageNumber, int pageSize, int? phongChucNangID)
    {
        var (items, totalCount) = await _repo.SearchPagedAsync(keyword, pageNumber, pageSize, phongChucNangID);
        return ApiResponse<PagedResult<PCNThietBiListReadModel>>.SuccessResponse(
            new PagedResult<PCNThietBiListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
}