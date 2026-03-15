using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class BenhNhanService
{
	private readonly IBenhNhanRepository _benhNhanRepo;
	private readonly IThongTinCaNhanRepository _thongTinRepo;
	public BenhNhanService(
		IBenhNhanRepository benhNhanRepo,
		IThongTinCaNhanRepository thongTinRepo)
	{
		_benhNhanRepo = benhNhanRepo;
		_thongTinRepo = thongTinRepo;
	}
	public async Task<ApiResponse<int>> AddAsync(BenhNhanRequestDTO dto)
	{
		int thongTinID;
		if (string.IsNullOrWhiteSpace(dto.HoTen))
			return ApiResponse<int>.Fail("Phải cung cấp họ tên");
		if (string.IsNullOrWhiteSpace(dto.SDT))
			return ApiResponse<int>.Fail("Phải cung cấp số điện thoại");
		if (string.IsNullOrWhiteSpace(dto.DiaChi))
			return ApiResponse<int>.Fail("Phải cung địa chỉ");
		var thongTin = new ThongTinCaNhan(
			taiKhoanID: dto.TaiKhoanID,
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: GioiTinhExtensions.ParseGioiTinhOrDefault(dto.GioiTinh),
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar,
			loai: LoaiThongTinEnum.BenhNhan
		);
		thongTinID = await _thongTinRepo.AddAsync(thongTin);
		var exists = await _benhNhanRepo.ExistsByThongTinIdAsync(thongTinID);
		if (exists)
			return ApiResponse<int>.Fail("Thông tin cá nhân này đã là bệnh nhân");
		var entity = new BenhNhan(
			thongTinID: thongTinID,
			ghiChu: dto.GhiChu
		);
		var id = await _benhNhanRepo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id);
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, BenhNhanUpdateRequestDTO dto)
	{
		var entity = await _benhNhanRepo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Bệnh nhân không tồn tại");
		entity.CapNhatGhiChu(dto.GhiChu);
		await _benhNhanRepo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<BenhNhanDetailReadModel>> GetDetailAsync(int id)
	{
		var result = await _benhNhanRepo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<BenhNhanDetailReadModel>.Fail("Bệnh nhân không tồn tại");
		return ApiResponse<BenhNhanDetailReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<BenhNhanReadModel>>> GetPagedAsync(int pageNumber, int pageSize)
	{
		var (items, totalCount) =
			await _benhNhanRepo.GetPagedAsync(pageNumber, pageSize);
		return ApiResponse<PagedResult<BenhNhanReadModel>>.SuccessResponse(
			new PagedResult<BenhNhanReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}
	public async Task<ApiResponse<PagedResult<BenhNhanReadModel>>> SearchAsync(	string? keyword, int pageNumber, int pageSize)
	{
		var (items, totalCount) =
			await _benhNhanRepo.SearchAsync(keyword, pageNumber, pageSize);
		return ApiResponse<PagedResult<BenhNhanReadModel>>.SuccessResponse(
			new PagedResult<BenhNhanReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var result = await _benhNhanRepo.GetComboboxAsync();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
	}
}