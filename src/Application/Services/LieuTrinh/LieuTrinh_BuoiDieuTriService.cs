using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class BuoiDieuTriService
{
	private readonly IBuoiDieuTriRepository _repo;
	public BuoiDieuTriService(IBuoiDieuTriRepository repo)
	{
		_repo = repo;
	}
	//public async Task<ApiResponse<int>> CreateAsync(BuoiDieuTriRequestDTO dto)
	//{
	//	if (dto.LieuTrinhID <= 0)
	//		return ApiResponse<int>.Fail("Liệu trình không hợp lệ");
	//	if (dto.CaKhamID <= 0)
	//		return ApiResponse<int>.Fail("Ca khám không hợp lệ");
	//	if (await _repo.ExistsByCaKhamAsync(dto.CaKhamID))
	//		return ApiResponse<int>.Fail("Ca khám này đã có buổi điều trị");
	//	var maxSoBuoi = await _repo.GetMaxSoBuoiAsync(dto.LieuTrinhID);
	//	//if (dto.SoBuoi <= maxSoBuoi)
	//	//	return ApiResponse<int>.Fail("Số buổi phải lớn hơn buổi hiện tại");
	//	//// validate lịch dự kiến dựa trên buổi trước
	//	//var last = await _repo.GetLastAsync(dto.LieuTrinhID);
	//	//if (last != null && dto.NgayDuKien.HasValue && last.NgayThucHien.HasValue)
	//	//{
	//	//	if (dto.NgayDuKien <= last.NgayThucHien)
	//	//		return ApiResponse<int>.Fail("Ngày dự kiến phải sau buổi trước");
	//	//}
	//	//BuoiDieuTri entity;
	//	//try
	//	//{
	//	//	entity = new BuoiDieuTri(
	//	//		dto.LieuTrinhID,
	//	//		dto.CaKhamID,
	//	//		dto.SoBuoi,
	//	//		dto.NgayDuKien);
	//	//}
	//	catch (ArgumentException ex)
	//	{
	//		return ApiResponse<int>.Fail(ex.Message);
	//	}
	//	var id = await _repo.AddAsync(entity);
	//	return ApiResponse<int>.SuccessResponse(id);
	//}
	public async Task<ApiResponse<bool>> StartAsync(int id, int nhanVienID)
	{
		if (nhanVienID <= 0)
			return ApiResponse<bool>.Fail("Nhân viên không hợp lệ");
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
		try
		{
			entity.BatDauDieuTri(nhanVienID);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> CompleteAsync(int id, BuoiDieuTriUpdateDTO dto)
	{
		if (dto.NgayThucHien == null)
			return ApiResponse<bool>.Fail("Ngày thực hiện không hợp lệ");
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
		try
		{
			entity.HoanThanh(dto.NgayThucHien.Value, dto.GhiChu);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> CancleAsync(int id, string? ghiChu)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
		try
		{
			entity.Huy(ghiChu);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> UpdateImageAsync(int id, string? hinhAnhJson)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
		entity.CapNhatHinhAnh(hinhAnhJson);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<BuoiDieuTriReadModel>> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<BuoiDieuTriReadModel>.Fail("Buổi điều trị không tồn tại");
		return ApiResponse<BuoiDieuTriReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<BuoiDieuTriListReadModel>>> GetByLieuTrinhAsync(int lieuTrinhID)
	{
		if (lieuTrinhID <= 0)
			return ApiResponse<List<BuoiDieuTriListReadModel>>.Fail("Liệu trình không hợp lệ");
		var result = await _repo.GetByLieuTrinhAsync(lieuTrinhID);
		return ApiResponse<List<BuoiDieuTriListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<int>> CountCompleteAsync(int lieuTrinhID)
	{
		if (lieuTrinhID <= 0)
			return ApiResponse<int>.Fail("Liệu trình không hợp lệ");
		var count = await _repo.CountHoanThanhAsync(lieuTrinhID);
		return ApiResponse<int>.SuccessResponse(count);
	}
}