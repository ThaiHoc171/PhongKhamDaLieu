using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class LoaiBenhService
{
	private readonly ILoaiBenhRepository _repo;

	public LoaiBenhService(ILoaiBenhRepository repo)
	{
		_repo = repo;
	}

	public async Task<List<LoaiBenhResponseDTO>> DanhSachAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToDto).ToList();
	}

	public async Task<LoaiBenhResponseDTO?> LayTheoIdAsync(int id)
	{
		var lb = await _repo.GetByIdAsync(id);
		return lb == null ? null : MapToDto(lb);
	}

	public async Task<List<LoaiBenhResponseDTO>> TimTheoTenAsync(string ten)
	{
		var list = await _repo.SearchByTenAsync(ten);
		return list.Select(MapToDto).ToList();
	}

	public async Task ThemAsync(LoaiBenhRequestDTO dto)
	{
		var lb = new LoaiBenh(
			dto.TenBenh, dto.TenKhoaHoc, dto.NhomBenh,
			dto.MoTa, dto.DoPhoBien, dto.MucDoNghiemTrong);

		var danhSach = await _repo.GetAllAsync();
		lb.KiemTraTrung(danhSach);

		await _repo.AddAsync(lb);
	}

	public async Task<bool> CapNhatAsync(int id, LoaiBenhRequestDTO dto)
	{
		var lb = await _repo.GetByIdAsync(id);
		if (lb == null) return false;

		lb.CapNhat(
			dto.TenBenh, dto.TenKhoaHoc, dto.NhomBenh,
			dto.MoTa, dto.DoPhoBien, dto.MucDoNghiemTrong);

		var danhSach = await _repo.GetAllAsync();
		lb.KiemTraTrung(danhSach);

		await _repo.UpdateAsync(lb);
		return true;
	}
	public async Task<List<LoaiBenhComboDTO>> DanhSachComboAsync(string? keyword)
	{
		var danhSach = string.IsNullOrWhiteSpace(keyword)
			? await _repo.GetAllAsync()
			: await _repo.SearchByTenAsync(keyword);

		return danhSach.Select(x => new LoaiBenhComboDTO
		{
			LoaiBenhID = x.LoaiBenhID,
			DisplayName = string.IsNullOrWhiteSpace(x.TenKhoaHoc)
				? x.TenBenh
				: $"{x.TenBenh} – {x.TenKhoaHoc}"
		}).ToList();
	}

	private static LoaiBenhResponseDTO MapToDto(LoaiBenh lb)
	{
		return new LoaiBenhResponseDTO
		{
			LoaiBenhID = lb.LoaiBenhID,
			TenBenh = lb.TenBenh,
			TenKhoaHoc = lb.TenKhoaHoc,
			NhomBenh = lb.NhomBenh,
			MoTa = lb.MoTa,
			DoPhoBien = lb.DoPhoBien,
			MucDoNghiemTrong = lb.MucDoNghiemTrong,
			NgayTao = lb.NgayTao
		};
	}
}
