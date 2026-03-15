using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CanLamSangController : ControllerBase
{
    private readonly CanLamSangService _service;

    public CanLamSangController(CanLamSangService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] string? loaiXetNghiem = null,
        [FromQuery] string? trangThai = null)
    {
        return Ok(await _service.GetPagedAsync(pageNumber, pageSize, loaiXetNghiem, trangThai));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("loaixetnghiem")]
    public async Task<IActionResult> GetByLoaiXetNghiem([FromQuery] string loai)
    {
        return Ok(await _service.GetByLoaiXetNghiemAsync(loai));
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15)
    {
        return Ok(await _service.SearchAsync(keyword, pageNumber, pageSize));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CanLamSangRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CanLamSangUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
    [HttpPost("import-excel")]
    public async Task<IActionResult> ImportExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File không hợp lệ");

        using var stream = file.OpenReadStream();
        var result = await _service.ImportExcelAsync(stream);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}