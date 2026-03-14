using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/thuoc")]
[Authorize]
public class ThuocController : ControllerBase
{
	private readonly ThuocService _service;

	public ThuocController(ThuocService service)
	{
		_service = service;
	}

	// CREATE
	[HttpPost]
	[Authorize(Policy = "THUOC_CREATE")]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThuocRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(
			nameof(GetDetail),
			new { id = result.Data },
			result);
	}

	// UPDATE
	[HttpPut("{id}")]
	[Authorize(Policy = "THUOC_UPDATE")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ThuocUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
		{
			if (result.Message.Contains("Không tìm thấy"))
				return NotFound(result);

			return BadRequest(result);
		}

		return Ok(result);
	}

	// DELETE
	[HttpDelete("{id}")]
	[Authorize(Policy = "THUOC_UPDATE")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// GET PAGED
	[HttpGet]
	[Authorize(Policy = "THUOC_VIEW")]
	public async Task<ActionResult<ApiResponse<PagedResult<ThuocListReadModel>>>> GetPaged(
		int page = 1,
		int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);

		return Ok(result);
	}

	// GET DETAIL
	[HttpGet("{id}")]
	[Authorize(Policy = "THUOC_VIEW")]
	public async Task<ActionResult<ApiResponse<ThuocReadModel>>> GetDetail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// SEARCH
	[HttpGet("search")]
	[Authorize(Policy = "THUOC_VIEW")]
	public async Task<ActionResult<ApiResponse<PagedResult<ThuocListReadModel>>>> Search(
		string keyword,
		int page = 1,
		int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// IMPORT EXCEL
	[HttpPost("import")]
	[Authorize(Policy = "THUOC_CREATE")]
	public async Task<ActionResult<ApiResponse<int>>> ImportExcel(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<int>.Fail("File không hợp lệ"));

		using var stream = file.OpenReadStream();

		var result = await _service.ImportExcelAsync(stream);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}