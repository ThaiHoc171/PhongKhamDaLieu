using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;

namespace Application.Services;

public class ThuocService
{
	private readonly IThuocRepository _repo;

	public ThuocService(IThuocRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<bool>> AddAsync(ThuocRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = new Thuoc(
				dto.TenThuoc.Trim(),
				dto.HoatChat.Trim()
			);

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo thuốc thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo thuốc thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên thuốc đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThuocUpdateDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thuốc");

			entity.CapNhat(
				dto.TenThuoc.Trim(),
				dto.HoatChat.Trim()
			);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật thuốc thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thuốc thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên thuốc đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<bool>.Fail("ID không hợp lệ");

		var entity = await _repo.GetByIdAsync(id);

		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy thuốc");

		await _repo.DeleteAsync(id);

		return ApiResponse<bool>.SuccessResponse(true, "Xóa thuốc thành công");
	}

	public async Task<ApiResponse<ThuocReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<ThuocReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<ThuocReadModel>.Fail("Không tìm thấy thuốc");

		return ApiResponse<ThuocReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<ThuocReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<ThuocReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<ThuocReadModel>>
			.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<ThuocReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ThuocReadModel>>
				.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<ThuocReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<ThuocReadModel>>
			.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();

		return ApiResponse<List<NameResponseDTO>>
			.SuccessResponse(data);
	}

	public async Task<ApiResponse<ExcelImportResult<ThuocRequestDTO>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<ThuocRequestDTO>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(item.TenThuoc))
				errors.Add($"Dòng {row}: Tên thuốc đang rỗng");
			if(string.IsNullOrWhiteSpace(item.HoatChat))
				errors.Add($"Dòng {row}: Tên hoạt chất đang rỗng");
			return errors;
		});
	}

	public async Task<ApiResponse<ExcelImportResult<ThuocRequestDTO>>>
	ValidateImport(List<ThuocRequestDTO> list)
	{
		var result = new ExcelImportResult<ThuocRequestDTO>();

		int row = 2;

		var tenThuocSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		foreach (var item in list)
		{
			var errors = new List<string>();

			// trùng trong file
			if (!tenThuocSet.Add(item.TenThuoc))
				errors.Add($"Dòng {row}: Tên thuốc bị trùng trong file");

			// trùng DB
			if (await _repo.ExistsTenThuocAsync(item.TenThuoc))
				errors.Add($"Dòng {row}: Tên thuốc đã tồn tại");

			if (errors.Any())
			{
				result.Errors.Add(new ExcelImportError
				{
					Row = row,
					Errors = errors
				});
			}
			else
			{
				result.Data.Add(item);
			}

			row++;
		}

		return ApiResponse<ExcelImportResult<ThuocRequestDTO>>
			.SuccessResponse(result);
	}

	public async Task<ApiResponse<bool>> ImportAsync(List<ThuocRequestDTO> list)
	{
		if (list == null || list.Count == 0)
			return ApiResponse<bool>.Fail("Danh sách import rỗng");

		var entities = list.Select(x => new Thuoc(
			x.TenThuoc.Trim(),
			x.HoatChat.Trim()
		)).ToList();

		await _repo.BulkInsertAsync(entities);

		return ApiResponse<bool>.SuccessResponse(true, "Import thuốc thành công");
	}
}