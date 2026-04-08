using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/phienkham")]
[Authorize]
public class PhienKhamController : ControllerBase
{
	private readonly PhienKhamService _service;

	public PhienKhamController(PhienKhamService service)
	{
		_service = service;
	}

	[Authorize(Policy = "PHIENKHAM_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromQuery] int caKhamId)
	{
		var result = await _service.CreateAsync(caKhamId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PhienKhamUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/start")]
	public async Task<ActionResult<ApiResponse<bool>>> Start(int id)
	{
		var result = await _service.StartAsync(id);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ================= COMPLETE =================

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/complete")]
	public async Task<ActionResult<ApiResponse<bool>>> Complete(int id, [FromBody] string chanDoanCuoi)
	{
		var result = await _service.CompleteAsync(id, chanDoanCuoi);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ================= CANCEL =================

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/cancel")]
	public async Task<ActionResult<ApiResponse<bool>>> Cancel(int id)
	{
		var result = await _service.CancelAsync(id);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamReadListModel>>>> GetByPatient(
		int benhNhanId,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10)
	{
		if (User.IsInRole("Bệnh nhân"))
		{
			var id = int.Parse(User.FindFirst("BenhNhanID")!.Value);

			if (benhNhanId != id)
				return Forbid();
		}

		var result = await _service.GetByBenhNhanAsync(benhNhanId, pageNumber, pageSize);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamReadListModel>>>> GetPaged(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15,
		[FromQuery] int? nhanVienID = null,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize, nhanVienID, trangThai);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamReadListModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15,
		[FromQuery] int? nhanVienID = null)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize, nhanVienID);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("cakham/{caKhamId}")]
	public async Task<ActionResult<ApiResponse<PhienKhamReadModel>>> GetByCaKham(int caKhamId)
	{
		var result = await _service.GetByCaKhamIdAsync(caKhamId);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
}