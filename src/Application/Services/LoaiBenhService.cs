using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class LoaiBenhService
{
    private readonly ILoaiBenhRepository _repo;

    public LoaiBenhService(ILoaiBenhRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<PagedResult<LoaiBenhListReadModel>>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var (items, totalCount) = await _repo.GetPagedAsync(pageNumber, pageSize);

        return ApiResponse<PagedResult<LoaiBenhListReadModel>>.SuccessResponse(
            new PagedResult<LoaiBenhListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }

    public async Task<ApiResponse<PagedResult<LoaiBenhListReadModel>>> SearchAsync(string keyword, int pageNumber, int pageSize)
    {
        var (items, totalCount) = await _repo.SearchPagedAsync(keyword, pageNumber, pageSize);

        return ApiResponse<PagedResult<LoaiBenhListReadModel>>.SuccessResponse(
            new PagedResult<LoaiBenhListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }

    public async Task<ApiResponse<LoaiBenhReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);

        if (result == null)
            return ApiResponse<LoaiBenhReadModel>.Fail("Loại bệnh không tồn tại");

        return ApiResponse<LoaiBenhReadModel>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
    {
        var list = await _repo.GetIdAndNameAsync();

        return ApiResponse<List<NameResponseDTO>>.SuccessResponse(
            list.Select(x => new NameResponseDTO
            {
                Id = x.Id,
                Name = x.Ten
            }).ToList());
    }
    public async Task<ApiResponse<int>> TaoMoiAsync(LoaiBenhRequestDTO dto)
    {
        var entity = new LoaiBenh(dto.TenBenh, dto.TenKhoaHoc, dto.NhomBenh, dto.MoTa, dto.DoPhoBien, dto.MucDoNghiemTrong);
        var danhSach = await _repo.GetAllAsync();
        try
        {
            entity.KiemTraTrung(danhSach);
        }
        catch (ArgumentException ex)
        {
            return ApiResponse<int>.Fail(ex.Message);
        }
        var id = await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(id);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, LoaiBenhUpdateDTO dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Loại bệnh không tồn tại");
        entity.CapNhat(dto.TenBenh, dto.TenKhoaHoc, dto.NhomBenh, dto.MoTa, dto.DoPhoBien, dto.MucDoNghiemTrong);
        var danhSach = await _repo.GetAllAsync();
        try
        {
            entity.KiemTraTrung(danhSach);
        }
        catch (ArgumentException ex)
        {
            return ApiResponse<bool>.Fail(ex.Message);
        }
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<string>> GetTenBenhAsync(int id)
    {
        var ten = await _repo.GetTenBenhByIdAsync(id);

        if (ten == null)
            return ApiResponse<string>.Fail("Loại bệnh không tồn tại");

        return ApiResponse<string>.SuccessResponse(ten);
    }
}