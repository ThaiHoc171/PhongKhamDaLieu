using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Domain.Enums;

namespace Services;

public class TaiKhoanService
{
	private readonly ITaiKhoanRepository _repo;
	private readonly IConfiguration _configuration;
	public TaiKhoanService(ITaiKhoanRepository repo, IConfiguration configuration)
	{
		_repo = repo;
		_configuration = configuration;
	}

	public async Task<TaiKhoanResponseDTO?> DangNhapAsync(LoginRequestDTO dto)
	{
		var tk = await _repo.GetByEmailAsync(dto.Email);
		if (tk == null) return null;

		if (!Helper.Password.VerifyPassword(dto.MatKhau, tk.MatKhau))
			return null;

		return MapToResponse(tk);
	}

	public async Task DangKyAsync(ThemTaiKhoanDTO dto)
	{
		var hash = Helper.Password.PassWordHash(dto.MatKhau);
		var vaiTro = VaiTroExtensions.ToEnum(dto.VaiTro);
		var tk = new TaiKhoan(dto.Email, hash, vaiTro);
		await _repo.AddAsync(tk);
	}

	public async Task<bool> DoiMatKhauAsync(int id, DoiMatKhauDTO dto)
	{
		var tk = await _repo.GetByIdAsync(id);
		if (tk == null) return false;

		if (!Helper.Password.VerifyPassword(dto.MatKhauCu, tk.MatKhau))
			return false;

		tk.DoiMatKhau(Helper.Password.PassWordHash(dto.MatKhauMoi));
		await _repo.UpdateAsync(tk);
		return true;
	}
	public async Task<bool> ResetMatKhauAsync(int taiKhoanId)
	{
		var tk = await _repo.GetByIdAsync(taiKhoanId);
		if (tk == null)
			return false;

		var defaultPassword = _configuration["DefaultPassword"];
		if (string.IsNullOrWhiteSpace(defaultPassword))
			throw new Exception("DefaultPassword chưa được cấu hình.");

		var hash = Helper.Password.PassWordHash(defaultPassword);
		tk.DoiMatKhau(hash);

		await _repo.UpdateAsync(tk);
		return true;
	}
	public async Task<List<TaiKhoanResponseDTO>> LayTatCaAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToResponse).ToList();
	}
	public async Task<bool> CapNhatTrangThaiAsync(int taiKhoanId, string trangThaiMoi)
	{
		var tk = await _repo.GetByIdAsync(taiKhoanId);
		if (tk == null)
			return false;

		tk.CapNhatTrangThai(trangThaiMoi);
		await _repo.UpdateAsync(tk);

		return true;
	}
	public async Task<TaiKhoanResponseDTO?> LayTaiKhoanTheoIdAsync(int id)
	{
		var tk = await _repo.GetByIdAsync(id);
		if (tk == null)
			return null;

		return MapToResponse(tk);
	}
	private static TaiKhoanResponseDTO MapToResponse(TaiKhoan tk)
		=> new()
		{
			Id = tk.Id,
			Email = tk.Email,
			VaiTro = tk.VaiTro,
			TrangThai = tk.TrangThai
		};
}
