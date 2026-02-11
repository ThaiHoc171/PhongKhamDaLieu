using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class PhienKhamController : ControllerBase
{
	private readonly PhienKhamService _service;

	public PhienKhamController(PhienKhamService service)
	{
		_service = service;
	}

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpPost]
	public async Task<IActionResult> TaoMoi([FromBody] PhienKhamCreateDTO dto)
	{
		var phienKhamId = await _service.TaoMoiAsync(dto);
		return Ok(new { message = "Tạo phiên khám thành công.", phienKhamId });
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhienKhamUpdateDTO dto)
	{
		await _service.CapNhatAsync(id, dto);
		return Ok(new { message = "Cập nhật phiên khám thành công." });
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/ket-thuc")]
	public async Task<IActionResult> KetThuc(int id, [FromBody] string chanDoanCuoi)
	{
		await _service.KetThucAsync(id, chanDoanCuoi);
		return Ok(new { message = "Kết thúc phiên khám thành công." });
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return Ok(await _service.LayTatCaAsync());
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("filter")]
	public async Task<IActionResult> Filter(
		[FromQuery] DateTime? tuNgay,
		[FromQuery] DateTime? denNgay,
		[FromQuery] string? trangThai,
		[FromQuery] int? nhanVienID)
	{
		return Ok(await _service.LocAsync(tuNgay, denNgay, trangThai, nhanVienID));
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		return result == null
			? NotFound("Phiên khám không tồn tại")
			: Ok(result);
	}
}
