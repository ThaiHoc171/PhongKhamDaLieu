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
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<IActionResult> LayTheoBenhNhan(int benhNhanId,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
	{
		var result = await _service.GetByBenhNhanAsync(benhNhanId, pageNumber, pageSize);
		return Ok(result);
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpGet]
	public async Task<IActionResult> LayDanhSach([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15, 
												 [FromQuery] int? nhanVienID = null, [FromQuery] string? trangThai = null)
		=> Ok(await _service.GetPagedAsync(pageNumber, pageSize,nhanVienID,trangThai));

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("timkiem")]
	public async Task<IActionResult> Search([FromQuery] string keyword,[FromQuery] int? nhanVienID)
		=> Ok(await _service.SearchAsync(keyword,nhanVienID));

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		return result == null
			? NotFound("Phiên khám không tồn tại")
			: Ok(result);
	}
}
