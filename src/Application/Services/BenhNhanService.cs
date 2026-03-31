using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using static Amazon.S3.Util.S3EventNotification;

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
		try
		{
			if (dto == null)
				return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");

			var thongTin = await _thongTinRepo.GetByEmailOrSDTAsync(dto.EmailLienHe, dto.SDT);
			int thongTinID;

			if (thongTin != null)
			{
				thongTin.CapNhat(
					hoTen: dto.HoTen,
					ngaySinh: dto.NgaySinh,
					gioiTinh: GioiTinhExtensions.FromDbValue(dto.GioiTinh),
					sdt: dto.SDT,
					emailLienHe: dto.EmailLienHe ?? "",
					diaChi: dto.DiaChi,
					avatar: dto.Avatar,
					loai: LoaiThongTinEnum.BenhNhan
				);

				await _thongTinRepo.UpdateAsync(thongTin);
				thongTinID = thongTin.ThongTinID;
			}
			else
			{
				var newThongTin = new ThongTinCaNhan(
					taiKhoanID: dto.TaiKhoanID,
					hoTen: dto.HoTen,
					ngaySinh: dto.NgaySinh,
					gioiTinh: GioiTinhExtensions.FromDbValue(dto.GioiTinh),
					sdt: dto.SDT,
					emailLienHe: dto.EmailLienHe ?? "",
					diaChi: dto.DiaChi,
					avatar: dto.Avatar,
					loai: LoaiThongTinEnum.BenhNhan
				);

				thongTinID = await _thongTinRepo.AddAsync(newThongTin);
			}

			var exists = await _benhNhanRepo.ExistsByThongTinIdAsync(thongTinID);
			if (exists)
				return ApiResponse<int>.Fail("Bệnh nhân đã tồn tại");

			var entity = new BenhNhan(thongTinID, dto.GhiChu);

			var id = await _benhNhanRepo.AddAsync(entity);

			if (id <= 0)
				return ApiResponse<int>.Fail("Tạo bệnh nhân thất bại");

			return ApiResponse<int>.SuccessResponse(id, "Tạo bệnh nhân thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<int>.Fail("Thông tin bệnh nhân đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, BenhNhanUpdateRequestDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			var benhnhan = await _benhNhanRepo.GetByIdAsync(id);
			if (benhnhan == null)
				return ApiResponse<bool>.Fail("Bệnh nhân không tồn tại");
			var thongtin = await _thongTinRepo.GetByIdAsync(benhnhan.ThongTinID);
			if (thongtin == null)
				return ApiResponse<bool>.Fail("Thông tin cá nhân không tồn tại");
			thongtin.CapNhat(
				dto.HoTen.Trim(),
				dto.NgaySinh,
				GioiTinhExtensions.FromDbValue(dto.GioiTinh),
				dto.SDT,
				dto.EmailLienHe,
				dto.DiaChi,
				dto.Avatar,
				LoaiThongTinEnum.BenhNhan
			);
			benhnhan.CapNhat(dto.GhiChu);
			int thongTinRow = await _thongTinRepo.UpdateAsync(thongtin);
			int benhNhanRow = await _benhNhanRepo.UpdateAsync(benhnhan);

			if (thongTinRow == 0 || benhNhanRow == 0)
				return ApiResponse<bool>.Fail("Cập nhật bệnh nhân thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<BenhNhanReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<BenhNhanReadModel>.Fail("ID không hợp lệ");

		var result = await _benhNhanRepo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<BenhNhanReadModel>.Fail("Bệnh nhân không tồn tại");

		return ApiResponse<BenhNhanReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<BenhNhanReadListModel>>> GetPagedAsync(int pageNumber, int pageSize)
	{
		if (pageNumber < 1) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (items, totalCount) =
			await _benhNhanRepo.GetPagedAsync(pageNumber, pageSize);

		return ApiResponse<PagedResult<BenhNhanReadListModel>>.SuccessResponse(
			new PagedResult<BenhNhanReadListModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}

	public async Task<ApiResponse<PagedResult<BenhNhanReadListModel>>> SearchAsync(string keyword, int pageNumber, int pageSize)
	{
		if (pageNumber < 1) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (items, totalCount) =
			await _benhNhanRepo.SearchAsync(keyword, pageNumber, pageSize);

		return ApiResponse<PagedResult<BenhNhanReadListModel>>.SuccessResponse(
			new PagedResult<BenhNhanReadListModel>
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