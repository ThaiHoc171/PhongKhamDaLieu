using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ThongTinCaNhanController : ControllerBase
	{
		private readonly ThongTinCaNhanService _service;

		public ThongTinCaNhanController(ThongTinCaNhanService service)
		{
			_service = service;
		}
		// THÊM NHÂN VIÊN
		[HttpPost("NhanVien")]
		public IActionResult ThemNhanVien([FromBody] ThemThongTinCaNhanDTO dto)
		{
			if (dto == null) return BadRequest();

			int id = _service.ThemThongTinNhanVien(dto);
			return CreatedAtAction(nameof(ThongTin), new { id }, null);
		}

		// THÊM BỆNH NHÂN
		[HttpPost("BenhNhan")]
		public IActionResult ThemBenhNhan([FromBody] ThemThongTinCaNhanDTO dto)
		{
			if (dto == null) return BadRequest();

			int id = _service.ThemThongTinBenhNhan(dto);
			return CreatedAtAction(nameof(ThongTin), new { id }, null);
		}

		// DANH SÁCH BỆNH NHÂN
		[HttpGet("BenhNhan")]
		public ActionResult<List<ThongTinCaNhanResponseDTO>> DanhSachBenhNhan()
		{
			return Ok(_service.DanhSachThongTinBenhNhan());
		}

		// DANH SÁCH NHÂN VIÊN
		[HttpGet("NhanVien")]
		public ActionResult<List<ThongTinCaNhanResponseDTO>> DanhSachNhanVien()
		{
			return Ok(_service.DanhSachThongTinNhanVien());
		}

		// CHI TIẾT THEO ID
		[HttpGet("{id}")]
		public ActionResult<ThongTinCaNhanResponseDTO> ThongTin(int id)
		{
			var result = _service.ThongTin(id);
			if (result == null)
				return NotFound();

			return Ok(result);
		}

		// CẬP NHẬT THÔNG TIN
		[HttpPut("{id}")]
		public IActionResult CapNhat(int id, [FromBody] CapNhatThongTinCaNhanDTO dto)
		{
			if (dto == null) return BadRequest();

			bool success = _service.CapNhatThongTin(id, dto);
			if (!success)
				return NotFound();

			return NoContent();
		}
	}
}
