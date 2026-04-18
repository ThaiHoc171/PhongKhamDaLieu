using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChucVuQuyenController : ControllerBase
{
	private readonly ChucVuQuyenService _service;
	public ChucVuQuyenController(ChucVuQuyenService service)
	{
		_service = service;
	}
	[Authorize(Policy = "NHANSU_READ")]
	[HttpGet("checklist/{chucVuId}")]
	public async Task<IActionResult> GetChecklist(int chucVuId)
	{
		var result = await _service.GetChecklistAsync(chucVuId);
		return Ok(result);
	}
	[Authorize(Policy = "NHANSU_WRITE")]
	[HttpPut("update")]
	public async Task<IActionResult> Update(ChucVuQuyenDTO dto)
	{
		var result = await _service.UpdateAsync(dto);
		return Ok(result);
	}
}