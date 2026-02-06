using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PCNThietBiService
{
	private readonly IPCNThietBiRepository _repo;

	public PCNThietBiService(IPCNThietBiRepository repo)
	{
		_repo = repo;
	}

	public async Task<List<PCNThietBiResponseDTO>> DanhSachAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(Map).ToList();
	}

	public async Task ThemAsync(PCNThietBiCreateDTO dto)
	{
		var existed = await _repo.GetByPhongAndThietBiAsync(dto.PhongChucNangID, dto.ThietBiID);
		if (existed != null)
			throw new InvalidOperationException("Thiết bị đã tồn tại trong phòng");

		var entity = new PCNThietBi(dto.PhongChucNangID, dto.ThietBiID);
		await _repo.AddAsync(entity);
	}

	public async Task<bool> XoaAsync(int pcnTbId)
	{
		var entity = await _repo.GetByIdAsync(pcnTbId);
		if (entity == null) return false;

		if (!entity.CoTheXoa())
			throw new InvalidOperationException("Không thể xóa khi vẫn còn thiết bị");

		await _repo.DeleteAsync(pcnTbId);
		return true;
	}

	private static PCNThietBiResponseDTO Map(PCNThietBi e)
	{
		return new PCNThietBiResponseDTO
		{
			PCN_TB_ID = e.PCN_TB_ID,
			PhongChucNangID = e.PhongChucNangID,
			ThietBiID = e.ThietBiID,
			TongSoLuong = e.TongSoLuong
		};
	}
}
