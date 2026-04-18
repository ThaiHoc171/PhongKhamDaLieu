using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/bacsi")]
[Authorize]
public class BacSiProfileController : ControllerBase
{
	private readonly BacSiProfileService _service;

	public BacSiProfileController(BacSiProfileService service)
	{
		_service = service;
	}

	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] BacSiProfileRequestDTO dto)
	{
		var result = await _service.TaoMoiAsync(dto);
		return Ok(result);
	}

	[Authorize(Policy = "PUBLIC_WRITE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(
		int id,
		[FromBody] BacSiProfileUpdateDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);
		return Ok(result);
	}

	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<BacSiProfileListReadModel>>>> GetPaged(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);
		return Ok(result);
	}

	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<BacSiProfileReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		return Ok(result);
	}

	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("nhanvien/{nhanVienId}")]
	public async Task<ActionResult<ApiResponse<BacSiProfileReadModel>>> GetByNhanVien(int nhanVienId)
	{
		var result = await _service.GetByNhanVienAsync(nhanVienId);
		return Ok(result);
	}

	[Authorize(Policy = "PUBLIC_READ")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<BacSiProfileListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
		return Ok(result);
	}
}