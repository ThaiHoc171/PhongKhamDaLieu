using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class KhungGioKhamController : ControllerBase
	{
		private readonly KhungGioKhamService _khungGioService;

		public KhungGioKhamController(KhungGioKhamService khungGioService)
		{
			_khungGioService = khungGioService;
		}

		// GET: api/KhungGioKham
		[HttpGet]
		public async Task<IActionResult> LayDanhSachKhungGio()
		{
			var result = await _khungGioService.DanhSachKhungGioAsync();
			return Ok(result);
		}

		// GET: api/KhungGioKham/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> LayKhungGioTheoId(int id)
		{
			var result = await _khungGioService.LayKhungGioTheoIdAsync(id);

			if (result == null)
				return NotFound(new { message = "Khung giờ khám không tồn tại." });

			return Ok(result);
		}

		// POST: api/KhungGioKham
		[HttpPost]
		public async Task<IActionResult> ThemKhungGio([FromBody] KhungGioKhamRequestDTO dto)
		{
			try
			{
				await _khungGioService.ThemKhungGioAsync(dto);
				return CreatedAtAction(nameof(LayKhungGioTheoId), new { id = dto }, new
				{
					message = "Thêm khung giờ khám thành công."
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// PUT: api/KhungGioKham/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> CapNhatKhungGio(int id, [FromBody] KhungGioKhamRequestDTO dto)
		{
			try
			{
				var result = await _khungGioService.CapNhatKhungGioAsync(id, dto);

				if (!result)
					return NotFound(new { message = "Khung giờ khám không tồn tại." });

				return Ok(new
				{
					message = "Cập nhật khung giờ khám thành công."
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					message = ex.Message
				});
			}
		}
	}
}
