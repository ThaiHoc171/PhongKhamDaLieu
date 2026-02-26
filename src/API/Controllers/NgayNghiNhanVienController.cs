using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = "LeTanOnly")]
[Route("api/ngaynghi")]
public class NgayNghiNhanVienController : ControllerBase
{
	private readonly NgayNghiNhanVienService _service;

	public NgayNghiNhanVienController(NgayNghiNhanVienService service)
	{
		_service = service;
	}
	[HttpPost]
	public async Task<IActionResult> TaoNgayNghi([FromBody] NgayNghiRequestDTO dto)
	{
		try
		{
			await _service.ThemNgayNghiAsync(dto);
			return Ok(new { message = "Tạo ngày nghỉ thành công." });
		}
		catch (Exception ex)
        {
			return BadRequest(new
			{
				message = "Lỗi: " + ex.Message
			});
		}
	}

	[HttpGet("nhanvien/{nhanVienID}")]
	public async Task<IActionResult> LayTheoNhanVien(int nhanVienID)
	{
		try
		{
			var result = await _service.GetByNhanVienAsync(nhanVienID);
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

	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhatLyDo(int id, [FromBody] string? lyDo)
	{
		try
		{
			var success = await _service.CapNhatNgayNghiAsync(id, lyDo);

			if (!success)
				return NotFound(new { message = "Ngày nghỉ không tồn tại." });

			return Ok(new { message = "Cập nhật lý do nghỉ thành công." });
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = "Lỗi: " + ex.Message
			});
		}
	}
	[HttpGet("thang")]
	public async Task<IActionResult> GetByMonth(int? thang, int? nam)
	{
		var now = DateTime.Now;

		int thangValue = thang ?? now.Month;
		int namValue = nam ?? now.Year;

		try
		{
			var result = await _service.GetByMonthAsync(thangValue, namValue);
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
