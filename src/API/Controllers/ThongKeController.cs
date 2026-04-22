using Application.DTOs.ThongKe;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/thongke")]
public class ThongKeController : ControllerBase
{
    private readonly ThongKeService _service;

    public ThongKeController(ThongKeService service)
    {
        _service = service;
    }

    [HttpGet("benh-nhan/tong-quan")]
    public async Task<IActionResult> GetTongQuanBenhNhan([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetTongQuanBenhNhanAsync(f));

    [HttpGet("benh-nhan/theo-ngay")]
    public async Task<IActionResult> GetBenhNhanTheoNgay([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetBenhNhanTheoNgayAsync(f));

    [HttpGet("benh-nhan/theo-gioi-tinh")]
    public async Task<IActionResult> GetBenhNhanTheoGioiTinh([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetBenhNhanTheoGioiTinhAsync(f));

    [HttpGet("benh-nhan/theo-do-tuoi")]
    public async Task<IActionResult> GetBenhNhanTheoDoTuoi([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetBenhNhanTheoDoTuoiAsync(f));

    [HttpGet("ca-kham/tong-quan")]
    public async Task<IActionResult> GetTongQuanCaKham([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetTongQuanCaKhamAsync(f));


    [HttpGet("ca-kham/theo-khoang")]
    public async Task<IActionResult> GetCaKhamTheoKhoang([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetCaKhamTheoKhoangAsync(f));

    [HttpGet("phien-kham/tong-quan")]
    public async Task<IActionResult> GetTongQuanPhienKham([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetTongQuanPhienKhamAsync(f));

    [HttpGet("phien-kham/theo-ngay")]
    public async Task<IActionResult> GetPhienKhamTheoNgay([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetPhienKhamTheoNgayAsync(f));

    [HttpGet("phien-kham/theo-phong")]
    public async Task<IActionResult> GetPhienKhamTheoPhong([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetPhienKhamTheoPhongAsync(f));

    [HttpGet("phien-kham/theo-loai-benh")]
    public async Task<IActionResult> GetPhienKhamTheoLoaiBenh(
        [FromQuery] ThongKeFilterRequest f,
        [FromQuery] int top = 10)
        => Ok(await _service.GetPhienKhamTheoLoaiBenhAsync(f, top));

    [HttpGet("toa-thuoc/tong-quan")]
    public async Task<IActionResult> GetTongQuanToaThuoc([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetTongQuanToaThuocAsync(f));

    [HttpGet("toa-thuoc/theo-khoang")]
    public async Task<IActionResult> GetToaThuocTheoKhoang([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetToaThuocTheoKhoangAsync(f));

    [HttpGet("toa-thuoc/top-thuoc")]
    public async Task<IActionResult> GetTopThuoc(
        [FromQuery] ThongKeFilterRequest f,
        [FromQuery] int top = 10)
        => Ok(await _service.GetTopThuocAsync(f, top));

    [HttpGet("toa-thuoc/top-bac-si-ke-don")]
    public async Task<IActionResult> GetTopBacSiKeDon(
        [FromQuery] ThongKeFilterRequest f,
        [FromQuery] int top = 5)
        => Ok(await _service.GetTopBacSiKeDonAsync(f, top));

    [HttpGet("nhan-vien/tong-quan")]
    public async Task<IActionResult> GetTongQuanNhanVien()
        => Ok(await _service.GetTongQuanNhanVienAsync());

    [HttpGet("nhan-vien/theo-chuc-vu")]
    public async Task<IActionResult> GetNhanVienTheoChucVu()
        => Ok(await _service.GetNhanVienTheoChucVuAsync());

    [HttpGet("nhan-vien/theo-phong")]
    public async Task<IActionResult> GetNhanVienTheoPhong()
        => Ok(await _service.GetNhanVienTheoPhongAsync());

    [HttpGet("nhan-vien/hieu-suat")]
    public async Task<IActionResult> GetHieuSuatBacSi([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetHieuSuatBacSiAsync(f));

    [HttpGet("nhan-vien/ngay-nghi")]
    public async Task<IActionResult> GetNgayNghiNhanVien([FromQuery] ThongKeFilterRequest f)
        => Ok(await _service.GetNgayNghiNhanVienAsync(f));
}
