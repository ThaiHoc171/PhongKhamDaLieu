using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/nhanvien")]
[Authorize]
public class NhanVienController : ControllerBase
{
	private readonly NhanVienService _service;

	public NhanVienController(NhanVienService service)
	{
		_service = service;
	}

	[Authorize(Policy = "NHANVIEN_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] NhanVienRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "NHANVIEN_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] NhanVienRequestUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "NHANVIEN_UPDATE")]
	[HttpPut("status/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Status(int id, [FromQuery] string trangThai)
	{
		var result = await _service.StatusAsync(id, trangThai);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<NhanVienReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<NhanVienReadListModel>>>>
		Paged([FromQuery] int page = 1, [FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}

	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<NhanVienReadListModel>>>>
		Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>>
		Combobox([FromQuery] int chucVuId)
	{
		var result = await _service.GetComboboxAsync(chucVuId);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("combobox/doctor")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>>	ComboboxDoctor()
	{
		var result = await _service.GetComboboxDoctorAsync();
		return Ok(result);
	}
}