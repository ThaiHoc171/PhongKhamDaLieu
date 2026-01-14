using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ThietBiService
{
	private readonly IThietBiRepository _repo;

	public ThietBiService(IThietBiRepository repo)
	{
		_repo = repo;
	}

	// Lấy tất cả thiết bị
	public async Task<List<ThietBiResponseDTO>> LayTatCaAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToResponse).ToList();
	}

	// Lấy theo ID
	public async Task<ThietBiResponseDTO?> LayTheoIdAsync(int id)
	{
		var tb = await _repo.GetByIdAsync(id);
		if (tb == null)
			return null;

		return MapToResponse(tb);
	}

	// Tìm theo tên
	public async Task<List<ThietBiResponseDTO>> TimTheoTenAsync(string tenTB)
	{
		var list = await _repo.SearchByTenAsync(tenTB);
		return list.Select(MapToResponse).ToList();
	}

	// Thêm mới
	public async Task ThemAsync(ThietBiRequestDTO dto)
	{
		var tb = new ThietBi(dto.TenTB, dto.LoaiTB);
		await _repo.AddAsync(tb);
	}

	// Cập nhật thông tin
	public async Task<bool> CapNhatAsync(int id, ThietBiRequestDTO dto)
	{
		var tb = await _repo.GetByIdAsync(id);
		if (tb == null)
			return false;

		tb.CapNhat(dto.TenTB, dto.LoaiTB);
		await _repo.UpdateAsync(tb);

		return true;
	}

	// Chuyển tình trạng
	public async Task<bool> ChuyenTinhTrangAsync(int id, string TrangThaiMoi)
	{
		var tb = await _repo.GetByIdAsync(id);
		if (tb == null)
			return false;
		tb.ChuyenTinhTrang(TrangThaiMoi);
		await _repo.UpdateAsync(tb);

		return true;
	}

	// Map Entity → DTO
	private static ThietBiResponseDTO MapToResponse(ThietBi tb)
		=> new()
		{
			Id = tb.Id,
			TenTB = tb.TenTB,
			LoaiTB = tb.LoaiTB,
			TinhTrang = tb.TinhTrang,
			NgayNhap = tb.NgayNhap
		};
}
