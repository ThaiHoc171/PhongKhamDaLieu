using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

public class ToaThuocService
{
	private readonly IToaThuocRepository _toaThuocRepo;
	private readonly IChiTietToaThuocRepository _chiTietRepo;
	private readonly IPhienKhamRepository _phienKhamRepo;

	public ToaThuocService(
		IToaThuocRepository toaThuocRepo,
		IChiTietToaThuocRepository chiTietRepo,
		IPhienKhamRepository phienKhamRepo)
	{
		_toaThuocRepo = toaThuocRepo;
		_chiTietRepo = chiTietRepo;
		_phienKhamRepo = phienKhamRepo;
	}

	public async Task<int> TaoToaThuocAsync(TaoToaThuocDTO dto)
	{
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID)
			?? throw new Exception("Phiên khám không tồn tại");

		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			throw new Exception("Không thể thêm toa thuốc khi phiên khám đã kết thúc");


		var toaThuoc = new ToaThuoc(
			dto.PhienKhamID,
			dto.NhanVienKeDonID,
			dto.GhiChu);

		var toaThuocID = await _toaThuocRepo.AddAsync(toaThuoc);

		var chiTiet = dto.Thuoc
			.Select(x => new ChiTietToaThuoc(
				x.ThuocID,
				x.LieuDung,
				x.SoLuong))
			.ToList();

		await _chiTietRepo.AddAsync(toaThuocID, chiTiet);

		return toaThuocID;
	}

	public async Task<ToaThuocChiTietDTO?> XemTheoPhienKhamAsync(int phienKhamID)
	{
		var toaThuoc = await _toaThuocRepo.GetByPhienKhamIdAsync(phienKhamID);
		if (toaThuoc == null) return null;

		var chiTiet = await _chiTietRepo.GetByToaThuocIdAsync(toaThuoc.ToaThuocID);

		return new ToaThuocChiTietDTO
		{
			ToaThuocID = toaThuoc.ToaThuocID,
			NgayLap = toaThuoc.NgayLap,
			GhiChu = toaThuoc.GhiChu,
			Thuoc = chiTiet.Select(x => new ChiTietToaThuocDTO
			{
				ThuocID = x.ThuocID,
				TenThuoc = x.TenThuoc,
				LieuDung = x.LieuDung,
				SoLuong = x.SoLuong
			}).ToList()
		};
	}
}
