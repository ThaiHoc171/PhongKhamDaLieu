using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacSiProfileController : ControllerBase
    {
        private readonly BacSiProfileService _service;

        public BacSiProfileController(BacSiProfileService service)
        {
            _service = service;
        }

        [HttpGet("DanhSachBacSiProfile")]
        public IActionResult DanhSach()
        {
            return Ok(_service.DanhSachBacSiProfile());
        }

        [HttpGet("TimBacSiProfileTheoID")]
        public IActionResult TimTheoID(int bacSiProfileID)
        {
            return Ok(_service.LayBacSiProfileByID(bacSiProfileID));
        }

        [HttpGet("TimBacSiProfileTheoNhanVien")]
        public IActionResult TimTheoNhanVien(int nhanVienID)
        {
            return Ok(_service.LayBacSiProfileByNhanVienID(nhanVienID));
        }

        [HttpPost("ThemBacSiProfile")]
        public IActionResult Them([FromBody] ThemBacSiProfileDTO bacSi)
        {
            if (_service.ThemBacSiProfile(bacSi))
                return Ok("Thêm hồ sơ bác sĩ thành công.");

            return BadRequest("Thêm hồ sơ bác sĩ thất bại.");
        }

        [HttpPut("CapNhatBacSiProfile")]
        public IActionResult CapNhat([FromBody] BacSiProfileDTO bacSi)
        {
            if (_service.CapNhatBacSiProfile(bacSi))
                return Ok("Cập nhật hồ sơ bác sĩ thành công.");

            return BadRequest("Cập nhật hồ sơ bác sĩ thất bại.");
        }
    }
}
