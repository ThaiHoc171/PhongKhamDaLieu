using Application.Common;
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
			return GioiTinhEnum.Khac;
		return GioiTinhExtensions.ToEnum(value);
	}
	public async Task<ApiResponse<int>> TaoNhanVienAsync(ThongTinRequestDTO dto)
	{
		var defaultPassword = _config["DefaultPassword"];
		if (string.IsNullOrWhiteSpace(defaultPassword))
			return ApiResponse<int>.Fail("Chưa cấu hình mật khẩu mặc định.");
		var hash = Helper.Password.PassWordHash(defaultPassword);
		var taiKhoan = new TaiKhoan(dto.EmailLienHe, hash, VaiTroEnum.NhanVien);
		await _taiKhoanRepo.AddAsync(taiKhoan);
		var created = await _taiKhoanRepo.GetByEmailAsync(dto.EmailLienHe);
		if (created == null)
			return ApiResponse<int>.Fail("Không tạo được tài khoản.");
		var entity = new ThongTinCaNhan(
			dto.HoTen,
			dto.NgaySinh,
			ParseGioiTinh(dto.GioiTinh),
			dto.SDT,
			dto.EmailLienHe,
			dto.DiaChi,
			dto.Avatar,
			LoaiThongTinEnum.NhanVien,
			created.Id
		);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id, "Tạo nhân viên thành công");
	}
	public async Task<ApiResponse<int>> AddAsync(ThongTinRequestDTO dto)
	{
		var entity = new ThongTinCaNhan(
			dto.HoTen,
			dto.NgaySinh,
			ParseGioiTinh(dto.GioiTinh),
			dto.SDT,
			dto.EmailLienHe,
			dto.DiaChi,
			dto.Avatar,
			LoaiThongTinEnum.BenhNhan,
			dto.TaiKhoanID
		);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id);
	}
	public async Task<ApiResponse<List<ThongTinCaNhanResponseDTO>>> DanhSachNhanVienAsync()
	{
		return await GetAllByLoaiAsync(LoaiThongTinEnum.NhanVien);
	}
	public async Task<ApiResponse<List<ThongTinCaNhanResponseDTO>>> DanhSachBenhNhanAsync()
	{
		return await GetAllByLoaiAsync(LoaiThongTinEnum.BenhNhan);
	}
	private async Task<ApiResponse<List<ThongTinCaNhanResponseDTO>>> GetAllByLoaiAsync(LoaiThongTinEnum loai)
	{
		var list = await _repo.GetAllByLoaiAsync(loai);
		var result = list.Select(e => new ThongTinCaNhanResponseDTO
		{
			ThongTinID = e.ThongTinID,
			TaiKhoanID = e.TaiKhoanID,
			HoTen = e.HoTen,
			SDT = e.SDT,
			EmailLienHe = e.EmailLienHe,
			Loai = e.Loai
		}).ToList();
		return ApiResponse<List<ThongTinCaNhanResponseDTO>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<ThongTinCaNhanResponseDTO?>> GetDetailAsync(int id)
	{
		var data = await _repo.GetDetailAsync(id);
		if (data == null)
			return ApiResponse<ThongTinCaNhanResponseDTO?>.Fail("Không tìm thấy.");
		var result = new ThongTinCaNhanResponseDTO
		{
			ThongTinID = data.ThongTinID,
			TaiKhoanID = data.TaiKhoanID,
			HoTen = data.HoTen,
			NgaySinh = data.NgaySinh,
			GioiTinh = data.GioiTinh ?? "",
			SDT = data.SDT,
			EmailLienHe = data.EmailLienHe,
			DiaChi = data.DiaChi,
			Avatar = data.Avatar,
			Loai = data.Loai
		};
		return ApiResponse<ThongTinCaNhanResponseDTO?>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThongTinUpdateRequestDTO dto)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy.");
		entity.CapNhat(
			dto.HoTen,
			dto.NgaySinh,
			ParseGioiTinh(dto.GioiTinh),
			dto.SDT,
			dto.EmailLienHe,
			dto.DiaChi,
			dto.Avatar
		);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetCombobox()
	{
		var result = await _repo.GetComboboxAsync();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
	}
}