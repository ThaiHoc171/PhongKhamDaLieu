using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NhanVienController : ControllerBase
{
	private readonly NhanVienService _service;
	public NhanVienController(NhanVienService service)
	{
		_service = service;
	}
	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<NhanVienReadListModel>>>> 
		GetPaged(int pageNumber = 1, int pageSize = 10)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<NhanVienReadListModel>>>> 
		Search(string keyword, int pageNumber = 1, int pageSize = 10)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<NhanVienReadModel>>> GetDetail(int id)
	{
		var result = await _service.GetDetailAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_CREATE")]
	[HttpPost("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] NhanVienRequestDTO dto)
	{
		var result = await _service.AddNhanVienAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id,[FromBody] NhanVienRequestUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_UPDATE")]
	[HttpPut("status/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Status(int id, [FromQuery] string trangthai)
	{
		var result = await _service.StatusAsync(id, trangthai);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("combobox/{chucVuId}")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox(int chucVuId)
	{
		var result = await _service.GetComboboxAsync(chucVuId);
		return Ok(result);
	}
}