using System;
using Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PhongChucNangController : ControllerBase
	{
		private readonly PhongChucNangService _phongChucNang;
		public PhongChucNangController(PhongChucNangService phongChucNang)
		{
			_phongChucNang = phongChucNang;
		}
		[HttpGet("DanhSach")]
		public IActionResult DanhSach()
		{
			var result = _phongChucNang.DanhSachPhongChucNang();
			return Ok(result);
		}
		[HttpGet("ChiTiet/{id}")]
		public IActionResult ChiTiet(int id)
		{
			var result = _phongChucNang.LayPhongChucNangTheoID(id);
			return Ok(result);
		}
		[HttpGet("TimKiem")]
		public IActionResult TimKiem([FromQuery] string kw)
		{
			var result = _phongChucNang.TimKiem(kw);
			return Ok(result);
		}
		[HttpPost("ThemPhongChucNang")]
		public IActionResult ThemPhongChucNang([FromBody] PhongChucNangCreateDTO pcn)
		{
			var result = _phongChucNang.ThemPhongChucNang(pcn);
			if (result)
			{
				return Ok("Thêm phòng chức năng thành công.");
			}
			return BadRequest("Thêm phòng chức năng thất bại.");
		}
		[HttpPut("CapNhatPhongChucNang")]
		public IActionResult CapNhatPhongChucNang([FromBody] PhongChucNangDTO pcn)
		{
			var result = _phongChucNang.CapNhatPhongChucNang(pcn);
			if (result)
			{
				return Ok("Cập nhật phòng chức năng thành công.");
			}
			return BadRequest("Cập nhật phòng chức năng thất bại.");
		}
		[HttpPut("ChuyenTrangThai")]
		public IActionResult ChuyenTrangThai(Status stt)
		{
			var result = _phongChucNang.ChuyenTrangThai(stt);
			if (result)
			{
				return Ok("Thay đổi thành công.");
			}
			return BadRequest("Thay đổi thất bại.");
		}
	}
}
