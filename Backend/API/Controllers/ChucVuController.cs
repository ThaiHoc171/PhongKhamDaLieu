using Services;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO;
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
		[HttpGet("DanhSach")]
		public IActionResult DanhSach()
		{
			var result = _chucVu.DanhSachChucVu();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult LayChucVuByID(int id)
		{
			var result = _chucVu.LayChucVuByID(id);
			if (result != null)
			{
				return Ok(result);
			}
			return NotFound("Chức vụ không tồn tại.");
		}
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
		[HttpPut("CapNhatChucVu")]
		public IActionResult CapNhatChucVu([FromBody] CapNhatChucVuDTO cv)
		{
			var result = _chucVu.CapNhatChucVu(cv);
			if (result)
			{
				return Ok("Cập nhật chức vụ thành công.");
			}
			return BadRequest("Cập nhật chức vụ thất bại.");
		}
		[HttpPut("VoHieuHoaChucVu/{id}")]
		public IActionResult VoHieuHoaChucVu(int id)
		{
			var result = _chucVu.VoHieuHoaChucVu(id);
			if (result)
			{
				return Ok("Vô hiệu hóa chức vụ thành công.");
			}
			return BadRequest("Vô hiệu hóa chức vụ thất bại.");
		}
	}
}
