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

}
