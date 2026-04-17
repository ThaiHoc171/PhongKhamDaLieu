using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/taikham")]
[Authorize]
public class TaiKhamController : ControllerBase
{
	private readonly TaiKhamService _service;

	public TaiKhamController(TaiKhamService service)
	{
		_service = service;
	}

	[Authorize(Policy = "PHIENKHAM_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create(
		[FromBody] TaiKhamRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(GetDetail), new { id = result.Data }, result);
	}

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(
		int id,
		[FromBody] TaiKhamUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/complete")]
	public async Task<ActionResult<ApiResponse<bool>>> Complete(int id)
	{
		var result = await _service.CompleteAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

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

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/cakham/{caKhamId}")]
	public async Task<ActionResult<ApiResponse<bool>>> AssignCaKham(
		int id,
		int caKhamId)
	{
		var result = await _service.GanCaKhamAsync(id, caKhamId);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<TaiKhamReadModel>>> GetDetail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("GetId/{id}")]
	public async Task<ActionResult<ApiResponse<int>>> GetId(int id)
	{
		var result = await _service.GetIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<TaiKhamReadListModel>>>> GetPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, trangThai);
		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<TaiKhamReadListModel>>>> Search(
		[FromQuery] string? keyword,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);
		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<ActionResult<ApiResponse<PagedResult<TaiKhamReadListModel>>>> GetByBenhNhan(
		int benhNhanId,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		if (User.IsInRole("Bệnh nhân"))
		{
			var id = int.Parse(User.FindFirst("BenhNhanID")!.Value);
			if (benhNhanId != id)
				return Forbid();
		}

		var result =
			await _service.GetByBenhNhanAsync(benhNhanId, page, size);

		return Ok(result);
	}
}