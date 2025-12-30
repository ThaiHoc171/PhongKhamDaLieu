using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoaiBenhController : ControllerBase
    {
        private readonly LoaiBenhService _loaibenh;
        public LoaiBenhController(LoaiBenhService loaibenh)
        {
            _loaibenh = loaibenh;
        }
        [HttpGet("DanhSachLoaiBenh")]
        public IActionResult DanhSach()
        {
            var result = _loaibenh.DanhSachLoaiBenh();
            return Ok(result);
        }
        [HttpGet("TimBenhTheoID")]
        public IActionResult TimTheoID(int loaiBenhID)
        {
            var result = _loaibenh.LayLoaiBenhByID(loaiBenhID);
            return Ok(result);
        }
        [HttpGet("TimBenhTheoTen")]
        public IActionResult TimTheoTen([FromQuery] string tenBenh)
        {
            var result = _loaibenh.LayLoaiBenhByTen(tenBenh);
            return Ok(result);
        }
        [HttpPost("ThemLoaiBenh")]
        public IActionResult ThemLoaiBenh([FromBody] ThemLoaiBenhDTO loaiBenh)
        {
            var result = _loaibenh.ThemLoaiBenh(loaiBenh);
            if (result)
            {
                return Ok("Thêm loại bệnh thành công.");
            }
            return BadRequest("Thêm loại bệnh thất bại");

        }
        [HttpPut("CapNhatLoaiBenh")]
        public IActionResult CapNhatLoaiBenh([FromBody] LoaiBenhDTO loaiBenh)
        {
            var result = _loaibenh.CapNhatLoaiBenh(loaiBenh);
            if (result)
            {
                return Ok("Cập nhật thông tin loại bệnh thành công.");
            }
            return BadRequest("Cập nhật thông tin loại bệnh thất bại");

        }
    }
}
