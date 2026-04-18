using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/phienkham-cls")]
[Authorize]
public class PhienKhamCLSController : ControllerBase
{
	private readonly PhienKhamCLSService _service;

	public PhienKhamCLSController(PhienKhamCLSService service)
	{
		_service = service;
	}

	// ==================== GET BY PHIEN KHAM ====================
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("phienkham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamClsReadListModel>>>> GetByPhienKham(int phienKhamID)
	{
		var result = await _service.GetByPhienKhamAsync(phienKhamID);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamClsReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamClsReadListModel>>>> 
		Paged([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(trangThai, page, size);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamClsReadListModel>>>> 
		Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? trangThai = null)
	{
		var result = await _service.SearchAsync(keyword, trangThai, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] PkClsRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== ACCEPT CLS ====================
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPut("{id}/accept")]
	public async Task<ActionResult<ApiResponse<bool>>> Accept(int id, [FromBody] AcceptClsDTO dto)
	{
		var result = await _service.AcceptAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== COMPLETE CLS ====================
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPut("{id}/complete")]
	public async Task<ActionResult<ApiResponse<bool>>> Complete(int id, [FromBody] PkClsUpdateRequestDTO dto)
	{
		var result = await _service.CompleteAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== CANCEL CLS ====================
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPut("{id}/cancel")]
	public async Task<ActionResult<ApiResponse<bool>>> Cancel(int id)
	{
		var result = await _service.CancelAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
}