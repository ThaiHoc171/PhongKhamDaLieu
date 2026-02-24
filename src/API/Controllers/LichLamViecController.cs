using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

	[Authorize(Policy = "LeTanOnly")]
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

	[Authorize(Policy = "LeTanOnly")]
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

	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("GetByWeek")]
    public async Task<IActionResult> GetByWeek([FromQuery] int page = 0)
    {
        try
        {
            var result = await _service.GetByWeekAsync(page);
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

	[Authorize(Roles = "Admin,Nhân viên")]
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

	[Authorize(Roles = "Admin,Nhân viên")]
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
