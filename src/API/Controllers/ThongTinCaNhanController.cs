using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/thongtincanhan")]
[Authorize]
public class ThongTinCaNhanController : ControllerBase
{
	private readonly ThongTinCaNhanService _service;

	public ThongTinCaNhanController(ThongTinCaNhanService service)
	{
		_service = service;
	}
	[Authorize(Policy = "USER_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThongTinRequestDTO dto)
	{
		var result = await _service.AddKhachAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ThongTinUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ThongTinReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	[Authorize(Policy = "USER_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<ThongTinReadListModel>>>> KhachPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<ThongTinReadListModel>>>> Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{thongTinId}/taikhoan/{taiKhoanId}")]
	public async Task<ActionResult<ApiResponse<bool>>> LinkTaiKhoan(
		int thongTinId,
		int taiKhoanId,
		[FromQuery] string email)
	{
		var result = await _service.UpdateAccountAsync(thongTinId, taiKhoanId, email);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}