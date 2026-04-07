using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class PhienKhamBenhService
{
	private readonly IPhienKhamBenhRepository _repo;
	private readonly IPhienKhamRepository _phienKhamRepo;
	private readonly ILoaiBenhRepository _loaiBenhRepo;

	public PhienKhamBenhService(
		IPhienKhamBenhRepository repo,
		IPhienKhamRepository phienKhamRepo,
		ILoaiBenhRepository loaiBenhRepo)
	{
		_repo = repo;
		_phienKhamRepo = phienKhamRepo;
		_loaiBenhRepo = loaiBenhRepo;
	}

	public async Task<ApiResponse<PhienKhamBenhResponseDTO>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<PhienKhamBenhResponseDTO>.Fail("ID không hợp lệ");

		var pkb = await _repo.GetByIdAsync(id);

		if (pkb == null)
			return ApiResponse<PhienKhamBenhResponseDTO>.Fail("Phiên khám không tồn tại");

		var result = new PhienKhamBenhResponseDTO
		{
			Id = pkb.PhienKham_BenhID,
			PhienKhamID = pkb.PhienKhamID,
			LoaiBenhID = pkb.LoaiBenhID,
			LoaiChanDoan = LoaiChanDoanExtensions.ToDbValue(pkb.LoaiChanDoan),
			GhiChu = pkb.GhiChu
		};

		return ApiResponse<PhienKhamBenhResponseDTO>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<PhienKhamBenhReadModel>>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		if (phienKhamID <= 0)
			return ApiResponse<List<PhienKhamBenhReadModel>>.Fail("ID phiên khám không hợp lệ");

		var phienKham = await _phienKhamRepo.GetByIdAsync(phienKhamID);

		if (phienKham == null)
			return ApiResponse<List<PhienKhamBenhReadModel>>.Fail("Phiên khám không tồn tại");

		var list = await _repo.GetByPhienKhamIdAsync(phienKhamID);

		return ApiResponse<List<PhienKhamBenhReadModel>>.SuccessResponse(list);
	}

	public async Task<ApiResponse<bool>> AddAsync(PhienKhamBenhRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID);

			if (phienKham == null)
				return ApiResponse<bool>.Fail("Phiên khám không tồn tại");

			if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
				return ApiResponse<bool>.Fail("Phiên khám đã kết thúc");

			var loaiBenh = await _loaiBenhRepo.GetByIdAsync(dto.LoaiBenhID);

			if (loaiBenh == null)
				return ApiResponse<bool>.Fail("Loại bệnh không tồn tại");

			var loaiChanDoanEnum = LoaiChanDoanExtensions.FromDb(dto.LoaiChanDoan);

			var daTonTaiChanDoanChinh =
				await _repo.PrimaryExistsAsync(dto.PhienKhamID);

			if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanChinh && daTonTaiChanDoanChinh)
				return ApiResponse<bool>.Fail("Đã có chẩn đoán chính");

			if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanPhatSinh && !daTonTaiChanDoanChinh)
				return ApiResponse<bool>.Fail("Chưa tồn tại chẩn đoán chính");

			var entity = new PhienKhamBenh(
				dto.PhienKhamID,
				dto.LoaiBenhID,
				dto.LoaiChanDoan,
				dto.GhiChu
			);

			await _repo.AddAsync(entity);

			return ApiResponse<bool>.SuccessResponse(true, "Thêm chẩn đoán thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int pkbId, PhienKhamBenhRequestDTO dto)
	{
		try
		{
			if (pkbId <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var pkb = await _repo.GetByIdAsync(pkbId);

			if (pkb == null)
				return ApiResponse<bool>.Fail("Chẩn đoán không tồn tại");

			var phienKham = await _phienKhamRepo.GetByIdAsync(pkb.PhienKhamID);

			if (phienKham == null)
				return ApiResponse<bool>.Fail("Phiên khám không tồn tại");

			if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
				return ApiResponse<bool>.Fail("Phiên khám đã kết thúc");

			var loaiBenh = await _loaiBenhRepo.GetByIdAsync(dto.LoaiBenhID);

			if (loaiBenh == null)
				return ApiResponse<bool>.Fail("Loại bệnh không tồn tại");

			var loaiChanDoanEnum = LoaiChanDoanExtensions.FromDb(dto.LoaiChanDoan);

			if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanChinh
				&& pkb.LoaiChanDoan != LoaiChanDoanEnum.ChanDoanChinh)
			{
				var daTonTai = await _repo.PrimaryExistsAsync(pkb.PhienKhamID);

				if (daTonTai)
					return ApiResponse<bool>.Fail("Đã có chẩn đoán chính");
			}

			pkb.CapNhat(dto.LoaiBenhID, dto.LoaiChanDoan, dto.GhiChu);

			await _repo.UpdateAsync(pkb);

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật chẩn đoán thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
}