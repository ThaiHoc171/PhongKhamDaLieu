using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/thuoc")]
[Authorize]
public class ThuocController : ControllerBase
{
    private readonly ThuocService _service;

    public ThuocController(ThuocService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThuocRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, ThuocUpdateDTO dto)
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
    public async Task<ActionResult<ApiResponse<PagedResult<ThuocListReadModel>>>> GetPaged(
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ThuocReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<ThuocListReadModel>>>> Search(
        string keyword,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
        return Ok(result);
    }
}