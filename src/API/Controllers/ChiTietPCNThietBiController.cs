using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chitiet-pcntb")]
[Authorize]
public class ChiTietPCNThietBiController : ControllerBase
{
	private readonly ChiTietPCNThietBiService _service;

	public ChiTietPCNThietBiController(ChiTietPCNThietBiService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ChiTietPCNThietBiRequestDTO dto)
	{
		var result = await _service.TaoMoiAsync(dto);
		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ChiTietPCNThietBiUpdateDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);
		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.XoaAsync(id);
		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ChiTietPCNThietBiReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET PAGED ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<List<ChiTietPCNThietBiListReadModel>>>> GetList([FromQuery] int pcnTbId)
	{
		var result = await _service.GetListAsync(pcnTbId);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}

}