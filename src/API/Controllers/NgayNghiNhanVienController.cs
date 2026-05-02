using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/ngaynghi")]
[Authorize]
public class NgayNghiNhanVienController : ControllerBase
{
	private readonly NgayNghiNhanVienService _service;

	public NgayNghiNhanVienController(NgayNghiNhanVienService service)
	{
		_service = service;
	}

	// ================= CREATE =================
	[HttpPost]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<IActionResult> Create([FromBody] NgayNghiRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result); // ❗ không cần CreatedAtAction vì chưa trả id thật
	}

	// ================= UPDATE =================
	[HttpPut("{id}")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<IActionResult> Update(int id, [FromBody] NgayNghiUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ================= DELETE =================
	[HttpDelete("{id}")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ================= DETAIL =================
	[HttpGet("{id}")]
	[Authorize(Policy = "LICH_READ")]
	public async Task<IActionResult> GetDetail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	[HttpGet]
	[Authorize(Policy = "LICH_READ")]
	public async Task<IActionResult> GetPaged(int page = 1, int size = 10)
	{
		return Ok(await _service.GetPagedAsync(page, size));
	}

	[HttpGet("search")]
	[Authorize(Policy = "LICH_READ")]
	public async Task<IActionResult> Search(string keyword, int page = 1, int size = 10)
	{
		return Ok(await _service.SearchAsync(keyword, page, size));
	}

	// ================= PREVIEW IMPORT =================
	[HttpPost("preview")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<IActionResult> Preview(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));

		using var stream = file.OpenReadStream();

		var result = await _service.PreviewImport(stream, "Sheet1");

		return Ok(result);
	}

	// ================= VALIDATE IMPORT =================
	[HttpPost("validate")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<IActionResult> Validate([FromBody] List<NgayNghiRequestDTO> list)
	{
		var result = await _service.ValidateImport(list);

		return Ok(result);
	}

	// ================= IMPORT =================
	[HttpPost("import")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<IActionResult> Import([FromBody] List<NgayNghiRequestDTO> list)
	{
		var result = await _service.Import(list);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}