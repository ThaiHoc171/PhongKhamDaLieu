using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LieuTrinhDieuTriController : ControllerBase
{
    private readonly LieuTrinhDieuTriService _service;

    public LieuTrinhDieuTriController(LieuTrinhDieuTriService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> LayDanhSachTheoID(int id)
    {
        var result = await _service.LayTheoIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Liệu trình không tồn tại." });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> LayDanhSach()
    {
        var result = await _service.DanhSachAsync();
        return Ok(result);
    }
    
    [HttpGet("benhnhan/{benhNhanId:int}")]
    public async Task<IActionResult> LayTheoBenhNhanId(int benhNhanId)
    {
        var result = await _service.LayTheoBenhNhanAsync(benhNhanId);
        if (result == null)
            return NotFound(new { message = "Bệnh nhân không tồn tại hoặc chưa có lịch tái khám." });

        return Ok(result);
    }

    [HttpGet("Ngay/NgayBatDau")]
    public async Task<IActionResult> LocTheoNgayBatDau(DateTime ngay, string trangThai)
    {
        var result = await _service.LocBatDauAsync(ngay, trangThai);
        if (result == null)
            return NotFound(new { message = "Ngày tìm kiếm hoặc trạng thái không tồn tại." });

        return Ok(result);
    }

    [HttpGet("Ngay/NgayKetThuc")]
    public async Task<IActionResult> LocTheoNgayKetThuc(DateTime ngay, string trangThai)
    {
        var result = await _service.LocKetThucAsync(ngay, trangThai);
        if (result == null)
            return NotFound(new { message = "Ngày tìm kiếm hoặc trạng thái không tồn tại." });

        return Ok(result);
    }

    [HttpGet("benhnhan/list")]
    public async Task<IActionResult> ListTheoBenhNhan(int benhNhanId)
    {
        var result = await _service.DanhSachTheoBenhNhanAsync(benhNhanId);
        if (result == null)
            return NotFound(new { message = "Bệnh nhân không tồn tại hoặc chưa có lịch tái khám." });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Them([FromBody] TaoLieuTrinhDieuTriDTO dto)
    {
        await _service.TaoLieuTrinhAsync(dto);
        return Ok(new { message = "Thêm liệu trình thành công." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(
        int id,
        [FromBody] CapNhatLieuTrinhDieuTriDTO dto)
    {
        var result = await _service.CapNhatAsync(
            lieuTrinhID: id,
            tenLieuTrinh: dto.TenLieuTrinh,
            tongSoBuoi: dto.TongSoBuoi,
            ngayBatDau: dto.NgayBatDau,
            ngayKetThuc: dto.NgayKetThuc
        );

        if (!result)
            return NotFound(new { Message = "Liệu trình không tồn tại" });

        return Ok(new { Message = "Cập nhật liệu trình thành công" });
    }

    [HttpPut("TrangThai/{id}")]
    public async Task<IActionResult> CapNhatTrangThai(
    int id,
    [FromBody] CapNhatTrangThaiLieuTrinhDieuTriDTO dto)
    {
        var result = await _service.CapNhatTrangThaiAsync(
            lieuTrinhID: id,
            trangThai: dto.TrangThai,
            ghiChu: dto.GhiChu
        );

        if (!result)
            return NotFound(new { Message = "Liệu trình không tồn tại" });

        return Ok(new { Message = "Cập nhật trạng thái liệu trình thành công" });
    }
}
