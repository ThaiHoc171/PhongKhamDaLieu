using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;
namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ChucVuController : ControllerBase
	{
		private readonly ChucVuService _chucVu;
		public ChucVuController(ChucVuService chucVu)
		{
			_chucVu = chucVu;
		}

		//Danh sách chức vụ
		[HttpGet("DanhSach")]
		public IActionResult LayDanhSachChucVu()
		{
			var result = _chucVu.DanhSachChucVu();
			return Ok(result);
		}
		// Lấy chức vụ theo ID
		[HttpGet("{id}")]
		public IActionResult LayChucVuTheoID(int id)
		{
			var result = _chucVu.LayChucVuTheoID(id);
			if (result == null)
			{
				return NotFound("Chức vụ không tồn tại.");
			}
			return Ok(result);
		}

		// Thêm chức vụ
		[HttpPost("ThemChucVu")]
		public IActionResult ThemChucVu([FromBody] ThemChucVuDTO cv)
		{
			var result = _chucVu.ThemChucVu(cv);
			if (result)
			{
				return Ok("Thêm chức vụ thành công.");
			}
			return BadRequest("Thêm chức vụ thất bại.");
		}

		// Cập nhật chức vụ
		[HttpPut("CapNhatChucVu")]
		public IActionResult CapNhatChucVu(int id, [FromBody] CapNhatChucVuDTO cv)
		{
			var result = _chucVu.CapNhatChucVu(id, cv);
			if (result)
			{
				return Ok("Cập nhật chức vụ thành công.");
			}
			return BadRequest("Cập nhật chức vụ thất bại.");
		}
		// Cập nhật trạng thái chức vụ
		[HttpPut("CapNhatTrangThai/{id}")]
		public IActionResult CapNhatTrangThai(int id, [FromBody] string trangThaiMoi)
		{
			var result = _chucVu.CapNhatTrangThaiChucVu(id, trangThaiMoi);
			if (result)
			{
				return Ok("Cập nhật trạng thái chức vụ thành công.");
			}
			return BadRequest("Cập nhật trạng thái chức vụ thất bại.");
		}
	}
}
