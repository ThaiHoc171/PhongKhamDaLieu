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
	public async Task<ApiResponse<bool>> AddAsync(ThietBiRequestDTO dto)
	{
		var validate = Validate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);
		var entity = new ThietBi(dto.TenTB, dto.LoaiTB, dto.TrangThai);
		int row = await _repo.AddAsync(entity);
		if (row == 0)
			return ApiResponse<bool>.Fail("Tạo thiết bị thất bại");
		return ApiResponse<bool>.SuccessResponse(true, "Tạo thiết bị thành công");
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThietBiRequestDTO dto)
	{
		var validate = Validate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy thiết bị");
		entity.CapNhat(dto.TenTB, dto.LoaiTB, dto.TrangThai);
		int row = await _repo.UpdateAsync(entity);
		if (row == 0)
			return ApiResponse<bool>.Fail("Cập nhật thiết bị thất bại");
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thiết bị thành công");
	}
	public async Task<ApiResponse<PagedResult<ThietBiReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.GetPagedAsync(page, size);
		var result = new PagedResult<ThietBiReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThietBiReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<ThietBiReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<ThietBiReadModel>.Fail("Không tìm thấy thiết bị");
		return ApiResponse<ThietBiReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThietBiReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ThietBiReadModel>>
				.Fail("Keyword không hợp lệ");
		var (items, total) =
			await _repo.SearchPagedAsync(keyword.Trim(), page, size);
		var result = new PagedResult<ThietBiReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThietBiReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<ExcelImportResult<ThietBiImportDTO>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<ThietBiImportDTO>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();
			if (string.IsNullOrWhiteSpace(item.TenTB))
				errors.Add($"Row {row}: TenTB rỗng");

			if (string.IsNullOrWhiteSpace(item.LoaiTB))
				errors.Add($"Row {row}: LoaiTB rỗng");

			if (item.TrangThai != "Hoạt động" && item.TrangThai != "Vô hiệu")
				errors.Add($"Row {row}: TrangThai không hợp lệ");

			return errors;
		});
	}
	public async Task<ApiResponse<bool>> Import(List<ThietBiImportDTO> list)
	{
		var entities = list.Select(x =>
			new ThietBi(x.TenTB, x.LoaiTB, x.TrangThai)
		).ToList();

		await _repo.BulkInsertAsync(entities);

		return ApiResponse<bool>.SuccessResponse(true,"Nhập dữ liệu từ excel thành công!");
	}
	private ApiResponse<bool> Validate(ThietBiRequestDTO dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenTB))
			return ApiResponse<bool>.Fail("Tên thiết bị không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.LoaiTB))
			return ApiResponse<bool>.Fail("Loại thiết bị không hợp lệ");
		if (dto.TrangThai != "Hoạt động" && dto.TrangThai != "Vô hiệu")
			return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");
		return ApiResponse<bool>.SuccessResponse(true);
	}
}