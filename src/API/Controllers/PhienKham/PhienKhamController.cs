using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Application.Common;

namespace API.Controllers;

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

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromQuery] int caKhamId)
	{
		var result = await _service.TaoMoiAsync(caKhamId);
		return Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PhienKhamUpdateDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);
		return Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/complete")]
	public async Task<ActionResult<ApiResponse<bool>>> Complete(int id, [FromBody] string chanDoanCuoi)
	{
		var result = await _service.KetThucAsync(id, chanDoanCuoi);
		return Ok(result);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamListReadModel>>>> GetByPatient(
		int benhNhanId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		var result = await _service.GetByBenhNhanAsync(benhNhanId, pageNumber, pageSize);
		return Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamListReadModel>>>> GetPaged(
		[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15, [FromQuery] int? nhanVienID = null, [FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize, nhanVienID, trangThai);
		return Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamListReadModel>>>> Search(
		[FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15, [FromQuery] int? nhanVienID = null)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize, nhanVienID);
		return Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		return Ok(result);
	}

	[Authorize]
	[HttpGet("cakham/{caKhamId}")]
	public async Task<ActionResult<ApiResponse<PhienKhamReadModel>>> GetByCaKham(int caKhamId)
	{
		var result = await _service.GetByCaKhamIdAsync(caKhamId);
		return Ok(result);
	}
}