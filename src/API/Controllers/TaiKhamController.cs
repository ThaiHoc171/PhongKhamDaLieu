using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TaiKhamController : ControllerBase
{
    private readonly TaiKhamService _service;

    public TaiKhamController(TaiKhamService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> LayDanhSach()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> LayTheoId(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Phòng chức năng không tồn tại." });

        return Ok(result);
    }

    [HttpGet("benhnhan/{benhNhanId:int}")]
    public async Task<IActionResult> LayTheoBenhNhanId(int benhNhanId)
    {
        var result = await _service.GetByBenhNhanAsync(benhNhanId);
        if (result == null)
            return NotFound(new { message = "Bệnh không tồn tại hoặc chưa có lịch tái khám." });

        return Ok(result);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Loc(DateTime ngayDuKien, string trangThai)
    {
        var result = await _service.LocAsync(ngayDuKien, trangThai);
        if (result == null)
            return NotFound(new { message = "Ngày dự kiến hoặc trạng thái không tồn tại." });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Them([FromBody] TaoTaiKhamDTO dto)
    {
        await _service.TaoTaiKhamAsync(dto);
        return Ok(new { message = "Thêm lịch tái khám thành công." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(
        int id,
        [FromBody] CapNhatTaiKhamDTO dto)
    {
        var result = await _service.CapNhatAsync(
            taiKhamID: id,
            ngayDuKien: dto.NgayDuKien,
            lyDo: dto.LyDo,
            trangThai: dto.TrangThai,
            caKhamID: dto.CaKhamID
        );

        if (!result)
            return NotFound(new { Message = "Lịch tái khám không tồn tại" });

        return Ok(new { Message = "Cập nhật lịch tái khám thành công" });
    }
}
