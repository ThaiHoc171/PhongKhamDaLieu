using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhungGioKhamController : ControllerBase
    {
        private readonly KhungGioKhamService _service;

        public KhungGioKhamController(KhungGioKhamService service)
        {
            _service = service;
        }

        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            return Ok(_service.DanhSach());
        }

        [HttpGet("TimKhungGioTheoID")]
        public IActionResult TimTheoID(int khungGioID)
        {
            return Ok(_service.LayKhungGioKhamByID(khungGioID));
        }

        [HttpGet("TimKhungGioTheoTen")]
        public IActionResult TimTheoTen([FromQuery] string tenKhung)
        {
            return Ok(_service.LayKhungGioKhamByTen(tenKhung));
        }

        [HttpPost("ThemKhungGioKham")]
        public IActionResult Them([FromBody] ThemKhungGioKhamDTO kg)
        {
            if (_service.ThemKhungGioKham(kg))
                return Ok("Thêm khung giờ khám thành công");

            return BadRequest("Thêm khung giờ khám thất bại");
        }

        [HttpPut("CapNhatKhungGioKham")]
        public IActionResult CapNhat([FromBody] KhungGioKhamDTO kg)
        {
            if (_service.CapNhatKhungGioKham(kg))
                return Ok("Cập nhật khung giờ khám thành công");

            return BadRequest("Cập nhật khung giờ khám thất bại");
        }
    }
}
