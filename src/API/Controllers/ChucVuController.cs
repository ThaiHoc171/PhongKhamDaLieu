using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/chucvu")]
[Authorize]
public class ChucVuController : ControllerBase
{
    private readonly ChucVuService _service;

    public ChucVuController(ChucVuService service)
    {
        _service = service;
    }

    [Authorize(Policy = "CHUCVU_VIEW")]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ChucVuListReadModel>>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string? trangThai = null)
    {
        var result = await _service.GetPagedAsync(page, size, trangThai);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ChucVuReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<ChucVuListReadModel>>>> Search(
        [FromQuery] string keyword,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var result = await _service.SearchAsync(keyword, page, size);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("name/{id}")]
    public async Task<ActionResult<ApiResponse<string>>> GetNameById(int id)
    {
        var result = await _service.GetNameByIdAsync(id);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("nhanvien/{id}")]
    public async Task<ActionResult<ApiResponse<List<ChucVuListReadModel>>>> GetByNhanVienId(int id)
    {
        var result = await _service.GetByNhanVienIdAsync(id);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetIdAndName()
    {
        var result = await _service.GetIdAndNameAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ChucVuRequestDTO dto)
    {
        var result = await _service.ThemAsync(dto);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ChucVuRequestDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}/trangthai")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateTrangThai(int id, [FromBody] string trangThai)
    {
        var result = await _service.CapNhatTrangThaiAsync(id, trangThai);
        return Ok(result);
    }
}