using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/baiviet")]
[Authorize]
public class BaiVietController : ControllerBase
{
	private readonly BaiVietService _service;

	public BaiVietController(BaiVietService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThemBaiVietDTO dto)
	{
		var result = await _service.CreateAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(
			nameof(GetById),
			new { id = result.Data },
			result
		);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CapNhatBaiVietDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPut("post/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Post(int id)
	{
		var result = await _service.PostAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPut("hide/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Hide(int id)
	{
		var result = await _service.HideAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPut("save/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Save(int id)
	{
		var result = await _service.SaveAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPut("delete/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<BaiVietReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<BaiVietListReadModel>>>> GetPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, trangThai);
		return Ok(result);
	}
	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<BaiVietListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.SearchPagedAsync(keyword, page, size,trangThai);
		return Ok(result);
	}

	// ==================== FILTER ====================
	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("loaibenh/{loaiBenhId}")]
	public async Task<ActionResult<ApiResponse<List<BaiVietListReadModel>>>> GetByLoaiBenh(int loaiBenhId)
	{
		var result = await _service.GetByLoaiBenhAsync(loaiBenhId);
		return Ok(result);
	}

	// ==================== TOP ====================
	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("top")]
	public async Task<ActionResult<ApiResponse<List<BaiVietListReadModel>>>> GetTop(
		[FromQuery] int top = 5)
	{
		var result = await _service.GetTopLuotXemAsync(top);
		return Ok(result);
	}
}