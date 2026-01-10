using Application.DTOs;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NhanVienController : ControllerBase
	{
		private readonly NhanVienService _service;

		public NhanVienController(NhanVienService service)
		{
			_service = service;
		}

		[HttpPost]
		public IActionResult ThemNhanVien([FromBody] ThemNhanVienDTO dto)
		{
			try
			{
				_service.ThemNhanVien(dto);
				return StatusCode(201, new { message = "Thêm nhân viên thành công" });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public IActionResult DanhSachNhanVien()
		{
			var result = _service.DanhSachNhanVien();
			return Ok(result);
		}

		[HttpGet("search")]
		public IActionResult TimKiemNhanVien([FromQuery] string keyword)
		{
			var result = _service.TimKiemNhanVien(keyword);
			return Ok(result);
		}


		[HttpGet("{id}")]
		public IActionResult ChiTietNhanVien(int id)
		{
			var nv = _service.ChiTietNhanVien(id);
			if (nv == null)
				return NotFound("Nhân viên không tồn tại");

			return Ok(nv);
		}

		[HttpPut("{id}")]
		public IActionResult CapNhatNhanVien(int id, [FromBody] ThemNhanVienDTO dto)
		{
			var success = _service.CapNhatNhanVien(id, dto);
			if (!success)
				return NotFound("Nhân viên không tồn tại");

			return Ok(new { message = "Cập nhật nhân viên thành công" });
		}

		[HttpPut("{id}/nghi-viec")]
		public IActionResult ChoNhanVienNghiViec(int id)
		{
			var success = _service.CapNhatTrangThai(id);
			if (!success)
				return NotFound("Nhân viên không tồn tại");

			return Ok(new { message = "Nhân viên đã nghỉ việc" });
		}
	}
}
