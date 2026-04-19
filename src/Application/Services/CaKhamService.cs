using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class CaKhamService
{
	private readonly ICaKhamRepository _repo;
	private readonly IKhungGioKhamRepository _khunggio;
	private readonly ITaiKhamRepository _taiKham;
	private readonly ILichLamViecRepository _lich;
    private readonly IFcmService _fcmService;
	private const int MAX_KHAM_PER_SLOT = 5;
	private const int MAX_DIEUTRI_PER_SLOT = 1;
	public CaKhamService(ICaKhamRepository repo, IKhungGioKhamRepository khungGio, ITaiKhamRepository taiKham, ILichLamViecRepository lich,IFcmService fcmService)
	{
		_repo = repo;
		_khunggio = khungGio;
		_taiKham = taiKham;
		_lich = lich;
		_fcmService = fcmService;
    }
	public async Task<ApiResponse<int>> GenerateAsync(CaKhamRequest request)
	{
		if (request.TuNgay > request.DenNgay)
			return ApiResponse<int>.Fail("Khoảng ngày không hợp lệ");

		if (request.TuNgay.Date < DateTime.Today)
			return ApiResponse<int>.Fail("Không tạo ca trong quá khứ");

		int created = 0;

		var khungGios = await _khunggio.GetAllAsync();

		for (var day = request.TuNgay.Date; day <= request.DenNgay.Date; day = day.AddDays(1))
		{
			var lichs = await _lich.GetByDateAsync(day);

			foreach (var khung in khungGios)
			{
				var bacSiTrongCa = lichs
					.Where(x => x.CaLamViec == khung.CaLamViec)
					.ToList();

				if (!bacSiTrongCa.Any())
					continue;

				var bsKham = bacSiTrongCa.Where(x => x.ChucVuID == 1).ToList();
				var bsDieuTri = bacSiTrongCa.Where(x => x.ChucVuID == 2).ToList();

				if (bsKham.Any())
				{
					for (int i = 0; i < MAX_KHAM_PER_SLOT; i++)
					{
						var bs = bsKham[i % bsKham.Count];

						await _repo.InsertAsync(new CaKham
						(
							"Khám", bs.NhanVienID, bs.LichLamViecID, bs.PhongChucNangID, khung.KhungGioID, day
						));

						created++;
					}
				}
				if (bsDieuTri.Any())
				{
					var bs = bsDieuTri.First(); 

					await _repo.InsertAsync(new CaKham
					(
						"Điều trị", bs.NhanVienID, bs.LichLamViecID, bs.PhongChucNangID, khung.KhungGioID, day
					));

					created++;
				}
			}
		}

		return ApiResponse<int>.SuccessResponse(created, "Tạo ca khám thành công");
	}
	public async Task<ApiResponse<bool>> StatusAsync(int caKhamId, string trangThai, string? ghiChu)
    {
        if (caKhamId <= 0)
            return ApiResponse<bool>.Fail("Ca khám không hợp lệ");

        if (string.IsNullOrWhiteSpace(trangThai))
            return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");

        try
        {
            await _repo.UpdateTrangThaiAsync(caKhamId, trangThai, ghiChu ?? "");
			if (trangThai == TrangThaiCaKham.DaXacNhan.ToDbValue())
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
	public async Task<ApiResponse<PagedResult<CaKhamListReadModel>>> GetChoXacNhanAsync(int pageNumber, int pageSize)
	{
		var (items, total) = await _repo.GetChoXacNhanAsync(pageNumber, pageSize);

		var result = new PagedResult<CaKhamListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = pageNumber,
			PageSize = pageSize
		};

		return ApiResponse<PagedResult<CaKhamListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<CaKhamReadModel>> GetDetailAsync(int caKhamId)
	{
		var data = await _repo.GetDetailAsync(caKhamId);
		if (data == null)
			return ApiResponse<CaKhamReadModel>.Fail("Không tìm thấy dữ liệu");
		return ApiResponse<CaKhamReadModel>.SuccessResponse(data);
	}
	public async Task<ApiResponse<PagedResult<CaKhamListReadModel>>> 
		GetPagedAsync( DateTime ngayKham, string trangThai, string loaiCaKham, int? nhanViednId, int pageNumber, int pageSize)
	{
		var (items, total) = await _repo.GetPagedAsync(
			ngayKham,
			trangThai,
			loaiCaKham,
			nhanViednId,
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

			var taiKhamId = await _taiKham.GetIdByCaKham(caKhamId);

			if (taiKhamId != 0)
			{
				var taikham = await _taiKham.GetByIdAsync(taiKhamId);
				if (taikham != null)
				{
					taikham.Cancel();
					await _taiKham.UpdateAsync(taikham);
				}
			}

			return ApiResponse<bool>.SuccessResponse(true, "Hủy đăng ký thành công");
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
    public async Task<ApiResponse<List<int>>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham, int? nhanVienId)
    {
        if (string.IsNullOrWhiteSpace(loaiCaKham))
            return ApiResponse<List<int>>.Fail("Loại ca khám không hợp lệ");

        try
        {
            var result = await _repo.GetKhungGioConTrongAsync(ngayKham, loaiCaKham, nhanVienId);
            return ApiResponse<List<int>>.SuccessResponse(result);
        }
        catch (Exception ex)
        {
            return ApiResponse<List<int>>.Fail(ex.Message);
        }
    }
    public async Task<ApiResponse<int>> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham, int? nhanVienId)
    {
        if (khungGioId <= 0)
            return ApiResponse<int>.Fail("Khung giờ không hợp lệ");

        var result = await _repo.GetCaKhamAsync(ngayKham, khungGioId, loaiCaKham, nhanVienId);

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