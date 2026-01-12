using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhongKhamController : ControllerBase
    {
        private readonly PhongKhamService _service;

        public PhongKhamController(PhongKhamService service)
        {
            _service = service;
        }

        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            return Ok(_service.DanhSachPhongKham());
        }


        [HttpPut("CapNhatPhongKham")]
        public IActionResult CapNhat([FromBody] PhongKhamDTO phongKham)
        {
            if (_service.CapNhatPhongKham(phongKham))
                return Ok("Cập nhật phòng khám thành công.");

            return BadRequest("Cập nhật phòng khám thất bại.");
        }
    }
}
