using Application.DTOs;
using Application.Services;
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
	[HttpGet("checklist/{chucVuId}")]
	public async Task<IActionResult> GetChecklist(int chucVuId)
	{
		var result = await _service.GetChecklistAsync(chucVuId);
		return Ok(result);
	}
	[HttpPut("update")]
	public async Task<IActionResult> Update(ChucVuQuyenDTO dto)
	{
		var result = await _service.UpdateAsync(dto);
		return Ok(result);
	}
	[HttpPost]
	public async Task<IActionResult> Add(int chucVuId, int quyenId)
	{
		var result = await _service.AddAsync(chucVuId, quyenId);
		return Ok(result);
	}
	[HttpDelete]
	public async Task<IActionResult> Delete(int chucVuId, int quyenId)
	{
		var result = await _service.DeleteAsync(chucVuId, quyenId);
		return Ok(result);
	}
}