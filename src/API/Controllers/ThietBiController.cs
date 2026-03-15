using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/thiet-bi")]
[Authorize]
public class ThietBiController : ControllerBase
{
	private readonly ThietBiService _service;
	public ThietBiController(ThietBiService service)
	{
		_service = service;
	}
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<IActionResult> Create(ThietBiRequestDTO dto)
	{
		var response = await _service.AddAsync(dto);
		if (!response.Success)
			return BadRequest(response);
		return CreatedAtAction(nameof(Detail), new { id = response.Data }, response);
	}
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, ThietBiUpdateDTO dto)
	{
		var response = await _service.UpdateAsync(id, dto);
		if (!response.Success)
			return NotFound(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_DELETE")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var response = await _service.DeleteAsync(id);
		if (!response.Success)
			return NotFound(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<IActionResult> Detail(int id)
	{
		var response = await _service.GetDetailAsync(id);
		if (!response.Success)
			return NotFound(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<IActionResult> List(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var response = await _service.GetPagedAsync(page, size);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<IActionResult> Search(
		[FromQuery] string keyword,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var response = await _service.SearchAsync(keyword, page, size);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_CREATE")]
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
}