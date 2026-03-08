using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class TaiKhamController : ControllerBase
{
    private readonly TaiKhamService _service;

    public TaiKhamController(TaiKhamService service)
    {
        _service = service;
    }

	[Authorize(Policy = "LeTanOnly")]
	[HttpGet]
    public async Task<IActionResult> LayDanhSach()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet("{id:int}")]
    public async Task<IActionResult> LayTheoId(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Lịch tái khám không tồn tại." });

        return Ok(result);
    }

	[Authorize]
	[HttpGet("benhnhan/{benhNhanId:int}")]
    public async Task<IActionResult> LayTheoBenhNhanId(int benhNhanId)
    {
        var result = await _service.GetListByBenhNhanAsync(benhNhanId);
        if (result == null)
            return NotFound(new { message = "Bệnh nhân không tồn tại hoặc chưa có lịch tái khám." });

        return Ok(result);
    }
    [Authorize]
    [HttpGet("benhnhan/{benhNhanId:int}/pending")]
    public async Task<IActionResult> LayTaiKhamChoXuLy(int benhNhanId)
    {
        var taiKhamId = await _service.GetIdByBenhNhanIdAsync(benhNhanId);

        if (taiKhamId == null)
            return NotFound(new { message = "Không có lịch tái khám chờ xử lý." });

        return Ok(new
        {
            taiKhamId = taiKhamId
        });
    }

    [Authorize(Policy = "LeTanOnly")]
	[HttpGet("filter")]
    public async Task<IActionResult> Loc(DateTime ngayDuKien, string trangThai)
    {
        var result = await _service.LocAsync(ngayDuKien, trangThai);
        if (result == null)
            return NotFound(new { message = "Ngày dự kiến hoặc trạng thái không tồn tại." });

        return Ok(result);
    }

	[Authorize(Policy = "BacSiOnly")]
	[HttpPost]
    public async Task<IActionResult> Them([FromBody] TaoTaiKhamDTO dto)
    {
        await _service.TaoTaiKhamAsync(dto);
        return Ok(new { message = "Thêm lịch tái khám thành công." });
    }

	[Authorize]
	[HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(
        int id,
        [FromBody] CapNhatTaiKhamDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);

        if (!result)
            return NotFound(new { Message = "Lịch tái khám không tồn tại" });

        return Ok(new { Message = "Cập nhật lịch tái khám thành công" });
    }
}
