using Application.Common;
using Application.DTOs.ThongKe;
using Application.Interfaces;

namespace Application.Services;

public class ThongKeService
{
    private readonly IThongKeRepository _repo;

    public ThongKeService(IThongKeRepository repo)
    {
        _repo = repo;
    }

    private static (DateTime tuNgay, DateTime denNgay) ResolveRange(ThongKeFilterRequest f)
    {
        if (f.TuNgay.HasValue && f.DenNgay.HasValue)
            return (f.TuNgay.Value.Date, f.DenNgay.Value.Date);

        var today = DateTime.Today;
        var nam   = f.Nam   ?? today.Year;
        var thang = f.Thang ?? today.Month;

        return f.LoaiKhoang.ToLower() switch
        {
            "day"   => (today, today),
            "week"  => (today.AddDays(-(int)today.DayOfWeek + 1), today),
            "year"  => (new DateTime(nam, 1, 1), new DateTime(nam, 12, 31)),
            _       => (new DateTime(nam, thang, 1),                          // month (default)
                        new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang)))
        };
    }

    public async Task<ApiResponse<TongQuanBenhNhanReadModel>> GetTongQuanBenhNhanAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetTongQuanBenhNhanAsync(tu, den);
        return ApiResponse<TongQuanBenhNhanReadModel>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<BenhNhanTheoNgayReadModel>>> GetBenhNhanTheoNgayAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetBenhNhanTheoNgayAsync(tu, den);
        return ApiResponse<List<BenhNhanTheoNgayReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<BenhNhanTheoGioiTinhReadModel>>> GetBenhNhanTheoGioiTinhAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetBenhNhanTheoGioiTinhAsync(tu, den);
        return ApiResponse<List<BenhNhanTheoGioiTinhReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<BenhNhanTheoDoTuoiReadModel>>> GetBenhNhanTheoDoTuoiAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetBenhNhanTheoDoTuoiAsync(tu, den);
        return ApiResponse<List<BenhNhanTheoDoTuoiReadModel>>.SuccessResponse(data);
    }

    // ── Ca khám ────────────────────────────────────────────────────────────

    public async Task<ApiResponse<TongQuanCaKhamReadModel>> GetTongQuanCaKhamAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetTongQuanCaKhamAsync(tu, den);
        return ApiResponse<TongQuanCaKhamReadModel>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<CaKhamTheoKhoangReadModel>>> GetCaKhamTheoKhoangAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetCaKhamTheoKhoangAsync(tu, den, f.LoaiKhoang);
        return ApiResponse<List<CaKhamTheoKhoangReadModel>>.SuccessResponse(data);
    }

    // ── Phiên khám ─────────────────────────────────────────────────────────

    public async Task<ApiResponse<TongQuanPhienKhamReadModel>> GetTongQuanPhienKhamAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetTongQuanPhienKhamAsync(tu, den);
        return ApiResponse<TongQuanPhienKhamReadModel>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<PhienKhamTheoNgayReadModel>>> GetPhienKhamTheoNgayAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetPhienKhamTheoNgayAsync(tu, den);
        return ApiResponse<List<PhienKhamTheoNgayReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<PhienKhamTheoPhongReadModel>>> GetPhienKhamTheoPhongAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetPhienKhamTheoPhongAsync(tu, den);
        return ApiResponse<List<PhienKhamTheoPhongReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<PhienKhamTheoLoaiBenhReadModel>>> GetPhienKhamTheoLoaiBenhAsync(ThongKeFilterRequest f, int top = 10)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetPhienKhamTheoLoaiBenhAsync(tu, den, top);
        return ApiResponse<List<PhienKhamTheoLoaiBenhReadModel>>.SuccessResponse(data);
    }

    // ── Toa thuốc ──────────────────────────────────────────────────────────

    public async Task<ApiResponse<TongQuanToaThuocReadModel>> GetTongQuanToaThuocAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetTongQuanToaThuocAsync(tu, den);
        return ApiResponse<TongQuanToaThuocReadModel>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<ToaThuocTheoKhoangReadModel>>> GetToaThuocTheoKhoangAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetToaThuocTheoKhoangAsync(tu, den, f.LoaiKhoang);
        return ApiResponse<List<ToaThuocTheoKhoangReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<TopThuocReadModel>>> GetTopThuocAsync(ThongKeFilterRequest f, int top = 10)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetTopThuocAsync(tu, den, top);
        return ApiResponse<List<TopThuocReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<TopBacSiKeDonReadModel>>> GetTopBacSiKeDonAsync(ThongKeFilterRequest f, int top = 5)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetTopBacSiKeDonAsync(tu, den, top);
        return ApiResponse<List<TopBacSiKeDonReadModel>>.SuccessResponse(data);
    }

    // ── Nhân viên & hiệu suất ──────────────────────────────────────────────

    public async Task<ApiResponse<TongQuanNhanVienReadModel>> GetTongQuanNhanVienAsync()
    {
        var data = await _repo.GetTongQuanNhanVienAsync();
        return ApiResponse<TongQuanNhanVienReadModel>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<NhanVienTheoChucVuReadModel>>> GetNhanVienTheoChucVuAsync()
    {
        var data = await _repo.GetNhanVienTheoChucVuAsync();
        return ApiResponse<List<NhanVienTheoChucVuReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<NhanVienTheoPhongReadModel>>> GetNhanVienTheoPhongAsync()
    {
        var data = await _repo.GetNhanVienTheoPhongAsync();
        return ApiResponse<List<NhanVienTheoPhongReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<HieuSuatBacSiReadModel>>> GetHieuSuatBacSiAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetHieuSuatBacSiAsync(tu, den);
        return ApiResponse<List<HieuSuatBacSiReadModel>>.SuccessResponse(data);
    }

    public async Task<ApiResponse<List<NgayNghiNhanVienReadModel>>> GetNgayNghiNhanVienAsync(ThongKeFilterRequest f)
    {
        var (tu, den) = ResolveRange(f);
        var data = await _repo.GetNgayNghiNhanVienAsync(tu, den);
        return ApiResponse<List<NgayNghiNhanVienReadModel>>.SuccessResponse(data);
    }
}
