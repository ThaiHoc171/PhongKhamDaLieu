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
	public async Task<bool> KiemTraTonTai(int phienKhamId)
	{
		return await _toaThuocRepo.IsToaThuocExits(phienKhamId);
	}
	public async Task<int> TaoToaThuocAsync(ToaThuocRequestDTO dto)
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

	public async Task<ToaThuocReadModel?> GetByPhienKham(int phienKhamID)
	{
		var toaThuoc = await _toaThuocRepo.GetByPhienKhamAsync(phienKhamID);
		if (toaThuoc == null) return null;
		return new ToaThuocReadModel
		{
			ToaThuocID = toaThuoc.ToaThuocID,
			NguoiLap = toaThuoc.NguoiLap,
			NgayLap = toaThuoc.NgayLap,
			GhiChu = toaThuoc.GhiChu
		};
	}
	public async Task<List<ChiTietToaThuocReadModel>> GetByToaThuoc(int toaThuocId)
		=> await _chiTietRepo.GetByToaThuocAsync(toaThuocId);
	public async Task<PagedResult<ToaThuocReadModel>> GetPagedAsync(int page, int size)
	{
		var (items, total) = await _toaThuocRepo.GetPagedAsync(page, size);

		return new PagedResult<ToaThuocReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
	}
	public async Task UpdateToaThuocAsync(int toaThuocID, List<ChiTietToaThuocRequestDTO> chiTiet)
	{
		var insertList = new List<ChiTietToaThuoc>();
		var updateList = new List<ChiTietToaThuoc>();
		var deleteList = new List<int>();
		var existedThuocIds = await _chiTietRepo.GetThuocIdsAsync(toaThuocID);
		foreach (var x in chiTiet)
		{
			if (x.SoLuong == 0)
			{
				deleteList.Add(x.ThuocID);
				continue;
			}
			var entity = new ChiTietToaThuoc(x.ThuocID,	x.LieuDung, x.SoLuong);
			if (existedThuocIds.Contains(x.ThuocID))
				updateList.Add(entity);
			else
				insertList.Add(entity);

		}
		if (insertList.Any())
			await _chiTietRepo.AddAsync(toaThuocID, insertList);
		if (updateList.Any())
			await _chiTietRepo.UpdateAsync(toaThuocID, updateList);

		foreach (var thuocId in deleteList)
			await _chiTietRepo.DeleteAsync(toaThuocID, thuocId);

		var count = await _chiTietRepo.CountAsync(toaThuocID);

		if (count == 0)
			await _toaThuocRepo.DeleteAsync(toaThuocID);
	}
}
