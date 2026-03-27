using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Application.Services;

public class CanLamSangService
{
	private readonly ICanLamSangRepository _repo;

	public CanLamSangService(ICanLamSangRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<bool>> AddAsync(CanLamSangRequest dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			var entity = new CanLamSang(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem, dto.TrangThai);

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo cận lâm sàng thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo cận lâm sàng thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên cận lâm sàng đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, CanLamSangRequest dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy cận lâm sàng");

			entity.CapNhat(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem, dto.TrangThai);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật cận lâm sàng thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật cận lâm sàng thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when(ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên cận lâm sàng đã tồn tại");
		}
	}

	public async Task<ApiResponse<PagedResult<CanLamSangListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<CanLamSangListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<CanLamSangListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<CanLamSangReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<CanLamSangReadModel>.Fail("Không tìm thấy cận lâm sàng");

		return ApiResponse<CanLamSangReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<CanLamSangListReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<CanLamSangListReadModel>>
				.Fail("Keyword không hợp lệ");

		var (items, total) =
			await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<CanLamSangListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<CanLamSangListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<CanLamSangListReadModel>>> GetByLoaiXetNghiemAsync(string loai)
	{
		if (string.IsNullOrWhiteSpace(loai))
			return ApiResponse<List<CanLamSangListReadModel>>
				.Fail("Loại xét nghiệm không hợp lệ");

		var result = await _repo.GetByLoaiXetNghiemAsync(loai);

		return ApiResponse<List<CanLamSangListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();

		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}

	public async Task<ApiResponse<ExcelImportResult<CanLamSangImport>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<CanLamSangImport>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();
			if (string.IsNullOrWhiteSpace(item.TenCLS))
				errors.Add($"Dòng {row}: Tên đang rỗng");
			if (string.IsNullOrWhiteSpace(item.MoTa))
				errors.Add($"Dòng {row}: Mô tả đang rỗng");
			if (string.IsNullOrWhiteSpace(item.LoaiXetNghiem))
				errors.Add($"Dòng {row}: Loại đang rỗng");
			if (item.TrangThai != "Hoạt động" && item.TrangThai != "Vô hiệu")
				errors.Add($"Dòng {row}: Trạng thái không hợp lệ");
			return errors;
		});
	}
	public async Task<ApiResponse<bool>> Import(List<CanLamSangImport> list)
	{
		var entities = list.Select(x =>
			new CanLamSang(x.TenCLS, x.MoTa,x.LoaiXetNghiem, x.TrangThai)
		).ToList();
		await _repo.BulkInsertAsync(entities);
		return ApiResponse<bool>.SuccessResponse(true, "Nhập dữ liệu từ excel thành công!");
	}
}