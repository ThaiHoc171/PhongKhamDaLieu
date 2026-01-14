using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PhongChucNangService
{
	private readonly IPhongChucNangRepository _repo;

	public PhongChucNangService(IPhongChucNangRepository repo)
	{
		_repo = repo;
	}

	// Lấy tất cả
	public async Task<List<PhongChucNangResponseDTO>> LayTatCaAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToResponse).ToList();
	}

	// Tìm kiếm
	public async Task<List<PhongChucNangResponseDTO>> TimKiemAsync(string keyword)
	{
		var list = await _repo.SearchAsync(keyword);
		return list.Select(MapToResponse).ToList();
	}

	// Lấy theo ID
	public async Task<PhongChucNangResponseDTO?> LayTheoIdAsync(int id)
	{
		var phong = await _repo.GetByIdAsync(id);
		if (phong == null)
			return null;

		return MapToResponse(phong);
	}

	// Thêm mới
	public async Task ThemAsync(PhongChucNangRequestDTO dto)
	{
		var phong = new PhongChucNang(
			dto.TenPhong,
			dto.LoaiPhong,
			dto.MoTa
		);

		await _repo.AddAsync(phong);
	}

	// Cập nhật thông tin
	public async Task<bool> CapNhatAsync(int id, PhongChucNangRequestDTO dto)
	{
		var phong = await _repo.GetByIdAsync(id);
		if (phong == null)
			return false;

		phong.CapNhat(
			dto.TenPhong,
			dto.LoaiPhong,
			dto.MoTa
		);
		await _repo.UpdateAsync(phong);
		return true;
	}

	// Chuyển trạng thái
	public async Task<bool> ChuyenTrangThaiAsync(int id, string TrangThaiMoi)
	{
		var phong = await _repo.GetByIdAsync(id);
		if (phong == null)
			return false;
		phong.ChuyenTrangThai(TrangThaiMoi);
		await _repo.UpdateAsync(phong);

		return true;
	}

	// Map Entity → DTO
	private static PhongChucNangResponseDTO MapToResponse(PhongChucNang p)
		=> new()
		{
			Id = p.Id,
			TenPhong = p.TenPhong,
			LoaiPhong = p.LoaiPhong,
			MoTa = p.MoTa,
			TrangThai = p.TrangThai,
			NgayTao = p.NgayTao
		};
}
