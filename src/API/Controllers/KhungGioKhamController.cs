using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/khunggiokham")]
[Authorize]
public class KhungGioKhamController : ControllerBase
{
	private readonly KhungGioKhamService _service;

	public KhungGioKhamController(KhungGioKhamService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[HttpPost]
	[Authorize(Policy = "HETHONG_WRITE")]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] KhungGioKhamRequest dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[HttpPut("{id}")]
	[Authorize(Policy = "HETHONG_WRITE")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] KhungGioKhamRequest dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[HttpDelete("{id}")]
	[Authorize(Policy = "HETHONG_WRITE")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[HttpGet("{id}")]
	[Authorize(Policy = "HETHONG_READ")]
	public async Task<ActionResult<ApiResponse<KhungGioKhamReadModel>>> GetById(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[HttpGet]
	[Authorize(Policy = "HETHONG_READ")]
	public async Task<ActionResult<ApiResponse<List<KhungGioKhamReadModel>>>> GetList()
	{
		var result = await _service.GetAllAsync();
		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[HttpGet("combobox")]
	[Authorize(Policy = "HETHONG_READ")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}

	// ==================== COUNT ====================
	[HttpGet("count")]
	[Authorize(Policy = "HETHONG_READ")]
	public async Task<ActionResult<ApiResponse<int>>> Count()
	{
		var result = await _service.CountAsync();
		return Ok(result);
	}

	// ==================== FILTER BY CA LAM VIEC ====================
	[HttpGet("calamviec/{caLamViec}")]
	[Authorize(Policy = "HETHONG_READ")]
	public async Task<ActionResult<ApiResponse<List<int>>>> GetByCaLamViec(int caLamViec)
	{
		var result = await _service.GetByCaLamViecAsync(caLamViec);
		return Ok(result);
	}
}