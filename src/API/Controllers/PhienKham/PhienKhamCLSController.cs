using Application.DTOs;
using Application.Services;
using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/phienkham-cls")]
public class PhienKhamCLSController : ControllerBase
{
	private readonly PhienKhamCLSService _service;

	public PhienKhamCLSController(PhienKhamCLSService service)
	{
		_service = service;
	}

	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("phienkham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamClsListReadModel>>>> LayTheoPhienKham(int phienKhamID)
	{
		var result = await _service.LayTheoPhienKhamAsync(phienKhamID);
		return Ok(ApiResponse<List<PhienKhamClsListReadModel>>.SuccessResponse(result));
	}

	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("chitiet/{phienKhamBenhID}")]
	public async Task<ActionResult<ApiResponse<PhienKhamClsReadModel>>> LayChiTiet(int phienKhamBenhID)
	{
		var result = await _service.LayChiTietAsync(phienKhamBenhID);
		if (result == null)
			return NotFound(ApiResponse<PhienKhamClsReadModel>.Fail("CLS không tồn tại"));
		return Ok(ApiResponse<PhienKhamClsReadModel>.SuccessResponse(result));
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<object>>> ThemMoi([FromBody] TaoPhienKhamCLSDTO dto)
	{
		await _service.ThemMoiAsync(dto);
		return Ok(ApiResponse<object>.SuccessResponse(null, "Chỉ định cận lâm sàng thành công"));
	}

	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPut("{id}/nhan")]
	public async Task<ActionResult<ApiResponse<object>>> NhanThucHien(int id, [FromBody] NhanThucHienCLSDTO dto)
	{
		var success = await _service.NhanThucHienAsync(id, dto);
		if (!success)
			return NotFound(ApiResponse<object>.Fail("CLS không tồn tại"));
		return Ok(ApiResponse<object>.SuccessResponse(null, "Đã nhận thực hiện cận lâm sàng"));
	}

	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPut("{id}/ketqua")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhatKetQua(int id, [FromBody] CapNhatKetQuaCLSDTO dto)
	{
		var success = await _service.CapNhatKetQuaAsync(id, dto);
		if (!success)
			return NotFound(ApiResponse<object>.Fail("CLS không tồn tại"));
		return Ok(ApiResponse<object>.SuccessResponse(null, "Cập nhật kết quả cận lâm sàng thành công"));
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/huy")]
	public async Task<ActionResult<ApiResponse<object>>> Huy(int id)
	{
		var success = await _service.HuyAsync(id);
		if (!success)
			return NotFound(ApiResponse<object>.Fail("CLS không tồn tại"));
		return Ok(ApiResponse<object>.SuccessResponse(null, "Đã hủy cận lâm sàng"));
	}
}