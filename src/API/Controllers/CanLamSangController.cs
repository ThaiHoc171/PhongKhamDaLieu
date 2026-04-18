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
	[Authorize(Policy = "CSVC_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] CanLamSangRequest dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_WRITE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CanLamSangRequest dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<CanLamSangReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_READ")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangReadListModel>>>> Paged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_READ")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangReadListModel>>>> Search([FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return BadRequest(ApiResponse<PagedResult<CanLamSangReadListModel>>.Fail("Keyword không hợp lệ"));

		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_READ")]
	[HttpGet("loai")]
	public async Task<ActionResult<ApiResponse<List<CanLamSangReadListModel>>>> GetByLoai([FromQuery] string loai)
	{
		var result = await _service.GetByLoaiXetNghiemAsync(loai);

		return Ok(result);
	}
	[Authorize(Policy = "CSVC_READ")]
    [HttpGet("combobox")]
    public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
    {
        var result = await _service.GetComboboxAsync();
        return Ok(result);
    }
	[Authorize(Policy = "CSVC_WRITE")]
	[HttpPost("import/preview")]
	public async Task<IActionResult> PreviewImport(IFormFile file,	[FromQuery] string sheet)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));

		if (string.IsNullOrWhiteSpace(sheet))
			return BadRequest(ApiResponse<string>.Fail("Sheet không hợp lệ"));

		using var stream = file.OpenReadStream();

		var response = await _service.PreviewImport(stream, sheet);

		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "CSVC_WRITE")]
	[HttpPost("import/confirm")]
	public async Task<IActionResult> Import([FromBody] List<CanLamSangImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var response = await _service.Import(list);

		if (!response.Success)
			return BadRequest(response);

		return Ok(response);
	}
}