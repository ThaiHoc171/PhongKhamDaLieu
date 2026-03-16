using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/canlamsang")]
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
    public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangListReadModel>>>> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] string? loaiXetNghiem = null,
        [FromQuery] string? trangThai = null)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize, loaiXetNghiem, trangThai);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CanLamSangReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("loaixetnghiem")]
    public async Task<ActionResult<ApiResponse<List<CanLamSangListReadModel>>>> GetByLoaiXetNghiem(
        [FromQuery] string loai)
    {
        var result = await _service.GetByLoaiXetNghiemAsync(loai);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangListReadModel>>>> Search(
        [FromQuery] string keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15)
    {
        var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
        return Ok(result);
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CanLamSangRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CanLamSangUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPost("import-excel")]
    public async Task<ActionResult<ApiResponse<int>>> ImportExcel(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var result = await _service.ImportExcelAsync(stream);
        return Ok(result);
    }
}