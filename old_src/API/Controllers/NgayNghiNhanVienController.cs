using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NgayNghiNhanVienController : ControllerBase
    {
        private readonly NgayNghiNhanVienService _service;

        public NgayNghiNhanVienController(NgayNghiNhanVienService service)
        {
            _service = service;
        }

        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            return Ok(_service.DanhSach());
        }

        [HttpGet("TimNgayNghiByID")]
        public IActionResult TimTheoID(int ngayNghiID)
        {
            return Ok(_service.LayNgayNghiByID(ngayNghiID));
        }

        [HttpGet("TimNgayNghiByNhanVien")]
        public IActionResult TimTheoNhanVien(int nhanVienID)
        {
            return Ok(_service.LayNgayNghiByNhanVien(nhanVienID));
        }

        [HttpPost("ThemNgayNghi")]
        public IActionResult Them([FromBody] ThemNgayNghiNhanVienDTO dto)
        {
            if (_service.ThemNgayNghi(dto))
                return Ok("Thêm ngày nghỉ thành công");

            return BadRequest("Thêm ngày nghỉ thất bại");
        }

        [HttpPut("CapNhatNgayNghi")]
        public IActionResult CapNhat([FromBody] NgayNghiNhanVienDTO dto)
        {
            if (_service.CapNhatNgayNghi(dto))
                return Ok("Cập nhật ngày nghỉ thành công");

            return BadRequest("Cập nhật ngày nghỉ thất bại");
        }
    }
}
