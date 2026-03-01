using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class BenhNhanController : ControllerBase
{
	private readonly BenhNhanService _benhNhanService;

	public BenhNhanController(BenhNhanService benhNhanService)
	{
		_benhNhanService = benhNhanService;
	}


	[Authorize(Policy = "LeTanOnly")]
	[HttpPost]
	public async Task<IActionResult> TaoBenhNhan([FromBody] ThemBenhNhanDTO dto)
	{
		var benhNhanID = await _benhNhanService.ThemBenhNhanAsync(dto);

		return Ok(new
		{
			message = "Tạo bệnh nhân thành công.",
			BenhNhanID = benhNhanID
		});
	}

	[Authorize(Policy = "LeTanOnly")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhatBenhNhan(
		int id,
		[FromBody] CapNhatBenhNhanDTO dto)
	{
		var result = await _benhNhanService.CapNhatBenhNhanAsync(id, dto.GhiChu);

		return result
			? Ok(new { message = "Cập nhật bệnh nhân thành công." })
			: NotFound(new { message = "Bệnh nhân không tồn tại" });
	}

	[Authorize(Policy = "LeTanOnly")]
	[HttpGet]
	public async Task<IActionResult> DanhSach([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		=> Ok(await _benhNhanService.DanhSachBenhNhanAsync(pageNumber,pageSize));

	[Authorize(Policy = "LeTanOnly")]
	[HttpGet("Search")]
	public async Task<IActionResult> Search([FromQuery] string keyword)
		=> Ok(await _benhNhanService.SearchdAsync(keyword));


	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var bn = await _benhNhanService.LayBenhNhanTheoIdAsync(id);

		return bn == null
			? NotFound(new { message = "Bệnh nhân không tồn tại" })
			: Ok(bn);
	}


	[Authorize(Roles = "Bệnh nhân")]
	[HttpGet("me")]
	public async Task<IActionResult> XemThongTinCuaToi()
	{
		var benhNhanId = int.Parse(User.FindFirst("BenhNhanID")!.Value);

		var bn = await _benhNhanService.LayBenhNhanTheoIdAsync(benhNhanId);

		return bn == null
			? NotFound()
			: Ok(bn);
	}
	//Get combobox
	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("combobox")]
	public async Task<IActionResult> GetIdAndName()
	{
		var result = await _benhNhanService.GetComboboxAsync();
		return Ok(result);
	}
}
