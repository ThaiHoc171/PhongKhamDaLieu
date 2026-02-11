using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ThuocController : ControllerBase
{
	private readonly ThuocService _service;

	public ThuocController(ThuocService service)
	{
		_service = service;
	}

	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet]
	public async Task<IActionResult> DanhSach()
	{
		return Ok(await _service.DanhSachAsync());
	}

	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("timkiem")]
	public async Task<IActionResult> TimKiem([FromQuery] string kw)
	{
		return Ok(await _service.TimKiemAsync(kw));
	}

	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Thuốc không tồn tại." });

		return Ok(result);
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] ThuocRequestDTO dto)
	{
		try
		{
			await _service.ThemAsync(dto);
			return Ok(new { message = "Thêm thuốc thành công." });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] ThuocRequestDTO dto)
	{
		try
		{
			var result = await _service.CapNhatAsync(id, dto);
			if (!result)
				return NotFound(new { message = "Thuốc không tồn tại." });

			return Ok(new { message = "Cập nhật thuốc thành công." });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
