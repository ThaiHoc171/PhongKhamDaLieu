using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
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
	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet]
	public async Task<IActionResult> LayDanhSach()
	{
		var result = await _service.LayTatCaAsync();
		return Ok(result);
	}

	// GET: api/PhongChucNang/{id}
	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Phòng chức năng không tồn tại." });

		return Ok(result);
	}

	// GET: api/PhongChucNang/timkiem?keyword=...
	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("timkiem")]
	public async Task<IActionResult> TimKiem([FromQuery] string keyword)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return BadRequest(new { message = "Từ khóa tìm kiếm không hợp lệ." });

		var result = await _service.TimKiemAsync(keyword);
		return Ok(result);
	}

	// POST: api/PhongChucNang
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] PhongChucNangRequestDTO dto)
	{
		await _service.ThemAsync(dto);
		return Ok(new { message = "Thêm phòng chức năng thành công." });
	}

	// PUT: api/PhongChucNang/{id}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhongChucNangRequestDTO dto)
	{
		var success = await _service.CapNhatAsync(id, dto);
		if (!success)
			return NotFound(new { message = "Phòng chức năng không tồn tại." });

		return Ok(new { message = "Cập nhật phòng chức năng thành công." });
	}

	// PUT: api/PhongChucNang/{id}/trangthai
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}/trangthai")]
	public async Task<IActionResult> ChuyenTrangThai(int id, [FromBody] TinhTrang trangThaiMoi)
	{
		if (!Enum.IsDefined(typeof(TinhTrang), trangThaiMoi))
			return BadRequest(new { message = "Trạng thái không hợp lệ." });

		var success = await _service.ChuyenTrangThaiAsync(id, trangThaiMoi);
		if (!success)
			return NotFound(new { message = "Phòng chức năng không tồn tại." });

		return Ok(new { message = "Chuyển trạng thái phòng chức năng thành công." });
	}
	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("combobox")]
	public async Task<IActionResult> GetIdAndName()
	{
		var result = await _service.GetIdAndNameAsync();
		return Ok(result);
	}
}
