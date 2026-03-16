using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/khunggiokham")]
[Authorize]
public class KhungGioKhamController : ControllerBase
{
    private readonly KhungGioKhamService _service;

    public KhungGioKhamController(KhungGioKhamService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] KhungGioKhamRequestDTO dto)
    {
        var result = await _service.TaoAsync(dto);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] KhungGioKhamRequestDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<KhungGioKhamListReadModel>>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<KhungGioKhamReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
    {
        var result = await _service.GetComboboxAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> Count()
    {
        var result = await _service.CountAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("calamviec/{caLamViec}")]
    public async Task<ActionResult<ApiResponse<List<int>>>> GetByCaLamViec(int caLamViec)
    {
        var result = await _service.GetByCaLamViecAsync(caLamViec);
        return Ok(result);
    }
}