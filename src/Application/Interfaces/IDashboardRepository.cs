using Application.DTOs.Dashboard;

namespace Application.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardKpiReadModel>              GetKpiAsync(DateTime today);
    Task<List<CaKhamTheoNgayReadModel>>      GetCaKhamTheoTuanAsync(DateTime endDate);
    Task<List<TrangThaiCaKhamReadModel>>     GetTrangThaiCaKhamAsync(int year, int month);
    Task<List<TopBenhReadModel>>             GetTopBenhAsync(int year, int month, int top = 5);
    Task<List<TopBacSiReadModel>>            GetTopBacSiAsync(int year, int month, int top = 4);
    Task<List<LieuTrinhProgressReadModel>>   GetLieuTrinhDangDieuTriAsync(int top = 4);
    Task<List<HoatDongReadModel>>            GetHoatDongGanDayAsync(int take = 6);
}
