using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

public class NgayNghiNhanVienService
{
	private readonly INgayNghiNhanVienRepository _repo;
	private readonly ILichLamViecRepository _lichRepo;
	private readonly INhanVienRepository _nvRepo;

	public NgayNghiNhanVienService(
		INgayNghiNhanVienRepository repo,
		ILichLamViecRepository lichRepo,
		INhanVienRepository nvRepo)
	{
		_repo = repo;
		_lichRepo = lichRepo;
		_nvRepo = nvRepo;
	}

	// ================= CRUD =================

	public async Task<ApiResponse<bool>> AddAsync(NgayNghiRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			if (dto.NhanVienID <= 0)
				return ApiResponse<bool>.Fail("Nhân viên không hợp lệ");

			if (dto.Ngay < DateTime.Today)
				return ApiResponse<bool>.Fail("Ngày nghỉ không hợp lệ");

			if (await _repo.ExistsAsync(dto.NhanVienID, dto.Ngay))
				return ApiResponse<bool>.Fail("Nhân viên đã có ngày nghỉ");

			if (await _lichRepo.ExistsAsync(dto.NhanVienID, dto.Ngay, 1) ||
				await _lichRepo.ExistsAsync(dto.NhanVienID, dto.Ngay, 2))
				return ApiResponse<bool>.Fail("Nhân viên đã có lịch làm");

			var entity = new NgayNghiNhanVien(
				dto.NhanVienID,
				dto.Ngay,
				dto.LyDo
			);

			await _repo.AddAsync(entity);

			return ApiResponse<bool>.SuccessResponse(true, "Thêm ngày nghỉ thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, NgayNghiUpdateRequestDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy ngày nghỉ");

			if (dto.Ngay < DateTime.Today)
				return ApiResponse<bool>.Fail("Ngày nghỉ không hợp lệ");

			if (entity.Ngay != dto.Ngay &&
				await _repo.ExistsAsync(entity.NhanVienID, dto.Ngay))
				return ApiResponse<bool>.Fail("Đã tồn tại ngày nghỉ");

			if (await _lichRepo.ExistsAsync(entity.NhanVienID, dto.Ngay, 1) ||
				await _lichRepo.ExistsAsync(entity.NhanVienID, dto.Ngay, 2))
				return ApiResponse<bool>.Fail("Nhân viên đã có lịch làm");

			entity.Update(dto.Ngay, dto.LyDo);

			await _repo.UpdateAsync(entity);

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);

		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy ngày nghỉ");

		await _repo.DeleteAsync(id);

		return ApiResponse<bool>.SuccessResponse(true, "Xóa thành công");
	}

	// ================= IMPORT =================

	public async Task<ApiResponse<ExcelImportResult<NgayNghiRequestDTO>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<NgayNghiRequestDTO>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();

			if (item.NhanVienID <= 0)
				errors.Add($"Dòng {row}: Nhân viên không hợp lệ");

			if (item.Ngay == default)
				errors.Add($"Dòng {row}: Ngày không hợp lệ");

			return errors;
		});
	}

	public async Task<ApiResponse<ExcelImportResult<NgayNghiRequestDTO>>> ValidateImport(List<NgayNghiRequestDTO> list)
	{
		var result = new ExcelImportResult<NgayNghiRequestDTO>();
		int row = 2;

		foreach (var item in list)
		{
			var errors = new List<string>();

			var nv = await _nvRepo.GetByIdAsync(item.NhanVienID);

			if (nv == null)
				errors.Add($"Dòng {row}: Nhân viên không tồn tại");

			if (item.Ngay < DateTime.Today)
				errors.Add($"Dòng {row}: Ngày nghỉ không hợp lệ");

			else
			{
				if (await _repo.ExistsAsync(item.NhanVienID, item.Ngay))
					errors.Add($"Dòng {row}: Đã có ngày nghỉ");

				if (await _lichRepo.ExistsAsync(item.NhanVienID, item.Ngay, 1) ||
					await _lichRepo.ExistsAsync(item.NhanVienID, item.Ngay, 2))
					errors.Add($"Dòng {row}: Nhân viên đã có lịch làm");
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

		return ApiResponse<ExcelImportResult<NgayNghiRequestDTO>>
			.SuccessResponse(result);
	}

	public async Task<ApiResponse<bool>> Import(List<NgayNghiRequestDTO> list)
	{
		try
		{
			var entities = list.Select(x =>
				new NgayNghiNhanVien(
					x.NhanVienID,
					x.Ngay,
					x.LyDo
				)
			).ToList();

			await _repo.BulkInsertAsync(entities);

			return ApiResponse<bool>
				.SuccessResponse(true, "Import ngày nghỉ thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	// ================= QUERY =================
	public async Task<ApiResponse<NgayNghiReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<NgayNghiReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<NgayNghiReadModel>.Fail("Không tìm thấy ngày nghỉ");

		return ApiResponse<NgayNghiReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<NgayNghiReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<NgayNghiReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<NgayNghiReadModel>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<NgayNghiReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<NgayNghiReadModel>>
				.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<NgayNghiReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<NgayNghiReadModel>>
			.SuccessResponse(result);
	}
}