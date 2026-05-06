using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Application.Services;

public class LoaiBenhService
{
	private readonly ILoaiBenhRepository _repo;

	public LoaiBenhService(ILoaiBenhRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<bool>> AddAsync(LoaiBenhRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = new LoaiBenh(
				dto.TenBenh.Trim(),
				dto.TenKhoaHoc.Trim(),
				dto.NhomBenh.Trim(),
				dto.MoTa.Trim(),
				dto.DoPhoBien,
				dto.MucDoNghiemTrong
			);

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo loại bệnh thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo loại bệnh thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên bệnh hoặc tên khoa học đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, LoaiBenhUpdateDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy loại bệnh");

			entity.CapNhat(
				dto.TenBenh.Trim(),
				dto.TenKhoaHoc.Trim(),
				dto.NhomBenh.Trim(),
				dto.MoTa.Trim(),
				dto.DoPhoBien,
				dto.MucDoNghiemTrong
			);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật loại bệnh thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật loại bệnh thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên bệnh hoặc tên khoa học đã tồn tại");
		}
	}
	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<bool>.Fail("ID không hợp lệ");

		var entity = await _repo.GetByIdAsync(id);

		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy loại bệnh");

		try
		{
			int row = await _repo.DeleteAsync(id);

			if (row == 0)
				return ApiResponse<bool>.Fail("Xóa thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Xóa loại bệnh thành công");
		}
		catch (SqlException ex) when (ex.Number == 547)
		{
			return ApiResponse<bool>.Fail("Không thể xóa vì loại bệnh đang được sử dụng");
		}
		catch (SqlException ex)
		{
			return ApiResponse<bool>.Fail($"Lỗi database: {ex.Message}");
		}
	}
	public async Task<ApiResponse<LoaiBenhReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<LoaiBenhReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<LoaiBenhReadModel>.Fail("Loại bệnh không tồn tại");

		return ApiResponse<LoaiBenhReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<LoaiBenhListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<LoaiBenhListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<LoaiBenhListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<LoaiBenhListReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<LoaiBenhListReadModel>>.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<LoaiBenhListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<LoaiBenhListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}

	public async Task<ApiResponse<string?>> GetTenBenhAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<string?>.Fail("ID không hợp lệ");

		var result = await _repo.GetTenBenhByIdAsync(id);

		return ApiResponse<string?>.SuccessResponse(result);
	}

	public async Task<ApiResponse<ExcelImportResult<LoaiBenhRequestDTO>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<LoaiBenhRequestDTO>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(item.TenBenh))
				errors.Add($"Dòng {row}: Tên bệnh đang rỗng");

			if (string.IsNullOrWhiteSpace(item.NhomBenh))
				errors.Add($"Dòng {row}: Nhóm bệnh đang rỗng");

			if (item.DoPhoBien != "Phổ biến"
				&& item.DoPhoBien != "Ít gặp"
				&& item.DoPhoBien != "Hiếm")
				errors.Add($"Dòng {row}: Độ phổ biến không hợp lệ");

			if (item.MucDoNghiemTrong != "Nhẹ"
				&& item.MucDoNghiemTrong != "Trung bình"
				&& item.MucDoNghiemTrong != "Nặng")
				errors.Add($"Dòng {row}: Mức độ nghiêm trọng không hợp lệ");

			return errors;
		});
	}
	public async Task<ApiResponse<ExcelImportResult<LoaiBenhRequestDTO>>>
ValidateImport(List<LoaiBenhRequestDTO> list)
	{
		var result = new ExcelImportResult<LoaiBenhRequestDTO>();

		int row = 2;

		var tenBenhSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		var tenKhoaHocSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		foreach (var item in list)
		{
			var errors = new List<string>();

			if (string.IsNullOrWhiteSpace(item.TenBenh))
				errors.Add($"Dòng {row}: Tên bệnh rỗng");

			if (string.IsNullOrWhiteSpace(item.NhomBenh))
				errors.Add($"Dòng {row}: Nhóm bệnh rỗng");

			// trùng trong file
			if (!tenBenhSet.Add(item.TenBenh))
				errors.Add($"Dòng {row}: Tên bệnh bị trùng trong file");

			if (!string.IsNullOrWhiteSpace(item.TenKhoaHoc))
			{
				if (!tenKhoaHocSet.Add(item.TenKhoaHoc))
					errors.Add($"Dòng {row}: Tên khoa học bị trùng trong file");
			}

			// trùng DB
			if (await _repo.ExistsTenBenhAsync(item.TenBenh))
				errors.Add($"Dòng {row}: Tên bệnh đã tồn tại");

			if (!string.IsNullOrWhiteSpace(item.TenKhoaHoc))
			{
				if (await _repo.ExistsTenKhoaHocAsync(item.TenKhoaHoc))
					errors.Add($"Dòng {row}: Tên khoa học đã tồn tại");
			}

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

		return ApiResponse<ExcelImportResult<LoaiBenhRequestDTO>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> ImportAsync(List<LoaiBenhRequestDTO> list)
	{
		if (list == null || list.Count == 0)
			return ApiResponse<bool>.Fail("Danh sách import rỗng");

		var entities = list.Select(x => new LoaiBenh(
			x.TenBenh.Trim(),
			x.TenKhoaHoc.Trim(),
			x.NhomBenh.Trim(),
			x.MoTa.Trim(),
			x.DoPhoBien,
			x.MucDoNghiemTrong
		)).ToList();

		await _repo.BulkInsertAsync(entities);

		return ApiResponse<bool>.SuccessResponse(true, "Import loại bệnh thành công");
	}
}