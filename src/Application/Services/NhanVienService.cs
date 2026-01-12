using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services;

public class NhanVienService
{
	private readonly INhanVienRepository _repo;
	private readonly ThongTinCaNhanService _thongTinService;

	public NhanVienService(INhanVienRepository repo,ThongTinCaNhanService thongTinService)
	{
		_repo = repo;
		_thongTinService = thongTinService;
	}

	public async Task TaoNhanVienAsync(TaoNhanVienDTO dto)
	{
		// Tạo tài khoản + thông tin cá nhân
		var thongTinID = await _thongTinService.TaoNhanVien(dto.ThongTin);

		// Tạo nhân viên
		var nv = new NhanVien(
			thongTinID,
			dto.ChucVuID,
			dto.NgayVaoLam,
			dto.BangCap,
			dto.KinhNghiem
		);

		await _repo.AddAsync(nv);
	}

	public async Task<bool> CapNhatNhanVienAsync(int nhanVienID, CapNhatNhanVienDTO dto)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);
		if (nv == null) return false;

		nv.CapNhatThongTin(
			dto.ChucVuID,
			dto.NgayVaoLam,
			dto.BangCap,
			dto.KinhNghiem
		);

		await _repo.UpdateAsync(nv);
		return true;
	}

	public async Task<bool> CapNhatTrangThaiAsync(int nhanVienID, string trangThai)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);
		if (nv == null) return false;

		nv.CapNhatTrangThai(trangThai);
		await _repo.UpdateAsync(nv);
		return true;
	}

	public async Task<List<NhanVienResponseDTO>> LayDanhSachAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToResponse).ToList();
	}
	public async Task<NhanVienResponseDTO?> LayTheoIDAsync(int nhanVienID)
	{
		var nv =  await _repo.GetByIdAsync(nhanVienID);
		if (nv == null) return null;
		return MapToResponse(nv);
	}
	public async Task<List<NhanVienResponseDTO?>> SearchAsync(string keyword)
	{
		var list = await _repo.SearchAsync(keyword);
		return list.Select(MapToResponse).ToList();
	}

	private static NhanVienResponseDTO MapToResponse(NhanVien nv)
	{
		return new NhanVienResponseDTO
		{
			NhanVienID = nv.NhanVienID,
			HoTen = nv.ThongTinCaNhan?.HoTen,
			Email = nv.ThongTinCaNhan?.EmailLienHe,
			TenChucVu = nv.TenChucVu,
			TrangThai = nv.TrangThai
		};
	}
}
