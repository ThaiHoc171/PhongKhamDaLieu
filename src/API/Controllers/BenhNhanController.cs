using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenhNhanController : ControllerBase
{
	private readonly BenhNhanService _benhNhanService;

	public BenhNhanController(BenhNhanService benhNhanService)
	{
		_benhNhanService = benhNhanService;
	}

	// POST: api/BenhNhan
	[HttpPost]
	public async Task<IActionResult> TaoBenhNhan([FromBody] ThemBenhNhanDTO dto)
	{
		try
		{
			var benhNhanID = await _benhNhanService.ThemBenhNhanAsync(dto);
			return Ok(new { message = "Tạo bênh nhân thành công.", BenhNhanID = benhNhanID });
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = "Tạo bênh nhân thất bại.", Message = ex.Message });
		}
	}

	// PUT: api/BenhNhan/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhatBenhNhan(
		int id,
		[FromBody] CapNhatBenhNhanDTO dto)
	{
		var result = await _benhNhanService.CapNhatBenhNhanAsync(
			id,
			dto.LoaiDa,
			dto.GhiChu
		);

		if (result) return Ok(new { message = "Cập nhật bệnh nhân thành công." });
		return NotFound(new { Message = "Bệnh nhân không tồn tại" });
	}

	// PATCH: api/BenhNhan/{id}TrangThai/
	[HttpPut("{id}/trangthai")]
	public async Task<IActionResult> CapNhatTrangThai(
		int id,
		[FromBody] string trangThai)
	{
		var result = await _benhNhanService.CapNhatTrangThaiAsync(id, trangThai);
		if (result) return Ok(new { message = "Cập nhật bệnh nhân thành công." });
		return NotFound(new { Message = "Bệnh nhân không tồn tại" });
	}

	// GET: api/BenhNhan
	[HttpGet]
	public async Task<IActionResult> DanhSach()
	{
		var list = await _benhNhanService.DanhSachBenhNhanAsync();
		return Ok(list);
	}

	// GET: api/BenhNhan/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var bn = await _benhNhanService.LayBenhNhanTheoIdAsync(id);
		if (bn == null) return NotFound(new { Success = false, Message = "Bệnh nhân không tồn tại" });
		return Ok(bn);
	}

	// GET: api/BenhNhan/Search?keyword=...
	[HttpGet("Search")]
	public async Task<IActionResult> Search([FromQuery] string keyword)
	{
		var list = await _benhNhanService.SearchdAsync(keyword);
		return Ok(list);
	}
}

