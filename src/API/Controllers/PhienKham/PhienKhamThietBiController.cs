using Application.DTOs;
using Application.Services;
using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "PHIENKHAM_UPDATE")]
public class PhienKhamThietBiController : ControllerBase
{
	private readonly PhienKhamThietBiService _service;
	public PhienKhamThietBiController(PhienKhamThietBiService service)
	{
		_service = service;
	}
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamThietBiReadModel>>>> LayDanhSachTheoPhienKham(int phienKhamId)
	{
		var result = await _service.DanhSachTheoPhienKhamAsync(phienKhamId);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<object>>> ThemMoi([FromBody] PhienKhamThietBiRequestDTO dto)
	{
		var result = await _service.ThemMoiAsync(dto);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhat(int id, [FromBody] string? ghiChu)
	{
		var result = await _service.CapNhatAsync(id, ghiChu);
		return Ok(result);
	}
}