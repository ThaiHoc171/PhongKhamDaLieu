using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PhienKhamThietBiService
{
	private readonly IPhienKhamThietBiRepository _repo;

	public PhienKhamThietBiService(IPhienKhamThietBiRepository repo)
	{
		_repo = repo;
	}

	public async Task<List<PhienKhamThietBiReadModel>> DanhSachTheoPhienKhamAsync(int phienKhamID)
		=> await _repo.GetByPhienKhamAsync(phienKhamID);
	public async Task<PhienKhamThietBiResponseDTO?> LayTheoIdAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null) return null;

		return MapToResponse(entity);
	}
	// THÊM THIẾT BỊ VÀO PHIÊN
	public async Task ThemMoiAsync(PhienKhamThietBiRequestDTO dto)
	{
		// Rule: 1 ChiTietID chỉ xuất hiện 1 lần trong 1 phiên khám
		var existed = await _repo.GetByPhienKhamAndChiTietAsync(
			dto.PhienKhamID,
			dto.ChiTietID
		);

		if (existed != null)
			throw new InvalidOperationException("Thiết bị này đã được sử dụng trong phiên khám.");

		var entity = new PhienKhamThietBi(
			dto.PhienKhamID,
			dto.ChiTietID,
			dto.GhiChu
		);

		await _repo.AddAsync(entity);
	}

	public async Task<bool> CapNhatAsync(int id, string? ghiChu)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null) return false;

		entity.CapNhatGhiChu(ghiChu);
		await _repo.UpdateAsync(entity);

		return true;
	}

	private static PhienKhamThietBiResponseDTO MapToResponse(PhienKhamThietBi entity)
	{
		return new PhienKhamThietBiResponseDTO
		{
			PhienKhamThietBiID = entity.PhienKhamThietBiID,
			PhienKhamID = entity.PhienKhamID,
			ChiTietID = entity.ChiTietID,
			GhiChu = entity.GhiChu
		};
	}
}
