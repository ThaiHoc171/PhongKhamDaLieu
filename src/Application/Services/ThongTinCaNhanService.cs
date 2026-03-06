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

	public ThongTinCaNhanService(
		IThongTinCaNhanRepository repo,
		ITaiKhoanRepository taiKhoanRepo,
		IConfiguration config)
	{
		_repo = repo;
		_taiKhoanRepo = taiKhoanRepo;
		_config = config;
	}

	private static GioiTinhEnum ParseGioiTinh(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new ArgumentException("Giới tính không hợp lệ");

		return GioiTinhExtensions.ToEnum(value);
	}

	public async Task<int> TaoNhanVienAsync(ThemThongTinCaNhanDTO dto)
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

		var createdTaiKhoan = await _taiKhoanRepo.GetByEmailAsync(dto.EmailLienHe)
			?? throw new Exception("Không tạo được tài khoản.");

		var entity = new ThongTinCaNhan(
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: ParseGioiTinh(dto.GioiTinh),
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar,
			loai: LoaiThongTinEnum.NhanVien,
			taiKhoanID: createdTaiKhoan.Id
		);

		return await _repo.AddAsync(entity);
	}

	public async Task<int> TaoBenhNhanAsync(ThemThongTinCaNhanDTO dto)
	{
		var entity = new ThongTinCaNhan(
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: ParseGioiTinh(dto.GioiTinh),
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar,
			loai: LoaiThongTinEnum.BenhNhan
		);

		return await _repo.AddAsync(entity);
	}

	public async Task<List<ThongTinCaNhanResponseDTO>> DanhSachNhanVienAsync()
	{
		return await LayTheoLoaiAsync(LoaiThongTinEnum.NhanVien);
	}

	public async Task<List<ThongTinCaNhanResponseDTO>> DanhSachBenhNhanAsync()
	{
		return await LayTheoLoaiAsync(LoaiThongTinEnum.BenhNhan);
	}

	private async Task<List<ThongTinCaNhanResponseDTO>> LayTheoLoaiAsync(
		LoaiThongTinEnum loai)
	{
		var list = await _repo.GetAllByLoaiAsync(loai);
		return list.Select(MapToResponse).ToList();
	}

	public async Task<ThongTinCaNhanResponseDTO?> LayChiTietAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		return entity == null ? null : MapToResponse(entity);
	}

	public async Task<bool> CapNhatAsync(
		int thongTinID,
		CapNhatThongTinCaNhanDTO dto)
	{
		var entity = await _repo.GetByIdAsync(thongTinID);
		if (entity == null)
			return false;

		entity.CapNhat(
			hoTen: dto.HoTen,
			ngaySinh: dto.NgaySinh,
			gioiTinh: ParseGioiTinh(dto.GioiTinh),
			sdt: dto.SDT,
			emailLienHe: dto.EmailLienHe,
			diaChi: dto.DiaChi,
			avatar: dto.Avatar
		);

		await _repo.UpdateAsync(entity);
		return true;
	}

	private static ThongTinCaNhanResponseDTO MapToResponse(ThongTinCaNhan e)
	{
		return new ThongTinCaNhanResponseDTO
		{
			ThongTinID = e.ThongTinID,
			TaiKhoanID = e.TaiKhoanID,
			HoTen = e.HoTen,
            NgaySinh = e.NgaySinh,
            GioiTinh = e.GioiTinh,
            SDT = e.SDT,
			EmailLienHe = e.EmailLienHe,
            DiaChi = e.DiaChi,
            Avatar = e.Avatar,
			Loai = e.Loai
		};
	}
}
