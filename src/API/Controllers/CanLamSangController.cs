using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/canlamsang")]
[Authorize]
public class CanLamSangController : ControllerBase
{
	private readonly CanLamSangService _service;

	public CanLamSangController(CanLamSangService service)
	{
		_service = service;
	}
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] CanLamSangRequest dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CanLamSangRequest dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<CanLamSangReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangListReadModel>>>> Paged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangListReadModel>>>> Search([FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return BadRequest(ApiResponse<PagedResult<CanLamSangListReadModel>>.Fail("Keyword không hợp lệ"));

		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("loai")]
	public async Task<ActionResult<ApiResponse<List<CanLamSangListReadModel>>>> GetByLoai([FromQuery] string loai)
	{
		var result = await _service.GetByLoaiXetNghiemAsync(loai);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_VIEW")]
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
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