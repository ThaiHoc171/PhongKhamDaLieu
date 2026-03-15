using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class ChiTietPCNThietBiService
{
	private readonly IChiTietPCNThietBiRepository _chiTietRepo;
	private readonly IPCNThietBiRepository _pcnRepo;
	public ChiTietPCNThietBiService(
		IChiTietPCNThietBiRepository chiTietRepo,
		IPCNThietBiRepository pcnRepo)
	{
		_chiTietRepo = chiTietRepo;
		_pcnRepo = pcnRepo;
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync(int pcnId)
	{
		var list = await _chiTietRepo.GetComboboxAsync(pcnId);
		return list.Select(x => new NameResponseDTO
		{
			Id = x.Id,
			Name = x.Ten
		}).ToList();
	}
	public async Task<IEnumerable<ChiTietPCNThietBi>> LayTheoPCNTBAsync(int pcnTbId)
	{
		return await _chiTietRepo.GetByPCNTBIdAsync(pcnTbId);
	}
	public async Task ThemChiTietAsync(ChiTietPCNThietBiCreateDTO dto)
	{
		// 1. Tìm PCNThietBi
		var pcn = await _pcnRepo.GetByPhongAndThietBiAsync(
			dto.PhongChucNangID,
			dto.ThietBiID);
		// 2. Nếu chưa có → tạo mới
		if (pcn == null)
		{
			pcn = new PCNThietBi(dto.PhongChucNangID, dto.ThietBiID);
			await _pcnRepo.AddAsync(pcn);
			// lấy lại ID (vì identity)
			pcn = await _pcnRepo.GetByPhongAndThietBiAsync(
				dto.PhongChucNangID,
				dto.ThietBiID)
				?? throw new InvalidOperationException("Không thể tạo PCN thiết bị");
		}
		// 3. Tạo chi tiết
		var chiTiet = new ChiTietPCNThietBi(
			pcn.PCN_TB_ID,
			dto.MaTaiSan,
			dto.GhiChu);
		await _chiTietRepo.AddAsync(chiTiet);
		// 4. Tăng tổng số lượng
		pcn.CapNhatSoLuong(pcn.TongSoLuong + 1);
		await _pcnRepo.UpdateAsync(pcn);
	}
	public async Task<bool> XoaChiTietAsync(int chiTietId)
	{
		var chiTiet = await _chiTietRepo.GetByIdAsync(chiTietId);
		if (chiTiet == null) return false;
		if (chiTiet.TinhTrang == TinhTrang.HoatDong)
			throw new InvalidOperationException("Không thể xóa thiết bị đang hoạt động");
		await _chiTietRepo.DeleteAsync(chiTietId);
		var pcn = await _pcnRepo.GetByIdAsync(chiTiet.PCN_TB_ID)
			?? throw new InvalidOperationException("PCN thiết bị không tồn tại");
		pcn.CapNhatSoLuong(pcn.TongSoLuong - 1);
		// 5. Nếu hết → xóa PCN
		if (pcn.CoTheXoa())
			await _pcnRepo.DeleteAsync(pcn.PCN_TB_ID);
		else
			await _pcnRepo.UpdateAsync(pcn);
		return true;
	}
	public async Task<bool> CapNhatChiTietAsync(int chiTietId, ChiTietPCNThietBiUpdateDTO dto)
	{
		var chiTiet = await _chiTietRepo.GetByIdAsync(chiTietId);
		if (chiTiet == null) return false;
		chiTiet.CapNhatGhiChu(dto.GhiChu);
		await _chiTietRepo.UpdateAsync(chiTiet);
		return true;
	}
	public async Task<bool> CapNhatTrangThaiAsync(int chiTietId, TinhTrang tinhTrang)
	{
		var chiTiet = await _chiTietRepo.GetByIdAsync(chiTietId);
		if (chiTiet == null) return false;
		chiTiet.ChuyenTinhTrang(tinhTrang);
		await _chiTietRepo.UpdateAsync(chiTiet);
		return true;
	}
}
