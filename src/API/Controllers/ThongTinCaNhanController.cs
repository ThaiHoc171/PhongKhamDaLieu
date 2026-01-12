using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThongTinCaNhanController : ControllerBase
{
	private readonly ThongTinCaNhanService _service;

	public ThongTinCaNhanController(ThongTinCaNhanService service)
	{
		_service = service;
	}


	[HttpPost("NhanVien")]
	public async Task<IActionResult> ThemNhanVien([FromBody] ThemThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest(new { message = "Dữ liệu không hợp lệ." });
		try
		{
			var id = await _service.TaoNhanVien(dto);
			return CreatedAtAction(
				nameof(ThongTin),
				new { id },
				new { message = "Tạo nhân viên thành công.", thongTinID = id }
			);
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPost("BenhNhan")]
	public async Task<IActionResult> ThemBenhNhan([FromBody] ThemThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest(new { message = "Dữ liệu không hợp lệ." });

		try
		{
			var id = await _service.ThemThongTinBenhNhan(dto);
			return CreatedAtAction(
				nameof(ThongTin),
				new { id },
				new { message = "Tạo bệnh nhân thành công.", thongTinID = id }
			);
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}


	[HttpGet("BenhNhan")]
	public async Task<IActionResult> DanhSachBenhNhan()
	{
		var result = await _service.DanhSachThongTinBenhNhan();
		return Ok(new
		{
			message = "Lấy danh sách bệnh nhân thành công.",
			data = result
		});
	}

	[HttpGet("NhanVien")]
	public async Task<IActionResult> DanhSachNhanVien()
	{
		var result = await _service.DanhSachThongTinNhanVien();
		return Ok(new
		{
			message = "Lấy danh sách nhân viên thành công.",
			data = result
		});
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> ThongTin(int id)
	{
		var result = await _service.ThongTin(id);
		if (result == null)
			return NotFound(new { message = "Không tìm thấy thông tin cá nhân." });

		return Ok(new
		{
			message = "Lấy thông tin cá nhân thành công.",
			data = result
		});
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(
		int id,
		[FromBody] CapNhatThongTinCaNhanDTO dto
	)
	{
		if (dto == null)
			return BadRequest(new { message = "Dữ liệu không hợp lệ." });

		var success = await _service.CapNhatThongTin(id, dto);
		if (!success)
			return NotFound(new { message = "Không tìm thấy thông tin để cập nhật." });

		return Ok(new { message = "Cập nhật thông tin cá nhân thành công." });
	}
}
