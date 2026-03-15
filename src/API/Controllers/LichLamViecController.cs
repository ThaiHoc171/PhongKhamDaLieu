using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/lich-lam-viec")]
[Authorize]
public class LichLamViecController : ControllerBase
{
	private readonly LichLamViecService _service;
	public LichLamViecController(LichLamViecService service)
	{
		_service = service;
	}
	[Authorize(Policy = "LICHLAMVIEC_CREATE")]
	[HttpPost("import")]
	public async Task<IActionResult> ImportExcel(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));
		using var stream = file.OpenReadStream();
		var response = await _service.ImportExcelAsync(stream);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "LICHLAMVIEC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, LichLamViecUpdateRequestDTO request)
	{
		var response = await _service.UpdateAsync(id, request);
		if (!response.Success)
			return NotFound(response);
		return Ok(response);
	}
	[Authorize(Policy = "LICHLAMVIEC_VIEW")]
	[HttpGet("{id}")]
	public async Task<IActionResult> Detail(int id)
	{
		var response = await _service.GetDetailAsync(id);
		if (!response.Success)
			return NotFound(response);
		return Ok(response);
	}
	[Authorize(Policy = "LICHLAMVIEC_VIEW")]
	[HttpGet]
	public async Task<IActionResult> List([FromQuery] int page = 0)
	{
		var response = await _service.GetWeekAsync(page);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "LICHLAMVIEC_VIEW")]
	[HttpGet("nhan-vien/{nhanVienID}")]
	public async Task<IActionResult> GetByNhanVien(int nhanVienID, [FromQuery] int page = 0)
	{
		var response = await _service.GetWeekByNhanVienAsync(nhanVienID, page);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
}