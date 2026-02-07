using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/ngay-nghi")]
public class NgayNghiNhanVienController : ControllerBase
{
	private readonly NgayNghiNhanVienService _service;

	public NgayNghiNhanVienController(NgayNghiNhanVienService service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<IActionResult> TaoNgayNghi([FromBody] NgayNghiRequestDTO dto)
	{
		await _service.ThemNgayNghiAsync(dto);
		return Ok(new { message = "Tạo ngày nghỉ thành công." });
	}

	[HttpGet("nhan-vien/{nhanVienID}")]
	public async Task<IActionResult> LayTheoNhanVien(int nhanVienID)
	{
		var result = await _service.GetByNhanVienAsync(nhanVienID);
		return Ok(result);
	}
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhatLyDo(int id, [FromBody] string? lyDo)
	{
		var success = await _service.CapNhatNgayNghiAsync(id, lyDo);

		if (!success)
			return NotFound(new { message = "Ngày nghỉ không tồn tại." });

		return Ok(new { message = "Cập nhật lý do nghỉ thành công." });
	}
}
