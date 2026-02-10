using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaiVietController : ControllerBase
{
    private readonly BaiVietService _service;

    public BaiVietController(BaiVietService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Tao([FromBody] ThemBaiVietDTO dto)
    {
        var id = await _service.ThemBaiVietAsync(dto);
        return Ok(new { Message = "Tạo bài viết thành công", BaiVietID = id });
    }

    [HttpGet]
    public async Task<IActionResult> DanhSach()
    {
        return Ok(await _service.DanhSachAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> LayTheoId(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Bài viết không tồn tại." });

        return Ok(result);
    }

    [HttpGet("Luotxem")]
    public async Task<IActionResult> SapXepTheoLuotXem()
    {
        var result = await _service.GetByLuotXemAsync();
        return Ok(result);
    }

    [HttpGet("LoaiBenh/{loaiBenhID:int}")]
    public async Task<IActionResult> LayTheoLoaiBenh(int loaiBenhID)
    {
        var result = await _service.GetByLoaiBenhAsync(loaiBenhID);
        if (result == null)
            return NotFound(new { message = "Loại bệnh không tồn tại hoặc chưa có bài viết về loại bệnh này!." });

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(
       int id,
       [FromBody] CapNhatBaiVietDTO dto)
    {
        var result = await _service.CapNhatBaiVietAsync(id, dto);

        if (!result)
            return NotFound(new { Message = "Bài viết không tồn tại" });

        return Ok(new { Message = "Cập nhật bài viết thành công" });
    }

    [HttpPut("{id}/luotxem")]
    public async Task<IActionResult> TangLuotXem(int id)
    {
        var ok = await _service.TangLuotXemAsync(id);
        if (!ok) return NotFound();
        return Ok();
    }
}
