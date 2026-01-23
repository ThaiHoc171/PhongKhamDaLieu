using Application.DTOs;
using Application.Interfaces;
using Application.ReadModels;
using Domain.Entities;

namespace Application.Services;

public class PhienKhamCLSService
{
	private readonly IPhienKhamCLSRepository _repo;

	public PhienKhamCLSService(IPhienKhamCLSRepository repo)
	{
		_repo = repo;
	}

	public async Task<List<PhienKhamCLSReadModel>> LayTheoPhienKhamAsync(int phienKhamID)
	{
		return await _repo.GetByPhienKhamAsync(phienKhamID);
	}


	public async Task ThemMoiAsync(TaoPhienKhamCLSDTO dto)
	{
		var entity = new PhienKhamCLS(
			dto.PhienKhamID,
			dto.CLSID,
			dto.NhanVienChiDinhID,
			dto.GhiChu
		);

		await _repo.AddAsync(entity);
	}


	public async Task<bool> NhanThucHienAsync(int phienKhamCLSID, NhanThucHienCLSDTO dto)
	{
		var entity = await _repo.GetByIdAsync(phienKhamCLSID);
		if (entity == null) return false;

		entity.NhanPhienKhamCLS(dto.NhanVienThucHienID);

		await _repo.UpdateAsync(entity);
		return true;
	}

	public async Task<bool> CapNhatKetQuaAsync(int phienKhamCLSID, CapNhatKetQuaCLSDTO dto)
	{
		var entity = await _repo.GetByIdAsync(phienKhamCLSID);
		if (entity == null) return false;

		entity.CapNhatKetQua(dto.KetQua, dto.FileDinhKem, dto.GhiChu);

		await _repo.UpdateAsync(entity);
		return true;
	}

	public async Task<bool> HuyAsync(int phienKhamCLSID)
	{
		var entity = await _repo.GetByIdAsync(phienKhamCLSID);
		if (entity == null) return false;

		entity.HuyPhienKhamCLS();

		await _repo.UpdateAsync(entity);
		return true;
	}
}
