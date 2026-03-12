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

	[Authorize(Policy = "NHANVIEN_CREATE")]
	[HttpPost("NhanVien")]
	public async Task<ActionResult<ApiResponse<int>>> TaoNhanVien([FromBody] ThongTinRequestDTO dto)
	{
		var result = await _service.TaoNhanVienAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(LayThongTin), new { id = result.Data }, result);
	}

	[Authorize(Policy = "BENHNHAN_CREATE")]
	[HttpPost("BenhNhan")]
	public async Task<ActionResult<ApiResponse<int>>> TaoBenhNhan([FromBody] ThongTinRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(LayThongTin), new { id = result.Data }, result);
	}

	[Authorize(Policy = "NHANVIEN_VIEW")]
	[HttpGet("NhanVien")]
	public async Task<ActionResult<ApiResponse<List<ThongTinCaNhanResponseDTO>>>> DanhSachNhanVien()
	{
		var result = await _service.DanhSachNhanVienAsync();
		return Ok(result);
	}

	[Authorize(Policy = "BENHNHAN_VIEW")]
	[HttpGet("BenhNhan")]
	public async Task<ActionResult<ApiResponse<List<ThongTinCaNhanResponseDTO>>>> DanhSachBenhNhan()
	{
		var result = await _service.DanhSachBenhNhanAsync();
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