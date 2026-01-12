using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;
using Domain.Entities;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CanLamSangController : ControllerBase
	{
		private readonly CanLamSangService _Service;
		public CanLamSangController(CanLamSangService service)
		{
			_Service = service;
		}

		[HttpGet("DanhSachCLS")]
		public IActionResult DanhSasch() 
		{
			return Ok(_Service.DanhSachCanLamSang());
		}
		[HttpGet("ChiTietCLS{id}")]
		public IActionResult ChiTietClS(int id) 
		{ 
			return Ok(_Service.CanLamSang(id));
		}
		[HttpPost("ThemCLS")]
		public IActionResult ThemCLS(ThemCanLamSangDTO cls)
		{
			var result = _Service.ThemCanLamSang(cls);
			if(result)
				return Ok();
			return BadRequest("Có lỗi khi tạo cận lâm sàng");
		}
		[HttpPut("CapNhatCLS")]
		public IActionResult CapNhatCLS(CapNhatCanLamSangDTO cls)
		{
			var result = _Service.CapNhatCLS(cls);
			if (result)
				return Ok();
			return BadRequest("Có lỗi khi cập nhật");
		}
		[HttpPut("ChuyenTrangThai")]
		public IActionResult ChuyenTrangThai(Status stt)
		{
			var result = _Service.ChuyenTrangThai(stt);
			if (result)
				return Ok();
			return BadRequest("Có lỗi khi cập nhật");
		}
	}
}
