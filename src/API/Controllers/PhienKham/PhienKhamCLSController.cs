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
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamClsReadListModel>>>> GetByPhienKham(int phienKhamID)
	{
		var result = await _service.GetByPhienKhamAsync(phienKhamID);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamClsReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<List<PhienKhamClsReadListModel>>>> List()
	{
		var result = await _service.GetListAsync();
		return Ok(result);
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] PkClsRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== ACCEPT CLS ====================
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
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
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
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
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
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