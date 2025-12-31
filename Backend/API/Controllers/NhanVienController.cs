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
			var result = _nhanVien.LayNhanVienTheoID(id);
			return Ok(result);
		}
		[HttpGet("TimKiem")]
		public IActionResult TimKiemNhanVien(string keyword)
		{
			var result = _nhanVien.LayNhanVienTheoTuKhoa(keyword);
			return Ok(result);
		}
		[HttpPost("ThemNhanVien")]
		public IActionResult ThemNhanVien([FromBody] NhanVienCreateDTO nv)
		{
			var result = _nhanVien.ThemNhanVien(nv);
			if (result.Success)
				return Ok(result.Message);
			return BadRequest(result.Message);
		}
		[HttpPut("CapNhatNhanVien")]
		public IActionResult CapNhatNhanVien([FromBody] NhanVienUpdateDTO nv)
		{
			var result = _nhanVien.CapNhatNhanVien(nv);
			if (result.Success)
				return Ok(result.Message);
			return BadRequest(result.Message);
		}
		[HttpPut("XoaNhanVien")]
		public IActionResult XoaNhanVien(int nhanVienID)
		{
			var result = _nhanVien.XoaNhanVien(nhanVienID);
			if (result)
			{
				return Ok("Xóa nhân viên thành công.");
			}
			return BadRequest("Xóa nhân viên thất bại.");
		}
	}
}
