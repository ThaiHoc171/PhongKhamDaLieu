using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanLamSangController : ControllerBase
{
	private readonly CanLamSangService _service;

	public CanLamSangController(CanLamSangService service)
	{
		_service = service;
	}

	// GET: api/CanLamSang
	[HttpGet]
	public async Task<IActionResult> LayDanhSach()
	{
		var result = await _service.DanhSachCanLamSangAsync();
		return Ok(result);
	}

	// GET: api/CanLamSang/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayCanLamSangTheoIdAsync(id);

		if (result == null)
			return NotFound(new { message = "Cận lâm sàng không tồn tại." });

		return Ok(result);
	}

	// POST: api/CanLamSang
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] CanLamSangRequestDTO dto)
	{
		try
		{
			await _service.ThemCanLamSangAsync(dto);

			return Ok(new
			{
				message = "Thêm cận lâm sàng thành công."
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	// PUT: api/CanLamSang/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] CanLamSangRequestDTO dto)
	{
		try
		{
			var result = await _service.CapNhatCanLamSangAsync(id, dto);

			if (!result)
				return NotFound(new { message = "Cận lâm sàng không tồn tại." });

			return Ok(new
			{
				message = "Cập nhật cận lâm sàng thành công."
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	// PUT: api/CanLamSang/{id}/ngungsudung
	[HttpPut("{id}/ngungsudung")]
	public async Task<IActionResult> NgungSuDung(int id)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, "Ngưng sử dụng");

		if (!result)
			return NotFound(new { message = "Cận lâm sàng không tồn tại." });

		return Ok(new
		{
			message = "Ngưng sử dụng cận lâm sàng thành công."
		});
	}

	// PUT: api/CanLamSang/{id}/kichhoat
	[HttpPut("{id}/kichhoat")]
	public async Task<IActionResult> KichHoat(int id)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, "Hoạt động");

		if (!result)
			return NotFound(new { message = "Cận lâm sàng không tồn tại." });

		return Ok(new
		{
			message = "Kích hoạt cận lâm sàng thành công."
		});
	}
}
