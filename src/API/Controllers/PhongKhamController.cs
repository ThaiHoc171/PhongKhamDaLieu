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
        [FromBody] PhongKhamUpdateTrangThaiDTO dto)
    {
        var result = await _service.DoiTrangThaiAsync(id, dto);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("chitiet")]
    public async Task<ActionResult<ApiResponse<PhongKhamReadModel>>> GetDetail()
    {
        var result = await _service.GetDetailAsync();
        return Ok(result);
    }
}