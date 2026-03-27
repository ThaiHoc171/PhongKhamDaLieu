using Application.Common;
using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/phongchucnang")]
[Authorize]
public class PhongChucNangController : ControllerBase
{
	private readonly PhongChucNangService _service;
	public PhongChucNangController(PhongChucNangService service)
	{
		_service = service;
	}
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] PhongChucNangRequestDTO dto)
	{
		var result = await _service.TaoMoiAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PhongChucNangRequestDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);
		if (!result.Success)
		{
			if (result.Message.Contains("không tồn tại"))
				return NotFound(result);
			return BadRequest(result);
		}
		return Ok(result);
	}
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}/status")]
	public async Task<ActionResult<ApiResponse<bool>>> ChangeStatus(int id, [FromBody] TinhTrang trangThaiMoi)
	{
		var result = await _service.ChuyenTrangThaiAsync(id, trangThaiMoi);
		if (!result.Success)
		{
			if (result.Message.Contains("không tồn tại"))
				return NotFound(result);
			return BadRequest(result);
		}
		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhongChucNangReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PhongChucNangListReadModel>>>> 
		GetPaged([FromQuery] int page = 1, [FromQuery] int size = 15, [FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, trangThai);
		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhongChucNangListReadModel>>>> 
		Search([FromQuery] string? keyword, [FromQuery] int page = 1, [FromQuery] int size = 15)
	{
		var result = await _service.SearchAsync(keyword, page, size);
		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> Combobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost("import")]
	public async Task<ActionResult<ApiResponse<ImportResult>>> ImportExcel(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<ImportResult>.Fail("File không hợp lệ"));
		using var stream = file.OpenReadStream();
		var result = await _service.ImportExcelAsync(stream);
		return Ok(result);
	}
}