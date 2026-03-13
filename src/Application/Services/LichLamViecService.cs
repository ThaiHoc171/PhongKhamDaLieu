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
	public async Task<ApiResponse<bool>> UpdateAsync(int id, LichLamViecUpdateRequestDTO request)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy lịch làm việc");
		entity.Update(
			request.Ngay,
			request.CaLamViec,
			request.GhiChu
		);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<LichLamViecReadModel>> GetDetailAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<LichLamViecReadModel>.Fail("Không tìm thấy lịch");
		var nv = await _nvRepo.GetDetailAsync(entity.NhanVienID);
		var result = new LichLamViecReadModel
		{
			LichLamViecID = entity.LichLamViecID,
			NhanVien = new NameResponseDTO
			{
				Id = entity.NhanVienID,
				Name = nv.HoTen
			},
			Ngay = entity.Ngay,
			CaLamViec = entity.CaLamViec,
			GhiChu = entity.GhiChu
		};
		return ApiResponse<LichLamViecReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<WeekLichLamViecReadModel>> GetWeekByNhanVienAsync(int nhanVienID, int page)
	{
		var (start, end) = DateTimeHelper.GetWeekByPage(page);
		var data = await _repo.GetWeekByNhanVienAsync(nhanVienID, start, end);
		var result = new WeekLichLamViecReadModel
		{
			Page = page,
			TuanBatDau = start,
			TuanKetThuc = end,
			LichLamViecs = data
		};
		return ApiResponse<WeekLichLamViecReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<LichLamViecChucVuReadModel>>> GetWeekAsync(int page)
	{
		var (start, end) = DateTimeHelper.GetWeekByPage(page);
		var data = await _repo.GetWeekAsync(start, end);
		return ApiResponse<List<LichLamViecChucVuReadModel>>.SuccessResponse(data);
	}
	public async Task<ApiResponse<ImportResult>> ImportExcelAsync(Stream fileStream)
	{
		var result = new ImportResult();
		using var package = new ExcelPackage(fileStream);
		var sheet = package.Workbook.Worksheets[0];
		var rowCount = sheet.Dimension.Rows;
		for (int row = 2; row <= rowCount; row++)
		{
			try
			{
				var nhanVienID = sheet.Cells[row, 1].GetValue<int>();
				var ngay = sheet.Cells[row, 2].GetValue<DateTime>();
				var ca = sheet.Cells[row, 3].GetValue<int>();
				var ghiChu = sheet.Cells[row, 4].GetValue<string>();
				var request = new LichLamViecRequestDTO
				{
					NhanVienID = nhanVienID,
					Ngay = ngay,
					CaLamViec = ca,
					GhiChu = ghiChu
				};
				var validate = await ValidateCreate(request);
				if (!validate.Success)
				{
					result.Errors.Add(new ImportError
					{
						Row = row,
						Message = validate.Message
					});
					continue;
				}
				var entity = new LichLamViec(
					request.NhanVienID,
					request.Ngay,
					request.CaLamViec,
					request.GhiChu
				);
				await _repo.AddAsync(entity);
				result.SuccessCount++;
			}
			catch (Exception ex)
			{
				result.Errors.Add(new ImportError
				{
					Row = row,
					Message = ex.Message
				});
			}
		}
		return ApiResponse<ImportResult>.SuccessResponse(result);
	}
	// VALIDATION
	private async Task<(bool Success, string Message)> ValidateCreate(LichLamViecRequestDTO request)
	{
		if (request.Ngay.Date < DateTime.Today)
			return (false, "Ngày làm việc không hợp lệ");
		if (await _repo.ExistsAsync(
				request.NhanVienID,
				request.Ngay,
				request.CaLamViec))
		{
			return (false, "Nhân viên đã có lịch ca này");
		}
		if (await _nghiRepo.IsNgayNghiAsync(
				request.NhanVienID,
				request.Ngay))
		{
			return (false, "Nhân viên đang nghỉ ngày này");
		}
		var nv = await _nvRepo.GetByIdAsync(request.NhanVienID);
		if (nv == null)
			return (false, "Nhân viên không tồn tại");
		var count = await _repo.CountByChucVuAsync(
			nv.ChucVuID,
			request.Ngay,
			request.CaLamViec);
		if (nv.ChucVuID == 3)
		{
			if (count >= 2)
				return (false, "Ca đã đủ 2 y tá");
		}
		else
		{
			if (count >= 1)
				return (false, "Ca đã có nhân viên chức vụ này");
		}
		return (true, "");
	}
}