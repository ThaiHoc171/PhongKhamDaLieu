using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Services;

public class ThuocService
{
	private readonly IThuocRepository _repo;

	public ThuocService(IThuocRepository repo)
	{
		_repo = repo;
	}

	public async Task<List<ThuocResponseDTO>> DanhSachAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToDto).ToList();
	}

	public async Task<List<ThuocResponseDTO>> TimKiemAsync(string keyword)
	{
		var list = await _repo.SearchAsync(keyword);
		return list.Select(MapToDto).ToList();
	}

	public async Task<ThuocResponseDTO?> LayTheoIdAsync(int id)
	{
		var thuoc = await _repo.GetByIdAsync(id);
		if (thuoc == null) return null;

		return MapToDto(thuoc);
	}

	public async Task ThemAsync(ThuocRequestDTO dto)
	{
		var thuoc = new Thuoc(dto.TenThuoc, dto.HoatChat);
		var danhsach = await _repo.GetAllAsync();
		thuoc.KiemTraTrungTen(danhsach);
		await _repo.AddAsync(thuoc);
	}

	public async Task<bool> CapNhatAsync(int id, ThuocRequestDTO dto)
	{
		var thuoc = await _repo.GetByIdAsync(id);
		if (thuoc == null) return false;

		thuoc.CapNhat(dto.TenThuoc, dto.HoatChat);
		var danhsach = await _repo.GetAllAsync();
		thuoc.KiemTraTrungTen(danhsach);
		await _repo.UpdateAsync(thuoc);

		return true;
	}

	private static ThuocResponseDTO MapToDto(Thuoc t)
	{
		return new ThuocResponseDTO
		{
			ThuocID = t.ThuocID,
			TenThuoc = t.TenThuoc,
			HoatChat = t.HoatChat
		};
	}
}
