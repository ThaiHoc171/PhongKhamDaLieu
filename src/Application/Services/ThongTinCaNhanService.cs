using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ThongTinCaNhanService
{
	private readonly IThongTinCaNhanRepository _repo;
	private readonly ITaiKhoanRepository _taiKhoanRepo;
	private readonly IConfiguration _config;
	public ThongTinCaNhanService(IThongTinCaNhanRepository repo, ITaiKhoanRepository taiKhoanRepo, IConfiguration config)
	{
		_repo = repo;
		_taiKhoanRepo = taiKhoanRepo;
		_config = config;
	}

	public async Task<int> TaoNhanVien(ThemThongTinCaNhanDTO dto)
	{
		var defaultPassword = _config["DefaultPassword"];
		if (string.IsNullOrWhiteSpace(defaultPassword))
			throw new Exception("DefaultPassword chưa được cấu hình.");

		var hash = Helper.Password.PassWordHash(defaultPassword);

		var taiKhoan = new TaiKhoan(
			email: dto.EmailLienHe,
			matKhau: hash,
			vaiTro: VaiTroEnum.NhanVien
		);

		await _taiKhoanRepo.AddAsync(taiKhoan);

		// Lấy lại tài khoản vừa tạo
		var createdTaiKhoan = await _taiKhoanRepo.GetByEmailAsync(dto.EmailLienHe)
			?? throw new Exception("Không tạo được tài khoản.");

		// Tạo thông tin cá nhân gắn TaiKhoanID
		var thongTin = new ThongTinCaNhan(
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: dto.GioiTinh,
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar,
			loai: LoaiThongTinEnum.NhanVien,
			taiKhoanID: createdTaiKhoan.Id
		);

		return await _repo.AddAsync(thongTin);
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
