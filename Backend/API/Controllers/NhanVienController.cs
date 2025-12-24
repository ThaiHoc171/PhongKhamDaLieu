using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NhanVienController : ControllerBase
	{
		private readonly NhanVienService _nhanVien;
		public NhanVienController(NhanVienService nhanVien)
		{
			_nhanVien = nhanVien;
		}
		[HttpGet("DanhSach")]
		public IActionResult DanhSach()
		{
			var result = _nhanVien.LayDanhSachNhanVien();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult NhanVien(int id)
		{
			var result = _nhanVien.LayThongTinTheoID(id);
			return Ok(result);
		}
		[HttpPost("ThemNhanVien")]
		public IActionResult ThemNhanVien([FromBody] NhanVienDetailDTO nv)
		{
			var result = _nhanVien.ThemNhanVien(nv);
			if (result)
			{
				return Ok("Thêm nhân viên thành công.");
			}
			return BadRequest("Thêm nhân viên thất bại.");
		}

	}
}
