using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaiVietController : ControllerBase
{
    private readonly BaiVietService _service;

    public BaiVietController(BaiVietService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _service.GetPagedAsync(page, size);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
    [HttpGet("loaibenh/{id}")]
    public async Task<IActionResult> GetByLoaiBenh(int id)
    {
        var result = await _service.GetByLoaiBenhAsync(id);
        return Ok(result);
    }
    [HttpGet("top")]
    public async Task<IActionResult> GetTopLuotXem([FromQuery] int top = 5)
    {
        var result = await _service.GetTopLuotXemAsync(top);
        return Ok(result);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ThemBaiVietDTO dto)
    {
        var result = await _service.ThemBaiVietAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CapNhatBaiVietDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.XoaAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}