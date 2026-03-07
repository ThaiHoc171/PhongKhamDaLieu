using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
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
	public async Task<List<PhienKhamBenhReadModel>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(phienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		var list = await _repo.GetByPhienKhamAsync(phienKhamID);
		return list;
	}

	public async Task ThemMoiAsync(PhienKhamBenhRequestDTO dto)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			throw new Exception("Không thể thêm chẩn đoán khi phiên khám đã kết thúc");
		var daTonTaiChanDoanChinh = await _repo.PrimaryPKBenhExitsAsync(dto.PhienKhamID);
		var loaiChanDoanEnum = LoaiChanDoanEnumExtensions.ToEnum(dto.LoaiChanDoan);
		if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanChinh)
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
			dto.PhienKhamID,dto.LoaiBenhID, loaiChanDoanEnum, dto.GhiChu
		);
		await _repo.AddAsync(phienKhamBenh);
	}
	public async Task CapNhatAsync(int pkbId, PhienKhamBenhRequestDTO dto)
	{
		var pkb = await _repo.GetByIdAsync(pkbId)
			?? throw new Exception("Chẩn đoán không tồn tại");

		var phienKham = await _phienKhamRepo.GetByIdAsync(pkb.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			throw new Exception("Không thể cập nhật chẩn đoán khi phiên khám đã kết thúc");

		var loaiChanDoanEnum = LoaiChanDoanEnumExtensions.ToEnum(dto.LoaiChanDoan);

		// Nếu chuyển thành chẩn đoán chính
		if (loaiChanDoanEnum == LoaiChanDoanEnum.ChanDoanChinh &&
			pkb.LoaiChanDoan != LoaiChanDoanEnum.ChanDoanChinh)
		{
			var daTonTai = await _repo.PrimaryPKBenhExitsAsync(pkb.PhienKhamID);
			if (daTonTai)
				throw new Exception("Mỗi phiên khám chỉ được có một chẩn đoán chính");
		}

		// cập nhật entity
		pkb.CapNhat(loaiChanDoanEnum, dto.GhiChu);

		await _repo.UpdateAsync(pkb);
	}
}
