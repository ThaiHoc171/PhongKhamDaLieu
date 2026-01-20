using Application.DTOs.PhienKham;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PhienKhamService
{
	private readonly IPhienKhamRepository _repo;

	public PhienKhamService(IPhienKhamRepository repo)
	{
		_repo = repo;
	}

	public async Task<int> TaoMoiAsync(PhienKhamCreateDTO dto)
	{
		var entity = new PhienKham(
			dto.CaKhamID,
			dto.BenhNhanID,
			dto.NhanVienID,
			dto.PhongChucNangID,
			dto.TrieuChung,
			dto.GhiChu,
			dto.HinhAnhJSON);

		return await _repo.AddAsync(entity);
	}

	public async Task CapNhatAsync(int id, PhienKhamUpdateDTO dto)
	{
		var pk = await _repo.GetByIdAsync(id)
			?? throw new Exception("Phiên khám không tồn tại");

		pk.CapNhat(
			dto.TrieuChung,
			dto.GhiChu,
			dto.PhongChucNangID,
			dto.HinhAnhJSON);

		await _repo.UpdateAsync(pk);
	}

}
