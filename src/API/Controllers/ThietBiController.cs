using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/thietbi")]
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
	public async Task<IActionResult> Create(ThietBiRequest dto)
	{
		var response = await _service.AddAsync(dto);
		if (!response.Success)
			return BadRequest(response);
		return CreatedAtAction(nameof(Detail), new { id = response.Data }, response);
	}
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, ThietBiRequest dto)
	{
		var response = await _service.UpdateAsync(id, dto);
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
	[HttpPost("import/preview")]
	public async Task<IActionResult> PreviewImport(
	IFormFile file,
	[FromQuery] string sheet)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));

		if (string.IsNullOrWhiteSpace(sheet))
			return BadRequest(ApiResponse<string>.Fail("Sheet không hợp lệ"));

		using var stream = file.OpenReadStream();

		var response = await _service.PreviewImport(stream, sheet);

		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost("import/confirm")]
	public async Task<IActionResult> Import(
	[FromBody] List<ThietBiImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var response = await _service.Import(list);

		if (!response.Success)
			return BadRequest(response);

		return Ok(response);
	}
}