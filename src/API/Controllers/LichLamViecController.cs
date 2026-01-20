using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LichLamViecController: ControllerBase
{
	private readonly LichLamViecService _service;
	public LichLamViecController(LichLamViecService service)
	{
		_service = service;
	}

	[HttpPost("TaoLich")]
	public async Task<IActionResult> TaoLichMulti(LichLamViecBatchDTO dto)
	{
		try
		{
			await _service.ThemLichLamViecAsync(dto);
			return Ok(new { message = "Tạo lịch thành công" });
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = "Lỗi: " + ex.Message
			});
		}
	}
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		try
		{
			var result = await _service.GetByIdAsync(id);
			if (result == null)
				return NotFound();

			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = "Lỗi: " + ex.Message
			});
		}
	}
	[HttpGet("GetAll")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var result = await _service.GetAllAsync();
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = "Lỗi: " + ex.Message
			});
		}
	}
	[HttpGet("GetByNhanVien/{nhanVienID}")]
	public async Task<IActionResult> GetByNhanVien(int nhanVienID, [FromQuery] int page = 0)
	{
		try
		{
			var result = await _service.GetLichTheoTuanAsync(nhanVienID,page);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = "Lỗi: " + ex.Message
			});
		}
	}
}
