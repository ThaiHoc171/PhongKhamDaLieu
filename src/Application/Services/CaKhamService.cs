using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
namespace Application.Services;
public class CaKhamService
{
	private readonly ICaKhamRepository _repo;
    private readonly IFcmService _fcmService;
    public CaKhamService(ICaKhamRepository repo, IFcmService fcmService)
	{
		_repo = repo;
        _fcmService = fcmService;
    }
	public async Task<ApiResponse<int>> AddAsync(CaKhamRequestDTO request)
	{
		if (string.IsNullOrWhiteSpace(request.LoaiCaKham))
			return ApiResponse<int>.Fail("Loại ca khám không hợp lệ");
		if (request.KhungGioID <= 0)
			return ApiResponse<int>.Fail("Khung giờ không hợp lệ");
		try
		{
			var entity = new CaKham(
				request.LoaiCaKham,
				request.KhungGioID,
				request.NgayKham);
			var id = await _repo.AddAsync(entity);
			return ApiResponse<int>.SuccessResponse(id, "Tạo ca khám thành công");
		}
		catch (Exception ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int caKhamId, CaKhamUpdateRequestDTO request)
	{
		var entity = await _repo.GetByIdAsync(caKhamId);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy ca khám");
		if (request.LichLamViecID.HasValue)
			entity.GanNhanVien(request.LichLamViecID.Value);
		if (request.PhongChucNangID.HasValue)
			entity.GanPhong(request.PhongChucNangID.Value);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
	}
    public async Task<ApiResponse<bool>> UpdateTrangThaiAsync(int caKhamId, string trangThai, string? ghiChu)
    {
        if (caKhamId <= 0)
            return ApiResponse<bool>.Fail("Ca khám không hợp lệ");

        if (string.IsNullOrWhiteSpace(trangThai))
            return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");

        try
        {
            await _repo.UpdateTrangThaiAsync(caKhamId, trangThai, ghiChu ?? "");
            if (trangThai == "Đã xác nhận")
            {
                var fcmToken = await _repo.GetFcmTokenByCaKhamIdAsync(caKhamId);
                if (!string.IsNullOrEmpty(fcmToken))
                {
                    await _fcmService.SendAsync(
                        fcmToken,
                        "Lịch khám đã được xác nhận",
                        "Vui lòng đến đúng giờ",
                        new Dictionary<string, string>
                        {
                        { "type", "xac_nhan_ca_kham" },
                        { "caKhamId", caKhamId.ToString() }
                        }
                    );
                }
            }
            return ApiResponse<bool>.SuccessResponse(true, "Cập nhật trạng thái thành công");
        }
        catch (Exception ex)
        {
            return ApiResponse<bool>.Fail(ex.Message);
        }
    }	
    public async Task<ApiResponse<CaKhamReadModel>> GetDetailAsync(int caKhamId)
	{
		var data = await _repo.GetDetailAsync(caKhamId);
		if (data == null)
			return ApiResponse<CaKhamReadModel>.Fail("Không tìm thấy dữ liệu");
		return ApiResponse<CaKhamReadModel>.SuccessResponse(data);
	}
	public async Task<ApiResponse<PagedResult<CaKhamListReadModel>>> 
		GetPagedAsync( DateTime ngayKham, string trangThai, string loaiCaKham, int pageNumber, int pageSize)
	{
		var (items, total) = await _repo.GetPagedAsync(
			ngayKham,
			trangThai,
			loaiCaKham,
			pageNumber,
			pageSize);
		var result = new PagedResult<CaKhamListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
		return ApiResponse<PagedResult<CaKhamListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<CaKhamListReadModel>>> 
		GetByThongTinAsync(int thongTinId, int pageNumber, int pageSize)
	{
		if (thongTinId <= 0)
			return ApiResponse<PagedResult<CaKhamListReadModel>>.Fail("Thông tin không hợp lệ");
		var (items, total) = await _repo.GetByThongTinAsync(
			thongTinId,
			pageNumber,
			pageSize);
		var result = new PagedResult<CaKhamListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
		return ApiResponse<PagedResult<CaKhamListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> RegisterAsync(int caKhamId, CaKhamRegisterDTO request)
	{
		var entity = await _repo.GetByIdAsync(caKhamId);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy ca khám");
		bool daDangKy = await _repo.CheckThongTinDaDangKyAsync(
			entity.NgayKham,
			entity.KhungGioID,
			entity.LoaiCaKham,
			request.ThongTinID);
		if (daDangKy)
			return ApiResponse<bool>.Fail("Bạn đã đăng ký ca khám này");
		try
		{
			entity.DangKyKham(
				request.ThongTinID,
				request.LyDoKham,
				request.NgayDat,
				request.GhiChu);
			await _repo.UpdateAsync(entity);
			return ApiResponse<bool>.SuccessResponse(true, "Đăng ký thành công");
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> CancelAsync(int caKhamId)
	{
		var entity = await _repo.GetByIdAsync(caKhamId);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy ca khám");
		try
		{
			entity.HuyDangKy();
			await _repo.UpdateAsync(entity);
			return ApiResponse<bool>.SuccessResponse(true,"Hủy đăng ký thành công");
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<AssignLichLamViecReport>> AssignLichLamViecAsync(DateTime tuNgay, DateTime denNgay)
	{
		if (tuNgay > denNgay)
			return ApiResponse<AssignLichLamViecReport>.Fail("Khoảng ngày không hợp lệ");
		try
		{
			var report = new AssignLichLamViecReport
			{
				TuNgay = tuNgay,
				DenNgay = denNgay
			};
			// 1 kiểm tra ca chưa gán lịch
			var count = await _repo.CountNotAssignedAsync(tuNgay, denNgay);
			report.TongCaChuaGan = count;
			if (count == 0)
			{
				report.Message = "Tất cả ca khám trong khoảng ngày này đã có lịch làm việc.";
				return ApiResponse<AssignLichLamViecReport>.SuccessResponse(report);
			}
			// 2 assign
			var updated = await _repo.AssignAsync(tuNgay, denNgay);
			report.SoCaDaCapNhat = updated;
			report.Message = $"Đã gán lịch làm việc cho {updated} ca khám.";
			return ApiResponse<AssignLichLamViecReport>.SuccessResponse(report);
		}
		catch (Exception ex)
		{
			return ApiResponse<AssignLichLamViecReport>.Fail(ex.Message);
		}
	}
    public async Task<ApiResponse<List<int>>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham)
    {
        if (string.IsNullOrWhiteSpace(loaiCaKham))
            return ApiResponse<List<int>>.Fail("Loại ca khám không hợp lệ");

        try
        {
            var result = await _repo.GetKhungGioConTrongAsync(ngayKham, loaiCaKham);
            return ApiResponse<List<int>>.SuccessResponse(result);
        }
        catch (Exception ex)
        {
            return ApiResponse<List<int>>.Fail(ex.Message);
        }
    }
    public async Task<ApiResponse<int>> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham)
    {
        if (khungGioId <= 0)
            return ApiResponse<int>.Fail("Khung giờ không hợp lệ");

        var result = await _repo.GetCaKhamAsync(ngayKham, khungGioId, loaiCaKham);

        if (result == 0)
            return ApiResponse<int>.Fail("Không còn ca trống");

        return ApiResponse<int>.SuccessResponse(result);
    }
    public async Task<ApiResponse<bool>> CheckThongTinDaDangKyAsync(DateTime ngay, int khungGioId, string loaiCaKham, int thongTinId)
    {
        var result = await _repo.CheckThongTinDaDangKyAsync(
            ngay,
            khungGioId,
            loaiCaKham,
            thongTinId);

        return ApiResponse<bool>.SuccessResponse(result);
    }
}