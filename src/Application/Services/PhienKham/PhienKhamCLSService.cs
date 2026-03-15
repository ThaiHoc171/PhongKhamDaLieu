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
	public async Task<ApiResponse<List<PhienKhamClsListReadModel>>> GetByPhienKhamAsync(int phienKhamID)
	{
		var result = await _repo.GetByPhienKhamAsync(phienKhamID);
		return ApiResponse<List<PhienKhamClsListReadModel>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<PhienKhamClsReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<PhienKhamClsReadModel>.Fail("CLS không tồn tại");
		return ApiResponse<PhienKhamClsReadModel>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<PhienKhamClsListReadModel>>> GetListAsync()
	{
		var result = await _repo.GetListAsync();
		return ApiResponse<List<PhienKhamClsListReadModel>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> AddAsync(PkClsRequestDTO dto)
	{
		var entity = new PhienKhamCLS(
			dto.PhienKhamID,
			dto.CLSID,
			dto.NhanVienChiDinhID,
			dto.GhiChu
		);
		await _repo.AddAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> AcceptAsync(int phienKhamCLSID, AcceptClsDTO dto)
	{
		var entity = await _repo.GetByIdAsync(phienKhamCLSID);
		if (entity == null)
			return ApiResponse<bool>.Fail("CLS không tồn tại");
		try
		{
			entity.Accept(dto.NhanVienThucHienID);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> CompleteAsync(int phienKhamCLSID, PkClsUpdateRequestDTO dto)
	{
		var entity = await _repo.GetByIdAsync(phienKhamCLSID);
		if (entity == null)
			return ApiResponse<bool>.Fail("CLS không tồn tại");
		try
		{
			entity.Complete(dto.KetQua, dto.FileDinhKem, dto.GhiChu);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> CancelAsync(int phienKhamCLSID)
	{
		var entity = await _repo.GetByIdAsync(phienKhamCLSID);
		if (entity == null)
			return ApiResponse<bool>.Fail("CLS không tồn tại");
		try
		{
			entity.Cancel();
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
}