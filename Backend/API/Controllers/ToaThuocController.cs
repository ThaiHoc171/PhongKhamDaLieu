using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToaThuocController : ControllerBase
    {
        private readonly ToaThuocService _toaThuocService;

        public ToaThuocController(ToaThuocService toaThuocService)
        {
            _toaThuocService = toaThuocService;
        }

        [HttpGet("DanhSachToaThuoc")]
        public IActionResult DanhSach()
        {
            return Ok(_toaThuocService.DanhSachToaThuoc());
        }

        [HttpGet("TimToaThuocTheoID")]
        public IActionResult TimTheoID(int toaThuocID)
        {
            return Ok(_toaThuocService.LayToaThuocByID(toaThuocID));
        }

        [HttpGet("TimToaThuocTheoPhienKham")]
        public IActionResult TimTheoPhienKham(int phienKhamID)
        {
            var result = _toaThuocService.LayToaThuocByPhienKhamID(phienKhamID);
            return Ok(result);
        }

        [HttpPost("ThemToaThuoc")]
        public IActionResult ThemToaThuoc([FromBody] ToaThuocCreateDTO toaThuoc)
        {
            if (_toaThuocService.ThemToaThuoc(toaThuoc))
                return Ok("Thêm toa thuốc thành công.");

            return BadRequest("Thêm toa thuốc thất bại.");
        }

        [HttpPut("CapNhatToaThuoc")]
        public IActionResult CapNhatToaThuoc([FromBody] ToaThuocDTO toaThuoc)
        {
            if (_toaThuocService.CapNhatToaThuoc(toaThuoc))
                return Ok("Cập nhật toa thuốc thành công.");

            return BadRequest("Cập nhật toa thuốc thất bại.");
        }
    }
}
