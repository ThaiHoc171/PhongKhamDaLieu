using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhienKhamThietBiController : ControllerBase
{
	private readonly PhienKhamThietBiService _service;

	public PhienKhamThietBiController(PhienKhamThietBiService service)
	{
		_service = service;
	}

	// GET: api/PhienKhamThietBi/phienkham/{phienKhamId}
	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<IActionResult> LayDanhSachTheoPhienKham(int phienKhamId)
	{
		try
		{
			var result = await _service.DanhSachTheoPhienKhamAsync(phienKhamId);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi lấy danh sách thiết bị theo phiên khám.",
				error = ex.Message
			});
		}
	}

	// GET: api/PhienKhamThietBi/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		try
		{
			var result = await _service.LayTheoIdAsync(id);

			if (result == null)
				return NotFound(new { message = "Thiết bị không tồn tại trong phiên khám." });

			return Ok(result);
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi lấy chi tiết thiết bị phiên khám.",
				error = ex.Message
			});
		}
	}

	// POST: api/PhienKhamThietBi
	[HttpPost]
	public async Task<IActionResult> ThemMoi([FromBody] PhienKhamThietBiRequestDTO dto)
	{
		try
		{
			await _service.ThemMoiAsync(dto);

			return Ok(new
			{
				message = "Thêm thiết bị vào phiên khám thành công."
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi thêm thiết bị vào phiên khám.",
				error = ex.Message
			});
		}
	}

	// PUT: api/PhienKhamThietBi/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhienKhamThietBiRequestDTO dto)
	{
		try
		{
			var result = await _service.CapNhatAsync(id, dto);

			if (!result)
				return NotFound(new { message = "Thiết bị không tồn tại trong phiên khám." });

			return Ok(new
			{
				message = "Cập nhật thiết bị phiên khám thành công."
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				message = "Lỗi khi cập nhật thiết bị phiên khám.",
				error = ex.Message
			});
		}
	}
}
