using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichLamViecNhanVienController : ControllerBase
    {
        private readonly LichLamViecNhanVienService _lich;
        public LichLamViecNhanVienController(LichLamViecNhanVienService lich)
        {
            _lich = lich;
        }

        [HttpGet("DanhSach")]
        public IActionResult DanhSach()
        {
            var result = _lich.DanhSach();
            return Ok(result);
        }

        [HttpGet("TimTheoID")]
        public IActionResult TimTheoID(int LichLamViecID)
        {
            var result = _lich.LayLichLamViecNhanVienByID(LichLamViecID);
            return Ok(result);
        }

        [HttpGet("TimTheoNhanVien")]
        public IActionResult TimTheoNhanVien(int NhanVienID)
        {
            var result = _lich.LayLichLamViecNhanVienByNhanVien(NhanVienID);
            return Ok(result);
        }

        [HttpPost("Them")]
        public IActionResult Them([FromBody] ThemLichLamViecNhanVienDTO lich)
        {
            var result = _lich.ThemLichLamViecNhanVien(lich);
            if (result)
            {
                return Ok("Thêm lịch làm việc nhân viên thành công.");
            }
            return BadRequest("Thêm lịch làm việc nhân viên thất bại");
        }

        [HttpPut("CapNhat")]
        public IActionResult CapNhat([FromBody] LichLamViecNhanVienDTO lich)
        {
            var result = _lich.CapNhatLichLamViecNhanVien(lich);
            if (result)
            {
                return Ok("Cập nhật lịch làm việc nhân viên thành công.");
            }
            return BadRequest("Cập nhật lịch làm việc nhân viên thất bại");
        }
    }
}
