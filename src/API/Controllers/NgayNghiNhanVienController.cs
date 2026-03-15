using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/ngaynghi")]
[Authorize]
public class NgayNghiNhanVienController : ControllerBase
{
	private readonly NgayNghiNhanVienService _service;
	public NgayNghiNhanVienController(NgayNghiNhanVienService service)
	{
		_service = service;
	}
	// CREATE
	[HttpPost]
	[Authorize(Policy = "LICHLAMVIEC_CREATE")]
	public async Task<IActionResult> Create([FromBody] NgayNghiRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(
			nameof(GetDetail),
			new { id = result.Data },
			result
		);
	}
	// UPDATE
	[HttpPut("{id}")]
	[Authorize(Policy = "LICHLAMVIEC_UPDATE")]
	public async Task<IActionResult> Update(int id, [FromBody] NgayNghiUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	// DETAIL
	[HttpGet("{id}")]
	[Authorize(Policy = "LICHLAMVIEC_VIEW")]
	public async Task<IActionResult> GetDetail(int id)
	{
		var result = await _service.GetDetailAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	// LIST BY NHANVIEN
	[HttpGet("nhanvien/{nhanVienID}")]
	[Authorize(Policy = "LICHLAMVIEC_VIEW")]
	public async Task<IActionResult> GetByNhanVien(int nhanVienID)
	{
		var result = await _service.GetByNhanVienAsync(nhanVienID);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	// LIST BY MONTH
	[HttpGet("month")]
	[Authorize(Policy = "LICHLAMVIEC_VIEW")]
	public async Task<IActionResult> GetByMonth([FromQuery] int? thang,	[FromQuery] int? nam)
	{
		var now = DateTime.Now;
		int thangValue = thang ?? now.Month;
		int namValue = nam ?? now.Year;
		var result = await _service.GetByMonthAsync(thangValue, namValue);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	// IMPORT EXCEL
	[HttpPost("import")]
	[Authorize(Policy = "LICHLAMVIEC_CREATE")]
	public async Task<IActionResult> ImportExcel(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));
		using var stream = file.OpenReadStream();
		var result = await _service.ImportExcelAsync(stream);
		return Ok(result);
	}
}