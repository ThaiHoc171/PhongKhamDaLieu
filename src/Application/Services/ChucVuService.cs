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

	public async Task<ApiResponse<bool>> AddAsync(ChucVuRequest dto)
	{
		var validate = Validate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);

		var entity = new ChucVu(
			dto.TenChucVu.Trim(),
			dto.MoTa,
			dto.TrangThai
		);

		int row = await _repo.AddAsync(entity);

		if (row == 0)
			return ApiResponse<bool>.Fail("Tạo chức vụ thất bại");

		return ApiResponse<bool>.SuccessResponse(true, "Tạo chức vụ thành công");
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, ChucVuRequest dto)
	{
		if (id <= 0)
			return ApiResponse<bool>.Fail("ID không hợp lệ");

		var validate = Validate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);

		var entity = await _repo.GetByIdAsync(id);

		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy chức vụ");

		entity.CapNhat(dto.TenChucVu, dto.MoTa, dto.TrangThai);

		int row = await _repo.UpdateAsync(entity);

		if (row == 0)
			return ApiResponse<bool>.Fail("Cập nhật chức vụ thất bại");

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật chức vụ thành công");
	}

	public async Task<ApiResponse<ChucVuReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<ChucVuReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<ChucVuReadModel>.Fail("Chức vụ không tồn tại");

		return ApiResponse<ChucVuReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<ChucVuListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<ChucVuListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<ChucVuListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<ChucVuListReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ChucVuListReadModel>>
				.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<ChucVuListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<ChucVuListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<string?>> GetByNhanVienIdAsync(int nhanVienId)
	{
		if (nhanVienId <= 0)
			return ApiResponse<string?>.Fail("ID nhân viên không hợp lệ");

		var result = await _repo.GetByNhanVienIdAsync(nhanVienId);

		return ApiResponse<string?>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();

		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}

	private ApiResponse<bool> Validate(ChucVuRequest dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenChucVu))
			return ApiResponse<bool>.Fail("Tên chức vụ không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.MoTa))
			return ApiResponse<bool>.Fail("Mô tả không hợp lệ");
		if (dto.TrangThai != "Hoạt động" && dto.TrangThai != "Vô hiệu")
			return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");

		return ApiResponse<bool>.SuccessResponse(true);
	}
}