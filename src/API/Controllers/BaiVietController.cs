using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/baiviet")]
[Authorize]
public class BaiVietController : ControllerBase
{
    private readonly BaiVietService _service;

    public BaiVietController(BaiVietService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<BaiVietListReadModel>>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var result = await _service.GetPagedAsync(page, size);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BaiVietReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("loaibenh/{id}")]
    public async Task<ActionResult<ApiResponse<List<BaiVietListReadModel>>>> GetByLoaiBenh(int id)
    {
        var result = await _service.GetByLoaiBenhAsync(id);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("top")]
    public async Task<ActionResult<ApiResponse<List<BaiVietListReadModel>>>> GetTopLuotXem(
        [FromQuery] int top = 5)
    {
        var result = await _service.GetTopLuotXemAsync(top);
        return Ok(result);
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThemBaiVietDTO dto)
    {
        var result = await _service.ThemAsync(dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CapNhatBaiVietDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _service.XoaAsync(id);
        return Ok(result);
    }
}