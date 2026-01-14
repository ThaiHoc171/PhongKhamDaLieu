using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhongChucNangController : ControllerBase
{
	private readonly PhongChucNangService _service;

	public PhongChucNangController(PhongChucNangService service)
	{
		_service = service;
	}

	// GET: api/PhongChucNang
	[HttpGet]
	public async Task<IActionResult> LayDanhSach()
	{
		var result = await _service.LayTatCaAsync();
		return Ok(result);
	}

	// GET: api/PhongChucNang/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Phòng chức năng không tồn tại." });

		return Ok(result);
	}

	// GET: api/PhongChucNang/timkiem?keyword=...
	[HttpGet("timkiem")]
	public async Task<IActionResult> TimKiem([FromQuery] string keyword)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return BadRequest(new { message = "Từ khóa tìm kiếm không hợp lệ." });

		var result = await _service.TimKiemAsync(keyword);
		return Ok(result);
	}

	// POST: api/PhongChucNang
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] PhongChucNangRequestDTO dto)
	{
		await _service.ThemAsync(dto);
		return Ok(new { message = "Thêm phòng chức năng thành công." });
	}

	// PUT: api/PhongChucNang/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhongChucNangRequestDTO dto)
	{
		var success = await _service.CapNhatAsync(id, dto);
		if (!success)
			return NotFound(new { message = "Phòng chức năng không tồn tại." });

		return Ok(new { message = "Cập nhật phòng chức năng thành công." });
	}

	// PUT: api/PhongChucNang/{id}/trangthai
	[HttpPut("{id}/trangthai")]
	public async Task<IActionResult> ChuyenTrangThai(int id, [FromBody] string trangThaiMoi)
	{
		if (string.IsNullOrWhiteSpace(trangThaiMoi))
			return BadRequest(new { message = "Trạng thái mới không hợp lệ." });

		var success = await _service.ChuyenTrangThaiAsync(id, trangThaiMoi);
		if (!success)
			return NotFound(new { message = "Phòng chức năng không tồn tại." });

		return Ok(new { message = "Chuyển trạng thái phòng chức năng thành công." });
	}
}
