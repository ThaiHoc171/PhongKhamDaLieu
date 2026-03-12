using Application.DTOs;
using Application.Interfaces;
using Application.Common;
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
		var pkb = await _repo.GetByIdAsync(id);
		if (pkb == null)
			return ApiResponse<PhienKhamBenhResponseDTO>.Fail("Phiên khám không tồn tại");
		var result = new PhienKhamBenhResponseDTO
		{
			Id = pkb.PhienKham_BenhID,
			PhienKhamID = pkb.PhienKhamID,
			LoaiBenhID = pkb.LoaiBenhID,
			LoaiChanDoan = LoaiChanDoanEnumExtensions.ToDbValue(pkb.LoaiChanDoan),
			GhiChu = pkb.GhiChu
		};
		return ApiResponse<PhienKhamBenhResponseDTO>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<PhienKhamBenhReadModel>>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(phienKhamID);
		if (phienKham == null)
			return ApiResponse<List<PhienKhamBenhReadModel>>.Fail("Phiên khám không tồn tại");
		var list = await _repo.GetByPhienKhamIdAsync(phienKhamID);
		return ApiResponse<List<PhienKhamBenhReadModel>>.SuccessResponse(list);
	}
	public async Task<ApiResponse<object>> ThemMoiAsync(PhienKhamBenhRequestDTO dto)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID);
		if (phienKham == null)
			return ApiResponse<object>.Fail("Phiên khám không tồn tại");
		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			return ApiResponse<object>.Fail("Không thể thêm chẩn đoán khi phiên khám đã kết thúc");
		var loaiBenh = await _loaiBenhRepo.GetByIdAsync(dto.LoaiBenhID);
		if (loaiBenh == null)
			return ApiResponse<object>.Fail("Loại bệnh không tồn tại");
		var loaiChanDoanEnum = LoaiChanDoanEnumExtensions.ToEnum(dto.LoaiChanDoan);
		var daTonTaiChanDoanChinh = await _repo.PrimaryExistsAsync(dto.PhienKhamID);
		if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanChinh && daTonTaiChanDoanChinh)
			return ApiResponse<object>.Fail("Mỗi phiên khám chỉ được có một chẩn đoán chính");
		if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanPhatSinh && !daTonTaiChanDoanChinh)
			return ApiResponse<object>.Fail("Phải có chẩn đoán chính trước khi thêm chẩn đoán phụ");
		var entity = new PhienKhamBenh(
			dto.PhienKhamID,
			dto.LoaiBenhID,
			loaiChanDoanEnum,
			dto.GhiChu
		);
		await _repo.AddAsync(entity);
		return ApiResponse<object>.SuccessResponse(null, "Thêm chẩn đoán thành công");
	}
	public async Task<ApiResponse<object>> CapNhatAsync(int pkbId, PhienKhamBenhRequestDTO dto)
	{
		var pkb = await _repo.GetByIdAsync(pkbId);
		if (pkb == null)
			return ApiResponse<object>.Fail("Chẩn đoán không tồn tại");
		var phienKham = await _phienKhamRepo.GetByIdAsync(pkb.PhienKhamID);
		if (phienKham == null)
			return ApiResponse<object>.Fail("Phiên khám không tồn tại");
		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			return ApiResponse<object>.Fail("Không thể cập nhật chẩn đoán khi phiên khám đã kết thúc");
		var loaiBenh = await _loaiBenhRepo.GetByIdAsync(dto.LoaiBenhID);
		if (loaiBenh == null)
			return ApiResponse<object>.Fail("Loại bệnh không tồn tại");
		var loaiChanDoanEnum = LoaiChanDoanEnumExtensions.ToEnum(dto.LoaiChanDoan);
		if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanChinh && pkb.LoaiChanDoan != LoaiChanDoanEnum.ChanDoanChinh)
		{
			var daTonTai = await _repo.PrimaryExistsAsync(pkb.PhienKhamID);
			if (daTonTai)
				return ApiResponse<object>.Fail("Mỗi phiên khám chỉ được có một chẩn đoán chính");
		}
		pkb.CapNhat(dto.LoaiBenhID, loaiChanDoanEnum, dto.GhiChu);
		await _repo.UpdateAsync(pkb);
		return ApiResponse<object>.SuccessResponse(null, "Cập nhật chẩn đoán thành công");
	}
}