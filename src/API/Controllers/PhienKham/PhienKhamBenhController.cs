using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhienKhamBenhController : ControllerBase
{
	private readonly PhienKhamBenhService _service;

	public PhienKhamBenhController(PhienKhamBenhService service)
	{
		_service = service;
	}

	[HttpGet("phien-kham/{phienKhamID:int}")]
	public async Task<IActionResult> GetByPhienKham(int phienKhamID)
	{
		try
		{
			var result = await _service.GetByPhienKhamIdAsync(phienKhamID);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}


	[HttpPost]
	public async Task<IActionResult> ThemMoi([FromBody] PhienKhamBenhRequestDTO dto)
	{
		try
		{
			await _service.ThemMoiAsync(dto);
			return Ok(new { message = "Thêm chẩn đoán bệnh thành công" });
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhienKhamBenhRequestDTO dto)
	{
		try
		{
			await _service.CapNhatAsync(id, dto);
			return Ok(new { message = "Cập nhật chẩn đoán bệnh thành công" });
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
