using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CaKhamController : ControllerBase
{
	private readonly CaKhamService _caKhamService;
	private readonly TaiKhamService _taiKhamService;

	public CaKhamController(
		CaKhamService caKhamService,
		TaiKhamService taiKhamService)
	{
		_caKhamService = caKhamService;
		_taiKhamService = taiKhamService;
	}


	[Authorize(Policy = "LeTanOnly")]
	[HttpPost]
	public async Task<IActionResult> TaoCaKham([FromBody] TaoCaKhamDTO dto)
	{
		var caKhamId = await _caKhamService.TaoCaKhamAsync(dto);
		return Ok(new
		{
			Message = "Tạo ca khám thành công",
			CaKhamID = caKhamId
		});
	}

	[Authorize(Policy = "LeTanOnly")]
	[HttpPut("{id}/dangky")]
	public async Task<IActionResult> DangKyKham(int id, [FromBody] DangKyCaKhamDTO dto)
	{
		var result = await _caKhamService.DangKyKhamAsync(id, dto);

		return result
			? Ok(new { Message = "Đăng ký ca khám thành công" })
			: NotFound(new { Message = "Ca khám không tồn tại" });
	}



	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/trangthai")]
	public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] string trangThai)
	{
		var result = await _caKhamService.UpdateTrangThaiAsync(id, trangThai);

		return result
			? Ok(new { Message = "Cập nhật trạng thái thành công" })
			: NotFound(new { Message = "Ca khám không tồn tại" });
	}


	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var caKham = await _caKhamService.LayCaKhamTheoIdAsync(id);

		return caKham == null
			? NotFound(new { Message = "Ca khám không tồn tại" })
			: Ok(caKham);
	}

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet("ngay")]
	public async Task<IActionResult> TheoNgay([FromQuery] DateTime ngay, string trangThai)
	{
		var list = await _caKhamService.DanhSachCaKhamTheoNgayAsync(ngay, trangThai);
		return Ok(list);
	}

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet]
	public async Task<IActionResult> TatCa()
		=> Ok(await _caKhamService.GetAllAsync());



	[Authorize(Roles = "Bệnh nhân")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<IActionResult> TheoBenhNhan(int benhNhanId)
	{
		var list = await _caKhamService.GetByBenhNhanAsync(benhNhanId);
		return Ok(list);
	}
}
