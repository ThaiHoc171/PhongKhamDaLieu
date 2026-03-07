using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services;

public class BenhNhanService
{
	private readonly IBenhNhanRepository _benhNhanRepo;
	private readonly IThongTinCaNhanRepository _thongTinRepo;

	public BenhNhanService(IBenhNhanRepository benhNhanRepo, IThongTinCaNhanRepository thongTinRepo)
	{
		_benhNhanRepo = benhNhanRepo;
		_thongTinRepo = thongTinRepo;
	}

	public async Task<int> ThemBenhNhanAsync(BenhNhanRequestDTO dto)
	{
		int thongTinID;

		if (dto.ThongTinID.HasValue)
		{
			// Bệnh nhân đã có thông tin cá nhân
			var existing = await _thongTinRepo.GetByIdAsync(dto.ThongTinID.Value);
			if (existing == null)
				throw new Exception("Thông tin cá nhân không tồn tại");

			thongTinID = existing.ThongTinID;
		}
		else
		{
			// Bệnh nhân mới, phải cung cấp ít nhất tên, SDT, Email
			if (string.IsNullOrWhiteSpace(dto.HoTen))
				throw new Exception("Phải cung cấp họ tên cho bệnh nhân mới");
			if (string.IsNullOrWhiteSpace(dto.SDT))
				throw new Exception("Phải cung cấp số điện thoại cho bệnh nhân mới");

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
		}
		// check trùng
		var exists = await _benhNhanRepo.ExistsByThongTinIdAsync(thongTinID);
		if (exists)
			throw new Exception("Thông tin cá nhân này đã là bệnh nhân");

		// Tạo BenhNhan mới
		var benhNhan = new BenhNhan(
			thongTinID: thongTinID,
			ghiChu: dto.GhiChu ?? ""
		);

		return await _benhNhanRepo.AddAsync(benhNhan);
	}

	public async Task<bool> CapNhatBenhNhanAsync(int benhNhanID, string ghiChu)
	{
		var benhNhan = await _benhNhanRepo.GetByIdAsync(benhNhanID);
		if (benhNhan == null) return false;

		benhNhan.CapNhat(ghiChu);
		await _benhNhanRepo.UpdateAsync(benhNhan);
		return true;
	}

	public async Task<PagedResult<BenhNhanResponseDTO>>	DanhSachBenhNhanAsync(int pageNumber, int pageSize)
	{
		if (pageNumber <= 0) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (data, totalCount) =
			await _benhNhanRepo.GetPagedAsync(pageNumber, pageSize);

		var result = data.Select(bn => new BenhNhanResponseDTO
		{
			BenhNhanID = bn.BenhNhanID,
			ThongTinID = bn.ThongTinCaNhan?.ThongTinID ?? 0,
			HoTen = bn.ThongTinCaNhan?.HoTen ?? "",
			SDT = bn.ThongTinCaNhan?.SDT ?? "",
			EmailLienHe = bn.ThongTinCaNhan?.EmailLienHe ?? "",
			GhiChu = bn.GhiChu
		}).ToList();

		return new PagedResult<BenhNhanResponseDTO>
		{
			Items = result,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}


	public async Task<BenhNhanIdResponseDTO?> LayBenhNhanTheoIdAsync(int benhNhanID)
	{
		var bn = await _benhNhanRepo.GetByIdAsync(benhNhanID);
		if (bn == null) return null;

		var thongTin = await _thongTinRepo.GetByIdAsync(bn.ThongTinID);
		if (bn == null) return null;
		return new BenhNhanIdResponseDTO
		{
			BenhNhanID = bn.BenhNhanID,
			ThongTinID = bn.ThongTinID,
			GhiChu = bn.GhiChu,
			HoTen = thongTin?.HoTen,
			NgaySinh = thongTin?.NgaySinh,
			GioiTinh = thongTin?.GioiTinh,
			SDT = thongTin?.SDT,
			EmailLienHe = thongTin?.EmailLienHe,
			DiaChi = thongTin?.DiaChi,
			Avatar = thongTin?.Avatar,
			NgayTao = bn.NgayTao,
			NgayCapNhat = bn.NgayCapNhat,
		};
	}

	public async Task<List<BenhNhanResponseDTO>> SearchdAsync(string keyword)
	{
		var data = await _benhNhanRepo.GetBenhNhans(keyword);
		var result = data.Select(bn => new BenhNhanResponseDTO
		{
			BenhNhanID = bn.BenhNhanID,
			ThongTinID = bn.ThongTinCaNhan?.ThongTinID ?? 0,
			HoTen = bn.ThongTinCaNhan?.HoTen ?? "",
			SDT = bn.ThongTinCaNhan?.SDT ?? "",
			EmailLienHe = bn.ThongTinCaNhan?.EmailLienHe ?? "",
			GhiChu = bn.GhiChu
		}).ToList();

		return result;
	}

	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = await _benhNhanRepo.GetIdAndNameAsync();
		return list.Select(e => new NameResponseDTO
		{
			Id = e.Id,
			Name = e.Ten
		}).ToList();
	}
}
