using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
	[Authorize(Policy = "HETHONG_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] KhungGioKhamRequestDTO dto)
	{
		var result = await _service.TaoAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "HETHONG_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] KhungGioKhamRequestDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "HETHONG_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<KhungGioKhamReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "HETHONG_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<List<KhungGioKhamListReadModel>>>> GetList()
	{
		var result = await _service.GetAllAsync();
		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[Authorize(Policy = "HETHONG_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}

	// ==================== COUNT ====================
	[Authorize(Policy = "HETHONG_VIEW")]
	[HttpGet("count")]
	public async Task<ActionResult<ApiResponse<int>>> Count()
	{
		var result = await _service.CountAsync();
		return Ok(result);
	}

	// ==================== FILTER BY CA LAM VIEC ====================
	[Authorize(Policy = "HETHONG_VIEW")]
	[HttpGet("calamviec/{caLamViec}")]
	public async Task<ActionResult<ApiResponse<List<int>>>> GetByCaLamViec(int caLamViec)
	{
		var result = await _service.GetByCaLamViecAsync(caLamViec);
		return Ok(result);
	}
}