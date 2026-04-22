using Application.Common;
using Application.DTOs.Dashboard;
using Application.Interfaces;

namespace Application.Services;

public class DashboardService
{
    private readonly IDashboardRepository _repo;

    public DashboardService(IDashboardRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<DashboardKpiReadModel>> GetKpiAsync()
    {
        var result = await _repo.GetKpiAsync(DateTime.Today);
        return ApiResponse<DashboardKpiReadModel>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<CaKhamTheoNgayReadModel>>> GetCaKhamTheoTuanAsync()
    {
        var result = await _repo.GetCaKhamTheoTuanAsync(DateTime.Today);
        return ApiResponse<List<CaKhamTheoNgayReadModel>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<TrangThaiCaKhamReadModel>>> GetTrangThaiCaKhamAsync(int? year, int? month)
    {
        var now = DateTime.Today;
        var result = await _repo.GetTrangThaiCaKhamAsync(year ?? now.Year, month ?? now.Month);
        return ApiResponse<List<TrangThaiCaKhamReadModel>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<TopBenhReadModel>>> GetTopBenhAsync(int? year, int? month)
    {
        var now = DateTime.Today;
        var result = await _repo.GetTopBenhAsync(year ?? now.Year, month ?? now.Month);
        return ApiResponse<List<TopBenhReadModel>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<TopBacSiReadModel>>> GetTopBacSiAsync(int? year, int? month)
    {
        var now = DateTime.Today;
        var result = await _repo.GetTopBacSiAsync(year ?? now.Year, month ?? now.Month);
        return ApiResponse<List<TopBacSiReadModel>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<LieuTrinhProgressReadModel>>> GetLieuTrinhDangDieuTriAsync()
    {
        var result = await _repo.GetLieuTrinhDangDieuTriAsync();
        return ApiResponse<List<LieuTrinhProgressReadModel>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<HoatDongReadModel>>> GetHoatDongGanDayAsync()
    {
        var result = await _repo.GetHoatDongGanDayAsync();
        return ApiResponse<List<HoatDongReadModel>>.SuccessResponse(result);
    }
}
