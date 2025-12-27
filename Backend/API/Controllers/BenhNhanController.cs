using Services;
using Domain.DTO;
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
		[HttpPost("ThemBenhNhan")]
		public IActionResult ThemBenhNhan([FromBody] BenhNhanCreateDTO bn)
		{
			var result = _benhNhan.ThemBenhNhan(bn);
			if (result)
			{
				return Ok("Thêm bệnh nhân thành công.");
			}
			return BadRequest("Thêm bệnh nhân thất bại.");
		}
		[HttpPut("CapNhatBenhNhan")]
		public IActionResult CapNhatBenhNhan([FromBody] BenhNhanCreateDTO bn)
		{
			var result = _benhNhan.CapNhatBenhNhan(bn);
			if (result)
			{
				return Ok("Cập nhật bệnh nhân thành công.");
			}
			return BadRequest("Cập nhật bệnh nhân thất bại.");
		}
		[HttpPut("XoaBenhNhan")]
		public IActionResult XoaBenhNhan(int benhNhanID)
		{
			var result = _benhNhan.XoaBenhNhan(benhNhanID);
			if (result)
			{
				return Ok("Xóa bệnh nhân thành công.");
			}
			return BadRequest("Xóa bệnh nhân thất bại.");
		}
	}
}
