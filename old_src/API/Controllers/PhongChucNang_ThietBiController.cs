using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhongChucNang_ThietBiController : ControllerBase
    {
        private readonly PhongChucNang_ThietBiService _pcn_tb;
        public PhongChucNang_ThietBiController(PhongChucNang_ThietBiService pcn_tb)
        {
            _pcn_tb = pcn_tb;
        }

        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            var result = _pcn_tb.DanhSach();
            return Ok(result);
        }

        [HttpGet("TimTheoID")]
        public IActionResult TimTheoID(int PCN_TB_ID)
        {
            var result = _pcn_tb.LayPhongChucNang_ThietBiByID(PCN_TB_ID);
            return Ok(result);
        }

        [HttpGet("TimTheoPhong")]
        public IActionResult TimTheoPhong(int PhongChucNangID)
        {
            var result = _pcn_tb.LayPhongChucNang_ThietBiByPhong(PhongChucNangID);
            return Ok(result);
        }

        [HttpPost("Them")]
        public IActionResult Them([FromBody] ThemPhongChucNang_ThietBiDTO pcn_tb)
        {
            var result = _pcn_tb.ThemPhongChucNang_ThietBi(pcn_tb);
            if (result)
            {
                return Ok("Thêm thiết bị vào phòng chức năng thành công.");
            }
            return BadRequest("Thêm thiết bị vào phòng chức năng thất bại");
        }

        [HttpPut("CapNhat")]
        public IActionResult CapNhat([FromBody] PhongChucNang_ThietBiDTO pcn_tb)
        {
            var result = _pcn_tb.CapNhatPhongChucNang_ThietBi(pcn_tb);
            if (result)
            {
                return Ok("Cập nhật thiết bị phòng chức năng thành công.");
            }
            return BadRequest("Cập nhật thiết bị phòng chức năng thất bại");
        }
    }
}
