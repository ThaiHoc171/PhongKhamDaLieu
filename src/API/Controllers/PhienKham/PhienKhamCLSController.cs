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
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamID}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamClsListReadModel>>>> LayTheoPhienKham(int phienKhamID)
	{
		var result = await _service.GetByPhienKhamAsync(phienKhamID);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamClsReadModel>>> LayChiTiet(int id)
	{
		var result = await _service.GetDetailAsync(id);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<List<PhienKhamClsListReadModel>>>> DanhSach()
	{
		var result = await _service.GetListAsync();
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> ThemMoi([FromBody] PkClsRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/nhan")]
	public async Task<ActionResult<ApiResponse<bool>>> NhanThucHien(int id, [FromBody] AcceptClsDTO dto)
	{
		var result = await _service.AcceptAsync(id, dto);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/ketqua")]
	public async Task<ActionResult<ApiResponse<bool>>> CapNhatKetQua(int id, [FromBody] PkClsUpdateRequestDTO dto)
	{
		var result = await _service.CompleteAsync(id, dto);
		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}/huy")]
	public async Task<ActionResult<ApiResponse<bool>>> Huy(int id)
	{
		var result = await _service.CancelAsync(id);
		return Ok(result);
	}
}