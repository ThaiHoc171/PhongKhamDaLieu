using Application.Common;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ToaThuocController : ControllerBase
{
	private readonly ToaThuocService _service;

	public ToaThuocController(ToaThuocService service)
	{
		_service = service;
	}
	[HttpGet("exists/{phienKhamId}")]
	public async Task<IActionResult> KiemTra(int phienKhamId)
	{
		var exists = await _service.KiemTraTonTai(phienKhamId);
		return Ok(exists);
	}
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> TaoToaThuoc([FromBody] ToaThuocRequestDTO dto)
	{
		var toaThuocId = await _service.TaoToaThuocAsync(dto);

		return Ok(ApiResponse<int>
			.SuccessResponse(toaThuocId, "Tạo toa thuốc thành công"));
	}

	[HttpGet("phien-kham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<ToaThuocReadModel?>>> GetByPhienKham(int phienKhamID)
	{
		var result = await _service.GetByPhienKham(phienKhamID);

		return Ok(ApiResponse<ToaThuocReadModel?>
			.SuccessResponse(result));
	}

	[HttpGet("chi-tiet/{toaThuocID}")]
	public async Task<ActionResult<ApiResponse<List<ChiTietToaThuocReadModel>>>> GetChiTiet(int toaThuocID)
	{
		var result = await _service.GetByToaThuoc(toaThuocID);

		return Ok(ApiResponse<List<ChiTietToaThuocReadModel>>
			.SuccessResponse(result));
	}

	[HttpGet("paged")]
	public async Task<ActionResult<ApiResponse<PagedResult<ToaThuocReadModel>>>> GetPaged(
		int pageNumber = 1,
		int pageSize = 10)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);

		return Ok(ApiResponse<PagedResult<ToaThuocReadModel>>
			.SuccessResponse(result));
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{toaThuocID}")]
	public async Task<ActionResult<ApiResponse<string>>> UpdateToaThuoc(int toaThuocID,[FromBody] List<ChiTietToaThuocRequestDTO> chiTiet)
	{
		await _service.UpdateToaThuocAsync(toaThuocID, chiTiet);
		return Ok(ApiResponse<string>
			.SuccessResponse("Cập nhật toa thuốc thành công"));
	}
}