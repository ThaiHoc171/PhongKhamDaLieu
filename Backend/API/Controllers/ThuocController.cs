using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThuocController : ControllerBase
    {
        private readonly ThuocService _thuoc;
        public ThuocController(ThuocService thuoc)
        {
            _thuoc = thuoc;
        }
        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            var result = _thuoc.DanhSach();
            return Ok(result);
        }
        [HttpGet("DanhSachThuocTheoKeyWords")]
        public IActionResult TimTheoTen([FromQuery] string kw)
        {
            var result = _thuoc.DanhSachThuocTheoKeywords(kw);
            return Ok(result);
        }
        [HttpGet("LayThuocTheoID")]
        public IActionResult TimTheoID(int thuocID)
        {
            var result = _thuoc.LayThuocTheoID(thuocID);
            return Ok(result);
        }
		[HttpPost("ThemThuoc")]
		public IActionResult ThemThuoc([FromBody] ThuocCreateDTO thuoc)
		{
			var result = _thuoc.ThemThuoc(thuoc);
			if (result)
			{
				return Ok("Thêm thuốc thành công.");
			}
			return BadRequest("Thêm thuốc thất bại");
		}
		[HttpPost("CapNhatThuoc")]
		public IActionResult CapNhatThuoc([FromBody] ThuocDTO thuoc)
		{
			var result = _thuoc.CapNhatThuoc(thuoc);
			if (result)
			{
				return Ok("Cập nhật thuốc thành công.");
			}
			return BadRequest("Cập nhật thuốc thất bại");
		}
	}
}
