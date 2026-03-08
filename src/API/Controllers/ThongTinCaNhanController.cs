using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ThongTinCaNhanController : ControllerBase
{
	private readonly ThongTinCaNhanService _service;

	public ThongTinCaNhanController(ThongTinCaNhanService service)
	{
		_service = service;
	}
	[Authorize(Roles = "Admin")]
	[HttpPost("NhanVien")]
	public async Task<IActionResult> TaoNhanVien(
		[FromBody] ThemThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest(new { message = "Dữ liệu không hợp lệ." });

		try
		{
			var id = await _service.TaoNhanVienAsync(dto);

			return CreatedAtAction(
				nameof(LayThongTin),
				new { id },
				new { message = "Tạo nhân viên thành công.", thongTinID = id }
			);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = ex.Message });
		}
	}

	[Authorize(Policy = "LeTanOnly")]
	[HttpPost("BenhNhan")]
	public async Task<IActionResult> TaoBenhNhan(
		[FromBody] ThemThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest(new { message = "Dữ liệu không hợp lệ." });

		try
		{
			var id = await _service.TaoBenhNhanAsync(dto);

			return CreatedAtAction(
				nameof(LayThongTin),
				new { id },
				new { message = "Tạo bệnh nhân thành công.", thongTinID = id }
			);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = ex.Message });
		}
	}
	[Authorize(Roles = "Admin")]
	[HttpGet("NhanVien")]
	public async Task<IActionResult> DanhSachNhanVien()
	{
		var result = await _service.DanhSachNhanVienAsync();
		return Ok(new
		{
			message = "Lấy danh sách nhân viên thành công.",
			data = result
		});
	}

	[Authorize(Policy = "LeTanOnly")]
	[HttpGet("BenhNhan")]
	public async Task<IActionResult> DanhSachBenhNhan()
	{
		var result = await _service.DanhSachBenhNhanAsync();
		return Ok(new
		{
			message = "Lấy danh sách bệnh nhân thành công.",
			data = result
		});
	}


	[HttpGet("{id}")]
	public async Task<IActionResult> LayThongTin(int id)
	{
		var result = await _service.LayChiTietAsync(id);

		if (result == null)
			return NotFound(new { message = "Không tìm thấy thông tin cá nhân." });

		return Ok(new
		{
			message = "Lấy thông tin cá nhân thành công.",
			data = result
		});
	}
	[Authorize(Policy = "LeTanOnly")]
	[HttpGet("BenhNhan/Combobox")]
	public async Task<IActionResult> GetComboboxAsync()
	{
		return Ok(await _service.GetCombobox());
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(
		int id,
		[FromBody] CapNhatThongTinCaNhanDTO dto)
	{
		if (dto == null)
			return BadRequest(new { message = "Dữ liệu không hợp lệ." });

		try
		{
			var success = await _service.CapNhatAsync(id, dto);

			if (!success)
				return NotFound(new { message = "Không tìm thấy thông tin để cập nhật." });

			return Ok(new
			{
				message = "Cập nhật thông tin cá nhân thành công."
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = ex.Message });
		}
	}
}
