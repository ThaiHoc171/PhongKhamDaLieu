using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThuocController : ControllerBase
{
	private readonly ThuocService _service;

	public ThuocController(ThuocService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> DanhSach()
	{
		return Ok(await _service.DanhSachAsync());
	}

	[HttpGet("timkiem")]
	public async Task<IActionResult> TimKiem([FromQuery] string kw)
	{
		return Ok(await _service.TimKiemAsync(kw));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Thuốc không tồn tại." });

		return Ok(result);
	}

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
