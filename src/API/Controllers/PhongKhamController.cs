using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/phongkham")]
[Authorize]
public class PhongKhamController : ControllerBase
{
    private readonly PhongKhamService _service;

    public PhongKhamController(PhongKhamService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] PhongKhamRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PhongKhamUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}/trangthai")]
    public async Task<ActionResult<ApiResponse<bool>>> ChangeStatus(
        int id,
        [FromBody] string trangThai)
    {
        var result = await _service.DoiTrangThaiAsync(id, trangThai);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PhongKhamReadModel>>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<PhongKhamListReadModel>>>> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize);
        return Ok(result);
    }
}