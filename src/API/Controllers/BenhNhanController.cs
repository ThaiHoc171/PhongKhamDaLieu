using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/benhnhan")]
[Authorize]
public class BenhNhanController : ControllerBase
{
	private readonly BenhNhanService _service;
	public BenhNhanController(BenhNhanService service)
	{
		_service = service;
	}
	[Authorize(Policy = "BENHNHAN_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] BenhNhanRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(nameof(Detail), new { id = result.Data }, result);
	}
	[Authorize(Policy = "BENHNHAN_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] BenhNhanUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "BENHNHAN_DETAIL")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<BenhNhanReadModel>>> Detail(int id)
	{
		if (User.IsInRole("Bệnh nhân"))
		{
			var benhNhanId = int.Parse(User.FindFirst("BenhNhanID")!.Value);
			if (benhNhanId != id)
				return Forbid();
		}
		var result = await _service.GetDetailAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "BENHNHAN_LIST")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<BenhNhanReadListModel>>>> List(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);
		return Ok(result);
	}
	[Authorize(Policy = "BENHNHAN_LIST")]
	[HttpGet("Search")]
	public async Task<ActionResult<ApiResponse<PagedResult<BenhNhanReadListModel>>>> Search(
		[FromQuery] string? keyword,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
		return Ok(result);
	}
	[Authorize(Policy = "BENHNHAN_LIST")]
	[HttpGet("Combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> Combobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}
}