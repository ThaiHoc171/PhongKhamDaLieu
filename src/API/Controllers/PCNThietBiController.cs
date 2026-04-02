using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/pcnthietbi")]
[Authorize]
public class PCNThietBiController : ControllerBase
{
	private readonly PCNThietBiService _service;

	public PCNThietBiController(PCNThietBiService service)
	{
		_service = service;
	}

	// ==================== GET PAGED ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PCNThietBiReadModel>>>> 
		GetPaged( [FromQuery] int page = 1, [FromQuery] int size = 15, [FromQuery] int? phongChucNangID = null)
	{
		var result = await _service.GetPagedAsync(page, size, phongChucNangID);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<PCNThietBiReadModel>>>> 
		Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 15, [FromQuery] int? phongChucNangID = null)
	{
		var result = await _service.SearchAsync(keyword, page, size, phongChucNangID);
		return Ok(result);
	}
}