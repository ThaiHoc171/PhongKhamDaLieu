using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using OfficeOpenXml;
namespace Application.Services;
public class PhongChucNangService
{
	private readonly IPhongChucNangRepository _repo;
	public PhongChucNangService(IPhongChucNangRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<int>> TaoMoiAsync(PhongChucNangRequestDTO dto)
	{
		var validate = ValidateCreate(dto);
		if (!validate.Success)
			return ApiResponse<int>.Fail(validate.Message);
		try
		{
			var entity = new PhongChucNang(
				dto.TenPhong,
				dto.MoTa
			);
			var id = await _repo.AddAsync(entity);
			return ApiResponse<int>.SuccessResponse(id);
		}
		catch (Exception ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> CapNhatAsync(int id, PhongChucNangRequestDTO dto)
	{
		var phong = await _repo.GetByIdAsync(id);
		if (phong == null)
			return ApiResponse<bool>.Fail("Phòng chức năng không tồn tại");
		var validate = ValidateUpdate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);
		try
		{
			phong.CapNhat(
				dto.TenPhong,
				dto.MoTa
			);
			await _repo.UpdateAsync(phong);
			return ApiResponse<bool>.SuccessResponse(true);
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> ChuyenTrangThaiAsync(int id, TinhTrang trangThaiMoi)
	{
		var phong = await _repo.GetByIdAsync(id);
		if (phong == null)
			return ApiResponse<bool>.Fail("Phòng chức năng không tồn tại");
		try
		{
			phong.ChuyenTrangThai(trangThaiMoi);
			await _repo.UpdateAsync(phong);
			return ApiResponse<bool>.SuccessResponse(true);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<PhongChucNangReadModel>> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<PhongChucNangReadModel>.Fail("Phòng chức năng không tồn tại");
		return ApiResponse<PhongChucNangReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<PhongChucNangListReadModel>>> GetPagedAsync(
		int page,
		int size,
		string? trangThai)
	{
		var (items, total) = await _repo.GetPagedAsync(page, size, trangThai);
		return ApiResponse<PagedResult<PhongChucNangListReadModel>>.SuccessResponse(
			new PagedResult<PhongChucNangListReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<PagedResult<PhongChucNangListReadModel>>> SearchAsync(
		string? keyword,
		int page,
		int size)
	{
		var (items, total) = await _repo.SearchPagedAsync(keyword, page, size);
		return ApiResponse<PagedResult<PhongChucNangListReadModel>>.SuccessResponse(
			new PagedResult<PhongChucNangListReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var list = await _repo.GetComboboxAsync();
		var result = list.Select(x => new NameResponseDTO
		{
			Id = x.Id,
			Name = x.Ten
		}).ToList();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
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
				var tenPhong = sheet.Cells[row, 1].GetValue<string>();
				var moTa = sheet.Cells[row, 2].GetValue<string>();
				var dto = new PhongChucNangRequestDTO
				{
					TenPhong = tenPhong ?? "",
					MoTa = moTa
				};
				var validate = ValidateCreate(dto);
				if (!validate.Success)
				{
					result.Errors.Add(new ImportError
					{
						Row = row,
						Message = validate.Message
					});
					continue;
				}
				var entity = new PhongChucNang(
					dto.TenPhong,
					dto.MoTa
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
	private ApiResponse<bool> ValidateCreate(PhongChucNangRequestDTO dto)
	{
		if (string.IsNullOrWhiteSpace(dto.TenPhong))
			return ApiResponse<bool>.Fail("Tên phòng không được để trống");
		if (dto.TenPhong.Length > 200)
			return ApiResponse<bool>.Fail("Tên phòng quá dài");
		return ApiResponse<bool>.SuccessResponse(true);
	}
	private ApiResponse<bool> ValidateUpdate(PhongChucNangRequestDTO dto)
	{
		if (string.IsNullOrWhiteSpace(dto.TenPhong))
			return ApiResponse<bool>.Fail("Tên phòng không được để trống");
		return ApiResponse<bool>.SuccessResponse(true);
	}
}