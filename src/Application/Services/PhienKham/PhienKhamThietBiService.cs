using Application.DTOs;
using Application.Interfaces;
using Application.ReadModels;
using Domain.Entities;

namespace Application.Services;

public class PhienKhamThietBiService
{
	private readonly IPhienKhamThietBiRepository _repo;
	private readonly IPhienKhamRepository _phienKhamRepo;

	public PhienKhamThietBiService(IPhienKhamThietBiRepository repo, IPhienKhamRepository phienKhamRepo)
	{
		_repo = repo;
		_phienKhamRepo = phienKhamRepo;
	}

	// Danh sách thiết bị theo phiên khám 
	public async Task<List<PhienKhamThietBiReadModel>> DanhSachTheoPhienKhamAsync(int phienKhamID)
	{
		return await _repo.GetByPhienKhamAsync(phienKhamID);
	}

	// Lấy chi tiết 
	public async Task<PhienKhamThietBiResponseDTO?> LayTheoIdAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null) return null;

		return new PhienKhamThietBiResponseDTO
		{
			PhienKhamThietBiID = entity.PhienKhamThietBiID,
			PhienKhamID = entity.PhienKhamID,
			ThietBiID = entity.ThietBiID,
			SoLuong = entity.SoLuong,
			GhiChu = entity.GhiChu
		};
	}

	// Thêm thiết bị vào phiên khám
	public async Task ThemMoiAsync(PhienKhamThietBiRequestDTO dto)
	{
		// 1. Kiểm tra đã tồn tại thiết bị trong phiên khám chưa
		var existed = await _repo.GetByPhienKhamAndThietBiAsync(dto.PhienKhamID, dto.ThietBiID);

		if (existed != null)
		{
			// 2. Nếu đã tồn tại → cộng dồn số lượng
			var soLuongMoi = existed.SoLuong + dto.SoLuong;
			existed.CapNhat(soLuongMoi, dto.GhiChu);
			await _repo.UpdateAsync(existed);
			return;
		}

		// 3. Nếu chưa tồn tại → thêm mới
		var entity = new PhienKhamThietBi(
			dto.PhienKhamID,
			dto.ThietBiID,
			dto.SoLuong,
			dto.GhiChu
		);

		await _repo.AddAsync(entity);
	}


	// Cập nhật số lượng / ghi chú
	public async Task<bool> CapNhatAsync(int id, PhienKhamThietBiRequestDTO dto)
	{
		var existed = await _repo.GetByPhienKhamAndThietBiAsync(dto.PhienKhamID, dto.ThietBiID);

		if (existed != null)
		{
			var entity = await _repo.GetByIdAsync(id);
			if (entity == null) return false;

			entity.CapNhat(dto.SoLuong, dto.GhiChu);
			await _repo.UpdateAsync(entity);

			return true;
		}
		return false;

	}

}
