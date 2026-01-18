using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaiKhamController : ControllerBase
{
    private readonly TaiKhamService _service;

    public TaiKhamController(TaiKhamService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> LayDanhSach()
    {
        var result = await _service.LayDanhSachAsync();
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Tao([FromBody] ThemTaiKhamDTO dto)
    {
        var id = await _service.TaoTaiKhamAsync(dto);
        return Ok(new { message = "Tạo lịch tái khám thành công", TaiKhamId = id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(int id, [FromBody] CapNhatTaiKhamDTO dto)
    {
        var ok = await _service.CapNhatAsync(id, dto);
        if (!ok) return NotFound();
        return Ok(new { message = "Cập nhật tái khám thành công" });
    }

    [HttpPut("{id}/trangthai")]
    public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] string trangThai)
    {
        var ok = await _service.CapNhatTrangThaiAsync(id, trangThai);
        if (!ok) return NotFound();
        return Ok(new { message = "Cập nhật trạng thái thành công" });
    }

    [HttpGet("benhnhan/{benhNhanId}")]
    public async Task<IActionResult> LayTheoBenhNhan(int benhNhanId)
    {
        return Ok(await _service.LayTheoBenhNhanAsync(benhNhanId));
    }
}
