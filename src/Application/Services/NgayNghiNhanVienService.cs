using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using OfficeOpenXml;
namespace Application.Services;
public class NgayNghiNhanVienService
{
	private readonly INgayNghiNhanVienRepository _repo;
	public NgayNghiNhanVienService(INgayNghiNhanVienRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<int>> AddAsync(NgayNghiRequestDTO dto)
	{
		if (dto.NhanVienID <= 0)
			return ApiResponse<int>.Fail("Nhân viên không hợp lệ");
		if (await _repo.IsNgayNghiAsync(dto.NhanVienID, dto.Ngay))
			return ApiResponse<int>.Fail("Nhân viên đã có ngày nghỉ trong ngày này");
		var entity = new NgayNghiNhanVien(
			dto.NhanVienID,
			dto.Ngay,
			dto.LyDo
		);
		await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(0, "Thêm ngày nghỉ thành công");
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, NgayNghiUpdateRequestDTO dto)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy ngày nghỉ");
		entity.Update(dto.Ngay, dto.LyDo);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
	}
	public async Task<ApiResponse<ImportResult>> ImportExcelAsync(Stream fileStream)
	{
		var result = new ImportResult();
		using var package = new ExcelPackage(fileStream);
		var ws = package.Workbook.Worksheets.First();
		var rowCount = ws.Dimension.Rows;
		for (int row = 2; row <= rowCount; row++)
		{
			try
			{
				var nhanVienIDText = ws.Cells[row, 1].Text;
				var ngayText = ws.Cells[row, 2].Text;
				var lyDo = ws.Cells[row, 3].Text;
				if (!int.TryParse(nhanVienIDText, out var nhanVienID))
					throw new Exception("NhanVienID không hợp lệ");
				if (!DateTime.TryParse(ngayText, out var ngay))
					throw new Exception("Ngày không hợp lệ");
				if (await _repo.IsNgayNghiAsync(nhanVienID, ngay))
					throw new Exception("Ngày nghỉ đã tồn tại");
				var entity = new NgayNghiNhanVien(
					nhanVienID,
					ngay,
					string.IsNullOrWhiteSpace(lyDo) ? null : lyDo
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
	public async Task<ApiResponse<NgayNghiReadModel>> GetDetailAsync(int id)
	{
		var data = await _repo.GetDetailAsync(id);
		if (data == null)
			return ApiResponse<NgayNghiReadModel>.Fail("Không tìm thấy dữ liệu");
		return ApiResponse<NgayNghiReadModel>.SuccessResponse(data);
	}
	public async Task<ApiResponse<List<NgayNghiReadModel>>> GetByNhanVienAsync(int nhanVienID)
	{
		if (nhanVienID <= 0)
			return ApiResponse<List<NgayNghiReadModel>>.Fail("Nhân viên không hợp lệ");
		var list = await _repo.GetByNhanVienIdAsync(nhanVienID);
		return ApiResponse<List<NgayNghiReadModel>>.SuccessResponse(list);
	}
	public async Task<ApiResponse<List<NgayNghiReadModel>>> GetByMonthAsync(int thang, int nam)
	{
		if (thang < 1 || thang > 12)
			return ApiResponse<List<NgayNghiReadModel>>.Fail("Tháng không hợp lệ");
		if (nam < 2000)
			return ApiResponse<List<NgayNghiReadModel>>.Fail("Năm không hợp lệ");
		var list = await _repo.GetByMonthAsync(thang, nam);
		return ApiResponse<List<NgayNghiReadModel>>.SuccessResponse(list);
	}
}