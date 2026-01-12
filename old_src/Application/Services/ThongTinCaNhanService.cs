using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class ThongTinCaNhanService
{
	private readonly IThongTinCaNhanRepository _repo;

	public ThongTinCaNhanService(IThongTinCaNhanRepository repo)
	{
		_repo = repo;
	}

	public async Task<int> ThemThongTinNhanVien(ThemThongTinCaNhanDTO dto)
	{
		return await ThemThongTin(dto, LoaiThongTinEnum.NhanVien);
	}

	public async Task<int> ThemThongTinBenhNhan(ThemThongTinCaNhanDTO dto)
	{
		return await ThemThongTin(dto, LoaiThongTinEnum.BenhNhan);
	}

	private async Task<int> ThemThongTin(
		ThemThongTinCaNhanDTO dto,
		LoaiThongTinEnum loai
	)
	{
		var entity = new ThongTinCaNhan(
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: dto.GioiTinh,
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar,
			loai: loai
		);

		return await _repo.AddAsync(entity);
	}


	public async Task<List<ThongTinCaNhanResponseDTO>> DanhSachThongTinNhanVien()
	{
		return await LayDanhSachTheoLoai(LoaiThongTinEnum.NhanVien);
	}

	public async Task<List<ThongTinCaNhanResponseDTO>> DanhSachThongTinBenhNhan()
	{
		return await LayDanhSachTheoLoai(LoaiThongTinEnum.BenhNhan);
	}

	private async Task<List<ThongTinCaNhanResponseDTO>> LayDanhSachTheoLoai(
		LoaiThongTinEnum loai
	)
	{
		var list = await _repo.GetAllByLoaiAsync(loai);
		return list.Select(MapToResponse).ToList();
	}

	public async Task<ThongTinCaNhanResponseDTO?> ThongTin(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		return entity == null ? null : MapToResponse(entity);
	}


	public async Task<bool> CapNhatThongTin(
		int thongTinID,
		CapNhatThongTinCaNhanDTO dto
	)
	{
		var entity = await _repo.GetByIdAsync(thongTinID);
		if (entity == null)
			return false;

		entity.CapNhat(
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: dto.GioiTinh,
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar
		);

		await _repo.UpdateAsync(entity);
		return true;
	}

	// ================== MAP ==================

	private static ThongTinCaNhanResponseDTO MapToResponse(ThongTinCaNhan e)
	{
		return new ThongTinCaNhanResponseDTO
		{
			ThongTinID = e.ThongTinID,
			TaiKhoanID = e.TaiKhoanID,
			HoTen = e.HoTen,
			SDT = e.SDT,
			EmailLienHe = e.EmailLienHe,
			Avatar = e.Avatar,
			Loai = e.Loai
		};
	}
}
