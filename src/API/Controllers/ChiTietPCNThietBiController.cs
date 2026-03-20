using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chitiet-pcntb")]
[Authorize]
public class ChiTietPCNThietBiController : ControllerBase
{
    private readonly ChiTietPCNThietBiService _service;
    public ChiTietPCNThietBiController(ChiTietPCNThietBiService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ChiTietPCNThietBiRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ChiTietPCNThietBiUpdateDTO dto)
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
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ChiTietPCNThietBiReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ChiTietPCNThietBiListReadModel>>>> GetPaged(
        [FromQuery] int pcnTbId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(pcnTbId, pageNumber, pageSize);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<ChiTietPCNThietBiListReadModel>>>> Search(
        [FromQuery] int pcnTbId,
        [FromQuery] string keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.SearchAsync(pcnTbId, keyword, pageNumber, pageSize);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("combobox/{pcnTbId}")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox(int pcnTbId)
    {
        var result = await _service.GetComboboxAsync(pcnTbId);
        return Ok(result);
    }
}