using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _service;

    public DashboardController(DashboardService service)
    {
        _service = service;
    }
    [HttpGet("kpi")]
    public async Task<IActionResult> GetKpi()
        => Ok(await _service.GetKpiAsync());
    [HttpGet("ca-kham-tuan")]
    public async Task<IActionResult> GetCaKhamTheoTuan()
        => Ok(await _service.GetCaKhamTheoTuanAsync());

    [HttpGet("trang-thai-ca-kham")]
    public async Task<IActionResult> GetTrangThaiCaKham([FromQuery] int? year, [FromQuery] int? month)
        => Ok(await _service.GetTrangThaiCaKhamAsync(year, month));

    [HttpGet("top-benh")]
    public async Task<IActionResult> GetTopBenh([FromQuery] int? year, [FromQuery] int? month)
        => Ok(await _service.GetTopBenhAsync(year, month));

    [HttpGet("top-bac-si")]
    public async Task<IActionResult> GetTopBacSi([FromQuery] int? year, [FromQuery] int? month)
        => Ok(await _service.GetTopBacSiAsync(year, month));

    [HttpGet("lieu-trinh")]
    public async Task<IActionResult> GetLieuTrinh()
        => Ok(await _service.GetLieuTrinhDangDieuTriAsync());

    [HttpGet("hoat-dong")]
    public async Task<IActionResult> GetHoatDong()
        => Ok(await _service.GetHoatDongGanDayAsync());
}
