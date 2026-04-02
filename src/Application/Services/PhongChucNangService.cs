using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
 namespace Application.Services;
 public class PhongChucNangService
{
	private readonly IPhongChucNangRepository _repo;
 	public PhongChucNangService(IPhongChucNangRepository repo)
	{
		_repo = repo;
	}
 	public async Task<ApiResponse<bool>> AddAsync(PhongChucNangRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
 			var entity = new PhongChucNang(
				dto.TenPhong.Trim(),
				dto.MoTa
			);
 			int row = await _repo.AddAsync(entity);
 			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo phòng chức năng thất bại");
 			return ApiResponse<bool>.SuccessResponse(true, "Tạo phòng chức năng thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên phòng đã tồn tại");
		}
	}
 	public async Task<ApiResponse<bool>> UpdateAsync(int id, PhongChucNangRequestDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");
 			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
 			var entity = await _repo.GetByIdAsync(id);
 			if (entity == null)
				return ApiResponse<bool>.Fail("Phòng chức năng không tồn tại");
 			entity.CapNhat(
				dto.TenPhong,
				dto.MoTa
			);
 			int row = await _repo.UpdateAsync(entity);
 			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật phòng chức năng thất bại");
 			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật phòng chức năng thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Tên phòng đã tồn tại");
		}
	}
 	public async Task<ApiResponse<bool>> ChangeStatusAsync(int id, string trangThaiMoi)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");
 			var entity = await _repo.GetByIdAsync(id);
 			if (entity == null)
				return ApiResponse<bool>.Fail("Phòng chức năng không tồn tại");
			TinhTrang status = TinhTrangExtensions.FromDb(trangThaiMoi);
			entity.ChuyenTrangThai(status);
 			int row = await _repo.UpdateAsync(entity);
 			if (row == 0)
				return ApiResponse<bool>.Fail("Chuyển trạng thái thất bại");
 			return ApiResponse<bool>.SuccessResponse(true, "Chuyển trạng thái thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
 	public async Task<ApiResponse<PhongChucNangReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<PhongChucNangReadModel>.Fail("ID không hợp lệ");
 		var result = await _repo.GetDetailAsync(id);
 		if (result == null)
			return ApiResponse<PhongChucNangReadModel>.Fail("Phòng chức năng không tồn tại");
 		return ApiResponse<PhongChucNangReadModel>.SuccessResponse(result);
	}
 	public async Task<ApiResponse<PagedResult<PhongChucNangReadListModel>>> GetPagedAsync(
		int page,
		int size,
		string? trangThai)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
 		var (items, total) = await _repo.GetPagedAsync(page, size, trangThai);
 		var result = new PagedResult<PhongChucNangReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
 		return ApiResponse<PagedResult<PhongChucNangReadListModel>>
			.SuccessResponse(result);
	}
 	public async Task<ApiResponse<PagedResult<PhongChucNangReadListModel>>> SearchAsync(
		string? keyword,
		int page,
		int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<PhongChucNangReadListModel>>
				.Fail("Từ khóa không hợp lệ");
 		if (page < 1) page = 1;
		if (size <= 0) size = 10;
 		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);
 		var result = new PagedResult<PhongChucNangReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
 		return ApiResponse<PagedResult<PhongChucNangReadListModel>>
			.SuccessResponse(result);
	}
 	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();
 		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}
}