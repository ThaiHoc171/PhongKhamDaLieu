using Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BenhNhanController : ControllerBase
	{
		private readonly BenhNhanService _benhNhan;
		public BenhNhanController(BenhNhanService benhNhan)
		{
			_benhNhan = benhNhan;
		}
		[HttpGet("DanhSach")]
		public IActionResult DanhSach()
		{
			var result = _benhNhan.LayDanhSachBenhNhan();
			return Ok(result);
		}
		[HttpGet("TimKiem")]
		public IActionResult TimKiem(string keyword)
		{
			var result = _benhNhan.LayBenhNhanByKeyWord(keyword);
			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult BenhNhan(int id)
		{
			var result = _benhNhan.LayBenhNhanByID(id);
			if (result != null)
			{
				return Ok(result);
			}
			return NotFound("Bệnh nhân không tồn tại.");
		}
		[HttpPost("ThemHoSoBenhNhan")]
		public IActionResult ThemHoSoBenhNhan([FromBody] HoSoBenhNhanDTO bn)
		{
			var result = _benhNhan.ThemThongTinBenhNhan(bn);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
		[HttpPost("ThemBenhNhan")]
		public IActionResult ThemBenhNhan([FromBody] ThemBenhNhanDTO bn)
		{
			var result = _benhNhan.ThemBenhNhan(bn);
			if (result)
			{
				return Ok("Thêm bệnh nhân thành công.");
			}
			return BadRequest("Thêm bệnh nhân thất bại.");
		}
		[HttpPut("CapNhatBenhNhan")]
		public IActionResult CapNhatBenhNhan([FromBody] BenhNhanUpdateDTO bn)
		{
			var result = _benhNhan.CapNhatBenhNhan(bn);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
		[HttpPut("ChuyenTrangThai")]
		public IActionResult ChuyenTrangThai([FromBody] Status stt)
		{
			var result = _benhNhan.ChuyenTrangThai(stt);
			if (result)
			{
				return Ok("Thay đổi thành công.");
			}
			return BadRequest("Thay đổi thất bại.");
		}
	}
}
