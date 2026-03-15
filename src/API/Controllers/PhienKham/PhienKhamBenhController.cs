using Application.DTOs;
using Application.Services;
using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PhienKhamBenhController : ControllerBase
{
	private readonly PhienKhamBenhService _service;
	public PhienKhamBenhController(PhienKhamBenhService service)
	{
		_service = service;
	}
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamBenhResponseDTO>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamBenhReadModel>>>> GetByPhienKham(int phienKhamID)
	{
		var result = await _service.GetByPhienKhamIdAsync(phienKhamID);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<object>>> ThemMoi([FromBody] PhienKhamBenhRequestDTO dto)
	{
		var result = await _service.ThemMoiAsync(dto);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhat(int id, [FromBody] PhienKhamBenhRequestDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);
		return Ok(result);
	}
}