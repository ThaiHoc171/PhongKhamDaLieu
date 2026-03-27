using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Microsoft.Data.SqlClient;
namespace Application.Services;
public class ThietBiService
{
	private readonly IThietBiRepository _repo;
	public ThietBiService(IThietBiRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<bool>> AddAsync(ThietBiRequest dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			var entity = new ThietBi(dto.TenTB, dto.LoaiTB, dto.TrangThai);
			int row = await _repo.AddAsync(entity);
			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo thiết bị thất bại");
			return ApiResponse<bool>.SuccessResponse(true, "Tạo thiết bị thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên thiết bị  đã tồn tại");
		}
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThietBiRequest dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			var entity = await _repo.GetByIdAsync(id);
			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thiết bị");
			entity.CapNhat(dto.TenTB, dto.LoaiTB, dto.TrangThai);
			int row = await _repo.UpdateAsync(entity);
			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật thiết bị thất bại");
			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thiết bị thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên thiết bị đã tồn tại");
		}
	}
	public async Task<ApiResponse<PagedResult<ThietBiReadListModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.GetPagedAsync(page, size);
		var result = new PagedResult<ThietBiReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThietBiReadListModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<ThietBiReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<ThietBiReadModel>.Fail("Không tìm thấy thiết bị");
		return ApiResponse<ThietBiReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThietBiReadListModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ThietBiReadListModel>>
				.Fail("Keyword không hợp lệ");
		var (items, total) =
			await _repo.SearchPagedAsync(keyword.Trim(), page, size);
		var result = new PagedResult<ThietBiReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThietBiReadListModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();

		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}
	public async Task<ApiResponse<ExcelImportResult<ThietBiImport>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<ThietBiImport>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();
			if (string.IsNullOrWhiteSpace(item.TenTB))
				errors.Add($"Dòng {row}: Tên đang rỗng");

			if (string.IsNullOrWhiteSpace(item.LoaiTB))
				errors.Add($"Dòng {row}: Loại đang rỗng");

			if (item.TrangThai != "Hoạt động" && item.TrangThai != "Vô hiệu")
				errors.Add($"Dòng {row}: Trạng thái không hợp lệ");

			return errors;
		});
	}
	public async Task<ApiResponse<bool>> Import(List<ThietBiImport> list)
	{
		var entities = list.Select(x =>
			new ThietBi(x.TenTB, x.LoaiTB, x.TrangThai)
		).ToList();

		await _repo.BulkInsertAsync(entities);

		return ApiResponse<bool>.SuccessResponse(true,"Nhập dữ liệu từ excel thành công!");
	}
}