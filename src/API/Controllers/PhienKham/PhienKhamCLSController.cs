using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/phienkham-cls")]
public class PhienKhamCLSController : ControllerBase
{
	private readonly PhienKhamCLSService _service;

	public PhienKhamCLSController(PhienKhamCLSService service)
	{
		_service = service;
	}

	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("phienkham/{phienKhamID}")]
	public async Task<IActionResult> LayTheoPhienKham(int phienKhamID)
	{
		try
		{
			var result = await _service.LayTheoPhienKhamAsync(phienKhamID);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi lấy danh sách cận lâm sàng",
				detail = ex.Message
			});
		}
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPost]
	public async Task<IActionResult> ThemMoi([FromBody] TaoPhienKhamCLSDTO dto)
	{
		try
		{
			await _service.ThemMoiAsync(dto);

			return Ok(new
			{
				message = "Chỉ định cận lâm sàng thành công"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi chỉ định cận lâm sàng",
				detail = ex.Message
			});
		}
	}

	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPut("{id}/nhan")]
	public async Task<IActionResult> NhanThucHien(int id, [FromBody] NhanThucHienCLSDTO dto)
	{
		try
		{
			var success = await _service.NhanThucHienAsync(id, dto);
			if (!success)
				return NotFound(new { message = "CLS không tồn tại" });

			return Ok(new
			{
				message = "Đã nhận thực hiện cận lâm sàng"
			});
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi nhận thực hiện cận lâm sàng",
				detail = ex.Message
			});
		}
	}

	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPut("{id}/ketqua")]
	public async Task<IActionResult> CapNhatKetQua(int id, [FromBody] CapNhatKetQuaCLSDTO dto)
	{
		try
		{
			var success = await _service.CapNhatKetQuaAsync(id, dto);
			if (!success)
				return NotFound(new { message = "CLS không tồn tại" });

			return Ok(new
			{
				message = "Cập nhật kết quả cận lâm sàng thành công"
			});
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi cập nhật kết quả cận lâm sàng",
				detail = ex.Message
			});
		}
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/huy")]
	public async Task<IActionResult> Huy(int id)
	{
		try
		{
			var success = await _service.HuyAsync(id);
			if (!success)
				return NotFound(new { message = "CLS không tồn tại" });

			return Ok(new
			{
				message = "Đã hủy cận lâm sàng"
			});
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi hủy cận lâm sàng",
				detail = ex.Message
			});
		}
	}
}
