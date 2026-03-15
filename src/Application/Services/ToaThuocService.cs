using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
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
		=> await _toaThuocRepo.ExistsByPhienKhamAsync(phienKhamId); 
	public async Task<ApiResponse<int>> CreateAsync(ToaThuocRequestDTO dto)
	{
		if (dto.PhienKhamID <= 0)
			return ApiResponse<int>.Fail("PhienKhamID không hợp lệ");
		if (dto.NhanVienKeDonID <= 0)
			return ApiResponse<int>.Fail("NhanVienKeDonID không hợp lệ");
		if (dto.Thuoc == null || !dto.Thuoc.Any())
			return ApiResponse<int>.Fail("Toa thuốc phải có ít nhất 1 thuốc");
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID);
		if (phienKham == null)
			return ApiResponse<int>.Fail("Phiên khám không tồn tại");
		if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
			return ApiResponse<int>.Fail("Không thể kê toa khi phiên khám đã kết thúc");
		var existed = await _toaThuocRepo.ExistsByPhienKhamAsync(dto.PhienKhamID);
		if (existed)
			return ApiResponse<int>.Fail("Phiên khám đã có toa thuốc");
		ToaThuoc entity;
		try
		{
			entity = new ToaThuoc(
				dto.PhienKhamID,
				dto.NhanVienKeDonID,
				dto.GhiChu);
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
		var toaThuocID = await _toaThuocRepo.AddAsync(entity);
		List<ChiTietToaThuoc> chiTiet;
		try
		{
			chiTiet = dto.Thuoc
				.Select(x => new ChiTietToaThuoc(
					x.ThuocID,
					x.LieuDung,
					x.SoLuong))
				.ToList();
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
		await _chiTietRepo.AddAsync(toaThuocID, chiTiet);
		return ApiResponse<int>.SuccessResponse(toaThuocID);
	}
	public async Task<ApiResponse<ToaThuocReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		var result = await _toaThuocRepo.GetByPhienKhamAsync(phienKhamID);
		if (result == null)
			return ApiResponse<ToaThuocReadModel>.Fail("Không tìm thấy toa thuốc");
		var chiTiet = await _chiTietRepo.GetByToaThuocAsync(result.ToaThuocID);
		result.Thuoc = chiTiet;
		return ApiResponse<ToaThuocReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ToaThuocListReadModel>>> GetPagedAsync(int page, int size)
	{
		var (items, total) = await _toaThuocRepo.GetPagedAsync(page, size);
		return ApiResponse<PagedResult<ToaThuocListReadModel>>.SuccessResponse(
			new PagedResult<ToaThuocListReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int toaThuocID, List<ChiTietToaThuocRequestDTO> chiTiet)
	{
		if (chiTiet == null || !chiTiet.Any())
			return ApiResponse<bool>.Fail("Danh sách thuốc không hợp lệ");
		var toaThuoc = await _toaThuocRepo.GetByIdAsync(toaThuocID);
		if (toaThuoc == null)
			return ApiResponse<bool>.Fail("Toa thuốc không tồn tại");
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
			ChiTietToaThuoc entity;
			try
			{
				entity = new ChiTietToaThuoc(
					x.ThuocID,
					x.LieuDung,
					x.SoLuong);
			}
			catch (ArgumentException ex)
			{
				return ApiResponse<bool>.Fail(ex.Message);
			}
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
		return ApiResponse<bool>.SuccessResponse(true);
	}
}