using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HoSoBenhAnController : ControllerBase
{
    private readonly HoSoBenhAnService _hoSoBenhAnService;

    public HoSoBenhAnController(HoSoBenhAnService hoSoBenhAnService)
    {
        _hoSoBenhAnService = hoSoBenhAnService;
    }

    // POST: api/HoSoBenhAn
    [HttpPost]
    public async Task<IActionResult> TaoHoSoBenhAn([FromBody] TaoHoSoBenhAnDTO dto)
    {
        try
        {
            var Hoso = await _hoSoBenhAnService.TaoHoSoBenhAn(dto);
            return Ok(new
            {
                Message = "Tạo hồ sơ bệnh án thành công",
                HoSoBenhAnID = Hoso
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = "Tạo hồ sơ bệnh án thất bại",
                Error = ex.Message
            });
        }
    }
    // GET: api/HoSoBenhAn
    [HttpGet]
    public async Task<IActionResult> TatCa()
    {
        var list = await _hoSoBenhAnService.GetAllAsync();
        return Ok(list);
    }

    // GET: api/HoSoBenhAn/benhnhan/{benhNhanID}
    [HttpGet("benhnhan/{benhNhanID}")]
    public async Task<IActionResult> LocTheoBenhNhan(int benhNhanId)
    {
        var list = await _hoSoBenhAnService.GetByBenhNhanIdAsync(benhNhanId);
        return Ok(list);
    }

    // GET: api/HoSoBenhAn/{hoSoBenhAnID}
    [HttpGet("{hoSoBenhAnID}")]
    public async Task<IActionResult> LocTheoID(int hoSoBenhAnID)
    {
        var list = await _hoSoBenhAnService.GetByBenhNhanIdAsync(hoSoBenhAnID);
        return Ok(list);
    }

    // PUT: api/HoSoBenhAn/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhatHoSo(int id,[FromBody] HoSoBenhAnUdateDTO dto)
    {
        var result = await _hoSoBenhAnService.CapNhatThongTinAsync(
            hoSoBenhAnID: id,
            benhNen: dto.BenhNen,
            diUng: dto.DiUng,
            tienSuBenh:dto.TienSuBenh,
            tienSuGiaDinh:dto.TienSuGiaDinh,
            thoiQuenSong:dto.ThoiQuenSong,
            thongTinKhac: dto.ThongTinKhac,
            ngayCapNhat: dto.NgayCapNhat
        );

        if (!result)
            return NotFound(new { Message = "Hồ sơ không tồn tại" });

        return Ok(new { Message = "Bổ sung hồ sơ thành công" });
    }

}

