using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LoaiBenhController : ControllerBase
{
	private readonly LoaiBenhService _service;
	public LoaiBenhController(LoaiBenhService service)
	{
		_service = service;
	}
    [Authorize(Policy = "BacSiOnly")]
    [HttpGet("combobox")]
    public async Task<IActionResult> GetComboboxAsync()
    {
        return Ok(await _service.GetComboboxAsync());
    }
    [Authorize]
	[HttpGet("paged")]
	public async Task<IActionResult> LayDanhSachPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
	{
		var result = await _service.DanhSachPagedAsync(pageNumber, pageSize);
		return Ok(result);
	}
	[Authorize]
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Loại bệnh không tồn tại." });
		return Ok(result);
	}
	[Authorize]
	[HttpGet("timkiem")]
	public async Task<IActionResult> TimTheoTen([FromQuery] string ten)
		=> Ok(await _service.TimTheoTenAsync(ten));
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] LoaiBenhRequestDTO dto)
	{
		try
		{
			await _service.ThemAsync(dto);
			return Ok(new { message = "Thêm loại bệnh thành công." });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] LoaiBenhRequestDTO dto)
	{
		try
		{
			var result = await _service.CapNhatAsync(id, dto);
			if (!result)
				return NotFound(new { message = "Loại bệnh không tồn tại." });
			return Ok(new { message = "Cập nhật loại bệnh thành công." });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
	// GET: api/LoaiBenh/combo?kw=viem
	[Authorize]
	[HttpGet("combo")]
	public async Task<IActionResult> Combo([FromQuery] string? kw)
	{
		var result = await _service.DanhSachComboAsync(kw);
		return Ok(result);
	}
}
