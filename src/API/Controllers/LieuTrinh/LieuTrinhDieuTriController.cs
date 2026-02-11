using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class LieuTrinhDieuTriController : ControllerBase
{
	private readonly LieuTrinhDieuTriService _service;

	public LieuTrinhDieuTriController(LieuTrinhDieuTriService service)
	{
		_service = service;
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> LayDanhSach()
	{
		return Ok(await _service.DanhSachAsync());
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("{id:int}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayTheoIdAsync(id);
		return result == null
			? NotFound(new { message = "Liệu trình không tồn tại." })
			: Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("benhnhan/{benhNhanId:int}")]
	public async Task<IActionResult> LayTheoBenhNhan(int benhNhanId)
	{
		var result = await _service.LayTheoBenhNhanAsync(benhNhanId);
		return result == null
			? NotFound(new { message = "Bệnh nhân chưa có liệu trình." })
			: Ok(result);
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] TaoLieuTrinhDieuTriDTO dto)
	{
		await _service.TaoLieuTrinhAsync(dto);
		return Ok(new { message = "Thêm liệu trình thành công." });
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id,
		[FromBody] CapNhatLieuTrinhDieuTriDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		return result
			? Ok(new { message = "Cập nhật liệu trình thành công" })
			: NotFound(new { message = "Liệu trình không tồn tại" });
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("TrangThai/{id}")]
	public async Task<IActionResult> CapNhatTrangThai(
		int id,
		[FromBody] CapNhatTrangThaiLieuTrinhDieuTriDTO dto)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, dto);

		return result
			? Ok(new { message = "Cập nhật trạng thái liệu trình thành công" })
			: NotFound(new { message = "Liệu trình không tồn tại" });
	}
}
