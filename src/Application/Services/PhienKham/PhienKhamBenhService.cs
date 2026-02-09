using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using Application.ReadModels;
using Domain.Enums;
using System.Threading.Tasks;

namespace Application.Services;

public class PhienKhamBenhService
{
	private readonly IPhienKhamBenhRepository _repo;
	private readonly IPhienKhamRepository _phienKhamRepo;
	private readonly ILoaiBenhRepository _loaiBenhRepo;

	public PhienKhamBenhService( IPhienKhamBenhRepository repo, IPhienKhamRepository phienKhamRepo, ILoaiBenhRepository loaiBenhRepo)
	{
		_repo = repo;
		_phienKhamRepo = phienKhamRepo;
		_loaiBenhRepo = loaiBenhRepo;
	}
	public async Task<List<PhienKhamBenhResponseDTO>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(phienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		var list = await _repo.GetByIdAsync(phienKhamID);

		var tasks = list.Select(MapToResponse);
		var results = await Task.WhenAll(tasks);

		return results.ToList();
	}

	public async Task ThemMoiAsync(PhienKhamBenhRequestDTO dto)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			throw new Exception("Không thể thêm chẩn đoán khi phiên khám đã kết thúc");
		var daTonTaiChanDoanChinh = await _repo.PrimaryPKBenhExitsAsync(dto.PhienKhamID);

		if (dto.LoaiChanDoan == LoaiChanDoanEnum.ChanDoanChinh)
		{
			if (daTonTaiChanDoanChinh)
				throw new Exception("Mỗi phiên khám chỉ được có một chẩn đoán chính");
		}
		else
		{
			if (!daTonTaiChanDoanChinh)
				throw new Exception("Phải có chẩn đoán chính trước khi thêm chẩn đoán phụ");
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
		var pkb = pkbs.FirstOrDefault(p => p.PhienKham_BenhID == PKB_ID)
			?? throw new Exception("Phiên khám bệnh không tồn tại");
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");
		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			throw new Exception("Không thể cập nhật chẩn đoán khi phiên khám đã kết thúc");
		if (dto.LoaiChanDoan == LoaiChanDoanEnum.ChanDoanChinh && pkb.LoaiChanDoan != LoaiChanDoanEnum.ChanDoanChinh)
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
		pkb.CapNhat(dto.LoaiChanDoan, dto.GhiChu);
		await _repo.UpdateAsync(pkb);

	}
	private async Task<PhienKhamBenhResponseDTO> MapToResponse(PhienKhamBenh pkb)
	{
		var lb = await _loaiBenhRepo.GetNameByIdAsync(pkb.LoaiBenhID);
		return new PhienKhamBenhResponseDTO
		{
			Id = pkb.PhienKham_BenhID,
			PhienKhamID = pkb.PhienKhamID,
			LoaiBenh = new NameResponseDTO
			{
				Id = pkb.LoaiBenhID,
				Name = lb
			},
			LoaiChanDoan = pkb.LoaiChanDoan,
			GhiChu = pkb.GhiChu
		};
	}
}
