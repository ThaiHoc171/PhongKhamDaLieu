using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
namespace Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaiVietController : ControllerBase
{
	private readonly BaiVietService _service;
	public BaiVietController(BaiVietService service)
	{
		_service = service;
	}
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Tao([FromBody] ThemBaiVietDTO dto)
	{
		var id = await _service.ThemBaiVietAsync(dto);
		return Ok(new
		{
			Message = "Tạo bài viết thành công",
			BaiVietID = id
		});
	}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(
		int id,
		[FromBody] CapNhatBaiVietDTO dto)
	{
		var result = await _service.CapNhatBaiVietAsync(id, dto);
		return result
			? Ok(new { Message = "Cập nhật bài viết thành công" })
			: NotFound(new { Message = "Bài viết không tồn tại" });
	}
	// =========================
	// PUBLIC
	// =========================
	[AllowAnonymous]
	[HttpGet]
	public async Task<IActionResult> DanhSach()
		=> Ok(await _service.DanhSachAsync());
	[AllowAnonymous]
	[HttpGet("{id:int}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.GetByIdAsync(id);
		return result == null
			? NotFound(new { message = "Bài viết không tồn tại." })
			: Ok(result);
	}
	[AllowAnonymous]
	[HttpGet("Luotxem")]
	public async Task<IActionResult> SapXepTheoLuotXem()
		=> Ok(await _service.GetByLuotXemAsync());
	[AllowAnonymous]
	[HttpGet("LoaiBenh/{loaiBenhID:int}")]
	public async Task<IActionResult> LayTheoLoaiBenh(int loaiBenhID)
	{
		var result = await _service.GetByLoaiBenhAsync(loaiBenhID);
		return result == null
			? NotFound(new { message = "Không có bài viết cho loại bệnh này." })
			: Ok(result);
	}
	[AllowAnonymous]
	[HttpPut("{id}/luotxem")]
	public async Task<IActionResult> TangLuotXem(int id)
	{
		var ok = await _service.TangLuotXemAsync(id);
		return ok ? Ok() : NotFound();
	}
}
