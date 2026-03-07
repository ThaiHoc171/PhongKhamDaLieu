using Application.DTOs;
using Application.Services;
using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "BacSiOnly")]
public class PhienKhamThietBiController : ControllerBase
{
	private readonly PhienKhamThietBiService _service;

	public PhienKhamThietBiController(PhienKhamThietBiService service)
	{
		_service = service;
	}

	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamThietBiReadModel>>>> LayDanhSachTheoPhienKham(int phienKhamId)
	{
		var result = await _service.DanhSachTheoPhienKhamAsync(phienKhamId);
		return Ok(ApiResponse<List<PhienKhamThietBiReadModel>>.SuccessResponse(result));
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse<object>>> ThemMoi([FromBody] PhienKhamThietBiRequestDTO dto)
	{
		await _service.ThemMoiAsync(dto);
		return Ok(ApiResponse<object>.SuccessResponse(null, "Thêm thiết bị vào phiên khám thành công"));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhat(int id, [FromBody] string ghiChu)
	{
		var result = await _service.CapNhatAsync(id, ghiChu);
		if (!result)
			return NotFound(ApiResponse<object>.Fail("Thiết bị không tồn tại trong phiên khám."));
		return Ok(ApiResponse<object>.SuccessResponse(null, "Cập nhật thiết bị phiên khám thành công"));
	}
}