using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("api/pcnthietbi")]
[Authorize]
public class PCNThietBiController : ControllerBase
{
    private readonly PCNThietBiService _service;
    public PCNThietBiController(PCNThietBiService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] PCNThietBiRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PCNThietBiUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _service.XoaAsync(id);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PCNThietBiReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [HttpGet("phong/{phongId}")]
    public async Task<ActionResult<ApiResponse<List<PCNThietBiReadModel>>>> GetByPhong(int phongId)
    {
        var result = await _service.GetByPhongAsync(phongId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<PCNThietBiListReadModel>>>> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] int? phongChucNangID = null)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize, phongChucNangID);
        return Ok(result);
    }
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<PCNThietBiListReadModel>>>> Search(
        [FromQuery] string keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] int? phongChucNangID = null)
    {
        var result = await _service.SearchAsync(keyword, pageNumber, pageSize, phongChucNangID);
        return Ok(result);
    }
}