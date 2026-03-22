using Application.Common;
using Application.DTOs;
using Domain.Entities;
using OfficeOpenXml;
namespace Application.Services;
public class ThietBiService
{
	private readonly IThietBiRepository _repo;
	public ThietBiService(IThietBiRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<int>> AddAsync(ThietBiRequestDTO dto)
	{
		var validate = ValidateCreate(dto);
		if (!validate.Success)
			return ApiResponse<int>.Fail(validate.Message);
		var entity = new ThietBi(dto.TenTB, dto.LoaiTB);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id, "Tạo thiết bị thành công");
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThietBiUpdateDTO dto)
	{
		var validate = ValidateUpdate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy thiết bị");
		entity.CapNhat(dto.TenTB, dto.LoaiTB);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thiết bị thành công");
	}
	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy thiết bị");
		await _repo.DeleteAsync(id);
		return ApiResponse<bool>.SuccessResponse(true, "Xóa thiết bị thành công");
	}
	public async Task<ApiResponse<PagedResult<ThietBiListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.GetPagedAsync(page, size);
		var result = new PagedResult<ThietBiListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThietBiListReadModel>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<ThietBiReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<ThietBiReadModel>.Fail("Không tìm thấy thiết bị");
		return ApiResponse<ThietBiReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThietBiListReadModel>>> SearchAsync(
		string keyword,
		int page,
		int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ThietBiListReadModel>>
				.Fail("Keyword không hợp lệ");
		var (items, total) =
			await _repo.SearchPagedAsync(keyword.Trim(), page, size);
		var result = new PagedResult<ThietBiListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThietBiListReadModel>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<int>> ImportExcelAsync(Stream stream)
	{
		using var package = new ExcelPackage(stream);
		var sheet = package.Workbook.Worksheets.FirstOrDefault();
		if (sheet == null)
			return ApiResponse<int>.Fail("File Excel không hợp lệ");
		var rowCount = sheet.Dimension.Rows;
		int success = 0;
		for (int row = 2; row <= rowCount; row++)
		{
			var tenTB = sheet.Cells[row, 1].Text?.Trim();
			var loaiTB = sheet.Cells[row, 2].Text?.Trim();
			if (string.IsNullOrWhiteSpace(tenTB))
				continue;
			var entity = new ThietBi(tenTB, loaiTB);
			await _repo.AddAsync(entity);
			success++;
		}
		return ApiResponse<int>.SuccessResponse(success, "Import thành công");
	}
	private ApiResponse<bool> ValidateCreate(ThietBiRequestDTO dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenTB))
			return ApiResponse<bool>.Fail("Tên thiết bị không được để trống");
		return ApiResponse<bool>.SuccessResponse(true);
	}
	private ApiResponse<bool> ValidateUpdate(ThietBiUpdateDTO dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenTB))
			return ApiResponse<bool>.Fail("Tên thiết bị không được để trống");
		return ApiResponse<bool>.SuccessResponse(true);
	}
}