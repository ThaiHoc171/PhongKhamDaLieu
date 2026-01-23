using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using Application.ReadModels;

namespace Application.Services;

public class PhienKhamBenhService
{
	private readonly IPhienKhamBenhRepository _repo;
	private readonly IPhienKhamRepository _phienKhamRepo;

	public PhienKhamBenhService( IPhienKhamBenhRepository repo, IPhienKhamRepository phienKhamRepo)
	{
		_repo = repo;
		_phienKhamRepo = phienKhamRepo;
	}
	public async Task<List<PhienKhamBenhReadModel>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(phienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");
		return await  _repo.GetByIdAsync(phienKhamID);
	}
	public async Task ThemMoiAsync(PhienKhamBenhRequestDTO dto)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		if (phienKham.TrangThai != "Đang khám")
			throw new Exception("Không thể thêm chẩn đoán khi phiên khám đã kết thúc");
		var daTonTai = await _repo.PrimaryPKBenhExitsAsync(dto.PhienKhamID);
		if (dto.LoaiChanDoan == "Chẩn đoán chính")
		{
			if (daTonTai)
				throw new Exception("Mỗi phiên khám chỉ được có một chuẩn đoán chính");
		}
		else
		{
			if(!daTonTai)
				throw new Exception("Phải có chuẩn đoán chính trước khi thêm chuẩn đoán phụ");
		}
		var phienKhamBenh = new PhienKhamBenh
		(
			dto.PhienKhamID,dto.LoaiBenhID, dto.LoaiChanDoan,dto.GhiChu
		);
		await _repo.AddAsync(phienKhamBenh);
	}
	public async Task CapNhatAsync(int PKB_ID, PhienKhamBenhRequestDTO dto)
	{
		var pkbs = await _repo.GetByIdAsync(dto.PhienKhamID);
		var pkb = pkbs.FirstOrDefault(p => p.Id == PKB_ID)
			?? throw new Exception("Phiên khám bệnh không tồn tại");
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");
		if (phienKham.TrangThai != "Đang khám")
			throw new Exception("Không thể cập nhật chẩn đoán khi phiên khám đã kết thúc");
		if (dto.LoaiChanDoan == "Chuẩn đoán chính" && pkb.LoaiChanDoan != "Chuẩn đoán chính")
		{
			var daTonTai = await _repo.PrimaryPKBenhExitsAsync(dto.PhienKhamID);
			if (daTonTai)
				throw new Exception("Mỗi phiên khám chỉ được có một chuẩn đoán chính");
		}
		var updatedPkb = new PhienKhamBenh(
			PKB_ID,
			pkb.PhienKhamID,
			pkb.LoaiBenhID,
			dto.LoaiChanDoan,
			dto.GhiChu);
		await _repo.UpdateAsync(updatedPkb);
	}
}
