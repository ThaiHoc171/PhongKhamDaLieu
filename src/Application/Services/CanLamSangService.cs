using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Services;

public class CanLamSangService
{
	private readonly ICanLamSangRepository _repo;

	public CanLamSangService(ICanLamSangRepository repo)
	{
		_repo = repo;
	}

	// GET ALL
	public async Task<List<CanLamSangResponseDTO>> DanhSachCanLamSangAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToDto).ToList();
	}

	// GET BY ID
	public async Task<CanLamSangResponseDTO?> LayCanLamSangTheoIdAsync(int id)
	{
		var cls = await _repo.GetByIdAsync(id);
		if (cls == null) return null;

		return MapToDto(cls);
	}

	// POST
	public async Task ThemCanLamSangAsync(CanLamSangRequestDTO dto)
	{
		var cls = new CanLamSang(
			dto.TenCLS,
			dto.MoTa,
			dto.Gia,
			dto.LoaiXetNghiem
		);

		await _repo.AddAsync(cls);
	}

	// PUT
	public async Task<bool> CapNhatCanLamSangAsync(int id, CanLamSangRequestDTO dto)
	{
		var cls = await _repo.GetByIdAsync(id);
		if (cls == null) return false;

		cls.CapNhat(dto.TenCLS, dto.MoTa, dto.Gia, dto.LoaiXetNghiem);
		await _repo.UpdateAsync(cls);

		return true;
	}

	// PUT trạng thái
	public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThaiMoi)
	{
		var cls = await _repo.GetByIdAsync(id);
		if (cls == null) return false;

		cls.CapNhatTrangThai(trangThaiMoi);
		await _repo.UpdateAsync(cls);

		return true;
	}

	// MAP ENTITY → DTO
	private static CanLamSangResponseDTO MapToDto(CanLamSang cls)
	{
		return new CanLamSangResponseDTO
		{
			CanLamSangID = cls.CanLamSangID,
			TenCLS = cls.TenCLS,
			MoTa = cls.MoTa,
			Gia = cls.Gia,
			LoaiXetNghiem = cls.LoaiXetNghiem,
			NgayTao = cls.NgayTao,
			TrangThai = cls.TrangThai
		};
	}
}
