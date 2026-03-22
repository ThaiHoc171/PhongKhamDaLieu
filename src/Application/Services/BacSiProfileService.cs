using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class BacSiProfileService
{
    private readonly IBacSiProfileRepository _repository;

    public BacSiProfileService(IBacSiProfileRepository repository)
    {
        _repository = repository;
    }
    public async Task<ApiResponse<int>> TaoMoiAsync(BacSiProfileRequestDTO dto)
    {
        if (dto.NhanVienID <= 0)
            return ApiResponse<int>.Fail("NhanVienID không hợp lệ");
        var existed = await _repository.GetByNhanVienIdAsync(dto.NhanVienID);
        if (existed != null)
            return ApiResponse<int>.Fail("Bác sĩ đã có profile");
        var entity = new BacSiProfile(
            dto.NhanVienID,
            dto.GioiThieu,
            dto.ChuyenMon,
            dto.ThanhTuu,
            dto.HinhAnh,
            dto.KinhNghiem
        );
        var id = await _repository.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(id, "Tạo profile thành công");
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, BacSiProfileUpdateDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Không tìm thấy profile bác sĩ");
        entity.CapNhat(
            dto.GioiThieu,
            dto.ChuyenMon,
            dto.ThanhTuu,
            dto.HinhAnh,
            dto.KinhNghiem
        );
        await _repository.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true, "Cập nhật profile thành công");
    }
    public async Task<ApiResponse<BacSiProfileReadModel>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<BacSiProfileReadModel>.Fail("ID không hợp lệ");
        var data = await _repository.GetDetailAsync(id);
        if (data == null)
            return ApiResponse<BacSiProfileReadModel>.Fail("Không tìm thấy dữ liệu");
        return ApiResponse<BacSiProfileReadModel>.SuccessResponse(data);
    }
    public async Task<ApiResponse<BacSiProfileReadModel>> GetByNhanVienAsync(int nhanVienId)
    {
        if (nhanVienId <= 0)
            return ApiResponse<BacSiProfileReadModel>.Fail("NhanVienID không hợp lệ");

        var data = await _repository.GetByNhanVienIdAsync(nhanVienId);

        if (data == null)
            return ApiResponse<BacSiProfileReadModel>.Fail("Không tìm thấy profile");

        return ApiResponse<BacSiProfileReadModel>.SuccessResponse(data);
    }
    public async Task<ApiResponse<PagedResult<BacSiProfileListReadModel>>> GetPagedAsync(int page, int size)
    {
        if (page < 1) page = 1;
        if (size <= 0) size = 10;
        var (items, total) = await _repository.GetPagedAsync(page, size);
        var result = new PagedResult<BacSiProfileListReadModel>
        {
            Items = items,
            TotalCount = total,
            PageNumber = page,
            PageSize = size
        };
        return ApiResponse<PagedResult<BacSiProfileListReadModel>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<BacSiProfileListReadModel>>> SearchAsync(string keyword, int page, int size)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return ApiResponse<PagedResult<BacSiProfileListReadModel>>
                .Fail("Keyword không hợp lệ");
        var (items, total) =
            await _repository.SearchPagedAsync(keyword.Trim(), page, size);
        var result = new PagedResult<BacSiProfileListReadModel>
        {
            Items = items,
            TotalCount = total,
            PageNumber = page,
            PageSize = size
        };
        return ApiResponse<PagedResult<BacSiProfileListReadModel>>.SuccessResponse(result);
    }
}