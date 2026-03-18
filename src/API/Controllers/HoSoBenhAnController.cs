using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/hosobenhan")]
public class HoSoBenhAnController : ControllerBase
{
    private readonly HoSoBenhAnService _service;

    public HoSoBenhAnController(HoSoBenhAnService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _service.GetPagedAsync(page, size);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("benhnhan/{id}")]
    public async Task<IActionResult> GetByBenhNhanId(int id)
    {
        var result = await _service.GetByBenhNhanIdAsync(id);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _service.SearchAsync(keyword, page, size);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HoSoBenhAnRequestDTO dto)
    {
        var result = await _service.TaoAsync(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] HoSoBenhAnUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}