using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ThuocController : ControllerBase
{
    private readonly ThuocService _service;

    public ThuocController(ThuocService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> DanhSach(int page = 1, int size = 15)
        => Ok(await _service.DanhSachAsync(page, size));

    [HttpGet("timkiem")]
    public async Task<IActionResult> TimKiem(string kw)
        => Ok(await _service.TimKiemAsync(kw));

    [HttpGet("combobox")]
    public async Task<IActionResult> Combobox()
        => Ok(await _service.ComboboxAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Them(ThuocRequestDTO dto)
    {
        await _service.ThemAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(int id, ThuocRequestDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok();
    }
}
