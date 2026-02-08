using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LieuTrinh_BuoiDieuTriController : ControllerBase
{
    private readonly LieuTrinh_BuoiDieuTriService _service;

    public LieuTrinh_BuoiDieuTriController(LieuTrinh_BuoiDieuTriService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> TaoBuoiDieuTri([FromBody] TaoBuoiDieuTriDTO dto)
    {
        try
        {
            await _service.TaoBuoiDieuTriAsync(dto);
            return Ok(new { message = "Tạo buổi điều trị thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{buoiDieuTriID:int}/trang-thai")]
    public async Task<IActionResult> CapNhatTrangThai(
        int buoiDieuTriID,
        [FromBody] CapNhatTrangThaiBuoiDieuTriDTO dto)
    {
        try
        {
            var result = await _service.CapNhatTrangThaiAsync(buoiDieuTriID, dto);
            if (!result)
                return NotFound(new { message = "Buổi điều trị không tồn tại" });

            return Ok(new { message = "Cập nhật trạng thái thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("lieutrinh/{lieuTrinhID:int}")]
    public async Task<IActionResult> LayTheoLieuTrinh(int lieuTrinhID)
    {
        var result = await _service.LayTheoLieuTrinhAsync(lieuTrinhID);
        return Ok(result);
    }

    [HttpGet("loc/ngay-du-kien")]
    public async Task<IActionResult> LocTheoNgayDuKien(
        [FromQuery] DateTime ngay,
        [FromQuery] string trangThai)
    {
        var result = await _service.LocDuKienAsync(ngay, trangThai);
        return Ok(result);
    }

    [HttpGet("loc/ngay-thuc-hien")]
    public async Task<IActionResult> LocTheoNgayThucHien(
        [FromQuery] DateTime ngay,
        [FromQuery] string trangThai)
    {
        var result = await _service.LocBatDauAsync(ngay, trangThai);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
}
