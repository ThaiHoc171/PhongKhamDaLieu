using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using OfficeOpenXml;

namespace Application.Services;

public class LichLamViecService
{
	private readonly ILichLamViecRepository _repo;
	private readonly INgayNghiNhanVienRepository _nghiRepo;
	private readonly INhanVienRepository _nvRepo;

	public LichLamViecService(
		ILichLamViecRepository repo,
		INgayNghiNhanVienRepository nghiRepo,
		INhanVienRepository nvRepo)
	{
		_repo = repo;
		_nghiRepo = nghiRepo;
		_nvRepo = nvRepo;
	}

	// WEEK BY NHANVIEN
	public async Task<ApiResponse<LichLamViecReadWeekModel>> GetWeekByNhanVienAsync(int nhanVienID, int week)
	{
		if (nhanVienID <= 0)
			return ApiResponse<LichLamViecReadWeekModel>.Fail("Nhân viên không hợp lệ");
		var (start, end) = DateTimeHelper.GetWeekByPage(week);
		var data = await _repo.GetWeekByNhanVienAsync(nhanVienID, start, end);
		var result = new LichLamViecReadWeekModel
		{
			Page = week,
			TuanBatDau = start,
			TuanKetThuc = end,
			LichLamViecs = data
		};

		return ApiResponse<LichLamViecReadWeekModel>.SuccessResponse(result);
	}

	// WEEK ALL
	public async Task<ApiResponse<List<LichLamViecReadListModel>>> GetWeekAsync(int week)
	{
		var (start, end) = DateTimeHelper.GetWeekByPage(week);
		var data = await _repo.GetWeekAsync(start, end);
		return ApiResponse<List<LichLamViecReadListModel>>.SuccessResponse(data);
	}

	public async Task<ApiResponse<ExcelImportResult<LichLamViecImport>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<LichLamViecImport>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();

			if (item.NhanVienID <= 0)
				errors.Add($"Dòng {row}: Nhân viên không hợp lệ");

			if (item.Ngay == default)
				errors.Add($"Dòng {row}: Ngày làm việc không hợp lệ");

			if (item.CaLamViec < 1 || item.CaLamViec > 2)
				errors.Add($"Dòng {row}: Ca làm việc phải là 1 hoặc 2");

			return errors;
		});
	}
	public async Task<ApiResponse<ExcelImportResult<LichLamViecImport>>> ValidateImport(List<LichLamViecImport> list)
	{
		var result = new ExcelImportResult<LichLamViecImport>();

		int row = 2;

		foreach (var item in list)
		{
			var errors = new List<string>();

			var nv = await _nvRepo.GetByIdAsync(item.NhanVienID);

			if (nv == null)
				errors.Add($"Dòng {row}: Nhân viên không tồn tại");
			if (item.Ngay < DateTime.Today)
				errors.Add($"Dòng {row}: Ngày làm việc không được trước ngày hiện tại");

			else
			{
				if (await _repo.ExistsAsync(item.NhanVienID, item.Ngay, item.CaLamViec))
					errors.Add($"Dòng {row}: Nhân viên đã có lịch ca này");

				if (await _nghiRepo.IsNgayNghiAsync(item.NhanVienID, item.Ngay))
					errors.Add($"Dòng {row}: Nhân viên đang nghỉ ngày này");

				var count = await _repo.CountByChucVuAsync(
					nv.ChucVuID,
					item.Ngay,
					item.CaLamViec);

				if (nv.ChucVuID == 3)
				{
					if (count >= 2)
						errors.Add($"Dòng {row}: Ca đã đủ 2 y tá");
				}
				else
				{
					if (count >= 1)
						errors.Add($"Dòng {row}: Ca đã có nhân viên chức vụ này");
				}
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

		return ApiResponse<ExcelImportResult<LichLamViecImport>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> Import(List<LichLamViecImport> list)
	{
		try
		{
			var entities = list.Select(x =>
				new LichLamViec(
					x.NhanVienID,
					x.Ngay,
					x.CaLamViec,
					x.GhiChu
				)
			).ToList();

			await _repo.BulkInsertAsync(entities);

			return ApiResponse<bool>
				.SuccessResponse(true, "Nhập dữ liệu từ excel thành công!");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
}