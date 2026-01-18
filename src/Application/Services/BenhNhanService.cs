using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

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

	public async Task<int> ThemBenhNhanAsync(ThemBenhNhanDTO dto)
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
			if (string.IsNullOrWhiteSpace(dto.EmailLienHe))
				throw new Exception("Phải cung cấp email cho bệnh nhân mới");

			var thongTin = new ThongTinCaNhan(
				hoTen: dto.HoTen,
				ngaySinh: dto.NgaySinh,
				gioiTinh: dto.GioiTinh ?? "Khác",
				sdt: dto.SDT,
				emailLienHe: dto.EmailLienHe,
				diaChi: dto.DiaChi,
				avatar: dto.Avatar,
				loai: LoaiThongTinEnum.BenhNhan
			);

			thongTinID = await _thongTinRepo.AddAsync(thongTin);
		}

		// Tạo BenhNhan mới
		var benhNhan = new BenhNhan(
			thongTinID: thongTinID,
			loaiDa: dto.LoaiDa ?? "",
			ghiChu: dto.GhiChu ?? ""
		);

		return await _benhNhanRepo.AddAsync(benhNhan);
	}

	public async Task<bool> CapNhatBenhNhanAsync(int benhNhanID, string loaiDa, string ghiChu)
	{
		var benhNhan = await _benhNhanRepo.GetByIdAsync(benhNhanID);
		if (benhNhan == null) return false;

		benhNhan.CapNhat(loaiDa, ghiChu);
		await _benhNhanRepo.UpdateAsync(benhNhan);
		return true;
	}
	public async Task<bool> CapNhatTrangThaiAsync(int benhNhanID, string trangThai)
	{
		var benhNhan = await _benhNhanRepo.GetByIdAsync(benhNhanID);
		if (benhNhan == null) return false;
		benhNhan.CapNhatTrangThai(trangThai);
		await _benhNhanRepo.UpdateAsync(benhNhan);
		return true;
	}

	public async Task<List<BenhNhanResponseDTO>> DanhSachBenhNhanAsync()
	{
		var list = await _benhNhanRepo.GetAllAsync();
		var result = new List<BenhNhanResponseDTO>();
		foreach (var bn in list)
		{
			var thongTin = await _thongTinRepo.GetByIdAsync(bn.ThongTinID);
			result.Add(new BenhNhanResponseDTO
			{
				BenhNhanID = bn.BenhNhanID,
				ThongTinID = bn.ThongTinID,
				HoTen = thongTin?.HoTen ?? "",
				SDT = thongTin?.SDT ?? "",
				EmailLienHe = thongTin?.EmailLienHe ?? "",
				LoaiDa = bn.LoaiDa,
				TrangThaiTheoDoi = bn.TrangThaiTheoDoi,
				GhiChu = bn.GhiChu
			});
		}
		return result;
	}
	public async Task<BenhNhanResponseDTO?> LayBenhNhanTheoIdAsync(int benhNhanID)
	{
		var bn = await _benhNhanRepo.GetByIdAsync(benhNhanID);
		if (bn == null) return null;

		var thongTin = await _thongTinRepo.GetByIdAsync(bn.ThongTinID);
		return new BenhNhanResponseDTO
		{
			BenhNhanID = bn.BenhNhanID,
			ThongTinID = bn.ThongTinID,
			HoTen = thongTin?.HoTen ?? "",
			SDT = thongTin?.SDT ?? "",
			EmailLienHe = thongTin?.EmailLienHe ?? "",
			LoaiDa = bn.LoaiDa,
			TrangThaiTheoDoi = bn.TrangThaiTheoDoi,
			GhiChu = bn.GhiChu
		};
	}

	public async Task<List<BenhNhanResponseDTO>> SearchdAsync(string keyword)
	{
		var list = await _benhNhanRepo.GetBenhNhans(keyword);
		var result = new List<BenhNhanResponseDTO>();

		foreach (var bn in list)
		{
			var thongTin = await _thongTinRepo.GetByIdAsync(bn.ThongTinID);

			result.Add(new BenhNhanResponseDTO
			{
				BenhNhanID = bn.BenhNhanID,
				ThongTinID = bn.ThongTinID,
				HoTen = thongTin?.HoTen ?? "",
				SDT = thongTin?.SDT ?? "",
				EmailLienHe = thongTin?.EmailLienHe ?? "",
				LoaiDa = bn.LoaiDa,
				TrangThaiTheoDoi = bn.TrangThaiTheoDoi,
				GhiChu = bn.GhiChu
			});
		}

		return result;
	}
}
