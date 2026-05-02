using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
namespace Application.Services;
public class ChucVuService
{
	private readonly IChucVuRepository _repo;
	public ChucVuService(IChucVuRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<bool>> AddAsync(ChucVuRequest dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			var entity = new ChucVu(
				dto.TenChucVu.Trim(),
				dto.MoTa,
				dto.TrangThai
			);
			int row = await _repo.AddAsync(entity);
			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo chức vụ thất bại");
			return ApiResponse<bool>.SuccessResponse(true, "Tạo chức vụ thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên chức vụ đã tồn tại");
		}
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ChucVuRequest dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			var entity = await _repo.GetByIdAsync(id);
			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy chức vụ");
			entity.CapNhat(dto.TenChucVu, dto.MoTa, dto.TrangThai);
			int row = await _repo.UpdateAsync(entity);
			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật chức vụ thất bại");
			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật chức vụ thành công");
		}
		catch(ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên chức vụ đã tồn tại");
		}
	}
	public async Task<ApiResponse<ChucVuReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<ChucVuReadModel>.Fail("ID không hợp lệ");
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<ChucVuReadModel>.Fail("Chức vụ không tồn tại");
		return ApiResponse<ChucVuReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ChucVuListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.GetPagedAsync(page, size);
		var result = new PagedResult<ChucVuListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ChucVuListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ChucVuListReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ChucVuListReadModel>>
				.Fail("Từ khóa không hợp lệ");
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);
		var result = new PagedResult<ChucVuListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ChucVuListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<string?>> GetByNhanVienIdAsync(int nhanVienId)
	{
		if (nhanVienId <= 0)
			return ApiResponse<string?>.Fail("ID nhân viên không hợp lệ");
		var result = await _repo.GetByNhanVienIdAsync(nhanVienId);
		return ApiResponse<string?>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}
	public async Task<ApiResponse<ExcelImportResult<ChucVuImport>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<ChucVuImport>(stream, sheet, (item, row) =>	
		{
			var errors = new List<string>();
			if (string.IsNullOrWhiteSpace(item.TenChucVu))
				errors.Add($"Dòng {row}: Tên đang rỗng");
			if (string.IsNullOrWhiteSpace(item.MoTa))
				errors.Add($"Dòng {row}: Mô tả đang rỗng");
			if (item.TrangThai != "Hoạt động" && item.TrangThai != "Vô hiệu")
				errors.Add($"Dòng {row}: Trạng thái không hợp lệ");
			return errors;
		});
	}
	public async Task<ApiResponse<bool>> Import(List<ChucVuImport> list)
	{
		var entities = list.Select(x =>
			new ChucVu(x.TenChucVu, x.MoTa, x.TrangThai)
		).ToList();
		await _repo.BulkInsertAsync(entities);
		return ApiResponse<bool>.SuccessResponse(true, "Nhập dữ liệu từ excel thành công!");
	}
}