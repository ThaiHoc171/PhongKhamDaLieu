using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class HoSoBenhAnService
{
	private readonly IHoSoBenhAnRepository _repo;

	public HoSoBenhAnService(IHoSoBenhAnRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<bool>> AddAsync(HoSoBenhAnRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			if (dto.BenhNhanID <= 0)
				return ApiResponse<bool>.Fail("Bệnh nhân không hợp lệ");

			var tonTai = await _repo.GetByBenhNhanIdAsync(dto.BenhNhanID);

			if (tonTai != null)
				return ApiResponse<bool>.Fail("Bệnh nhân đã có hồ sơ bệnh án");

			var entity = new HoSoBenhAn(
				dto.BenhNhanID,
				dto.BenhNen,
				dto.DiUng,
				dto.TienSuBenh,
				dto.TienSuGiaDinh,
				dto.ThoiQuenSong,
				dto.ThongTinKhac
			);

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo hồ sơ bệnh án thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo hồ sơ bệnh án thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, HoSoBenhAnUpdateDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Hồ sơ bệnh án không tồn tại");

			entity.CapNhatThongTin(
				dto.BenhNen,
				dto.DiUng,
				dto.TienSuBenh,
				dto.TienSuGiaDinh,
				dto.ThoiQuenSong,
				dto.ThongTinKhac
			);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật hồ sơ bệnh án thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật hồ sơ bệnh án thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<HoSoBenhAnReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<HoSoBenhAnReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<HoSoBenhAnReadModel>.Fail("Hồ sơ bệnh án không tồn tại");

		return ApiResponse<HoSoBenhAnReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<HoSoBenhAnReadModel?>> GetByBenhNhanIdAsync(int benhNhanId)
	{
		if (benhNhanId <= 0)
			return ApiResponse<HoSoBenhAnReadModel?>.Fail("ID bệnh nhân không hợp lệ");

		var result = await _repo.GetByBenhNhanIdAsync(benhNhanId);

		return ApiResponse<HoSoBenhAnReadModel?>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<HoSoBenhAnListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<HoSoBenhAnListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<HoSoBenhAnListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<HoSoBenhAnListReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<HoSoBenhAnListReadModel>>
				.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<HoSoBenhAnListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<HoSoBenhAnListReadModel>>
			.SuccessResponse(result);
	}
}