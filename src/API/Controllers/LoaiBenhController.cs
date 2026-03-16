using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/loaibenh")]
[Authorize]
public class LoaiBenhController : ControllerBase
{
    private readonly LoaiBenhService _service;

    public LoaiBenhController(LoaiBenhService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
    {
        var result = await _service.GetComboboxAsync();
        return Ok(result);
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<LoaiBenhListReadModel>>>> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<LoaiBenhListReadModel>>>> Search(
        [FromQuery] string keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15)
    {
        var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<LoaiBenhReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] LoaiBenhRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] LoaiBenhUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}/ten")]
    public async Task<ActionResult<ApiResponse<string>>> GetTenBenh(int id)
    {
        var result = await _service.GetTenBenhAsync(id);
        return Ok(result);
    }
}