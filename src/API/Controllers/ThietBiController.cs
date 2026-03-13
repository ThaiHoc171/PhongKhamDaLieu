using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/thietbi")]
[Authorize]
public class ThietBiController : ControllerBase
{
    private readonly ThietBiService _service;

    public ThietBiController(ThietBiService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThietBiRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, ThietBiUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ThietBiListReadModel>>>> GetPaged(
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ThietBiReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<ThietBiListReadModel>>>> Search(
        string keyword,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
        return Ok(result);
    }
}