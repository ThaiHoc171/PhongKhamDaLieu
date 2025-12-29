using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietToaThuocController : ControllerBase
    {
        private readonly ChiTietToaThuocService _service;

        public ChiTietToaThuocController(ChiTietToaThuocService service)
        {
            _service = service;
        }

        [HttpGet("DanhSachChiTietToaThuoc")]
        public IActionResult DanhSach()
        {
            return Ok(_service.DanhSachChiTietToaThuoc());
        }

        [HttpGet("TimChiTietTheoID")]
        public IActionResult TimTheoID(int chiTietToaThuocID)
        {
            return Ok(_service.LayChiTietToaThuocByID(chiTietToaThuocID));
        }

        [HttpGet("TimChiTietTheoToaThuoc")]
        public IActionResult TimTheoToaThuoc(int toaThuocID)
        {
            return Ok(_service.LayChiTietToaThuocByToaThuocID(toaThuocID));
        }

        [HttpPost("ThemChiTietToaThuoc")]
        public IActionResult Them([FromBody] ChiTietToaThuocCreateDTO ct)
        {
            if (_service.ThemChiTietToaThuoc(ct))
                return Ok("Thêm chi tiết toa thuốc thành công.");

            return BadRequest("Thêm chi tiết toa thuốc thất bại.");
        }

        [HttpPut("CapNhatChiTietToaThuoc")]
        public IActionResult CapNhat([FromBody] ChiTietToaThuocDTO ct)
        {
            if (_service.CapNhatChiTietToaThuoc(ct))
                return Ok("Cập nhật chi tiết toa thuốc thành công.");

            return BadRequest("Cập nhật chi tiết toa thuốc thất bại.");
        }
    }
}
