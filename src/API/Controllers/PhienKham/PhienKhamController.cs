using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Application.Common;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PhienKhamController : ControllerBase
{
	private readonly PhienKhamService _service;
	public PhienKhamController(PhienKhamService service)
	{
		_service = service;
	}
	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> TaoMoi([FromQuery] int caKhamID)
	{
		var phienKhamId = await _service.TaoMoiAsync(caKhamID);
		return Ok(ApiResponse<int>.SuccessResponse(phienKhamId, "Tạo phiên khám thành công"	));
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhat(
		int id,
		[FromBody] PhienKhamUpdateDTO dto)
	{
		await _service.CapNhatAsync(id, dto);
		return Ok(ApiResponse<object>.SuccessResponse(null, "Cập nhật phiên khám thành công"));
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/ket-thuc")]
	public async Task<ActionResult<ApiResponse<object>>> KetThuc(int id,[FromBody] string chanDoanCuoi)
	{
		try
		{
			await _service.KetThucAsync(id, chanDoanCuoi);

			return Ok(ApiResponse<object>.SuccessResponse(
				null,
				"Kết thúc phiên khám thành công"));
		}
		catch (Exception ex)
		{
			return BadRequest(ApiResponse<object>.Fail(ex.Message));
		}
	}
	[Authorize(Roles = "Admin")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamListReadModel>>>> LayTheoBenhNhan(
		int benhNhanId,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 10)
	{
		var result = await _service.GetByBenhNhanAsync(benhNhanId,pageNumber,pageSize);
		return Ok(ApiResponse<PagedResult<PhienKhamListReadModel>>.SuccessResponse(result));
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamListReadModel>>>> LayDanhSach(
		[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 15,[FromQuery] int? nhanVienID = null,[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(pageNumber,pageSize,nhanVienID,trangThai);
		return Ok(ApiResponse<PagedResult<PhienKhamListReadModel>>
			.SuccessResponse(result));
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("timkiem")]
	public async Task<ActionResult<ApiResponse<PagedResult<PhienKhamListReadModel>>>> Search(
		[FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15,[FromQuery] int? nhanVienID = null)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize, nhanVienID);
		return Ok(ApiResponse<PagedResult<PhienKhamListReadModel>>.SuccessResponse(result));
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		if (result == null)
			return NotFound(ApiResponse<PhienKhamReadModel>
				.Fail("Phiên khám không tồn tại"));
		return Ok(ApiResponse<PhienKhamReadModel>
			.SuccessResponse(result));
	}
}
