using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThietBiController : ControllerBase
    {
        private readonly ThietBiService _thietbi;
        public ThietBiController(ThietBiService thietbi)
        {
            _thietbi = thietbi;
        }

        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            var result = _thietbi.DanhSachThietBi();
            return Ok(result);
        }

        [HttpGet("TimThietBiTheoID")]
        public IActionResult TimTheoID(int ThietBiID)
        {
            var result = _thietbi.LayThietBiByID(ThietBiID);
            return Ok(result);
        }

        [HttpGet("TimThietBiTheoTen")]
        public IActionResult TimTheoTen([FromQuery] string tenTB)
        {
            var result = _thietbi.LayThietBiByTen(tenTB);
            return Ok(result);
        }

        [HttpPost("ThemThietBi")]
        public IActionResult ThemThietBi([FromBody] ThemThietBiDTO thietBi)
        {
            var result = _thietbi.ThemThietBi(thietBi);
            if (result)
            {
                return Ok("Thêm thiết bị thành công.");
            }
            return BadRequest("Thêm thiết bị thất bại");
        }

        [HttpPut("CapNhatThietBi")]
        public IActionResult CapNhatThietBi([FromBody] ThietBiDTO thietBi)
        {
            var result = _thietbi.CapNhatThietBi(thietBi);
            if (result)
            {
                return Ok("Cập nhật thiết bị thành công.");
            }
            return BadRequest("Cập nhật thiết bị thất bại");
        }
    }
}
