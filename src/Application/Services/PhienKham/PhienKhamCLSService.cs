using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PhienKhamCLSService
{
	private readonly IPhienKhamCLSRepository _repo;

	public PhienKhamCLSService(IPhienKhamCLSRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<List<PhienKhamClsReadListModel>>> GetByPhienKhamAsync(int phienKhamID)
	{
		if (phienKhamID <= 0)
			return ApiResponse<List<PhienKhamClsReadListModel>>.Fail("ID phiên khám không hợp lệ");
		var result = await _repo.GetByPhienKhamAsync(phienKhamID);

		return ApiResponse<List<PhienKhamClsReadListModel>> .SuccessResponse(result);
	}

	public async Task<ApiResponse<PhienKhamClsReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<PhienKhamClsReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<PhienKhamClsReadModel>.Fail("CLS không tồn tại");

		return ApiResponse<PhienKhamClsReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<PhienKhamClsReadListModel>>> GetListAsync()
	{
		var result = await _repo.GetListAsync();
		return ApiResponse<List<PhienKhamClsReadListModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<bool>> AddAsync(PkClsRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = new PhienKhamCLS(
				dto.PhienKhamID,
				dto.CLSID,
				dto.NhanVienChiDinhID,
				dto.GhiChu
			);

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo chỉ định CLS thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo chỉ định CLS thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> AcceptAsync(int phienKhamCLSID, AcceptClsDTO dto)
	{
		try
		{
			if (phienKhamCLSID <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(phienKhamCLSID);

			if (entity == null)
				return ApiResponse<bool>.Fail("CLS không tồn tại");

			entity.Accept(dto.NhanVienThucHienID);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Nhận CLS thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Nhận CLS thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> CompleteAsync(int phienKhamCLSID, PkClsUpdateRequestDTO dto)
	{
		try
		{
			if (phienKhamCLSID <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(phienKhamCLSID);

			if (entity == null)
				return ApiResponse<bool>.Fail("CLS không tồn tại");

			entity.Complete(dto.KetQua, dto.FileDinhKem, dto.GhiChu);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Hoàn thành CLS thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Hoàn thành CLS thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> CancelAsync(int phienKhamCLSID)
	{
		try
		{
			if (phienKhamCLSID <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var entity = await _repo.GetByIdAsync(phienKhamCLSID);

			if (entity == null)
				return ApiResponse<bool>.Fail("CLS không tồn tại");

			entity.Cancel();

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Hủy CLS thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Hủy CLS thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
}