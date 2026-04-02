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

	public async Task<ApiResponse<int>> AddAsync(PCNThietBiRequestDTO dto)
	{
		if (dto == null)
			return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");

		var existed = await _repo.GetByPhongAndThietBiAsync(dto.PhongChucNangID, dto.ThietBiID);
		if (existed != null)
			return ApiResponse<int>.Fail("Thiết bị đã tồn tại trong phòng");

		var entity = new PCNThietBi(dto.PhongChucNangID, dto.ThietBiID);
		var id = await _repo.AddAsync(entity);
		if (id == 0)
			return ApiResponse<int>.Fail("Tạo thiết bị thất bại");

		return ApiResponse<int>.SuccessResponse(id, "Tạo thiết bị thành công");
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, PCNThietBiUpdateDTO dto)
	{
		if (id <= 0)
			return ApiResponse<bool>.Fail("ID không hợp lệ");
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Thiết bị không tồn tại");

		try
		{
			entity.Update(dto.TongSoLuong);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}

		var row = await _repo.UpdateAsync(entity);
		if (row == 0)
			return ApiResponse<bool>.Fail("Cập nhật thiết bị thất bại");

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thiết bị thành công");
	}

	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<bool>.Fail("ID không hợp lệ");

		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Thiết bị không tồn tại");

		await _repo.DeleteAsync(id);
		return ApiResponse<bool>.SuccessResponse(true, "Xóa thiết bị thành công");
	}

	public async Task<ApiResponse<PCNThietBiReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<PCNThietBiReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<PCNThietBiReadModel>.Fail("Thiết bị không tồn tại");

		return ApiResponse<PCNThietBiReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<PCNThietBiReadModel>>> GetByPhongAsync(int phongId)
	{
		if (phongId <= 0)
			return ApiResponse<List<PCNThietBiReadModel>>.Fail("ID phòng không hợp lệ");

		var result = await _repo.GetByPhongAsync(phongId);
		return ApiResponse<List<PCNThietBiReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<PCNThietBiReadListModel>>> GetPagedAsync(int page, int size, int? phongChucNangID)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size, phongChucNangID);
		var result = new PagedResult<PCNThietBiReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<PCNThietBiReadListModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<PCNThietBiReadListModel>>> SearchAsync(string keyword, int page, int size, int? phongChucNangID)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<PCNThietBiReadListModel>>.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size, phongChucNangID);
		var result = new PagedResult<PCNThietBiReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<PCNThietBiReadListModel>>.SuccessResponse(result);
	}
}