using Application.DTOs;
using Application.Interfaces;
using Application.ReadModels;
using Domain.Entities;

namespace Application.Services;

public class PCNThietBiService
{
	private readonly IPCNThietBiRepository _repo;

	public PCNThietBiService(IPCNThietBiRepository repo)
	{
		_repo = repo;
	}


	public async Task<int> AddAsync(int pcnId, PCNThietBiRequestDTO dto)
	{
		var entity = new PCN_TB(
			pcnId,
			dto.TB_Id,
			dto.SoLuong,
			dto.GhiChu
		);

		return await _repo.AddAsync(entity);
	}

	public async Task<bool> UpdateAsync(int id, PCNThietBiRequestDTO dto)
	{
		// Lấy dữ liệu hiện tại để biết PCN_Id
		var current = await _repo.GetByIdAsync(id);
		if (current == null)
			return false;

		// Rule nghiệp vụ: SoLuong = 0 ⇒ delete
		if (dto.SoLuong == 0)
		{
			await _repo.DeleteAsync(id);
			return true;
		}

		var entity = new PCN_TB(
			id,
			current.PCN_Id,
			dto.TB_Id,
			dto.SoLuong,
			dto.GhiChu
		);

		await _repo.UpdateAsync(entity);
		return true;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var exists = await _repo.GetByIdAsync(id);
		if (exists == null)
			return false;

		await _repo.DeleteAsync(id);
		return true;
	}

	public async Task<PCNThietBiReadModel?> GetByIdAsync(int id)
	{
		return await _repo.GetByIdAsync(id);
	}

	public async Task<List<PCNThietBiReadModel>> GetByPCNAsync(int pcnId)
	{
		return await _repo.GetByPCNAsync(pcnId);
	}
}
