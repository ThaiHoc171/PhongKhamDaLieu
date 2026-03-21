using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/can-lam-sang")]
[Authorize]
public class CanLamSangController : ControllerBase
{
	private readonly CanLamSangService _service;

	public CanLamSangController(CanLamSangService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CanLamSangRequestDTO dto)
	{
		var result = await _service.TaoMoiAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(
			nameof(GetById),
			new { id = result.Data },
			result
		);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CanLamSangUpdateDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}


	// ==================== GET DETAIL ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<CanLamSangReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangListReadModel>>>> GetPaged(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15,
		[FromQuery] string? loaiXetNghiem = null,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize, loaiXetNghiem, trangThai);

		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return BadRequest(ApiResponse<PagedResult<CanLamSangListReadModel>>.Fail("Keyword không hợp lệ"));

		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);

		return Ok(result);
	}

	// ==================== FILTER BY LOAI ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("loai")]
	public async Task<ActionResult<ApiResponse<List<CanLamSangListReadModel>>>> GetByLoai(
		[FromQuery] string loai)
	{
		var result = await _service.GetByLoaiXetNghiemAsync(loai);

		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[Authorize(Policy = "CSVC_VIEW")]
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
    {
        var result = await _service.GetIdAndNameAsync();
        return Ok(result);
    }
}