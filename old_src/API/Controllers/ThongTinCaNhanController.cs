using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThongTinCaNhanController : ControllerBase
{
	private readonly ThongTinCaNhanService _service;

	public ThongTinCaNhanController(ThongTinCaNhanService service)
	{
		_service = service;
	}


	[HttpPost("NhanVien")]
	public async Task<IActionResult> ThemNhanVien([FromBody] ThemThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest();

		var id = await _service.ThemThongTinNhanVien(dto);
		return CreatedAtAction(nameof(ThongTin), new { id }, null);
	}

	[HttpPost("BenhNhan")]
	public async Task<IActionResult> ThemBenhNhan([FromBody] ThemThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest();

		var id = await _service.ThemThongTinBenhNhan(dto);
		return CreatedAtAction(nameof(ThongTin), new { id }, null);
	}


	[HttpGet("BenhNhan")]
	public async Task<ActionResult<List<ThongTinCaNhanResponseDTO>>> DanhSachBenhNhan()
	{
		var result = await _service.DanhSachThongTinBenhNhan();
		return Ok(result);
	}

	[HttpGet("NhanVien")]
	public async Task<ActionResult<List<ThongTinCaNhanResponseDTO>>> DanhSachNhanVien()
	{
		var result = await _service.DanhSachThongTinNhanVien();
		return Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ThongTinCaNhanResponseDTO>> ThongTin(int id)
	{
		var result = await _service.ThongTin(id);
		if (result == null)
			return NotFound();

		return Ok(result);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(
		int id,
		[FromBody] CapNhatThongTinCaNhanDTO dto
	)
	{
		if (dto == null)
			return BadRequest();

		var success = await _service.CapNhatThongTin(id, dto);
		if (!success)
			return NotFound();

		return NoContent();
	}
}
