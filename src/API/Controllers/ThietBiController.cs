using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize(Roles = "Admin,Nhân viên")]
[ApiController]
[Route("api/[controller]")]
public class ThietBiController : ControllerBase
{
	private readonly ThietBiService _service;

	public ThietBiController(ThietBiService service)
	{
		_service = service;
	}

	// GET: api/ThietBi
	[HttpGet]
	public async Task<IActionResult> LayTatCa()
	{
		var result = await _service.LayTatCaAsync();
		return Ok(result);
	}

	// GET: api/ThietBi/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Thiết bị không tồn tại." });

		return Ok(result);
	}

	// GET: api/ThietBi/timkiem?tenTB=...
	[HttpGet("timkiem")]
	public async Task<IActionResult> TimTheoTen([FromQuery] string tenTB)
	{
		if (string.IsNullOrWhiteSpace(tenTB))
			return BadRequest(new { message = "Tên thiết bị không hợp lệ." });

		var result = await _service.TimTheoTenAsync(tenTB);
		return Ok(result);
	}

	// POST: api/ThietBi
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] ThietBiRequestDTO dto)
	{
		await _service.ThemAsync(dto);
		return Ok(new { message = "Thêm thiết bị thành công." });
	}

	// PUT: api/ThietBi/{id}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] ThietBiRequestDTO dto)
	{
		var success = await _service.CapNhatAsync(id, dto);
		if (!success)
			return NotFound(new { message = "Thiết bị không tồn tại." });

		return Ok(new { message = "Cập nhật thiết bị thành công." });
	}
	[Authorize(Roles = "Admin")]
	[HttpGet("combobox")]
	public async Task<IActionResult> GetComboboxAsync()
	{
		return Ok(await _service.GetComboboxAsync());
	}
}
