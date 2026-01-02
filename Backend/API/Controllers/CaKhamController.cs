using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaKhamController : ControllerBase
    {
        private readonly CaKhamService _caKham;
        public CaKhamController(CaKhamService CaKham)
        {
            _caKham = CaKham;
        }
        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            var result = _caKham.DanhSach();
            return Ok(result);
        }
        [HttpGet("ChiTiet/{id}")]
        public IActionResult ChiTiet(int id)
        {
            var result = _caKham.GetByID(id);
            return Ok(result);
        }
        [HttpGet("TimKiem")]
        public IActionResult TimKiem([FromQuery] string dm,string kw)
        {
            var result = _caKham.TimKiem(dm,kw);
            return Ok(result);
        }
        [HttpPost("ThemCaKham")]
        public IActionResult ThemCaKham([FromBody] ThemCaKhamDTO ck)
        {
            var result = _caKham.ThemCaKham(ck);
            if (result)
            {
                return Ok("Thêm ca khám thành công.");
            }
            return BadRequest("Thêm ca khám thất bại.");
        }
        [HttpPut("CapNhatCaKham")]
        public IActionResult CapNhatCaKham([FromBody] CaKhamDTO ck)
        {
            var result = _caKham.CapNhatCaKham(ck);
            if (result)
            {
                return Ok("Cập nhật ca khám thành công.");
            }
            return BadRequest("Cập nhật ca khám thất bại.");
        }

    }
}
