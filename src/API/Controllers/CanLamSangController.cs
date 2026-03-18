using Application.Common;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/can-lam-sang")]
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
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] CanLamSangRequestDTO dto)
	{
		await _service.ThemCanLamSangAsync(dto);

		return CreatedAtAction(
			nameof(GetById),
			new { id = 0 }, // nếu service trả id thì thay vào đây
			ApiResponse<bool>.SuccessResponse(true, "Tạo cận lâm sàng thành công")
		);
	}

	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CanLamSangRequestDTO dto)
	{
		var result = await _service.CapNhatCanLamSangAsync(id, dto);

		if (!result)
			return NotFound(ApiResponse<bool>.Fail("Cận lâm sàng không tồn tại"));

		return Ok(ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công"));
	}

	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}/status")]
	public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(int id, [FromQuery] string trangThai)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, trangThai);

		if (!result)
			return NotFound(ApiResponse<bool>.Fail("Cận lâm sàng không tồn tại"));

		return Ok(ApiResponse<bool>.SuccessResponse(true, "Cập nhật trạng thái thành công"));
	}

	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<CanLamSangResponseDTO>>> GetById(int id)
	{
		var data = await _service.LayCanLamSangTheoIdAsync(id);

		if (data == null)
			return NotFound(ApiResponse<CanLamSangResponseDTO>.Fail("Không tìm thấy dữ liệu"));

		return Ok(ApiResponse<CanLamSangResponseDTO>.SuccessResponse(data));
	}

	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<CanLamSangResponseDTO>>>> GetPaged(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15)
	{
		var data = await _service.DanhSachCanLamSangAsync(pageNumber, pageSize);

		return Ok(ApiResponse<PagedResult<CanLamSangResponseDTO>>.SuccessResponse(data));
	}

	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<List<CanLamSangResponseDTO>>>> Search([FromQuery] string keyword)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return BadRequest(ApiResponse<List<CanLamSangResponseDTO>>.Fail("Keyword không hợp lệ"));

		var data = await _service.TimTheoTenAsync(keyword);

		return Ok(ApiResponse<List<CanLamSangResponseDTO>>.SuccessResponse(data));
	}

	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
	{
		var data = await _service.GetComboboxAsync();

		return Ok(ApiResponse<List<NameResponseDTO>>.SuccessResponse(data));
	}
}