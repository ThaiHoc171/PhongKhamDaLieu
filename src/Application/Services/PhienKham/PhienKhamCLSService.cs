using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services;

public class PhienKhamCLSService
{
	private readonly IPhienKhamCLSRepository _repo;
	private readonly INhanVienRepository _nhanVienRepo;

	public PhienKhamCLSService(IPhienKhamCLSRepository repo, INhanVienRepository nhanVienRepo)
	{
		_repo = repo;
		_nhanVienRepo = nhanVienRepo;
	}

	public async Task<List<PhienKhamCLSResponseDTO>> LayTheoPhienKhamAsync(int phienKhamID)
	{
		var entities = await _repo.GetByPhienKhamAsync(phienKhamID);
		var tasks= entities.Select(e => MapToDto(e));
		var results = await Task.WhenAll(tasks);
		return results.ToList();
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

	private async Task<PhienKhamCLSResponseDTO> MapToDto(PhienKhamCLS e)
	{
		var nvChiDinh = new NameResponseDTO()
		{
			Id = e.NhanVienChiDinhID,
			Name = await _nhanVienRepo.GetNameByIdAsync(e.NhanVienChiDinhID)
		};
		var nvThucHien = e.NhanVienThucHienID.HasValue
			? new NameResponseDTO()
			{
				Id = e.NhanVienThucHienID.Value,
				Name = await _nhanVienRepo.GetNameByIdAsync(e.NhanVienThucHienID.Value)
			}
			: null;

		return new PhienKhamCLSResponseDTO
		{
			PhienKhamCLSID = e.PhienKhamCLSID,
			CLSID = e.CLSID,
			TrangThai = e.TrangThai.ToString(),
			KetQua = e.KetQua,
			FileDinhKem = e.FileDinhKem,
			NgayThucHien = e.NgayThucHien,
			NhanVienChiDinh = nvChiDinh,
			NhanVienThucHien = nvThucHien,
			GhiChu = e.GhiChu
		};
	}
}
