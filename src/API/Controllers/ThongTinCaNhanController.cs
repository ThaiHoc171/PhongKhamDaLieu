using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ThongTinCaNhanController : ControllerBase
{
	private readonly ThongTinCaNhanService _service;
	public ThongTinCaNhanController(ThongTinCaNhanService service)
	{
		_service = service;
	}
	[Authorize(Policy = "KHACH_CREATE")]
	[HttpPost("Khach")]
	public async Task<ActionResult<ApiResponse<int>>> TaoKhach([FromBody] ThongTinRequestDTO dto)
	{
		var result = await _service.AddKhachAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(nameof(LayThongTin), new { id = result.Data }, result);
	}
	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("NhanVien")]
	[Authorize(Policy = "KHACH_VIEW")]
	[HttpGet("Khach")]
	public async Task<ActionResult<ApiResponse<List<ThongTinCaNhanResponseDTO>>>> DanhSachKhach()
	{
		var result = await _service.DanhSachKhachAsync();
		return Ok(result);
	}
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ThongTinCaNhanResponseDTO>>> LayThongTin(int id)
	{
		var result = await _service.GetDetailAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "BENHNHAN_VIEW")]
	[HttpGet("BenhNhan/Combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetComboboxAsync()
	{
		var result = await _service.GetCombobox();
		return Ok(result);
	}
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> CapNhat(int id, [FromBody] ThongTinUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
}