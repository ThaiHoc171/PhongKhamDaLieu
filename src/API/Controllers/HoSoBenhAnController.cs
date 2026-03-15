using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace API.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class HoSoBenhAnController : ControllerBase
{
    private readonly HoSoBenhAnService _hoSoBenhAnService;
    public HoSoBenhAnController(HoSoBenhAnService hoSoBenhAnService)
    {
        _hoSoBenhAnService = hoSoBenhAnService;
    }
	// POST: api/HoSoBenhAn
	[Authorize(Policy = "BacSiOnly")]
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
	[Authorize(Policy = "BacSiOnly")]
	[HttpGet]
    public async Task<IActionResult> TatCa()
    {
        var list = await _hoSoBenhAnService.GetAllAsync();
        return Ok(list);
    }
	// GET: api/HoSoBenhAn/benhnhan/{benhNhanID}
	[Authorize(Roles = "Admin,Nhân viên,Bệnh nhân")]
	[HttpGet("benhnhan/{benhNhanID:int}")]
    public async Task<IActionResult> GetByBenhNhan(int benhNhanID)
    {
        var hs = await _hoSoBenhAnService.GetByBenhNhanIdAsync(benhNhanID);
        return hs == null ? NotFound() : Ok(hs);
    }
    // GET: api/HoSoBenhAn/{hoSoBenhAnID}
    [HttpGet("{hoSoBenhAnID}")]
    public async Task<IActionResult> LocTheoID(int hoSoBenhAnID)
    {
        var list = await _hoSoBenhAnService.GetByIdAsync(hoSoBenhAnID);
        return Ok(list);
    }
	// PUT: api/HoSoBenhAn/{id}
	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}")]
    public async Task<IActionResult> CapNhatHoSo(int id,[FromBody] HoSoBenhAnUpdateDTO dto)
    {
        var result = await _hoSoBenhAnService.CapNhatThongTinAsync(id, dto);
        if (!result)
            return NotFound(new { Message = "Hồ sơ không tồn tại" });
        return Ok(new { Message = "Bổ sung hồ sơ thành công" });
    }
}
