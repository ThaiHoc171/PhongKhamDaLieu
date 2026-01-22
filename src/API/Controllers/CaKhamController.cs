using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaKhamController : ControllerBase
{
    private readonly CaKhamService _caKhamService;

    public CaKhamController(CaKhamService caKhamService)
    {
        _caKhamService = caKhamService;
    }

    // POST: api/CaKham
    [HttpPost]
    public async Task<IActionResult> TaoCaKham([FromBody] TaoCaKhamDTO dto)
    {
        try
        {
            var caKhamId = await _caKhamService.TaoCaKhamAsync(dto);
            return Ok(new
            {
                Message = "Tạo ca khám thành công",
                CaKhamID = caKhamId
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = "Tạo ca khám thất bại",
                Error = ex.Message
            });
        }
    }

    // PUT: api/CaKham/{id}/dangky
    [HttpPut("{id}/dangky")]
    public async Task<IActionResult> DangKyKham(
        int id,
        [FromBody] DangKyCaKhamDTO dto)
    {
        var result = await _caKhamService.DangKyKhamAsync(
            caKhamID: id,
            benhNhanID: dto.BenhNhanID,
            lyDoKham: dto.LyDoKham,
            ngayDat: dto.NgayDat,
            ghiChu: dto.GhiChu
        );

        if (!result)
            return NotFound(new { Message = "Ca khám không tồn tại" });

        return Ok(new { Message = "Đăng ký ca khám thành công" });
    }

    // PUT: api/CaKham/{id}/trangthai
    [HttpPut("{id}/trangthai")]
    public async Task<IActionResult> CapNhatTrangThai(
        int id,
        [FromBody] string trangThai)
    {
        var result = await _caKhamService.UpdateTrangThaiAsync(id, trangThai);
        if (!result)
            return NotFound(new { Message = "Ca khám không tồn tại" });

        return Ok(new { Message = "Cập nhật trạng thái thành công" });
    }

    // GET: api/CaKham/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> LayTheoId(int id)
    {
        var caKham = await _caKhamService.LayCaKhamTheoIdAsync(id);
        if (caKham == null)
            return NotFound(new { Message = "Ca khám không tồn tại" });

        return Ok(caKham);
    }

    // GET: api/CaKham/ngay?ngay=2026-01-20
    [HttpGet("ngay")]
    public async Task<IActionResult> TheoNgay([FromQuery] DateTime ngay, string trangThai)
    {
        var list = await _caKhamService.DanhSachCaKhamTheoNgayAsync(ngay, trangThai);
        return Ok(list);
    }

    // GET: api/CaKham/benhnhan/{benhNhanId}
    [HttpGet("benhnhan/{benhNhanId}")]
    public async Task<IActionResult> TheoBenhNhan(int benhNhanId)
    {
        var list = await _caKhamService.GetByBenhNhanAsync(benhNhanId);
        return Ok(list);
    }


    // GET: api/CaKham
    [HttpGet]
    public async Task<IActionResult> TatCa()
    {
        var list = await _caKhamService.GetAllAsync();
        return Ok(list);
    }
}
