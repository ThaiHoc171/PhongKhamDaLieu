using Application.DTOs;
using Application.Services;
using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "BacSiOnly")]
public class PhienKhamBenhController : ControllerBase
{
	private readonly PhienKhamBenhService _service;

	public PhienKhamBenhController(PhienKhamBenhService service)
	{
		_service = service;
	}

	[HttpGet("phien-kham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamBenhReadModel>>>> GetByPhienKham(int phienKhamID)
	{
		var result = await _service.GetByPhienKhamIdAsync(phienKhamID);

		return Ok(ApiResponse<List<PhienKhamBenhReadModel>>
			.SuccessResponse(result));
	}

	[HttpPost]
	public async Task<ActionResult<ApiResponse<object>>> ThemMoi([FromBody] PhienKhamBenhRequestDTO dto)
	{
		await _service.ThemMoiAsync(dto);

		return Ok(ApiResponse<object>
			.SuccessResponse(null, "Thêm chẩn đoán bệnh thành công"));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhat(int id,[FromBody] PhienKhamBenhRequestDTO dto)
	{
		await _service.CapNhatAsync(id, dto);

		return Ok(ApiResponse<object>
			.SuccessResponse(null, "Cập nhật chẩn đoán bệnh thành công"));
	}
}